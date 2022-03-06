namespace MVCapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anotherreq1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRoles", "RoleName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRoles", "RoleName", c => c.String());
        }
    }
}
