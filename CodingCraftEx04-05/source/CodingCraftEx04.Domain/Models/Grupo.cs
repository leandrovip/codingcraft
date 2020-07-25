using System;
using System.ComponentModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftEx04.Domain.Models
{
    public class Grupo : IdentityRole<Guid, UsuarioGrupo>
    {

    }
}