using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public NotaFiscal NovaNota { get; set; } = new();

    [BindProperty]
    public IFormFile? Arquivo { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("Perfil") != "Contabil")
            return RedirectToPage("/Login");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (HttpContext.Session.GetString("Perfil") != "Contabil")
            return RedirectToPage("/Login");

        //  VALIDAÇÃO BÁSICA
        if (!ModelState.IsValid)
            return Page();

        //  VALIDA EMPRESA
        if (string.IsNullOrEmpty(NovaNota.Empresa))
        {
            ModelState.AddModelError("", "Selecione uma empresa");
            return Page();
        }

        //  VALIDA SETOR 
        if (string.IsNullOrEmpty(NovaNota.Setor))
        {
            ModelState.AddModelError("", "Selecione um setor");
            return Page();
        }

        string caminhoArquivo = "";

        //  GARANTE QUE A PASTA EXISTE
        var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        if (!Directory.Exists(pastaUploads))
            Directory.CreateDirectory(pastaUploads);

        // UPLOAD DO PDF
        if (Arquivo != null && Arquivo.Length > 0)
        {
            var extensao = Path.GetExtension(Arquivo.FileName).ToLower();

            if (extensao != ".pdf")
            {
                ModelState.AddModelError("", "Apenas arquivos PDF são permitidos");
                return Page();
            }

            var nomeArquivo = Guid.NewGuid() + extensao;
            var caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await Arquivo.CopyToAsync(stream);
            }

            caminhoArquivo = "/uploads/" + nomeArquivo;
        }

        //  DADOS DA NOTA
        NovaNota.CaminhoArquivo = caminhoArquivo;
        NovaNota.Status = StatusNota.Recebida;

        _context.Notas.Add(NovaNota);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Contabil");
    }
}