using SistemaNotas.Data;
using SistemaNotas.Models;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Services;

var builder = WebApplication.CreateBuilder(args);

// Razor
builder.Services.AddRazorPages();

// Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=notas.db"));

// Sessão
builder.Services.AddSession();

var app = builder.Build();

// 🔥 MOSTRA ERRO NA TELA
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ⚠️ TEM QUE VIR ANTES DO MAP
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

// SEED
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Notas.Any())
    {
        db.Notas.AddRange(
            new NotaFiscal { Numero = "NF001", Fornecedor = "Dell", Valor = 5000, Status = StatusNota.Recebida, Setor = "TI" },
            new NotaFiscal { Numero = "NF002", Fornecedor = "Contábil LTDA", Valor = 2000, Status = StatusNota.Recebida, Setor = "Financeiro" }
        );
    }

    if (!db.Usuarios.Any())
    {
        db.Usuarios.AddRange(
            new Usuario { Username = "admin", SenhaHash = PasswordService.HashSenha("123"), Perfil = "Contabil", Setor = "" },
            new Usuario { Username = "ti", SenhaHash = PasswordService.HashSenha("123"), Perfil = "Setor", Setor = "TI" }
        );
    }

    db.SaveChanges();
}

app.Run();