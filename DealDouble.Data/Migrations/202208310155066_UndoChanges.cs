namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UndoChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Auctions", "AuctionPicturesID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "AuctionPicturesID", c => c.Int(nullable: false));
        }
    }
}
