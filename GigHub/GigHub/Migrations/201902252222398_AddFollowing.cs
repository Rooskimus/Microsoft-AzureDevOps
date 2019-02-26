namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        FollowerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.FollowerId })
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "FollowerId" });
            DropIndex("dbo.Follows", new[] { "ArtistId" });
            DropTable("dbo.Follows");
        }
    }
}
