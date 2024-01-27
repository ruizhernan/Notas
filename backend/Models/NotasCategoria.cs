using System;
using System.Collections.Generic;

namespace NotasEnsolvers.Models
{
    public partial class NotasCategoria
    {
        public int IdNotaCategoria { get; set; }
        public int? IdNota { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual Nota? IdNotaNavigation { get; set; }
    }
}
