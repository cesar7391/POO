using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class CajaAhorro
    {
        private double montoInicial;
        private double dineroRetenido;
        private double dineroPrestado;
        private String claveAdmin;
        private List<Cuenta> cuentasAlta = new List<Cuenta>();
        public List<String> historial = new List<string>();

        public double MontoInicial { get => montoInicial; set => montoInicial = value; }
        public double DineroRetenido { get => dineroRetenido; set => dineroRetenido = value; }
        public double DineroPrestado { get => dineroPrestado; set => dineroPrestado = value; }
        public string ClaveAdmin { get => claveAdmin; set => claveAdmin = value; }
        internal List<Cuenta> CuentasAlta { get => cuentasAlta; set => cuentasAlta = value; }

        public CajaAhorro(double montoInicial, String pass, double dineroRetenido, double dineroPrestado)
        {
            this.claveAdmin = pass;
            this.montoInicial = montoInicial;
            this.dineroPrestado = dineroPrestado;
            this.dineroRetenido = dineroRetenido;
        }

        public void DarAlta(Cuenta cuenta)
        {
            CuentasAlta.Add(cuenta);
        }

        public void DarBaja(int id)
        {
            Cuenta cuenta = CuentasAlta.FirstOrDefault(x => x.Id == id);
            if(cuenta != null)
            {
                if(ObtenerUsuario(id).Deuda > 0)
                {
                    Console.WriteLine("EL USUARIO TIENE DEUDAS, NO SE PUEDE DAR DE BAJA");
                }
                else
                {
                    CuentasAlta.Remove(cuenta);
                    Console.WriteLine("USUARIO ELIMINADO\n");
                }
                
            }
            else
            {
                Console.WriteLine("EL ID NO CORRESPONDE A NINGÚN USUARIO\n");
            }            
        }

        public Cuenta ObtenerUsuario(int id)
        {
            Cuenta cuenta = CuentasAlta.FirstOrDefault(x => x.Id == id);
            return cuenta;
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
            DineroRetenido += reten;

            if(cantidad > montoInicial)
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
                MontoInicial -= cantidad;
                switch (plazo)
                {
                    case 1:
                        deuda = cantidad + (cantidad * 0.08);
                        ObtenerUsuario(id).Deuda += deuda;
                        DineroPrestado += deuda;                        
                        Console.WriteLine("Se prestaron ${0} a la cuenta con ID [{1}] con un interés de 8%", cantidad, id);
                        ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", true);
                        break;
                    case 2:
                        deuda = cantidad + (cantidad * 0.04);
                        ObtenerUsuario(id).Deuda += deuda;
                        DineroPrestado += deuda;
                        Console.WriteLine("Se prestaron ${0} a la cuenta con ID [{1}] con un interés de 4%", cantidad, id);
                        ATexto(id,ObtenerUsuario(id).Nombre, cantidad, "PRÉSTAMO", true);
                        break;
                    case 3:
                        deuda = cantidad + (cantidad * 0.02);
                        ObtenerUsuario(id).Deuda += deuda;
                        DineroPrestado += deuda;
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
            historial.Add(id+"\t | "+texto);
            sw.WriteLine("==========================================================================");
            sw.Close();
        }

        public void MostrarCaja()
        {
            Console.WriteLine("\n=============== OPERACIONES ===============");
            for (int i = 0; i < historial.Count; i++)
            {                
                Console.WriteLine(historial[i]);
                Console.WriteLine("===============================");
            }
        }

        public void MostrarCajaOrden()
        {
            historial.Sort();
            MostrarCaja();
        }

        public void Informacion()
        {
            Console.WriteLine("\n=============== INFORMACIÓN CAJA DE AHORRO ===============");
            Console.WriteLine("DINERO EN CAJA:  $ "+MontoInicial);
            Console.WriteLine("DINERO RETENIDO: $ "+DineroRetenido);
            Console.WriteLine("DINERO PRESTADO: $ "+DineroPrestado);
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
    }
}
