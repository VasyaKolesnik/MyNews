namespace News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.News", newName: "FakeNews");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FakeNews", newName: "News");
        }
    }
}
