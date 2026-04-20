using SistemaNotas.Data;
using SistemaNotas.Models;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// 🔥 BANCO 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=notas.db"));

// 🔥 SESSÃO
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 🔥 SESSÃO
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();


// 🔥 SEED
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // 🧾 NOTAS (ATUALIZADO)
    if (!db.Notas.Any())
    {
        db.Notas.AddRange(

            new NotaFiscal
            {
                Numero = "NF001",
                Fornecedor = "Dell",
                Valor = 5000,
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                Setor = "TI",
                Status = StatusNota.Recebida
            },

            new NotaFiscal
            {
                Numero = "NF002",
                Fornecedor = "Contábil LTDA",
                Valor = 2000,
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                Setor = "Financeiro",
                Status = StatusNota.Recebida
            },

            new NotaFiscal
            {
                Numero = "NF003",
                Fornecedor = "Auto Peças Brasil",
                Valor = 8000,
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                Setor = "Operacional",
                Status = StatusNota.Recebida
            }
        );

        db.SaveChanges();
    }

    // 👤 USUÁRIOS
    if (!db.Usuarios.Any())
    {
        db.Usuarios.AddRange(
            new Usuario
            {
                Username = "admin",
                SenhaHash = PasswordService.HashSenha("123"),
                Perfil = "Contabil",
                Setor = ""
            },

            new Usuario
            {
                Username = "ti",
                SenhaHash = PasswordService.HashSenha("123"),
                Perfil = "Setor",
                Setor = "TI"
            },

            new Usuario
            {
                Username = "financeiro",
                SenhaHash = PasswordService.HashSenha("123"),
                Perfil = "Setor",
                Setor = "Financeiro"
            }
        );

        db.SaveChanges();
    }
}

app.Run();