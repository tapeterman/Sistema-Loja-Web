using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Sobrenome { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }
    }
}