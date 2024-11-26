namespace FacturacionIso.Models
{
    public class AsientosContables
    {
    public int IdAsiento { get; set; }
    public string Descripcion { get; set; }

    public int IdCliente { get; set; }

    public string Cuenta { get; set; }

    public string TipoMovimiento { get; set; }
    public DateTime FechaAsiento { get; set; }

    public double MontoAsiento { get; set; }

    public bool Estado { get; set; }

    //Para servicio SOAP
    public int CuentaDB { get; set; }
    public int CuentaCR { get; set; }
    }
}
