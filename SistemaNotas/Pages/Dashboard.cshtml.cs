using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages;

public class DashBoardModel : PageModel
{
    private readonly AppDbContext _context;

    public int TotalPendentes { get; set; }

    public Dictionary<string, int> NotasPorSetor { get; set; } = new();

    //  CONSTRUTOR
    public DashBoardModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var usuario = HttpContext.Session.GetString("Usuario");

        if (usuario == null)
        {
            return RedirectToPage("/Login");
        }

        // USANDO BANCO
        var notasPendentes = _context.Notas
            .Where(n => n.Status != StatusNota.Concluida)
            .ToList();

        TotalPendentes = notasPendentes.Count;

        NotasPorSetor = notasPendentes
            .GroupBy(n => n.Setor ?? "Não definido")
            .ToDictionary(g => g.Key, g => g.Count());

        return Page();
    }
}