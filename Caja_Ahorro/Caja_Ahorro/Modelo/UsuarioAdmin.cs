using Caja_Ahorro.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class UsuarioAdmin : UsuarioSistema
    {
        public UsuarioAdmin(string nombre, CajaAhorro caja) : base(nombre, caja) { }

    }
}
