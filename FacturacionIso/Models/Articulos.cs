namespace FacturacionIso.Models
{
    public class Articulos
    {
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }
}
