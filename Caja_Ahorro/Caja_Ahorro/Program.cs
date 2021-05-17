using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class Program
    {
        static List<String> valoresIniciales = LeerArchivo();
        static CajaAhorro caja = new CajaAhorro(Convert.ToDouble(valoresIniciales[0]), valoresIniciales[1], 0, 0);
        static bool flag = false;
        
        static void Main(string[] args)
        {
            MenuInicio();
        }

        static void MenuInicio()
        {
            Console.WriteLine("=========== CAJA DE AHORRO ===========\n");
            Console.Write("¿Cuál es tu nombre? ");
            String nombreS = Console.ReadLine();
            UsuarioSistema user = new UsuarioSistema(nombreS, caja);
            user.MostrarMenuGeneral();
            //Console.WriteLine("1. Iniciar sesión:");
            //Console.WriteLine("2. Iniciar sesión administrador.");
            //Console.Write("\nSelecciona una opción: ");
            //int op = Convert.ToInt32(Console.ReadLine());
            /*
            switch (op)
            {
                case 1:
                    Console.Write("¿Cuál es tu nombre? ");
                    String nombreS = Console.ReadLine();
                    UsuarioSistema user = new UsuarioSistema(nombreS,caja);
                    user.MostrarMenuGeneral();
                    break;
                case 2:
                    Console.Write("¿Cuál es tu nombre? ");
                    String nombreA = Console.ReadLine();
                    Validar();
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
                default:
                    Console.WriteLine("No existe esa opción");
                    break;
            }
            */
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

        /*
        static void Validar()
        {
            Console.WriteLine("Introduce la contraseña: ");
            String pass = Console.ReadLine();
            if (pass == caja.ClaveAdmin)
                flag = true;
            else
                flag = false;
        }
        */
    }
}
