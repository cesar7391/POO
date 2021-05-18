using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class ControladorCuenta
    {
        public Cuenta CrearUsuario(int id, String nombre)
        {
            Cuenta user = new Cuenta(id,nombre);
            return user;
        }

        public void Depositar(Cuenta user, double cantidad)
        {
            user.Dinero += cantidad;
            Console.WriteLine("¡SE DEPOSITARON ${0}!\n", cantidad);
        }

        public void Retirar(Cuenta user, double cantidad)
        {
            user.Dinero -= cantidad;
            Console.WriteLine("¡SE RETIRARON ${0}!\n", cantidad);
        }

        public void Info(Cuenta user)
        {
            Console.WriteLine("\n=============== INFORMACIÓN CUENTA ID [{0}] ===============", user.Id);
            Console.WriteLine("DINERO EN CUENTA:  $ " + user.Dinero);
            Console.WriteLine("DINERO RETENIDO: $ " + user.DineroRetenido);
            Console.WriteLine("DEUDAS: $ " + user.Deuda);
            Console.WriteLine("=============================================================\n");
        }

    }
}
