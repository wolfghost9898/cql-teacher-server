﻿using cql_teacher_server.CQL.Arbol;
using cql_teacher_server.CQL.Componentes.Ciclos;
using cql_teacher_server.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes
{
    public class inFor : InstruccionCQL
    {
        int l { set; get; }
        int c { set; get; }
        object inicializacion { set; get; }
        Expresion condicion { set; get; }
        Expresion actualizacion { set; get; }
        LinkedList<InstruccionCQL> cuerpo { set; get; }

        /*
         * Constructor de la clase
         * @param {l} linea del for
         * @param {c} columna del for
         * @param {inicializacion} asignacion / declaracion
         * @param {condicion} condicion del for
         * @param {actualizacion} id++ / id--
         * @param {cuerpo} listado de expresiones a ejecutar
         */
        public inFor(int l, int c, object inicializacion, Expresion condicion, Expresion actualizacion, LinkedList<InstruccionCQL> cuerpo)
        {
            this.l = l;
            this.c = c;
            this.inicializacion = inicializacion;
            this.condicion = condicion;
            this.actualizacion = actualizacion;
            this.cuerpo = cuerpo;
        }

        /*
            * Constructor de la clase padre
            * @ts tabla de simbolos padre
            * @user usuario que esta ejecutando las acciones
            * @baseD string por referencia de que base de datos estamos trabajando
            * @mensajes el output de la ejecucion
            */

        public object ejecutar(TablaDeSimbolos ts,Ambito ambito, TablaDeSimbolos tsT)
        {

            TablaDeSimbolos nuevoAmbito = new TablaDeSimbolos();
            foreach (Simbolo s in ts)
            {
                nuevoAmbito.AddLast(s);
            }
            object inici = (inicializacion == null) ? null : ((InstruccionCQL)inicializacion).ejecutar(nuevoAmbito, ambito, tsT);
            
            if (inici != null)
            {
                System.Diagnostics.Debug.WriteLine("VALOR: " + inici);
                object res = (condicion == null) ? null : condicion.ejecutar(nuevoAmbito,ambito, tsT);
                object condi = verificarCondicion(res, ambito.mensajes);
                if (condi != null)
                {
                    while ((Boolean)condi)
                    {
                        TablaDeSimbolos nuevoAmbito2 = new TablaDeSimbolos();
                        foreach (Simbolo s in nuevoAmbito)
                        {
                            nuevoAmbito2.AddLast(s);
                        }
                        //---------------------------------------------------- instrucciones del for -----------------------------------------------
                        foreach (InstruccionCQL i in cuerpo)
                        {
                            object resultado = i.ejecutar(nuevoAmbito2, ambito, tsT);
                            if (resultado == null) return null;
                            else if (resultado.GetType() == typeof(Retorno)) return ((Retorno)resultado);
                            else if (i.GetType() == typeof(Continue) || resultado.GetType() == typeof(Continue)) break;
                        }

                        object actu = (actualizacion == null) ? null : actualizacion.ejecutar(nuevoAmbito, ambito, tsT);
                        if (actu == null) return null;

                        res = (condicion == null) ? null : condicion.ejecutar(nuevoAmbito, ambito, tsT);
                        condi = verificarCondicion(res, ambito.mensajes);
                        if (condi == null) return null;
                    }
                    return "";
                }
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
