using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages;

public class ContabilModel : PageModel
{
    private readonly AppDbContext _context;

    public List<NotaFiscal> Notas { get; set; } = new();

    
    public ContabilModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
        {
            return RedirectToPage("/Setor"); 
        }

        Notas = _context.Notas.ToList();

        return Page();
    }

    public IActionResult OnPostClassificar(int id, string setor)
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
        {
            return RedirectToPage("/Setor"); // 🔥 BLOQUEIA
        }

        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            nota.Setor = setor;
            nota.Status = StatusNota.Classificada;

            _context.SaveChanges();
        }

        return RedirectToPage();
    }

    public IActionResult OnPostExcluir(int id)
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        //Segurança
        if (perfil != "Contabil")
        {
            return RedirectToPage("/Login");
        }

        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            _context.Notas.Remove(nota);
            _context.SaveChanges();
        }

        return RedirectToPage();
    }
}