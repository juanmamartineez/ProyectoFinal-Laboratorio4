using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_Laboratorio4.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio del producto es requerido")]
        public float Precio { get; set; }

        [Display(Name = "Descripción del producto")]
        [Required(ErrorMessage = "La desripción del producto es requerida")]
        public string Descripción { get; set; }

        [Display(Name = "Imagen del producto")]
        public string Imagen { get; set; }

        public bool Favorito { get; set; }

        [Display(Name = "Categoría")]
        public int CategoríaId { get; set; }

        [Display(Name = "Categoría")]
        public Categoría CategoríaProducto { get; set; }

        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [Display(Name = "Marca")]
        public Marca MarcaProducto { get; set; }

        public List<ProveedorProducto> ProveedorProducto { get; set; }
    }
}
