using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class Ordem
    {
        [Key]
        public int OrdemId { get; set; }

        public DateTime OrdemData { get; set; }

        public int CustomizarId { get; set; }

        public OrdemStatus OrdemStatus { get; set; }

        public virtual Customizar Customizar { get; set; }

        public virtual ICollection<OrdemDetalhe> OrdensDetalhes { get; set; }
    }
}