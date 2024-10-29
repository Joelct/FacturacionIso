namespace FacturacionIso.Models
{
    public class AsientosContables
    {
        public int IdAsiento { get; set; }
        public string Descripcion { get; set; }

        public int IdCliente { get; set; }
       // public Clientes Clientes { get; set; }

        public string Cuenta { get; set; }

        public string TipoMovimiento { get; set; }
        public DateTime FechaAsiento { get; set; }

        public double MontoAsiento { get; set; }

        public bool Estado { get; set;}
    }
}
