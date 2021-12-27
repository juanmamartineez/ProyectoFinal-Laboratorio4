using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_Laboratorio4.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del proveedor es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Numero de teléfono")]
        [Required(ErrorMessage = "El teléfono del proveedor es requerido")]
        public string Teléfono { get; set; }

        [Display(Name = "Domicilio")]
        [Required(ErrorMessage = "La domicilio del proveedor es requerido")]
        public string Domicilio { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "La localidad del proveedor es requerida")]
        public string Localidad { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "La provincia del proveedor es requerida")]
        public string Provincia { get; set; }

        public List<Producto> productos { get; set; }

        public List<ProveedorProducto> ProveedorProducto { get; set; }
    }
}
