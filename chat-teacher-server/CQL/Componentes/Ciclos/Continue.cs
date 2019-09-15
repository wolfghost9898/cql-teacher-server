﻿using cql_teacher_server.CQL.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes.Ciclos
{
    public class Continue : InstruccionCQL
    {
        /*
         * CONSTRUCTOR DE LA CLASE
         */
        public Continue()
        {
        }
        /*
            * Constructor de la clase padre
            * @ts tabla de simbolos padre
            * @user usuario que esta ejecutando las acciones
            * @baseD string por referencia de que base de datos estamos trabajando
            * @mensajes el output de la ejecucion
        */
        public object ejecutar(TablaDeSimbolos ts, Ambito ambito, TablaDeSimbolos tsT)
        {
            return "";
        }
    }
}
