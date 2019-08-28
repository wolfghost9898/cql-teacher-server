﻿using cql_teacher_server.CQL.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes
{

    
    public class IfSuperior : InstruccionCQL
    {
        LinkedList<SubIf> lista { set; get; }

        /*
         * Constructor de la clase que tendra muchos if,else if o else
         * @lista lista las condiciones
         */
        public IfSuperior(LinkedList<SubIf> lista)
        {
            this.lista = lista;
        }

        /*
        * Constructor de la clase padre
        * @ts tabla de simbolos padre
        * @user usuario que esta ejecutando las acciones
        * @baseD string por referencia de que base de datos estamos trabajando
        * @mensajes el output de la ejecucion
        */
        public object ejecutar(TablaDeSimbolos ts, string user, ref string baseD, LinkedList<string> mensajes)
        {
            object r;
            foreach(InstruccionCQL i in lista)
            {
                r = i.ejecutar(ts, user, ref baseD, mensajes);
                if (r == null) return null;
                SubIf a = (SubIf)i;
                if (a.flagCondicion || a.flagElse) return r;
                
              
            }
            return "";
        }
    }
}