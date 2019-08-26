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
    public class Drop : InstruccionCQL
    {
        string id { set; get; }
        int l { set; get; }
        int c { set; get; }


        /*
         * Constructor de la clase
         * @id el id de la base de datos que se eliminara
         * @l linea del id
         * @c columna del id
         */
        public Drop(string id, int l, int c)
        {
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
            Mensaje mensa = new Mensaje();
            BaseDeDatos db = TablaBaseDeDatos.getBase(id);
            Usuario us = TablaBaseDeDatos.getUsuario(user);
            if (db != null)
            {
                if(us != null)
                {
                    Boolean permiso = TablaBaseDeDatos.getPermiso(us, id);
                    if (permiso)
                    {
                        Boolean enUso = TablaBaseDeDatos.getEnUso(id, user);
                        if (!enUso)
                        {
                            TablaBaseDeDatos.global.Remove(db);
                            mensajes.AddLast(mensa.message("La base de datos: " + id + " ha sido eliminada con exito"));
                            return "";
                        }
                        else mensajes.AddLast(mensa.error("La base de datos: " + id + "esta siendo utilizada por otro usuario", l, c, "Semantico"));
                    }
                    else mensajes.AddLast(mensa.error("El usuario " + user + " no tiene permisos sobre esta base de datos", l, c, "Semantico"));
                }
                else mensajes.AddLast(mensa.error("El usuario " + user + " no existe", l, c, "Semantico"));
            }
            else mensajes.AddLast(mensa.error("La base de datos ha eliminar: " + id + " no existe", l, c, "Semantico"));
            return null;
        }
    }
}
