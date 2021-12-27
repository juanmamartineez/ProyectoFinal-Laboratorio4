using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_Laboratorio4.Models
{
    public class ProveedorProducto
    {
        public int Id { get; set; }

        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
    }
}
