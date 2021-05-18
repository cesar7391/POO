﻿using Caja_Ahorro.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class CajaAhorroL
    {
        static List<String> valoresIniciales = LeerArchivo();
        CajaAhorro caja = new CajaAhorro(Convert.ToDouble(valoresIniciales[0]), valoresIniciales[1], 0, 0);        
        private int idU = 1;
        public bool flag = false;
        public int idCuenta;
        public double cantidad;
        public String nombre;


        public Cuenta ObtenerUsuario(int id)
        {
            Cuenta cuenta = caja.CuentasAlta.FirstOrDefault(x => x.Id == id);
            return cuenta;
        }

        public CajaAhorro getCaja()
        {
            return caja;
        }

        public void DarAlta(Cuenta cuenta)
        {
            caja.CuentasAlta.Add(cuenta);
        }

        public void DarBaja(int id)
        {
            Cuenta cuenta = caja.CuentasAlta.FirstOrDefault(x => x.Id == id);
            if(cuenta != null)
            {
                if(ObtenerUsuario(id).Deuda > 0)
                {
                    Console.WriteLine("EL USUARIO TIENE DEUDAS, NO SE PUEDE DAR DE BAJA");
                }
                else
                {
                    caja.CuentasAlta.Remove(cuenta);
                    Console.WriteLine("USUARIO ELIMINADO\n");
                }
                
            }
            else
            {
                Console.WriteLine("EL ID NO CORRESPONDE A NINGÚN USUARIO\n");
            }            
        }

        public void Depositar(int id, double cantidad)
        {
            ObtenerUsuario(id).Dinero += cantidad;
            Console.WriteLine("¡SE DEPOSITARON ${0}!\n", cantidad);

            ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "DEPÓSITO", true);
        }

        public void Retirar(int id, double cantidad)
        {
            if(cantidad <= ObtenerUsuario(id).Dinero)
            {
                ObtenerUsuario(id).Dinero -= cantidad;
                Console.WriteLine("¡SE RETIRARON ${0}!\n", cantidad);

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "RETIRO", true);

            }
            else
            {
                Console.WriteLine("¡LA CUENTA NO TIENE SUFICIENTE SALDO!\n");

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "RETIRO", false);
            }

        }

        public void Prestar(int id, double cantidad, int plazo)
        {
            double deuda = 0;
            double reten = cantidad * 0.20;
            ObtenerUsuario(id).Dinero -= reten;
            ObtenerUsuario(id).DineroRetenido += reten;
            caja.DineroRetenido += reten;

            if(cantidad > caja.MontoInicial)
            {
                Console.WriteLine("¡NO HAY SUFICIENTE DINERO EN CAJA!");

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", false);
            }
            else if(ObtenerUsuario(id).Dinero < reten)
            {
                Console.WriteLine("¡NO TIENE 20% DE LA CANTIDAD SOLICITADA!");

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", false);

            }
            else
            {
                caja.MontoInicial -= cantidad;
                switch (plazo)
                {
                    case 1:
                        deuda = cantidad + (cantidad * 0.08);
                        ObtenerUsuario(id).Deuda += deuda;
                        caja.DineroPrestado += deuda;                        
                        Console.WriteLine("Se prestaron ${0} a la cuenta con ID [{1}] con un interés de 8%", cantidad, id);
                        ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", true);
                        break;
                    case 2:
                        deuda = cantidad + (cantidad * 0.04);
                        ObtenerUsuario(id).Deuda += deuda;
                        caja.DineroPrestado += deuda;
                        Console.WriteLine("Se prestaron ${0} a la cuenta con ID [{1}] con un interés de 4%", cantidad, id);
                        ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", true);
                        break;
                    case 3:
                        deuda = cantidad + (cantidad * 0.02);
                        ObtenerUsuario(id).Deuda += deuda;
                        caja.DineroPrestado += deuda;
                        Console.WriteLine("Se prestaron ${0} a la cuenta con ID [{1}] con un interés de 2%", cantidad, id);
                        ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", true);
                        break;
                }
            }
        }

        public void Aportar(int id, double cantidad, String fecha)
        {
            //Se obtiene la fecha en formato dd/MM/yyyy
            DateTime fechaU = Convert.ToDateTime(fecha);
            Console.WriteLine(fechaU.ToString());
            if (cantidad <= ObtenerUsuario(id).Dinero)
            {
                ObtenerUsuario(id).Dinero -= cantidad;
                Console.WriteLine("¡SE APORTARON ${0}!\n", cantidad);

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "APORTACIÓN", true);

            }
            else
            {
                Console.WriteLine("¡LA CUENTA NO TIENE SUFICIENTE SALDO!\n");

                ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "APORTACIÓN", false);
            }

        }

        public void ATexto(int id, String nombre, double cantidad, String tipo, bool estado)
        {
            DateTime now = DateTime.Now;
            String fecha = now.ToString();
            String texto = "";
            if (estado)
                texto = nombre + "\t" + " | " + fecha + "\t | " + tipo + "\t | $" + cantidad.ToString() + "\t | SUCCESS";
            else
                texto = nombre + "\t" + " | " + fecha + "\t | " + tipo + "\t | $" + cantidad.ToString() + "\t | ERROR";

            EscribirArchivo(id, texto);
        }

        public void EscribirArchivo(int id, string texto)
        {
            //StreamWriter sw = new StreamWriter(@"C:\archivos\OperacionesDia.txt", true);
            StreamWriter sw = new StreamWriter(@"OperacionesDia.txt", true);
            //sw.WriteLine("Nombre\t | Fecha\t\t\t | Tipo\t\t | Monto\t\t | Estado");
            sw.WriteLine();
            sw.WriteLine(texto);
            caja.Historial.Add(id+"\t | "+texto);
            sw.WriteLine("==========================================================================");
            sw.Close();
        }

        public void MostrarCaja()
        {
            Console.WriteLine("\n=============== OPERACIONES ===============");
            for (int i = 0; i < caja.Historial.Count; i++)
            {                
                Console.WriteLine(caja.Historial[i]);
                Console.WriteLine("===============================");
            }
        }

        public void MostrarCajaOrden()
        {
            caja.Historial.Sort();
            MostrarCaja();
        }

        public void Informacion()
        {
            Console.WriteLine("\n=============== INFORMACIÓN CAJA DE AHORRO ===============");
            Console.WriteLine("DINERO EN CAJA:  $ "+ caja.MontoInicial);
            Console.WriteLine("DINERO RETENIDO: $ "+ caja.DineroRetenido);
            Console.WriteLine("DINERO PRESTADO: $ "+ caja.DineroPrestado);
            Console.WriteLine("==========================================================\n");
        }

        public void InformacionUsuario(int id)
        {
            Console.WriteLine("\n=============== INFORMACIÓN CUENTA ID [{0}] ===============",id);
            Console.WriteLine("DINERO EN CUENTA:  $ " + ObtenerUsuario(id).Dinero);
            Console.WriteLine("DINERO RETENIDO: $ " + ObtenerUsuario(id).DineroRetenido);
            Console.WriteLine("DEUDAS: $ " + ObtenerUsuario(id).Deuda);
            Console.WriteLine("=============================================================\n");
            ;
        }

        static List<String> LeerArchivo()
        {
            List<String> valores = new List<string>();
            String linea;
            int contador = 0;
            //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\archivos\CajaAhorro.properties");
            System.IO.StreamReader file = new System.IO.StreamReader(@"CajaAhorro.properties");
            while ((linea = file.ReadLine()) != null)
            {
                valores.Add(linea);
                contador++;
            }
            file.Close();

            return valores;
        }

        public void MostrarMenuGeneral()
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
                        Cuenta user = new Cuenta(idU, nUser, 0, 0, 0);
                        DarAlta(user);
                        Console.WriteLine("¡Usuario agregado! SU ID ES: {0}", idU);
                        idU++;
                        MostrarMenuGeneral();
                        break;
                    case 2:
                        Console.Write("id del usuario a eliminar: ");
                        idCuenta = Convert.ToInt32(Console.ReadLine());
                        DarBaja(idCuenta);
                        MostrarMenuGeneral();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a aportar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Fecha de depósito: (dd/MM/YYYY)");
                        String fecha = Console.ReadLine();
                        Aportar(idCuenta, cantidad, fecha);

                        MostrarMenuGeneral();
                        break;
                    case 4:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a depositar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Depositar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 5:
                        idCuenta = PedirID();
                        Console.Write("Cuenta ID [{0}], Cantidad a depositar: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Retirar(idCuenta, cantidad);

                        MostrarMenuGeneral();
                        break;
                    case 6:
                        idCuenta = PedirID();
                        int plazo = Plazo();
                        Console.Write("Cuenta ID [{0}], Cantidad de préstamo: $", idCuenta);
                        cantidad = Convert.ToDouble(Console.ReadLine());
                        Prestar(idCuenta, cantidad, plazo);

                        MostrarMenuGeneral();

                        break;
                    case 7:
                        MostrarCaja();

                        MostrarMenuGeneral();
                        break;
                    case 8:
                        //Console.Write("¿Cuál es tu nombre? ");
                        //String nombreA = Console.ReadLine();
                        Validar(caja);
                        if (flag)
                        {
                            //UsuarioAdmin userA = new UsuarioAdmin(nombreA, caja);
                            //userA.MostrarMenuAdmin();
                            MostrarMenuAdmin();
                        }
                        else
                        {
                            Console.Write("Contraseña incorrecta - ");
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

        public void MostrarMenuAdmin()
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
                        MostrarCajaOrden();

                        MostrarMenuAdmin();
                        break;
                    case 2:
                        Informacion();

                        MostrarMenuAdmin();
                        break;
                    case 3:
                        idCuenta = PedirID();
                        InformacionUsuario(idCuenta);

                        MostrarMenuAdmin();
                        break;
                    case 4:
                        //Console.Write("¿Cuál es tu nombre? ");
                        //String nombreA = Console.ReadLine();
                        //UsuarioSistema userA = new UsuarioSistema(nombreA, Caja);
                        MostrarMenuGeneral(); ;
                        break;
                    default:
                        Console.WriteLine("NO EXISTE ESA OPCIÓN");
                        break;
                }
            } while (op < 1 || op >= 5);
        }

        public void EscribirMonto(double monto)
        {
            //StreamWriter sw = new StreamWriter(@"C:\archivos\CajaAhorro.properties", false);
            StreamWriter sw = new StreamWriter(@"CajaAhorro.properties", false);
            //sw.WriteLine("Nombre\t | Fecha\t\t\t | Tipo\t\t | Monto\t\t | Estado");
            sw.WriteLine(monto + "     ");
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
                Console.Write("Introduce la contraseña: ");
                String pass = Console.ReadLine();
                if (pass == caja.ClaveAdmin)
                    flag = true;
                else if (pass == Convert.ToString(0))
                {
                    MostrarMenuGeneral();
                } else
                {
                    Console.WriteLine("¡CONTRASEÑA INCORRECTA! (presiona 0 para regresar)");
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
