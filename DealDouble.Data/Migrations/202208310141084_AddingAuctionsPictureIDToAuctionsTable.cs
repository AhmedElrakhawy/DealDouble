namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAuctionsPictureIDToAuctionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "AuctionPicturesID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "AuctionPicturesID");
        }
    }
}
