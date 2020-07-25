namespace CodingCraftEx04.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UsuariosGrupos",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Grupos", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UsuariosIdentificacoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsuarioEndereco",
                c => new
                    {
                        UsuarioEnderecoId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Cep = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioEnderecoId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuariosLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Usuarios", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosGrupos", "UserId", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosLogins", "UserId", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioEndereco", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosIdentificacoes", "UserId", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosGrupos", "RoleId", "dbo.Grupos");
            DropIndex("dbo.UsuariosLogins", new[] { "UserId" });
            DropIndex("dbo.UsuarioEndereco", new[] { "UsuarioId" });
            DropIndex("dbo.UsuariosIdentificacoes", new[] { "UserId" });
            DropIndex("dbo.Usuarios", "UserNameIndex");
            DropIndex("dbo.UsuariosGrupos", new[] { "RoleId" });
            DropIndex("dbo.UsuariosGrupos", new[] { "UserId" });
            DropIndex("dbo.Grupos", "RoleNameIndex");
            DropTable("dbo.UsuariosLogins");
            DropTable("dbo.UsuarioEndereco");
            DropTable("dbo.UsuariosIdentificacoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.UsuariosGrupos");
            DropTable("dbo.Grupos");
        }
    }
}
