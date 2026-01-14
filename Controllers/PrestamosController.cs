using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PrestamosController : Controller
{
    private readonly AppDbContext _context;

    //private const decimal TASA_MENSUAL_FIJA = 0.0175m;
    public PrestamosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Prestamos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Prestamos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Prestamo prestamo)
    {
        if (!ModelState.IsValid)
            return View(prestamo);

        // VARIABLES BASE
        decimal monto = prestamo.Cantidad;
        int meses = prestamo.Meses;
        decimal tasa = prestamo.TasaMensual;

        prestamo.TasaMensual = tasa;

        // COMISIÓN (5%)
        prestamo.Comision = Math.Round(monto * 0.05m, 2);

        // CÁLCULO DE CUOTA
        double p = (double)monto;
        double r = (double)tasa;

        double cuotaDouble =
            p * (r * Math.Pow(1 + r, meses)) /
            (Math.Pow(1 + r, meses) - 1);

        prestamo.PagoMensual = Math.Round((decimal)cuotaDouble, 2);

        // AMORTIZACIÓN
        decimal saldo = monto;

        for (int mes = 1; mes <= meses; mes++)
        {
            decimal interes = Math.Round(saldo * tasa, 2);
            decimal amortizacion;
            decimal cuota;

            //Ajuste para el ultimo mes
            if (mes == meses)
            {
                amortizacion = saldo;
                cuota = Math.Round(amortizacion + interes, 2);
                saldo = 0;
            }
            else
            {
                cuota = prestamo.PagoMensual;
                amortizacion = Math.Round(cuota - interes, 2);
                saldo = Math.Round(saldo - amortizacion, 2);
            }

            prestamo.Calendario.Add(new DetallePrestamo
            {
                NumeroMes = mes,
                Cuota = cuota,
                Interes = interes,
                Amortizacion = amortizacion,
                SaldoPendiente = saldo
            });
        }

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return RedirectToAction(nameof(Details), new { id = prestamo.Id });
    }

    // GET: Prestamos/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var prestamo = await _context.Prestamos
            .Include(p => p.Calendario)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prestamo == null)
            return NotFound();

        return View(prestamo);
    }
}
