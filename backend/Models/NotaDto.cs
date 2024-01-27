namespace NotasEnsolvers.Models
{
    public class NotaDto
    {
        public int IdNota { get; set; }
        public string Descripcion { get; set; }
        public bool? Archivada { get; set; }
        public string Categoria { get; set; }
    }
}
