﻿using cql_teacher_server.CHISON.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CHISON
{
   
    class TablaBaseDeDatos
    {
        public static LinkedList<BaseDeDatos> global = new LinkedList<BaseDeDatos>();

        public static LinkedList<BaseDeDatos> getTabla()
        {
            return global;
        }
        
        
    }
}
