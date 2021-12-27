using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_Laboratorio4.Models
{
    public class Marca
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de la marca")]
        public string Nombre { get; set; }

        public List<Producto> productos { get; set; }
    }
}
