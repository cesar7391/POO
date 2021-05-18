using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class Cuenta
    {
        private int id;
        private String nombre;
        private double dinero;
        private double dineroRetenido;
        private double deuda;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public double Dinero { get => dinero; set => dinero = value; }
        public double DineroRetenido { get => dineroRetenido; set => dineroRetenido = value; }
        public double Deuda { get => deuda; set => deuda = value; }

        public Cuenta(int id, string nombre, double dinero, double dineroRetenido, double deuda)
        {
            this.id = id;
            this.nombre = nombre;
            this.dinero = dinero;
            this.dineroRetenido = dineroRetenido;
            this.deuda = deuda;
        }

        public Cuenta(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
            this.dinero = 0;
            this.dineroRetenido = 0;
            this.deuda = 0;
        }
    }


}
