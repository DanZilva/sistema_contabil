using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages.Notas;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public NotaFiscal Nota { get; set; } = new();

    //  Carregar dados
    public IActionResult OnGet(int id)
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
            return RedirectToPage("/Login");

        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota == null)
            return RedirectToPage("/Contabil");

        Nota = nota;

        return Page();
    }

    //  Salvar edição
    public IActionResult OnPost()
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
            return RedirectToPage("/Login");

        var notaDb = _context.Notas.FirstOrDefault(n => n.Id == Nota.Id);

        if (notaDb != null)
        {
            notaDb.Numero = Nota.Numero;
            notaDb.Fornecedor = Nota.Fornecedor;
            notaDb.Valor = Nota.Valor;
            notaDb.DataEmissao = Nota.DataEmissao;
            notaDb.DataVencimento = Nota.DataVencimento;
            notaDb.Setor = Nota.Setor;

            _context.SaveChanges();
        }

        return RedirectToPage("/Contabil");
    }
}