using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMigrator;

namespace PlayerEvaluator.Tests.Migrations
{
    [Migration(201108290108)]
    public class CreatePlayerTable : Migration
    {
        public override void Up()
        {
            Create.Table("Players")
                .WithColumn("Id").AsInt64().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Team").AsString();
        }

        public override void Down()
        {
            Delete.Table("Players");
        }
    }
}
