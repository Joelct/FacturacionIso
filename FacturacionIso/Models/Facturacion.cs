namespace FacturacionIso.Models;
    public class Facturacion

    {
        public int IdFacturacion { get; set; }
        public int IdVendedor { get; set; }
	   // public Vendedores Vendedores { get; set; }

	    public int IdCliente { get; set; }
	  //  public Clientes Clientes { get; set; }

        public int IdArticulo { get; set; }
	   // public Articulos Articulos { get; set; }

	    public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

