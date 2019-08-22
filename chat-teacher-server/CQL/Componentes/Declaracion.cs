﻿using cql_teacher_server.CQL.Arbol;
using cql_teacher_server.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes
{
    public class Declaracion : InstruccionCQL
    {
        string tipo { set; get; }
        Expresion valor { set; get; }

        string id { set; get; }

        int l { set; get; }
        int c { set; get; }

        /*
         * Constructor de la clase
         * @tipo es el tipo de variable
         * @valor es el valor a guardar
         * @id de la variable
         * @l linea de codigo
         * @c columna de codigo
         */
        public Declaracion(string tipo, Expresion valor, string id, int l, int c)
        {
            this.tipo = tipo;
            this.valor = valor;
            this.id = id;
            this.l = l;
            this.c = c;
        }

        /*
         * Metodo de la implementacion
         * @ts tabla de simbolos global
         * @user usuario que ejecuta la accion
         * @baseD base de datos donde estamos ejecutando todo
         * @mensajes linkedlist con la salida deseada
         */
        public object ejecutar(TablaDeSimbolos ts, string user, ref string baseD, LinkedList<string> mensajes)
        {
            object a = (valor == null) ? null : valor.ejecutar(ts, user, ref baseD, mensajes);

            object res = ts.getValor(id);
            string existe = (res == null) ? "si" : res.ToString();

            if (existe.Equals("none"))
            {
                if(valor == null)
                {
                    if (tipo.Equals("int"))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, 0);
                    }
                    else if (tipo.Equals("double"))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, 0.0);
                    }
                    else if (tipo.Equals("boolean"))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, false);
                    }
                    else
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, null);
                    }
                   
                }
                else
                {
                    if(tipo.Equals("string") && a.GetType() == typeof(string))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (string)a);
                    }
                    else if (tipo.Equals("int") && a.GetType() == typeof(int))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (int)a);
                    }
                    else if (tipo.Equals("int") && a.GetType() == typeof(Double))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, Convert.ToInt32((Double)a));
                    }
                    else if (tipo.Equals("double") && a.GetType() == typeof(Double))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (Double)a);
                    }
                    else if (tipo.Equals("double") && a.GetType() == typeof(int))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, Convert.ToDouble((int)a));
                    }
                    else if (tipo.Equals("boolean") && a.GetType() == typeof(Boolean))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (Boolean)a);
                    }
                    else if (tipo.Equals("date") && a.GetType() == typeof(DateTime))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (DateTime)a);
                    }
                    else if (tipo.Equals("time") && a.GetType() == typeof(TimeSpan))
                    {
                        ts.AddLast(new Simbolo(tipo, id));
                        ts.setValor(id, (TimeSpan)a);
                    }
                    else
                    {
                        Mensaje me = new Mensaje();
                        mensajes.AddLast(me.error("La variable " + id + " no se le puede asignar este valor " + a.ToString(), l, c, "Semantico"));
                    }
                }
            }
            else
            {
                Mensaje me = new Mensaje();
                mensajes.AddLast(me.error("La variable " + id + " ya existe en este ambito",l,c,"Semantico"));
            }

            return null;
        }
    }
}
