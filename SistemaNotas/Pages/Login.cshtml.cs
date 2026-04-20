using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaNotas.Data;
using SistemaNotas.Models;
using SistemaNotas.Services;

namespace SistemaNotas.Pages;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;

    public LoginModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Senha { get; set; }

    public string MensagemErro { get; set; }

    public IActionResult OnPost()
    {
        // 🔍 Busca usuário pelo username
        var user = _context.Usuarios
            .FirstOrDefault(u => u.Username == Username);

        // Verifica senha com hash
        if (user != null && PasswordService.VerificarSenha(Senha, user.SenhaHash))
        {
            HttpContext.Session.SetString("Usuario", user.Username);
            HttpContext.Session.SetString("Perfil", user.Perfil);
            HttpContext.Session.SetString("Setor", user.Setor ?? "");

            if (user.Perfil == "Contabil")
                return RedirectToPage("/Contabil");
            else
                return RedirectToPage("/Setor");
        }

        MensagemErro = "Usuário ou senha inválidos";
        return Page();
    }
}