﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Estado
    {
        public int IdEstado {  get; set; }
        public string NombreEstado { get; set; }
        public override string ToString() => NombreEstado;
    }
}
