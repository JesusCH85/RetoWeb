using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CepdiRetoWeb.Models
{
    public class MedicamentosViewModel
    {
        [Key]
        public int IdMedicamento { get; set; }


        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Nombre { get; set; } = null;

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Concentracion { get; set; } = null;

        public  int idformafarmaceutica { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        public int stock { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Presentacion { get; set; } = null;

        public int Bhabilitado { get; set; }
    }
}
