using System;
using System.Collections.Generic;

namespace NotasEnsolvers.Models
{
    public partial class Nota
    {
        public Nota()
        {
            NotasCategoria = new HashSet<NotasCategoria>();
        }

        public int IdNota { get; set; }
        public string? Descripcion { get; set; }
        public bool? Archivada { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual ICollection<NotasCategoria> NotasCategoria { get; set; }
    }
}
