namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingActualEntityNameToCommentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "EntityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "EntityName");
        }
    }
}
