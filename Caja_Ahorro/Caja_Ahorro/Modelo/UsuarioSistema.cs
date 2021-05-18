using Caja_Ahorro.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class UsuarioSistema
    {
        private String nombre;
        private CajaAhorro caja;

        public string Nombre { get => nombre; set => nombre = value; }
        internal CajaAhorro Caja { get => caja; set => caja = value; }

        public UsuarioSistema() { }

        public UsuarioSistema(String nombre, CajaAhorro caja)
        {
            this.nombre = nombre;
            this.caja = caja;
        }
    }
}
