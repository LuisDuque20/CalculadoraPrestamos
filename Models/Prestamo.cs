using System.ComponentModel.DataAnnotations;

public class Prestamo
{
    public int Id { get; set; }

    [Required]
    [Range(1001, 40000)]
    public decimal Cantidad { get; set; }

    [Required]
    [Range(12, int.MaxValue)]
    public int Meses { get; set; }

    public decimal TasaMensual { get; set; } = 0.0175m;

    public decimal Comision { get; set; }

    public decimal PagoMensual { get; set; }

    public DateTime FechaDeCreacion { get; set; } = DateTime.Now;

    public ICollection<DetallePrestamo> Calendario { get; set; } = new List<DetallePrestamo>();
}
