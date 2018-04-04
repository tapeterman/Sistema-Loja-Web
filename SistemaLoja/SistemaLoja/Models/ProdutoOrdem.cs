using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class ProdutoOrdem : Produto
    {
        [Display(Name = "Quantidade")]
        [DataType(DataType.Currency)]

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Quantidade { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get { return Preco * (decimal)Quantidade; } }

    }
}