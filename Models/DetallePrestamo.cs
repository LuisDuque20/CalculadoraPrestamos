public class DetallePrestamo
{
    public int Id { get; set; }

    public int PrestamoId { get; set; }
    public Prestamo Prestamo { get; set; } = null!;

    public int NumeroMes { get; set; }

    public decimal Cuota { get; set; }
    public decimal Interes { get; set; }
    public decimal Amortizacion { get; set; }
    public decimal SaldoPendiente { get; set; }
}
