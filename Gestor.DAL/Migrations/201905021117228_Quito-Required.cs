namespace Gestor.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitoRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrabajosBatches", "IdClase", c => c.String(maxLength: 20));
            AlterColumn("dbo.TrabajosBatches", "IdOrigen", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrabajosBatches", "IdOrigen", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.TrabajosBatches", "IdClase", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
