namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingActualEntityIdToCommentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ActualEntityID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ActualEntityID");
        }
    }
}
