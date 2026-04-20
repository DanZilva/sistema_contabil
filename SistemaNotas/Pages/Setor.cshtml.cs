using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages;

public class SetorModel : PageModel
{
    private readonly AppDbContext _context;

    public List<NotaFiscal> Notas { get; set; } = new();

    public string NomeSetor { get; set; } = "";

    public SetorModel(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        // 🔒 BLOQUEIO DE ACESSO
        if (perfil != "Setor")
        {
            return RedirectToPage("/Login");
        }

        // 🔥 NORMALIZAÇÃO (evita erro com maiúscula/espaço)
        NomeSetor = (HttpContext.Session.GetString("Setor") ?? "")
            .Trim()
            .ToLower();

        // 🔥 FILTRO CORRETO E SEGURO
        Notas = _context.Notas
            .Where(n =>
                n.Setor != null &&
                n.Setor.ToLower() == NomeSetor &&
                n.Status != StatusNota.Concluida
            )
            .OrderByDescending(n => n.DataEmissao) // 👈 ordena (opcional, mas top)
            .ToList();

        return Page();
    }

    public IActionResult OnPostConcluir(int id)
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        // 🔒 SEGURANÇA
        if (perfil != "Setor")
        {
            return RedirectToPage("/Login");
        }

        var setor = (HttpContext.Session.GetString("Setor") ?? "")
            .Trim()
            .ToLower();

        // 🔥 GARANTE QUE SÓ ALTERA NOTA DO PRÓPRIO SETOR
        var nota = _context.Notas
            .FirstOrDefault(n =>
                n.Id == id &&
                n.Setor != null &&
                n.Setor.ToLower() == setor
            );

        if (nota != null)
        {
            nota.Status = StatusNota.Concluida;
            _context.SaveChanges();
        }

        return RedirectToPage();
    }
}