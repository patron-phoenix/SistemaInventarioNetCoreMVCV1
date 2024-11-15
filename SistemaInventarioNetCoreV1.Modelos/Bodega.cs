using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioNetCoreV1.Modelos
{
     public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El Nombre es obligatorio")]
        [MaxLength(60,ErrorMessage ="El Nombre debe ser máximo de 60 carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Descripción es obligatorio")]
        [MaxLength(100, ErrorMessage = "La Descripción debe ser máximo de 100 carácteres")]
        public string Descripcion  { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        public bool Estado { get; set; }
    }
}
