using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaNotas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string SenhaHash { get; set; }

        [NotMapped] 
        public string Senha { get; set; }

        [Required]
        public string Perfil { get; set; }

        public string Setor { get; set; }
    }
}