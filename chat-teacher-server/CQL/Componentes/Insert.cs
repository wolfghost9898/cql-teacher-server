﻿using cql_teacher_server.CHISON;
using cql_teacher_server.CHISON.Componentes;
using cql_teacher_server.CQL.Arbol;
using cql_teacher_server.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Componentes
{
    public class Insert : InstruccionCQL
    {
        string id { set; get; }
        LinkedList<Expresion> values { set; get; }

        string operacion { set; get; }

        int l { set; get; }
        int c { set; get; }


        /*
        * Constructor de la clase
        * @id nombre de la tabla
        * @values lista de valores a guardar
        * @l linea del id
        * @c columna del id
        */

        public Insert(string id, LinkedList<Expresion> values, string operacion, int l, int c)
        {
            this.id = id;
            this.values = values;
            this.operacion = operacion;
            this.l = l;
            this.c = c;
        }


        /*
        * Constructor de la clase
        * @id nombre de la tabla
        * @l linea del id
        * @c columna del id
        */
        public object ejecutar(TablaDeSimbolos ts, string user, ref string baseD, LinkedList<string> mensajes)
        {
            Mensaje mensa = new Mensaje();
            BaseDeDatos db = TablaBaseDeDatos.getBase(baseD);
            Usuario us = TablaBaseDeDatos.getUsuario(user);
            if (db != null)
            {
                if (user.Equals("admin"))
                {


                }
                else
                {
                    if (us != null)
                    {
                        Boolean permiso = TablaBaseDeDatos.getPermiso(us, baseD);
                        if (permiso)
                        {
                            Boolean enUso = TablaBaseDeDatos.getEnUso(baseD, user);
                            if (!enUso)
                            {
                                Tabla tabla = TablaBaseDeDatos.getTabla(db, id);
                                if (tabla != null)
                                {
                                    object res = guardarNormal(tabla, mensajes, ts, user, db, ref baseD);
                                    if (res != null) return res;
                                }
                                else mensajes.AddLast(mensa.error("La tabla: " + id + " no existe en la DB: " + baseD, l, c, "Semantico"));
                            }
                            else mensajes.AddLast(mensa.error("La DB: " + baseD + " ya esta siendo utilizada por alguien mas", l, c, "Semantico"));
                        }
                        else mensajes.AddLast(mensa.error("El usuario " + user + " no tiene permisos sobre esta base de datos", l, c, "Semantico"));
                    }
                    else mensajes.AddLast(mensa.error("El usuario " + user + " no existe", l, c, "Semantico"));

                }

            }
            else mensajes.AddLast(mensa.error("La base de datos ha eliminar: " + id + " no existe", l, c, "Semantico"));
            return null;
        }



        /*
         * Metodo que inserta Normalmente en la tabla
         */
        public object guardarNormal(Tabla t, LinkedList<string> mensajes, TablaDeSimbolos ts, string user, BaseDeDatos db, ref string baseD)
        {
            Mensaje mensa = new Mensaje();
            int i = 0;
            int cantidad = cantidadCounters(t);
            if ((cantidad + values.Count()) == t.columnas.Count())
            {
                LinkedList<Atributo> insercion = new LinkedList<Atributo>();
                LinkedList<int> posiciones = new LinkedList<int>();
                foreach (Columna co in t.columnas)
                {
                    if (co.tipo.Equals("counter"))
                    {
                        int counter = getLastCounter(i, t.datos);
                        Atributo atributo = new Atributo(co.name, counter + 1, "counter");
                        insercion.AddLast(atributo);
                    }
                    else
                    {
                        object op1 = (values.ElementAt(i) == null) ? null : values.ElementAt(i).ejecutar(ts, user, ref baseD, mensajes);
                        Atributo atributo = checkinfo(co, op1, values.ElementAt(i), mensajes, db);
                        if (atributo == null) return null;
                        else insercion.AddLast(atributo);
                    }
                    if (co.pk) posiciones.AddLast(i);
                    i++;
                }
                if (!checkPrimaryKey(posiciones, insercion, mensajes, t.datos, 0))
                {
                    t.datos.AddLast(new Data(insercion));
                    mensajes.AddLast(mensa.message("Informacion insertada con exito"));
                    return "";
                }
                else mensajes.AddLast(mensa.error("Ya hay un dato que posee esa clave primaria",l,c,"Semantico"));

            }
            else mensajes.AddLast(mensa.error("La cantidad de elementos en Values no coincide con la cantidad de Columnas de la tabla: " + t.nombre, l, c, "Semantico"));

            return null;
        }

        /*
         * Metodo que contara cuantos atributos tipo counter hay
         * @t tabla donde se encuentran las columnas
         */

        private int cantidadCounters(Tabla t)
        {
            int contador = 0;
            foreach (Columna co in t.columnas)
            {
                if (co.tipo.Equals("counter")) contador++;
            }
            return contador;
        }


        /*
         * Metodo que verificara el tipo a guardar
         * @columna columan actual
         * @valor es el valor  a guardar
         * @original es la expresion sin ejecutar
         * @mensajes output de salida
         * @db BaseDeDatos actual
         */

        private Atributo checkinfo(Columna columna, object valor, object original, LinkedList<string> mensajes, BaseDeDatos db)
        {
            Mensaje mensa = new Mensaje();
            if (original == null)
            {
                if (!columna.pk)
                {
                    if (columna.tipo.Equals("string") || columna.tipo.Equals("date") || columna.tipo.Equals("time")) return new Atributo(columna.name, null, columna.tipo);
                    else if (columna.tipo.Equals("int") || columna.tipo.Equals("double") || columna.tipo.Equals("boolean"))
                        mensajes.AddLast(mensa.error("No se le puede asignar a un tipo: " + columna.tipo + " un valor null", l, c, "Semantico"));
                    else
                    {
                        Boolean temp = TablaBaseDeDatos.getUserType(columna.tipo, db);
                        if (temp) return new Atributo(columna.name, new InstanciaUserType(columna.tipo, null), columna.tipo);
                        else mensajes.AddLast(mensa.error("No existe el USER TYPE: " + columna.tipo + " en la DB: " + db.nombre, l, c, "Semantico"));
                    }
                }
                else mensajes.AddLast(mensa.error("La columna: " + columna.name + " es clave primaria no puede asignarsele un valor null", l, c, "Semantico"));

            }
            else
            {
                if (valor != null)
                {
                    if (columna.tipo.Equals("string") && valor.GetType() == typeof(string)) return new Atributo(columna.name, (string)valor, "string");
                    else if (columna.tipo.Equals("int") && valor.GetType() == typeof(int)) return new Atributo(columna.name, (int)valor, "int");
                    else if (columna.tipo.Equals("double") && valor.GetType() == typeof(Double)) return new Atributo(columna.name, (Double)valor, "double");
                    else if (columna.tipo.Equals("boolean") && valor.GetType() == typeof(Boolean)) return new Atributo(columna.name, (Boolean)valor, "boolean");
                    else if (columna.tipo.Equals("date") && valor.GetType() == typeof(DateTime)) return new Atributo(columna.name, (DateTime)valor, "date");
                    else if (columna.tipo.Equals("time") && valor.GetType() == typeof(TimeSpan)) return new Atributo(columna.name, (TimeSpan)valor, "time");
                    else if (valor.GetType() == typeof(InstanciaUserType))
                    {
                        InstanciaUserType temp = (InstanciaUserType)valor;
                        if (columna.tipo.Equals(temp.tipo)) return new Atributo(columna.name, (InstanciaUserType)valor, columna.tipo);
                        else mensajes.AddLast(mensa.error("No se le puede asignar a la columna: " + columna.name + " un tipo: " + temp.tipo, l, c, "Semantico"));
                    }
                    else mensajes.AddLast(mensa.error("No se puede asignar a la columna: " + columna.name + " el valor: " + valor, l, c, "Semantico"));
                }
            }
            return null;
        }

        /*
         * Metodo que devuelve el ultimo counter guardado
         * @index es la posicion de la columna
         * @datas es toda la informacion de la tabla
         */
        private int getLastCounter(int index, LinkedList<Data> datas)
        {
            if (datas.Count() == 0) return 0;
            var node = datas.Last;
            LinkedList<Atributo> atributos = ((Data)node.Value).valores;
            Atributo atributo = atributos.ElementAt(index);
            return (int)atributo.valor;
        }
        
        /*
         * Metodo que busca en toda la informacion guardada que no haya otras claves primarias iguales
         * @posiciones es una lista de posiciones donde se encuentran las pk
         * @atributos es una lista con la informacion a guardar
         * @mensajes output
         * @datas es toda la informacion de la base de datos
         * @pos es la posicion de la lista de posiciones
         */
        private Boolean checkPrimaryKey(LinkedList<int> posiciones, LinkedList<Atributo> atributos, LinkedList<string> mensajes, LinkedList<Data> datas, int pos)
        {
            if (posiciones.Count() > 0)
            {
                //----------------------------------------Esto significa que estamos en la ultima posicion a verificar
                if (pos + 1 == posiciones.Count())
                {
                    foreach(Data data in datas)
                    {
                        Atributo at = data.valores.ElementAt(posiciones.ElementAt(pos));
                        Atributo at2 = atributos.ElementAt(posiciones.ElementAt(pos));
                        if (at.valor.Equals(at2.valor)) return true;
                    }
                }
                else
                {
                    foreach (Data data in datas)
                    {
                        Atributo at = data.valores.ElementAt(posiciones.ElementAt(pos));
                        Atributo at2 = atributos.ElementAt(posiciones.ElementAt(pos));
                        if (at.valor.Equals(at2.valor)) return true && checkPrimaryKey(posiciones,atributos,mensajes,datas,pos + 1);
                    }
                }
            }
            return false;
        }
    }



   
}
