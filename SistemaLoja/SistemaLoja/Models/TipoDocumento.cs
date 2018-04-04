using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class TipoDocumento
    {
        [Key]
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Descricao { get; set; }


        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Customizar> Customizacao { get; set; }
    }
}