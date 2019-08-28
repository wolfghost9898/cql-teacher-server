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
    public class TruncateTable : InstruccionCQL
    {
        string id { set; get; }

        int l { set; get; }
        int c { set; get; }

        /*
         * Constructor de la clase
         * @id nombre de la tabla
         * @l linea del id
         * @c columna del id
         */
        public TruncateTable(string id, int l, int c)
        {
            this.id = id;
            this.l = l;
            this.c = c;
        }

        public object ejecutar(TablaDeSimbolos ts, string user, ref string baseD, LinkedList<string> mensajes)
        {
            Mensaje mensa = new Mensaje();
            BaseDeDatos db = TablaBaseDeDatos.getBase(baseD);
            if (db != null)
            {
                if (user.Equals("admin"))
                {
                    Tabla tabla = TablaBaseDeDatos.getTabla(db, id);
                    if (tabla != null)
                    {
                        tabla.columnas = new LinkedList<Columna>();
                        tabla.datos = new LinkedList<Data>();
                        mensajes.AddLast(mensa.message("La tabla: " + id + " fue truncada con exito"));
                        return "";
                    }
                    else mensajes.AddLast(mensa.error("La tabla:" + id + " no existe en a DB: " + baseD, l, c, "Semantico"));
                }
                else
                {
                    Usuario usuario = TablaBaseDeDatos.getUsuario(user);
                    if (usuario != null)
                    {
                        Boolean permiso = TablaBaseDeDatos.getPermiso(usuario, baseD);
                        if (permiso)
                        {
                            Boolean enUso = TablaBaseDeDatos.getEnUso(baseD, user);
                            if (!enUso)
                            {
                                Tabla tabla = TablaBaseDeDatos.getTabla(db, id);
                                if (tabla != null)
                                {
                                    tabla.columnas = new LinkedList<Columna>();
                                    tabla.datos = new LinkedList<Data>();
                                    mensajes.AddLast(mensa.message("La tabla: " + id + " fue truncada con exito"));
                                    return "";
                                }
                                else mensajes.AddLast(mensa.error("La tabla:" + id + " no existe en a DB: " + baseD, l, c, "Semantico"));
                                   
                            }
                            else mensajes.AddLast(mensa.error("La DB: " + baseD + " esta siendo utilizada por otro usuario", l, c, "Semantico"));
                        }
                        else mensajes.AddLast(mensa.error("El usuario " + user + " no tiene permisos en la DB: " + baseD, l, c, "Semantico"));
                    }
                    else mensajes.AddLast(mensa.error("No existe el usuario: " + user, l, c, "Semantico"));
                }
            }
            else mensajes.AddLast(mensa.error("No existe la DB: " + baseD + " o no se ha usado el comando USE", l, c, "Semantico"));


            return null;
        }
    }
}
