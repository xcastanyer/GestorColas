namespace Gestor.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoParametrosEx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrabajosBatches", "ParametrosEx", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrabajosBatches", "ParametrosEx");
        }
    }
}
