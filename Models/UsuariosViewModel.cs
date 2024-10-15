using System.ComponentModel.DataAnnotations;

namespace CepdiRetoWeb.Models
{
    public class UsuariosViewModel
    {
        [Key]
        public int IdUsuario { get; set; }
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required]
        public string? Nombre { get; set; } = null;
        [Required]
        public DateTime? Fechacreacion { get; set; } = DateTime.Now;
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required]
        public string? Usuario { get; set; } = null;
        [Required]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "El Password debe tener minimo 8 carecteres , Contener al menos una Mayuscula (A-Z), al menos una minuscula (a-z),al menos un numero (0-9) y un caracter especial (por ej. !@#$%^&*)")]
        public string? Password { get; set; } = null;
        public int idPerfil { get; set; }
        [Required]
        public int estatus { get; set; }
    }
}
