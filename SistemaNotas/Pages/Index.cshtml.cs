using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public List<NotaFiscal> Notas { get; set; } = new();

    // CONSTRUTOR
    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet(string? setor)
    {
        var usuario = HttpContext.Session.GetString("Usuario");

        if (usuario == null)
            return RedirectToPage("/Login");

        if (!string.IsNullOrEmpty(setor))
        {
            Notas = _context.Notas
                .Where(n => n.Setor == setor && n.Status != StatusNota.Concluida)
                .ToList();
        }
        else
        {
            Notas = _context.Notas.ToList();
        }

        return Page();
    }

public IActionResult OnPostClassificar(int id, string setor)
{
        if (HttpContext.Session.GetString("Perfil") != "Contabil")
            return RedirectToPage("/Setor");

        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            nota.Setor = setor;
            nota.Status = StatusNota.Classificada;

            _context.SaveChanges();
        }

        return RedirectToPage();
}

    public IActionResult OnPostConcluir(int id)
{
        if (HttpContext.Session.GetString("Perfil") != "Contabil")
            return RedirectToPage("/Setor");

        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            nota.Status = StatusNota.Concluida;

            _context.SaveChanges();
        }

        return RedirectToPage();
    }
}