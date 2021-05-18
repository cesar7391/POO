using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class Usuario
    {
        private String nombre;
        private double dinero;

        public string Nombre { get => nombre; set => nombre = value; }
        public double Dinero { get => dinero; set => dinero = value; }

        public Usuario(String nombre, double dinero)
        {
            this.nombre = nombre;
            this.dinero = dinero;
        }
    }
}
