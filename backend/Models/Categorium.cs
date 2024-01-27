using System;
using System.Collections.Generic;

namespace NotasEnsolvers.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Nota = new HashSet<Nota>();
            NotasCategoria = new HashSet<NotasCategoria>();
        }

        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }

        public virtual ICollection<Nota> Nota { get; set; }
        public virtual ICollection<NotasCategoria> NotasCategoria { get; set; }
    }
}
