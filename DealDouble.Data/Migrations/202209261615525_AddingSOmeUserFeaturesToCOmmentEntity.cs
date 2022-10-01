namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSOmeUserFeaturesToCOmmentEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String());
            AddColumn("dbo.Comments", "UserImageUrl", c => c.String());
            DropColumn("dbo.Comments", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "UserID", c => c.String());
            DropColumn("dbo.Comments", "UserImageUrl");
            DropColumn("dbo.Comments", "UserName");
        }
    }
}
