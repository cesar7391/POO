using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro.Modelo
{
    class CajaAhorro
    {
        private double montoInicial;
        private double dineroRetenido;
        private double dineroPrestado;
        private String claveAdmin;
        private List<Cuenta> cuentasAlta = new List<Cuenta>();
        private List<String> historial = new List<string>();

        public double MontoInicial { get => montoInicial; set => montoInicial = value; }
        public double DineroRetenido { get => dineroRetenido; set => dineroRetenido = value; }
        public double DineroPrestado { get => dineroPrestado; set => dineroPrestado = value; }
        public string ClaveAdmin { get => claveAdmin; set => claveAdmin = value; }
        public List<string> Historial { get => historial; set => historial = value; }
        internal List<Cuenta> CuentasAlta { get => cuentasAlta; set => cuentasAlta = value; }

        public CajaAhorro(double montoInicial, String pass, double dineroRetenido, double dineroPrestado)
        {
            this.claveAdmin = pass;
            this.montoInicial = montoInicial;
            this.dineroPrestado = dineroPrestado;
            this.dineroRetenido = dineroRetenido;
        }
    }
}
