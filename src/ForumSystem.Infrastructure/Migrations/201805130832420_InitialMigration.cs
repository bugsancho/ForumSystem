namespace ForumSystem.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ThreadId = c.Int(nullable: false),
                        UserId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumThreads", t => t.ThreadId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ThreadId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ForumThreads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UserId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()

        {
            DropForeignKey("dbo.ForumPosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.ForumThreads", "UserId", "dbo.Users");
            DropForeignKey("dbo.ForumPosts", "ThreadId", "dbo.ForumThreads");
            DropIndex("dbo.ForumThreads", new[] { "UserId" });
            DropIndex("dbo.ForumPosts", new[] { "UserId" });
            DropIndex("dbo.ForumPosts", new[] { "ThreadId" });
            DropTable("dbo.Users");
            DropTable("dbo.ForumThreads");
            DropTable("dbo.ForumPosts");
        }
    }
}
