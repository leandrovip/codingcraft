namespace VipControle.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaCst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CstCofins",
                c => new
                    {
                        CstCofinsId = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                        TipoCst = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CstCofinsId);
            
            CreateTable(
                "dbo.CstIcms",
                c => new
                    {
                        CstIcmsId = c.String(nullable: false, maxLength: 3, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.CstIcmsId);
            
            CreateTable(
                "dbo.CstIpi",
                c => new
                    {
                        CstIpiId = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                        TipoCst = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CstIpiId);
            
            CreateTable(
                "dbo.CstPis",
                c => new
                    {
                        CstPisId = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                        TipoCst = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CstPisId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CstPis");
            DropTable("dbo.CstIpi");
            DropTable("dbo.CstIcms");
            DropTable("dbo.CstCofins");
        }
    }
}
