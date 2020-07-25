namespace VipControle.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_07122017 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Atendimentos", "DescricaoAtendimento", c => c.String(nullable: false, maxLength: 1000, unicode: false));
            AlterColumn("dbo.Atendimentos", "DescricaoConclusao", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.Atendimentos", "Observacao", c => c.String(maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Atendimentos", "Observacao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Atendimentos", "DescricaoConclusao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Atendimentos", "DescricaoAtendimento", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
