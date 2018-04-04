using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Descricao { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Preco { get; set; }

        [Display(Name = "Última Compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime UltimaCompra { get; set; }

        [Display(Name = "Estoque")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Estoque { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }
        public virtual ICollection<OrdemDetalhe> OrdensDetalhes { get; set; }
    }
}