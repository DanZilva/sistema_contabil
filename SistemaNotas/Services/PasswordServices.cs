using System.Security.Cryptography;
using System.Text;

namespace SistemaNotas.Services
{
    public static class PasswordService
    {
        public static string HashSenha (string senha)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));

                StringBuilder builder = new StringBuilder();

                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }

        public static bool VerificarSenha(string senhaDigitada, string hash)
        {
            var hashDigitado = HashSenha(senhaDigitada);
            return hashDigitado == hash;
        }
    }
}