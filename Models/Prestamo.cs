using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prestamo
{
    public int Id { get; set; }

    [Required]
    [Range(1001, 40000)]
    public decimal Cantidad { get; set; }

    [Required]
    [Range(12, 36)]
    public int Meses { get; set; }

    [Column(TypeName = "decimal(6,4)")]
    public decimal TasaMensual { get; set; } = 0.0175m;

    public decimal Comision { get; set; }
    public decimal PagoMensual { get; set; }

    public DateTime FechaDeCreacion { get; set; } = DateTime.Now;

    public ICollection<DetallePrestamo> Calendario { get; set; } = new List<DetallePrestamo>();
}
