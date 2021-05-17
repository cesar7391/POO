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

        public void MostrarMenuAdmin()
        {
            int op = 1;
            do
            {
                Console.WriteLine("\n============ Bienvenido, {0} - MENÚ ADMINISTRADOR ============", Nombre);
                Console.WriteLine("1. Procesos del día");
                Console.WriteLine("2. Dinero en caja");
                Console.WriteLine("3. Dinero de cuenta");
                Console.WriteLine("4. Regresar");

                Console.Write("\nSeleccionar opción: ");
                op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Caja.MostrarCajaOrden();

                        MostrarMenuAdmin();
                        break;
                    case 2:
                        Caja.Informacion();

                        MostrarMenuAdmin();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        Caja.InformacionUsuario(idCuenta);

                        MostrarMenuAdmin();
                        break;
                    case 4:
                        Console.Write("¿Cuál es tu nombre? ");
                        String nombreA = Console.ReadLine();
                        UsuarioSistema userA = new UsuarioSistema(nombreA, Caja);
                        userA.MostrarMenuGeneral(); ;
                        break;
                    default:
                        Console.WriteLine("NO EXISTE ESA OPCIÓN");
                        break;
                }
            }while (op < 1 || op >= 5);

        }

        public int PedirID()
        {
            Console.Write("ID del usuario: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

    }
}
