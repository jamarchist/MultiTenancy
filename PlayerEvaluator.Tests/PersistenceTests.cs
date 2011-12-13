using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using NUnit.Framework;

namespace PlayerEvaluator.Tests
{
    public static class ModelBuilderHelper
    {
        public static void MapExtensionProperties<TExtensionProperties>(this DbModelBuilder modelBuilder) where TExtensionProperties : Extension
        {
            var type = typeof(TExtensionProperties).Name;
            var entity = type.Substring(0, type.Length - "Extension".Length);
            var entityPluralized = String.Format("{0}s", entity);
            var pluralizedExtension = String.Format("{0}Extended", entityPluralized);

            modelBuilder.Entity<TExtensionProperties>().ToTable("PlayersExtended");            
        }

        public static void MapExtendedEntity<TExtendedEntity>(this DbModelBuilder modelBuilder) where TExtendedEntity : class, IExtendedEntity
        {
            modelBuilder.Entity<TExtendedEntity>().HasOptional(p => p.ExtendedProperties).WithRequired();
        }
    }

    public class ExtensionTableConfiguration<TExtension> : EntityTypeConfiguration<TExtension> where TExtension : Extension
    {
        public ExtensionTableConfiguration()
        {
            var type = typeof (TExtension).Name;
            var entity = type.Substring(0, type.Length - "Extension".Length);
            var entityPluralized = String.Format("{0}s", entity);
            var pluralizedExtension = String.Format("{0}Extended", entityPluralized);

            Map(x => x.ToTable(pluralizedExtension));
        }
    }

    public interface IExtendedEntity
    {
        long Id { get; set; }
        Extension ExtendedProperties { get; set; }
    }

    public class ExtendedEntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class, IExtendedEntity
    {
        public ExtendedEntityConfiguration()
        {
            HasOptional(x => x.ExtendedProperties).WithRequired();
        }
    }

    public class TestContext : DbContext
    {
        public TestContext(string name) : base(name)
        {  
        }
        
        public DbSet<Player> Players { get; set; }
        public DbSet<Ball> Balls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new ExtensionTableConfiguration<PlayerExtension>());
            //modelBuilder.Configurations.Add(new ExtendedEntityConfiguration<Player>());

            //modelBuilder.MapExtensionProperties<PlayerExtension>();
            //modelBuilder.MapExtendedEntity<Player>();

            //modelBuilder.Entity<Extension>().Map<BallExtension>(x => x.ToTable("BallsExtended"));
            //modelBuilder.Entity<Extension>().Map<PlayerExtension>(x => x.ToTable("PlayersExtended"));

            modelBuilder.Entity<PlayerExtension>().ToTable("PlayersExtended");
            modelBuilder.Entity<BallExtension>().ToTable("BallsExtended");

            modelBuilder.Entity<Player>().HasOptional(p => p.ExtendedProperties).WithRequired();
            modelBuilder.Entity<Ball>().HasOptional(p => p.ExtendedProperties).WithRequired();

            //modelBuilder.Entity<BallExtension>().ToTable("BallsExtended");

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Player : IExtendedEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public virtual Extension ExtendedProperties { get; set; }
    }

    public abstract class Extension
    {
        public long Id { get; set; }
    }

    public class PlayerExtension : Extension
    {
        public bool IsOverrated { get; set; }
    }

    public class Ball : IExtendedEntity
    {
        public long Id { get; set; }
        public string BarCode { get; set; }
        public Extension ExtendedProperties { get; set; }
    }

    public class BallExtension : Extension
    {
        public string LogoType { get; set; }
    }


    [TestFixture]
    public class PersistenceTests
    {
        [Test]
        public void CanPersist()
        {
            using(var context = new TestContext("SQLite"))
            {
                context.Database.ExecuteSqlCommand(
                    "DROP TABLE IF EXISTS 'Players'");
                context.Database.ExecuteSqlCommand(
                    "DROP TABLE IF EXISTS 'PlayersExtended'");
                context.Database.ExecuteSqlCommand(
                    "DROP TABLE IF EXISTS 'Balls'");
                context.Database.ExecuteSqlCommand(
                    "DROP TABLE IF EXISTS 'BallsExtended'");
                
                context.Database.ExecuteSqlCommand(
                    "CREATE TABLE Players (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name NVARCHAR(255), Team NVARCHAR(255))");
                context.Database.ExecuteSqlCommand(
                    "CREATE TABLE PlayersExtended (Id INTEGER, IsOverrated BIT, FOREIGN KEY(Id) REFERENCES Players(Id))");
                context.Database.ExecuteSqlCommand(
                    "CREATE TABLE Balls (Id INTEGER PRIMARY KEY AUTOINCREMENT, BarCode NVARCHAR(255))");
                context.Database.ExecuteSqlCommand(
                    "CREATE TABLE BallsExtended (Id INTEGER, LogoType NVARCHAR(255), FOREIGN KEY (Id) REFERENCES Balls(Id))");

                foreach (var number in Enumerable.Range(1, 1))
                {
                    var id = new Random().Next();
                    var isOverrated = (number%2) == 0;
                    context.Players.Add(new Player { Id = id, Name = String.Format("Player {0}", number), Team = "Kansas City", ExtendedProperties = new PlayerExtension { Id = id, IsOverrated = isOverrated }});
                }

                var ballId = new Random().Next();
                context.Balls.Add(new Ball { Id = ballId, BarCode = Guid.NewGuid().ToString(), ExtendedProperties = new BallExtension { Id = ballId, LogoType = "NFL" } });

                context.SaveChanges();

                var players = from p in context.Players select p;
                foreach (var player in players)
                {
                    Console.WriteLine("{0} {1} {2} {3}", player.Id, player.Name, player.Team, (player.ExtendedProperties as PlayerExtension).IsOverrated);
                }

                var balls = from b in context.Balls select b;
                foreach (var b in balls)
                {
                    Console.WriteLine("Ball: {0} {1} {2}", b.Id, b.BarCode, (b.ExtendedProperties as BallExtension).LogoType);
                }
            }
        }
    }
}
