using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.Modelos
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "El nobre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [Display(Name = "Nombre de la bodega")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio.")]
        [MaxLength(200, ErrorMessage = "La descripcion no puede exceder los 200 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? FechaModificacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Display(Name = "Estado")]
        public bool Activa { get; set; }
    }
}
