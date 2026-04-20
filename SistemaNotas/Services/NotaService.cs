using SistemaNotas.Data;
using SistemaNotas.Models;

namespace SistemaNotas.Services;

public class NotaService
{
    private readonly AppDbContext _context;

    public NotaService(AppDbContext context)
    {
        _context = context;
    }

    public List<NotaFiscal> GetAll()
    {
        return _context.Notas.ToList();
    }

    public List<NotaFiscal> GetBySetor(string setor)
    {
        return _context.Notas
            .Where(n => n.Setor == setor && n.Status != StatusNota.Concluida)
            .ToList();
    }

    public void Classificar(int id, string setor)
    {
        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            nota.Setor = setor;
            nota.Status = StatusNota.Classificada;

            _context.SaveChanges();
        }
    }

    public void Concluir(int id)
    {
        var nota = _context.Notas.FirstOrDefault(n => n.Id == id);

        if (nota != null)
        {
            nota.Status = StatusNota.Concluida;

            _context.SaveChanges(); 
        }
    }
}