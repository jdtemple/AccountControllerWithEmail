namespace AccountControllerWithEmail.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallbackUrl = c.String(),
                        From = c.String(nullable: false),
                        Sent = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Subject = c.String(nullable: false),
                        To = c.String(nullable: false),
                        ViewModel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            DropTable("dbo.EmailLogs");
        }
    }
}
