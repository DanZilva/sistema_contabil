using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Pages.Notas;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public NotaFiscal NovaNota { get; set;}

    public IActionResult OnGet()
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
        {
            return RedirectToPage("/Login");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        var perfil = HttpContext.Session.GetString("Perfil");

        if (perfil != "Contabil")
        {
            return RedirectToPage("/Login");
        }

        NovaNota.Status = StatusNota.Recebida;

        _context.Notas.Add(NovaNota);
        _context.SaveChanges();

        return RedirectToPage("/Contabil");
    }







}