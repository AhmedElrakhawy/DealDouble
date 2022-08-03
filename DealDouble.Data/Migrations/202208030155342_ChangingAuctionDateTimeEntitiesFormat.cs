namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAuctionDateTimeEntitiesFormat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
