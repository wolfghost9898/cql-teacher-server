﻿using cql_teacher_server.CQL.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cql_teacher_server.Herramientas;
using System.Globalization;
using cql_teacher_server.CHISON.Componentes;
using cql_teacher_server.CHISON;

namespace cql_teacher_server.CQL.Componentes
{
    public class Expresion : InstruccionCQL
    {
        Expresion a { set; get; }
        Expresion b { set; get; }

        Object valor { set; get; }

        string operacion { set; get; }

        int linea1 { set; get; }
        int columna1 { set; get; }

        string casteo { set; get; }

        string tipoA { set; get; }

        LinkedList<Expresion> listaUser { set; get; }
        string idAs { set; get; }
  

        /* 
         * Constructor de la clase para casteos explicitos tambien sirve para acceder a User Types:
         * CADENA,ENTERO,DECIMAL,FECHA,HORA,BOOLEAN
         * @a expresion a evalular
         * @casteo a que se quiere convertir
         * @operacion tipo de operacion
         * @l1 linea del operador izquierdo
         * @c1 columna del operador izquierdo
         */
        public Expresion(Expresion a, string operacion, int linea1, int columna1, string casteo)
        {
            this.a = a;
            this.operacion = operacion;
            this.linea1 = linea1;
            this.columna1 = columna1;
            this.casteo = casteo;
        }


        /*
         * Constructor de la clase para una operacion binario:
         * SUMA,RESTA,MULTIPLICACION,DIVISION,MODULAR,MENOR,MAYOR,MENORIGUAL,MAYORIGUAL,IGUAL,POTENCIA
         * @a operador izquierdo
         * @b operador derecho
         * @operacion tipo de operacion.
         * @l1 linea del operador izquierdo
         * @c1 columna del operador izquierdo
         */
        public Expresion(Expresion a, Expresion b, string operacion, int l1, int c1)
        {
            this.a = a;
            this.b = b;
            this.operacion = operacion;
            this.linea1 = l1;
            this.columna1 = c1;
        }

        /* 
         * Constructor de la clase para operaciones unarias:
         * NEGACION,NEGATIVO
         * @a operador izquierdo
         * @operacion tipo de operacion
         * @l1 linea del operador izquierdo
         * @c1 columna del operador izquierdo
         */
        public Expresion(Expresion a, string operacion, int l1, int c1)
        {
            this.a = a;
            this.operacion = operacion;
            this.linea1 = l1;
            this.columna1 = c1;
        }

        /* 
         * Constructor de la clase para valores puntuales:
         * CADENA,ENTERO,DECIMAL,FECHA,HORA,BOOLEAN
         * @valor operador izquierdo
         * @operacion tipo de operacion
         * @l1 linea del operador izquierdo
         * @c1 columna del operador izquierdo
         */
        public Expresion(object valor, string operacion, int linea1, int columna1)
        {
            this.valor = valor;
            this.operacion = operacion;
            this.linea1 = linea1;
            this.columna1 = columna1;
        }


        /*
         * Constructor de la clase para instacias de user types
         * @operacion el tipo de operacion que se realizara
         * @linea1 es la linea del id
         * @columna1 el la columna del id
         * @tipoB es el tipo de Instancia
         */

        public Expresion(string operacion, int linea1, int columna1, string tipoB)
        {
            this.operacion = operacion;
            this.linea1 = linea1;
            this.columna1 = columna1;
            this.tipoA = tipoB;
        }

        /*
         * Constructor de la clase para asignar valores a un usertype
         * @operacion el tipo de operacion que se realizara
         * @linea1 es la linea del id
         * @columna es la columna del id
         * @lista es una lista de valores a asignar
         * @idAs es la referencia a que USER TYPE nos referimos
         */


        public Expresion(string operacion, int linea1, int columna1, LinkedList<Expresion> lista, string idAs)
        {
            this.operacion = operacion;
            this.linea1 = linea1;
            this.columna1 = columna1;
            this.listaUser = lista;
            this.idAs = idAs;
        }










