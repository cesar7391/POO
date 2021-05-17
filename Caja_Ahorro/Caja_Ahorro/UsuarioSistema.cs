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
        private int idU = 1;

        public bool flag = false;
        public int idCuenta;
        public double cantidad;

        public string Nombre { get => nombre; set => nombre = value; }
        internal CajaAhorro Caja { get => caja; set => caja = value; }

        public UsuarioSistema() { }

        public UsuarioSistema(String nombre, CajaAhorro caja)
        {
            this.nombre = nombre;
            this.caja = caja;
        }

        public void MostrarMenuGeneral()
        {
            int op = 1;
            do
            {
                Console.WriteLine("\n============ Bienvenido, {0} - MENÚ CAJA DE AHORRO ============", Nombre);
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
                        Cuenta user = new Cuenta(idU, nUser, 0, 0, 0);
                        caja.DarAlta(user);
                        Console.WriteLine("¡Usuario agregado! SU ID ES: {0}", idU);
                        idU++;
                        MostrarMenuGeneral();
                        break;
                    case 2:
                        Console.Write("id del usuario a eliminar: ");
                        idCuenta = Convert.ToInt32(Console.ReadLine());
                        caja.DarBaja(idCuenta);
                        MostrarMenuGeneral();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a aportar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Fecha de depósito: (dd/MM/YYYY)");
                        String fecha = Console.ReadLine();
                        caja.Aportar(idCuenta,cantidad,fecha);

                        MostrarMenuGeneral();
                        break;
                    case 4:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a depositar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        caja.Depositar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 5:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a depositar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        caja.Retirar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 6:
                        idCuenta = PedirID();
                        int plazo = Plazo();
                        Console.Write("Cuenta ID [{0}], Cantidad de préstamo: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        caja.Prestar(idCuenta, cantidad, plazo);

                        MostrarMenuGeneral();

                        break;
                    case 7:
                        caja.MostrarCaja();

                        MostrarMenuGeneral();
                        break;
                    case 8:
                        Console.Write("¿Cuál es tu nombre? ");
                        String nombreA = Console.ReadLine();
                        Validar(caja);
                        if (flag)
                        {
                            UsuarioAdmin userA = new UsuarioAdmin(nombreA, caja);
                            userA.MostrarMenuAdmin();
                        }
                        else
                        {
                            Console.WriteLine("Contraseña incorrecta");
                        }
                        break;
                    case 9:
                        double montoFinal = caja.MontoInicial;
                        EscribirMonto(montoFinal);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("NO EXISTE ESA OPCIÓN");
                        break;
                }
            } while (op < 1 || op >= 10);
        }

        public void EscribirMonto(double monto)
        {
            //StreamWriter sw = new StreamWriter(@"C:\archivos\CajaAhorro.properties", false);
            StreamWriter sw = new StreamWriter(@"CajaAhorro.properties", false);
            //sw.WriteLine("Nombre\t | Fecha\t\t\t | Tipo\t\t | Monto\t\t | Estado");
            sw.WriteLine(monto+"     ");
            sw.WriteLine("admin");
            sw.Close();
        }

        public int PedirID()
        {
            Console.Write("ID del usuario: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public void Validar(CajaAhorro caja)
        {
            do
            {
                Console.WriteLine("Introduce la contraseña: ");
                String pass = Console.ReadLine();
                if (pass == caja.ClaveAdmin)
                    flag = true;
                else
                {
                    Console.WriteLine("Contraseña incorrecta");
                    flag = false;
                }
                    
            } while (flag == false);
            
        }

        public int Plazo()
        {
            int plazo;
            do
            {
                Console.WriteLine("\n=== SELECCIONA EL PLAZO ===");
                Console.WriteLine("1. 3 MESES (8% de interés)");
                Console.WriteLine("2. 6 MESES (4% de interés)");
                Console.WriteLine("3. 9 MESES (2% de interés)");
                plazo = Convert.ToInt32(Console.ReadLine());
            } while (plazo < 1 || plazo > 3);

            return plazo;
        }

    }
}
