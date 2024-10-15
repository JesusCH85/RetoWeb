using System.ComponentModel.DataAnnotations;

namespace CepdiRetoWeb.Models
{
    public class FormasfarmaceuticasViewModel
    {
        [Key]
        public int Idformafarmaceutica { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Nombre { get; set; } = null;
        public int Habilitado { get; set; }


    }
}
