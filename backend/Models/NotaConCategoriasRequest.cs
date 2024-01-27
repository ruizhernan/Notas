namespace NotasEnsolvers.Models
{
    public class NotaConCategoriasRequest
    {
        public string Descripcion { get; set; }
        public bool Archivada { get; set; }
        public List<int> IdCategorias { get; set; }
    }
}
