namespace Gestor.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoIdCola : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrabajosBatches", "IdCola", c => c.String());
            AlterColumn("dbo.TrabajosBatches", "IdProceso", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrabajosBatches", "IdProceso", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.TrabajosBatches", "IdCola");
        }
    }
}
