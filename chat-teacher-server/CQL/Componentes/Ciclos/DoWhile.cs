﻿using cql_teacher_server.CQL.Arbol;
using cql_teacher_server.CQL.Componentes.Ciclos;
using cql_teacher_server.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes
{
    public class DoWhile : InstruccionCQL
    { 
    
        int l { set; get; }
        int c { set; get; }
        Expresion condicion { set; get; }
        LinkedList<InstruccionCQL> cuerpo { set; get; }

        /*
         * Constructor de la clase
         * @param {l} linea del id
         * @param {c} columna del id
         * @param {condicion} condicion del do while
         * @param {cuerpo} lista de instrucciones
         */
        public DoWhile(int l, int c, Expresion condicion, LinkedList<InstruccionCQL> cuerpo)
        {
            this.l = l;
            this.c = c;
            this.condicion = condicion;
            this.cuerpo = cuerpo;
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
            object res = (condicion == null) ? null : condicion.ejecutar(ts,ambito, tsT);
            object condi = verificarCondicion(res, ambito.mensajes);
            if (condi != null)
            {
                TablaDeSimbolos nuevoAmbito = new TablaDeSimbolos();
                foreach (Simbolo s in ts)
                {
                    nuevoAmbito.AddLast(s);
                }
                foreach (InstruccionCQL i in cuerpo)
                {
                    object resultado = i.ejecutar(nuevoAmbito, ambito, tsT);
                    if (resultado == null) return null;
                }
                while ((Boolean)condi)
                {
                    nuevoAmbito = new TablaDeSimbolos();
                    foreach (Simbolo s in ts)
                    {
                        nuevoAmbito.AddLast(s);
                    }
                    //------------------------------------------------------------ INSTRUCCIONES DO WHILE ------------------------------------------
                    foreach (InstruccionCQL i in cuerpo)
                    {
                        object resultado = i.ejecutar(nuevoAmbito,ambito, tsT);
                        if (resultado == null) return null;
                        else if (resultado.GetType() == typeof(Retorno)) return ((Retorno)resultado);
                        else if (i.GetType() == typeof(Continue) || resultado.GetType() == typeof(Continue)) break;
                    }


                    res = (condicion == null) ? null : condicion.ejecutar(nuevoAmbito, ambito, tsT);
                    condi = verificarCondicion(res, ambito.mensajes);
                    if (condi == null) return null;
                }
                return "";
            }
            return null;
        }

        private object verificarCondicion(object res, LinkedList<string> mensajes)
        {
            Mensaje mensa = new Mensaje();
            if (res != null)
            {
                if (res.GetType() == typeof(Boolean)) return (Boolean)res;
                else
                {
                    mensajes.AddLast(mensa.error("La condicion tiene que ser de tipo Booleana, no se reconoce: " + res, l, c, "Semantico"));
                    return null;
                }
            }
            else
            {
                mensajes.AddLast(mensa.error("La condicion no puede ser null", l, c, "Semantico"));
                return null;
            }
            return false;
        }
    }
}
