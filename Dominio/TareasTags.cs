using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TareasTags
    {
        public int IdTarea {  get; set; }
        public int IdTag { get; set; }

        public Tarea Tarea { get; set; }
        public Tag Tag { get; set; }


    }
}
