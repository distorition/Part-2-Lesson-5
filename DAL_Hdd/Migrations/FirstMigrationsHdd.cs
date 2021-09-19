using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL_Hdd.Migrations
{
    public class FirstMigrationsHdd : Migration
    {
        public override void Down()
        {
            Delete.Table("hddmetrics");
        }

        public override void Up()
        {
            Create.Table("hddmetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt64()
                .WithColumn("Time").AsInt64();
        }
    }
}
