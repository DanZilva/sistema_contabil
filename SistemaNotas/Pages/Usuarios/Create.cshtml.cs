using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;
using SistemaNotas.Services;

namespace SistemaNotas.Pages.Usuarios;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;

    }

    [BindProperty]
    public Usuario NovoUsuario { get; set;}

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

        NovoUsuario.SenhaHash = PasswordService.HashSenha(NovoUsuario.Senha);

        _context.Usuarios.Add(NovoUsuario);
        _context.SaveChanges();

        return RedirectToPage("/Contabil");


    }



}
