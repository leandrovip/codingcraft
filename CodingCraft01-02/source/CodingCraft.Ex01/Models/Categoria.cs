using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Categorias")]
    public class Categoria : Entidade
    {
        [Key]
        public Guid CategoriaId { get; set; }

        [Required]
        [DisplayName("Categoria")]
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}