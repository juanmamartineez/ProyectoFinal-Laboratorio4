using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_Laboratorio4.Models
{
    public class ProductoView
    {
        public List<Producto> Producto { get; set; }

        public string Nombre { get; set; }

        public int? CategoríaId { get; set; }

        public SelectList Categoría { get; set; }

        public int? MarcaId { get; set; }

        public SelectList Marca { get; set; }
    }
}
