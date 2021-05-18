using Caja_Ahorro.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class Program
    {
        static String nombre;
        static ControladorCaja controladorCaja = new ControladorCaja();
        static ControladorCuenta controladorUsuario = new ControladorCuenta();
        static int idU =1;
        static int idCuenta;
        static double cantidad;

        static void Main(string[] args)
        {
            MostrarMenuGeneral();
        }

        static void MostrarMenuGeneral()
        {
            nombre = "USUARIO GENERAL";
            int op = 1;
            do
            {
                Console.WriteLine("\n============ Bienvenido, {0} - MENÚ CAJA DE AHORRO ============", nombre);
                Console.WriteLine("1. Dar de alta un usuario");
                Console.WriteLine("2. Dar de baja un usuario");
                Console.WriteLine("3. Aportar");
                Console.WriteLine("4. Depósito");
                Console.WriteLine("5. Retiro");
                Console.WriteLine("6. Pedir Préstamo");
                Console.WriteLine("7. Mostrar Caja");
                Console.WriteLine("8. Iniciar sesión admin");
                Console.WriteLine("9. SALIR");
                Console.Write("\nSeleccionar opción: ");
                op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Console.Write("Nombre de usuario: ");
                        String nUser = Console.ReadLine();
                        controladorCaja.DarAlta(controladorUsuario.CrearUsuario(idU, nUser));
                        Console.WriteLine("¡Usuario agregado! SU ID ES: {0}", idU);
                        idU++;
                        MostrarMenuGeneral();
                        break;
                    case 2:
                        Console.Write("id del usuario a eliminar: ");
                        idCuenta = Convert.ToInt32(Console.ReadLine());
                        controladorCaja.DarBaja(idCuenta);
                        MostrarMenuGeneral();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a aportar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Fecha de depósito: (dd/MM/YYYY)");
                        String fecha = Console.ReadLine();
                        controladorCaja.Aportar(idCuenta, cantidad, fecha);

                        MostrarMenuGeneral();
                        break;
                    case 4:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a depositar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        controladorCaja.Depositar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 5:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a retirar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        controladorCaja.Retirar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 6:
                        idCuenta = PedirID();
                        int plazo = controladorCaja.Plazo();
                        Console.Write("Cuenta ID [{0}], Cantidad de préstamo: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        controladorCaja.Prestar(idCuenta, cantidad, plazo);

                        MostrarMenuGeneral();

                        break;
                    case 7:
                        controladorCaja.MostrarCaja();
                        MostrarMenuGeneral();
                        break;
                    case 8:                        
                        if (PedirContraseña(controladorCaja.getCaja()))
                        {
                            Console.Clear();
                            MostrarMenuAdmin();
                        }
                        break;
                    case 9:
                        double montoFinal = controladorCaja.getCaja().MontoInicial;
                        controladorCaja.EscribirMonto(montoFinal);
                        Console.WriteLine("\n======== FINALIZACIÓN DEL PROGRAMA ========\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("NO EXISTE ESA OPCIÓN");
                        break;
                }
            } while (op < 1 || op >= 10);
        }

        static void MostrarMenuAdmin()
        {
            nombre = "ADMINISTRADOR";
            int op = 1;            
            do
            {
                Console.WriteLine("\n============ Bienvenido, {0} - MENÚ ADMINISTRADOR ============", nombre);
                Console.WriteLine("1. Procesos del día");
                Console.WriteLine("2. Dinero en caja");
                Console.WriteLine("3. Dinero de cuenta");
                Console.WriteLine("4. Regresar");

                Console.Write("\nSeleccionar opción: ");
                op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        controladorCaja.MostrarCajaOrden();

                        MostrarMenuAdmin();
                        break;
                    case 2:
                        controladorCaja.Informacion();

                        MostrarMenuAdmin();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        controladorCaja.InformacionUsuario(idCuenta);

                        MostrarMenuAdmin();
                        break;
                    case 4:
                        Console.Clear();
                        MostrarMenuGeneral(); ;
                        break;
                    default:
                        Console.WriteLine("NO EXISTE ESA OPCIÓN");
                        break;
                }
            } while (op < 1 || op >= 5);
        }

        static bool PedirContraseña(CajaAhorro caja)
        {
            bool flag = false;
            do
            {
                Console.Write("Introduce la contraseña: ");
                String pass = Console.ReadLine();
                if (pass == caja.ClaveAdmin)
                {
                    flag = true;
                }                                   
                else if (pass == Convert.ToString(0))
                {
                    MostrarMenuGeneral();
                }
                else
                {
                    Console.WriteLine("¡CONTRASEÑA INCORRECTA! (presiona 0 para regresar)");
                    flag = false;
                }

            } while (flag == false);

            return flag;
        }

        static int PedirID()
        {
            Console.Write("ID del usuario: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }
    }
}
