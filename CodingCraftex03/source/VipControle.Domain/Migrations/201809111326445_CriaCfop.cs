using System.Data.Entity.Migrations;

namespace VipControle.Domain.Migrations
{
    public partial class CriaCfop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cfop",
                    c => new
                    {
                        CfopId = c.Int(false, true),
                        Descricao = c.String(false, 200, unicode: false),
                        TipoCfop = c.Int(false),
                        DestinoOperacao = c.Int(false)
                    })
                .PrimaryKey(t => t.CfopId);
        }

        public override void Down()
        {
            DropTable("dbo.Cfop");
        }
    }
}