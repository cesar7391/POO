﻿using Caja_Ahorro.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja_Ahorro
{
    class Program
    {
        static CajaAhorroL logica = new CajaAhorroL();
        static void Main(string[] args)
        {
            logica.MostrarMenuGeneral();
        }       
    }
}