        /*
         * Metodo de la implementacion de la clase InstruccionCQL
         * @ts Tabla de simbolos global
         * @user usuario que ejecuta la accion
         * @baseD base de datos en la que se realizara la accion, es pasada por referencia
         */

        public object ejecutar(TablaDeSimbolos ts, string user, ref string baseD, LinkedList<string> mensajes)
        {
            object op1 = (a == null) ? null : a.ejecutar(ts, user, ref baseD, mensajes);
            object op2 = (b == null) ? null : b.ejecutar(ts, user, ref baseD, mensajes);

            //-------------------------------------------------- TIPO DE OPERACION ------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------------

            //-------------------------------------------------- SUMA -------------------------------------------------------------------------
            if (operacion.Equals("SUMA") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 + (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)((int)op1 + (Double)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)((Double)op1 + (int)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)((Double)op1 + (Double)op2);
                else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(int)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(string)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(Double)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(string)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(Boolean)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(string)) return op1.ToString() + op2.ToString();
                else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(string)) return op1.ToString() + op2.ToString();
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede sumar " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //--------------------------------------------------- RESTA ----------------------------------------------------------------------
            else if (operacion.Equals("RESTA") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 - (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)((int)op1 - (Double)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)((Double)op1 - (int)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)((Double)op1 - (Double)op2);
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede restar " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //---------------------------------------------------- MULTIPLICACION --------------------------------------------------------------
            else if (operacion.Equals("MULTIPLICACION") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 * (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)((int)op1 * (Double)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)((Double)op1 * (int)op2);
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)((Double)op1 * (Double)op2);
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede multiplicar " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //----------------------------------------------------- POTENCIA -------------------------------------------------------------------
            else if (operacion.Equals("POTENCIA") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (Double)(Math.Pow((int)op1, (int)op2));
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)(Math.Pow((int)op1, (Double)op2));
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)(Math.Pow((Double)op1, (int)op2));
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)(Math.Pow((Double)op1, (Double)op2));
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede obtener una potencia de " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-------------------------------------------------------MODULO --------------------------------------------------------------------
            else if (operacion.Equals("MODULO") && op1 != null && op2 != null)
            {
                if (!op2.ToString().Equals("0"))
                {
                    if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 % (int)op2;
                    else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)((int)op1 % (Double)op2);
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)((Double)op1 % (int)op2);
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)((Double)op1 % (Double)op2);
                    else
                    {
                        Mensaje mes = new Mensaje();
                        mensajes.AddLast(mes.error("No se puede obtener el modulo de  " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede obtener el modulo un divisor 0", linea1, columna1, "Semantico"));
                    return null;
                }

            }
            //------------------------------------------------------DIVISION -------------------------------------------------------------------
            else if (operacion.Equals("DIVISION") && op1 != null && op2 != null)
            {
                if (!op2.ToString().Equals("0"))
                {
                    if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 / (int)op2;
                    else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (Double)((int)op1 / (Double)op2);
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)((Double)op1 / (int)op2);
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)((Double)op1 / (Double)op2);
                    else
                    {
                        Mensaje mes = new Mensaje();
                        mensajes.AddLast(mes.error("No se puede dividir  " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede dividir entre 0", linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-----------------------------------------------------MAYOR------------------------------------------------------------------------
            else if (operacion.Equals("MAYOR") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 > (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 > (Double)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 > (int)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 > (Double)op2;
                else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 > (TimeSpan)op2;
                else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 > (DateTime)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede conocer el mayor de   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-----------------------------------------------------MENOR------------------------------------------------------------------------
            else if (operacion.Equals("MENOR") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 < (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 < (Double)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 < (int)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 < (Double)op2;
                else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 < (TimeSpan)op2;
                else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 < (DateTime)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede conocer el menor de   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-----------------------------------------------------MAYORIGUAL------------------------------------------------------------------------
            else if (operacion.Equals("MAYORIGUAL") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 >= (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 >= (Double)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 >= (int)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 >= (Double)op2;
                else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 >= (TimeSpan)op2;
                else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 >= (DateTime)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede conocer el mayor igual de   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-----------------------------------------------------MENORIGUAL------------------------------------------------------------------------
            else if (operacion.Equals("MENORIGUAL") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 <= (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 <= (Double)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 <= (int)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 <= (Double)op2;
                else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 <= (TimeSpan)op2;
                else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 <= (DateTime)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede conocer el menor igual de   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-----------------------------------------------------IGUALIGUAL------------------------------------------------------------------------
            else if (operacion.Equals("IGUALIGUAL") )
            {
                if (op1 != null && op2 != null)
                {
                    if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 == (int)op2;
                    else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 == (Double)op2;
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 == (int)op2;
                    else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 == (Double)op2;
                    else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 == (TimeSpan)op2;
                    else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 == (DateTime)op2;
                    else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(string)) return op1.ToString().Equals(op2.ToString());
                    else if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(Boolean)) return (Boolean)op1 == (Boolean)op2;
                    else if (op1.GetType() == typeof(InstanciaUserType) && op2.GetType() == typeof(InstanciaUserType)) return ((InstanciaUserType)op1).tipo.Equals(((InstanciaUserType)op2).tipo);
                    else
                    {
                        Mensaje mes = new Mensaje();
                        mensajes.AddLast(mes.error("No se puede conocer si es igual   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else if (op1 != null && op2 == null)
                {
                    if (op1.GetType() == typeof(InstanciaUserType) && op2 == null) return ((InstanciaUserType)op1).lista == null;
                    else
                    {
                        Mensaje mes = new Mensaje();
                        mensajes.AddLast(mes.error("No se puede conocer si es igual   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else if (op2 != null && op1 == null)
                {
                    if (op2.GetType() == typeof(InstanciaUserType) && op1 == null) return ((InstanciaUserType)op2).lista == null;
                    else
                    {
                        Mensaje mes = new Mensaje();
                        mensajes.AddLast(mes.error("No se puede conocer si es igual   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else return true;
            }
            //-----------------------------------------------------DIFERENTE------------------------------------------------------------------------
            else if (operacion.Equals("DIFERENTE") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(int) && op2.GetType() == typeof(int)) return (int)op1 != (int)op2;
                else if (op1.GetType() == typeof(int) && op2.GetType() == typeof(Double)) return (int)op1 != (Double)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(int)) return (Double)op1 != (int)op2;
                else if (op1.GetType() == typeof(Double) && op2.GetType() == typeof(Double)) return (Double)op1 != (Double)op2;
                else if (op1.GetType() == typeof(TimeSpan) && op2.GetType() == typeof(TimeSpan)) return (TimeSpan)op1 != (TimeSpan)op2;
                else if (op1.GetType() == typeof(DateTime) && op2.GetType() == typeof(DateTime)) return (DateTime)op1 != (DateTime)op2;
                else if (op1.GetType() == typeof(string) && op2.GetType() == typeof(string)) return !op1.ToString().Equals(op2.ToString());
                else if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(Boolean)) return (Boolean)op1 != (Boolean)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede conocer si es diferente   " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //----------------------------------------------------- OR -----------------------------------------------------------------------------
            else if (operacion.Equals("OR") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(Boolean)) return (Boolean)op1 || (Boolean)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede aplicar (OR) " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //----------------------------------------------------- AND -----------------------------------------------------------------------------
            else if (operacion.Equals("AND") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(Boolean)) return (Boolean)op1 && (Boolean)op2;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede aplicar (AND) " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //----------------------------------------------------- XOR -----------------------------------------------------------------------------
            else if (operacion.Equals("XOR") && op1 != null && op2 != null)
            {
                if (op1.GetType() == typeof(Boolean) && op2.GetType() == typeof(Boolean))
                {
                    if ((Boolean)op1 && (Boolean)op2) return false;
                    else if (!(Boolean)op1 && !(Boolean)op2) return false;
                    else return true;
                }
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede aplicar (XOR) " + op1.ToString() + " con " + op2.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //------------------------------------------------------ NEGATIVO ( - NUMERO ) ----------------------------------------------------------
            else if (operacion.Equals("NEGATIVO") && op1 != null)
            {
                if (op1.GetType() == typeof(int)) return (int)op1 * -1;
                else if (op1.GetType() == typeof(Double)) return (Double)op1 * -1;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede convertir a negativo a  " + op1.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //------------------------------------------------------ NEGATIVO ( - NUMERO ) ----------------------------------------------------------
            else if (operacion.Equals("NEGACION") && op1 != null)
            {
                if (op1.GetType() == typeof(Boolean)) return !(Boolean)op1;
                else
                {
                    Mensaje mes = new Mensaje();
                    mensajes.AddLast(mes.error("No se puede convertir a negar a  " + op1.ToString(), linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //------------------------------------------------------- CONVERSION EXPLICITA --------------------------------------------------------
            else if (operacion.Equals("CONVERSION") && op1 != null)
            {
                if (casteo.Equals("string")) return op1.ToString();
                else if (op1.GetType() == typeof(string) && casteo.Equals("int"))
                {
                    Boolean salida = false;
                    int datoS = 0;
                    Boolean conver = Int32.TryParse(op1.ToString(), out datoS);
                    if (!conver)
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No se puede castear: " + op1 + " to (int)", linea1, columna1, "Semantico"));
                        return null;
                    }
                    else return datoS;
                }
                else if (op1.GetType() == typeof(string) && casteo.Equals("time"))
                {
                    try
                    {
                        TimeSpan result = TimeSpan.ParseExact(op1.ToString(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
                        return result;
                    }
                    catch (Exception e)
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No se puede castear: " + op1 + " to (Time)", linea1, columna1, "Semantico"));
                        return null;
                    }


                }
                else if (op1.GetType() == typeof(string) && casteo.Equals("double"))
                {
                    Boolean salida = false;
                    Double result;

                    Boolean conver = Double.TryParse(op1.ToString(), out result);
                    if (!conver)
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No se puede castear: " + op1 + " to (Double)", linea1, columna1, "Semantico"));
                        return null;
                    }
                    else return result;
                }
                else if (op1.GetType() == typeof(string) && casteo.Equals("date"))
                {

                    try
                    {
                        DateTime result = DateTime.ParseExact(op1.ToString(), "yyyy\\-MM\\-dd", CultureInfo.InvariantCulture);
                        return result;
                    }
                    catch (Exception e)
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No se puede castear: " + op1 + " to (Date)", linea1, columna1, "Semantico"));
                        return null;
                    }

                }
                else
                {
                    Mensaje men = new Mensaje();
                    mensajes.AddLast(men.error("No se puede castear: " + op1 + " to (" + casteo + ")", linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //-------------------------------------------------------- INSTANCIA -------------------------------------------------------------------
            else if (operacion.Equals("INSTANCIA"))
            {
                BaseDeDatos db = TablaBaseDeDatos.getBase(baseD);
                if (db != null)
                {
                    User_Types a = TablaBaseDeDatos.getUserTypeV(tipoA.ToLower(), db);
                    if(a != null)
                    {
                        LinkedList<Atributo> lista = getAtributos(a,db);
                        if (lista != null)
                        {
                            return new InstanciaUserType(tipoA, lista);
                        }
                        else return null;

                    }
                    else
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No existe el USER TYPE " + tipoA , linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else
                {
                    Mensaje men = new Mensaje();
                    mensajes.AddLast(men.error("No se puede acceder a la base de datos: " + baseD + " asegurese de usar el comando USE", linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //--------------------------------------------------------- ACCESO A USER TYPES ------------------------------------------------------
            else if(operacion.Equals("ACCESOUSER") && op1 != null)
            {
                if(op1.GetType() == typeof(InstanciaUserType))
                {
                    InstanciaUserType temp = (InstanciaUserType)op1;
                    foreach (Atributo a in temp.lista)
                    {
                        if (a.nombre.Equals(casteo)) return a.valor;
                    }
                    Mensaje men = new Mensaje();
                    mensajes.AddLast(men.error("No se encontro el atributo: " + casteo, linea1, columna1, "Semantico"));
                    return null;
                }
                else
                {
                    Mensaje men = new Mensaje();
                    mensajes.AddLast(men.error("Se necesita un User Type para acceder a sus atributos  ", linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //------------------------------------------------------- ASIGNACION DE VALORES A UN USER TYPE ---------------------------------------
            else if (operacion.Equals("ASIGNACIONUSER"))
            {
                BaseDeDatos db = TablaBaseDeDatos.getBase(baseD);
                if(db != null)
                {
                    User_Types a = TablaBaseDeDatos.getUserTypeV(idAs.ToLower(), db);
                    if(a != null)
                    {
                        LinkedList<Atributo> listaA = getAtributos(a, db);
                        if (listaA.Count() == listaUser.Count())
                        {
                            LinkedList<Atributo> newLista = compareListas(listaA, listaUser, ts, user, ref baseD, mensajes);
                            if (newLista == null) return null;

                            InstanciaUserType ius = new InstanciaUserType(idAs.ToLower(), newLista);
                            return ius;
                        }
                        else
                        {
                            Mensaje men = new Mensaje();
                            mensajes.AddLast(men.error("La cantidad de atributos no con cuerda con la del user type" + idAs, linea1, columna1, "Semantico"));
                            return null;
                        }
                    }
                    else
                    {
                        Mensaje men = new Mensaje();
                        mensajes.AddLast(men.error("No existe el USER TYPE " + idAs, linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else
                {
                    Mensaje men = new Mensaje();
                    mensajes.AddLast(men.error("No se puede acceder a la base de datos: " + baseD + " asegurese de usar el comando USE", linea1, columna1, "Semantico"));
                    return null;
                }
            }
            //------------------------------------------------- ENTERO -------------------------------------------------------------------------
            else if (operacion.Equals("ENTERO")) return Int32.Parse(valor.ToString());
            //------------------------------------------------- DOUBLE -------------------------------------------------------------------------
            else if (operacion.Equals("DECIMAL")) return Double.Parse(valor.ToString());
            //-------------------------------------------------- CADENA ------------------------------------------------------------------------
            else if (operacion.Equals("CADENA")) return valor.ToString();
            //--------------------------------------------------- BOOLEAN ----------------------------------------------------------------------
            else if (operacion.Equals("BOOLEAN")) return Boolean.Parse(valor.ToString());
            //---------------------------------------------------- HORA ------------------------------------------------------------------------
            else if (operacion.Equals("HORA")) return TimeSpan.Parse(valor.ToString());
            //-----------------------------------------------------FECHA------------------------------------------------------------------------
            else if (operacion.Equals("FECHA")) return DateTime.Parse(valor.ToString());
            //----------------------------------------------------- ID --------------------------------------------------------------------------
            else if (operacion.Equals("ID"))
            {
                object a = ts.getValor(valor.ToString().TrimStart().TrimEnd());
                if (a == null)
                {
                    Mensaje me = new Mensaje();
                    mensajes.AddLast(me.error("La variable " + valor + " no ha sido instanciada", linea1, columna1, "Semantico"));
                    return null;
                }
                else if (a.ToString().Equals("none"))
                {
                    Mensaje me = new Mensaje();
                    mensajes.AddLast(me.error("La variable " + valor + " no existe en este ambito", linea1, columna1, "Semantico"));
                    return null;
                }
                else
                {
                    return a;
                }
            }
            //----------------------------------------------------- NULL ------------------------------------------------------------------------
            return null;
        }


        /*
         * Metodo que recorre un USER TYPE y crear una declaracion de cada atributo
         * @u User type que se recorrera
         * @bd base de datos donde se encuentra el user type
         */
        private LinkedList<Atributo> getAtributos(User_Types u, BaseDeDatos bd)
        {
            LinkedList<Atributo> at = new LinkedList<Atributo>();
            foreach(Attrs a in u.type)
            {
                Atributo att;
                if (a.type.ToLower().Equals("string")) att = new Atributo(a.name.ToLower(), null, "string");
                else if (a.type.ToLower().Equals("date")) att = new Atributo(a.name.ToLower(), null, "date");
                else if (a.type.ToLower().Equals("time")) att = new Atributo(a.name.ToLower(), null, "time");
                else if (a.type.ToLower().Equals("int")) att = new Atributo(a.name.ToLower(), 0, "int");
                else if (a.type.ToLower().Equals("double")) att = new Atributo(a.name.ToLower(), 0.0, "double");
                else if (a.type.ToLower().Equals("boolean")) att = new Atributo(a.name.ToLower(), false, "boolean");
                else
                {
                    User_Types temp = TablaBaseDeDatos.getUserTypeV(a.type.ToLower(), bd);

                    if (temp != null)
                    {
                        LinkedList<Atributo> tempA = getAtributos(temp, bd);
                        InstanciaUserType tm = new InstanciaUserType(a.type, tempA);
                        if (tempA != null)
                        {
                            att = new Atributo(a.name.ToLower(), tm, a.type.ToLower());
                        }
                        else return null;
                    }
                    else return null;
                }
                at.AddLast(att);
            }
            return at;
        }

        /*
         * Metodo que recorre la lista de expresiones y la lista de atributos y los compara
         * 
         */

        public LinkedList<Atributo> compareListas(LinkedList<Atributo> a , LinkedList<Expresion> e, TablaDeSimbolos ts, string user,ref string baseD, LinkedList<string> mensaje)
        {
            LinkedList<Atributo> listaAtributo = new LinkedList<Atributo>();
            for (int i = 0; i < a.Count(); i++)
            {
                Atributo at = a.ElementAt(i);
                object operador1 = e.ElementAt(i).ejecutar(ts, user, ref baseD, mensaje);
                if (operador1 != null)
                {
                    if (at.tipo.Equals("string") && (operador1.GetType() == typeof(string))) listaAtributo.AddLast(new Atributo(at.nombre, (string)operador1, "string"));
                    else if (at.tipo.Equals("int") && (operador1.GetType() == typeof(int))) listaAtributo.AddLast(new Atributo(at.nombre, (int)operador1, "int"));
                    else if (at.tipo.Equals("double") && (operador1.GetType() == typeof(Double))) listaAtributo.AddLast(new Atributo(at.nombre, (Double)operador1, "double"));
                    else if (at.tipo.Equals("boolean") && (operador1.GetType() == typeof(Boolean))) listaAtributo.AddLast(new Atributo(at.nombre, (Boolean)operador1, "boolean"));
                    else if (at.tipo.Equals("date") && (operador1.GetType() == typeof(DateTime))) listaAtributo.AddLast(new Atributo(at.nombre, (DateTime)operador1, "date"));
                    else if (at.tipo.Equals("time") && (operador1.GetType() == typeof(TimeSpan))) listaAtributo.AddLast(new Atributo(at.nombre, (TimeSpan)operador1, "time"));
                    else if (operador1.GetType() == typeof(InstanciaUserType))
                    {
                        InstanciaUserType temp = (InstanciaUserType)operador1;
                        if(at.tipo.Equals(temp.tipo.ToLower())) listaAtributo.AddLast(new Atributo(at.nombre, temp, temp.tipo.ToLower()));
                        else
                        {
                            Mensaje men = new Mensaje();
                            mensaje.AddLast(men.error("No conincide el tipo: " + at.tipo + " con el Tipo: " + temp.tipo, linea1, columna1, "Semantico"));
                            return null;
                        }
                    }
                    else
                    {
                        Mensaje men = new Mensaje();
                        mensaje.AddLast(men.error("No coincide el tipo: " + at.tipo + " con el valor: " + operador1, linea1, columna1, "Semantico"));
                        return null;
                    }
                }
                else
                {
                    if (at.tipo.Equals("string") || at.tipo.Equals("int") || at.tipo.Equals("double") || at.tipo.Equals("map") || at.tipo.Equals("boolean") || at.tipo.Equals("date") || at.tipo.Equals("time"))
                    {
                        Mensaje men = new Mensaje();
                        mensaje.AddLast(men.error("No coincide el tipo: " + at.tipo + " con el valor: null " , linea1, columna1, "Semantico"));
                        return null;
                    }
                    InstanciaUserType temp = new InstanciaUserType(at.tipo, null);
                    listaAtributo.AddLast(new Atributo(at.nombre, temp, temp.tipo.ToLower()));
                }
            }
            return listaAtributo;
        }
    }


}
