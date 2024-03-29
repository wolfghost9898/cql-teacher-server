﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Gramatica
{
    public class GramaticaCQL : Grammar 
    {

        public GramaticaCQL() : base(caseSensitive: false)
        {
            #region ER
            StringLiteral CADENA = new StringLiteral("cadena", "\"");
            var FECHA = new RegexBasedTerminal("fecha", "\\'\\d{4}\\-(((0)[0-9])|((1)[0-2]))\\-([0-2][0-9]|(3)[0-1])\\'");
            var HORA = new RegexBasedTerminal("hora", "\\'(([0-1][0-9])|2[0-3])\\:([0-5][0-9])\\:([0-5][0-9])\\'");
            var DECIMALN = new RegexBasedTerminal("decimal", "[0-9]+\\.[0-9]+");
            var ENTERO = new NumberLiteral("entero");


            IdentifierTerminal ID = new IdentifierTerminal("ID");

            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "/*", "*/");
            #endregion

            #region Terminales



            var INT = ToTerm("int");
            var DECIMAL = ToTerm("double");
            var STRING = ToTerm("string");
            var BOOL = ToTerm("boolean");
            var DATE = ToTerm("date");
            var TIME = ToTerm("time");
            var COUNTER = ToTerm("counter");
            var MAP = ToTerm("MAP");


            var TRUE = ToTerm("True");
            var FALSE = ToTerm("False");
            var NULL = ToTerm("null");

            var MAYOR = ToTerm(">");
            var MENOR = ToTerm("<");
            var MAYORIG = ToTerm(">=");
            var MENORIG = ToTerm("<=");
            var IGUAIG = ToTerm("==");
            var DIFERENTE = ToTerm("!=");
            var NEGACION = ToTerm("!");
            var OR = ToTerm("||");
            var AND = ToTerm("&&");
            var XOR = ToTerm("^");


            var DIVISION = ToTerm("/");
            var MODULO = ToTerm("%");
            var POTENCIA = ToTerm("**");
            var POR = ToTerm("*");
            var RESTA = ToTerm("-");
            var SUMA = ToTerm("+");

            var INCREMENTO = ToTerm("++");
            var DECREMENTO = ToTerm("--");




            var USE = ToTerm("USE");
            var CREATE = ToTerm("CREATE");
            var TYPE = ToTerm("TYPE");
            var DATABASE = ToTerm("DATABASE");
            var NEW = ToTerm("new");
            var AS = ToTerm("AS");
            var DROP = ToTerm("DROP");
            var TABLE = ToTerm("TABLE");
            var PRIMARY = ToTerm("PRIMARY");
            var KEY = ToTerm("KEY");
            var ALTER = ToTerm("ALTER");
            var ADD = ToTerm("ADD");
            var TRUNCATE = ToTerm("TRUNCATE");
            var USER = ToTerm("USER");
            var WITH = ToTerm("WITH");
            var PASSWORD = ToTerm("PASSWORD");
            var GRANT = ToTerm("GRANT");
            var ON = ToTerm("ON");
            var REVOKE = ToTerm("REVOKE");
            var INSERT = ToTerm("INSERT");
            var INTO = ToTerm("INTO");
            var VALUES = ToTerm("VALUES");
            var UPDATE = ToTerm("UPDATE");
            var SET = ToTerm("SET");
            var WHERE = ToTerm("WHERE");
            var DELETE = ToTerm("DELETE");
            var FROM = ToTerm("FROM");
            var SELECT = ToTerm("SELECT");
            var LIMIT = ToTerm("LIMIT");
            var ORDER = ToTerm("ORDER");
            var BY = ToTerm("BY");
            var ASC = ToTerm("ASC");
            var DESC = ToTerm("DESC");
            var IN = ToTerm("IN");


            var COUNT = ToTerm("COUNT");
            var MIN = ToTerm("MIN");
            var MAX = ToTerm("MAX");
            var SUM = ToTerm("SUM");
            var AVG = ToTerm("AVG");

            var IF = ToTerm("IF");
            var ELSE = ToTerm("ELSE");
            var NOT = ToTerm("NOT");
            var EXISTS = ToTerm("EXISTS");

            var SWITCH = ToTerm("SWITCH");
            var CASE = ToTerm("CASE");
            var BREAK = ToTerm("Break");
            var DEFAULT = ToTerm("Default");
            var WHILE = ToTerm("WHILE");
            var DO = ToTerm("DO");
            var FOR = ToTerm("FOR");
            var GET = ToTerm("GET");
            var REMOVE = ToTerm("REMOVE");
            var SIZE = ToTerm("SIZE");
            var CLEAR = ToTerm("CLEAR");
            var CONTAINS = ToTerm("CONTAINS");
            var LIST = ToTerm("LIST");

            var RETURN = ToTerm("RETURN");

            var LOG = ToTerm("LOG");

            var PROCEDURE = ToTerm("PROCEDURE");

            var CALL = ToTerm("CALL");

            var LENGTH = ToTerm("LENGTH");
            var TOUPPERCASE = ToTerm("ToUpperCase");
            var TOLOWERCASE = ToTerm("ToLowerCase");
            var STARSTWITH = ToTerm("StartsWith");
            var ENDSWITH = ToTerm("EndsWith");
            var SUBSTRING = ToTerm("subString");

            var GETYEAR = ToTerm("getYear");
            var GETMONTH = ToTerm("getMonth");
            var GETDAY = ToTerm("getDay");
            var GETHOUR = ToTerm("GetHour");
            var GETMINUTS = ToTerm("getMinuts");
            var GETSECONDS = ToTerm("getSeconds");
            var TODAY = ToTerm("TODAY");
            var NOW = ToTerm("NOW");

            var CONTINUE = ToTerm("Continue");

            var CURSOR = ToTerm("CURSOR");
            var IS = ToTerm("IS");
            var OPEN = ToTerm("OPEN");
            var CLOSE = ToTerm("CLOSE");
            var EACH = ToTerm("EACH");

            var TRY = ToTerm("TRY");
            var CATCH = ToTerm("CATCH");
            var ARITHMETICEXCEPTION = ToTerm("ArithmeticException");
            var TYPEALREDYEXISTS = ToTerm("TypeAlreadyExists");
            var THROW = ToTerm("THROW");
            var MESSAGE = ToTerm("MESSAGE");
            var BDALREADYEXISTS = ToTerm("BDAlreadyExists");
            var BDDONTEXISTS = ToTerm("BDDontExists");
            var USEDBEXCEPTION = ToTerm("useDBException");
            var TABLEALREADYEXISTS = ToTerm("TableAlreadyExists");
            var TABLEDONTEXISTS = ToTerm("TableDontExists");
            var COUNTERTYPEXCEPTION = ToTerm("CounterTypeException");
            var USERALREADYEXISTS = ToTerm("UserAlreadyExists");
            var USERDONTEXISTS = ToTerm("UserDontExists");
            var VALUESEXCEPTION = ToTerm("ValuesException");
            var COLUMNEXCEPTION = ToTerm("ColumnException");
            var INDEXOUTEXCEPTION = ToTerm("IndexOutException");
            var NULLPOINTEREXCEPTION = ToTerm("NullPointerException");
            var NUMBERRETURNSEXCEPTION = ToTerm("NumberReturnsException");
            var FUNCTIONALREADYEXISTS = ToTerm("FunctionAlreadyExists");
            var PROCEDUREALREADYEXISTS = ToTerm("ProcedurealreadyExists");
            var OBJECTALREADYEXISTS = ToTerm("ObjectAlreadyExists");
            var BATCHEXCEPTION = ToTerm("BatchException");
            var EXCEPTION = ToTerm("Exception");

            var COMMIT = ToTerm("COMMIT");
            var ROLLBACK = ToTerm("ROLLBACK");

            var BEGIN = ToTerm("BEGIN");
            var BATCH = ToTerm("BATCH");
            var APPLY = ToTerm("APPLY");




            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            #endregion

            #region No Terminales

            NonTerminal inicio = new NonTerminal("inicio");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");

            NonTerminal use = new NonTerminal("use");

            NonTerminal createDatabase = new NonTerminal("createdatabase");

            NonTerminal expresion = new NonTerminal("expresion");

            NonTerminal tipoVariable = new NonTerminal("tipovariable");
            NonTerminal tipoVariable2 = new NonTerminal("tipovariable");

            NonTerminal declaracion = new NonTerminal("declaracion");
            NonTerminal parametros = new NonTerminal("declaracion");
            NonTerminal declaracionA = new NonTerminal("declaracionA");

            NonTerminal user_type = new NonTerminal("usertype");
            NonTerminal lista_user_type = new NonTerminal("listausertype");

            NonTerminal asignacion = new NonTerminal("asignacion");
            NonTerminal asignacionA = new NonTerminal("asignaciona");

            NonTerminal ifsuperior = new NonTerminal("ifsuperior");
            NonTerminal insif = new NonTerminal("if");
            NonTerminal inselseif = new NonTerminal("elseif");
            NonTerminal inselse = new NonTerminal("else");

            NonTerminal inSwitch = new NonTerminal("inswitch");
            NonTerminal lisCase = new NonTerminal("liscase");
            NonTerminal inCase = new NonTerminal("incase");
            NonTerminal inDefault = new NonTerminal("indefault");

            NonTerminal inDrop = new NonTerminal("indrop");

            NonTerminal inTable = new NonTerminal("intable");
            NonTerminal compPrimarias = new NonTerminal("compprimarias");
            NonTerminal defColumn = new NonTerminal("defcolumn");
            NonTerminal listaPrimary = new NonTerminal("listprimary");

            NonTerminal inAlterTable = new NonTerminal("inaltertable");
            NonTerminal colTable = new NonTerminal("coltable");

            NonTerminal inDropTable = new NonTerminal("indroptable");

            NonTerminal inTruncaTable = new NonTerminal("intruncatetable");

            NonTerminal inUser = new NonTerminal("inuser");

            NonTerminal editUser = new NonTerminal("edituser");

            NonTerminal inInsert = new NonTerminal("ininsert");
            NonTerminal listValues = new NonTerminal("listvalues");

            NonTerminal inUpdate = new NonTerminal("inupdate");
            NonTerminal listaSet = new NonTerminal("listaset");

            NonTerminal inDelete = new NonTerminal("indelete");
            NonTerminal tipoDelete = new NonTerminal("tipodelete");

            NonTerminal tiposCampos = new NonTerminal("tipocampos");
            NonTerminal inSelect = new NonTerminal("inselect");
            NonTerminal inWhere = new NonTerminal("inwhere");
            NonTerminal inLimit = new NonTerminal("inlimit");
            NonTerminal inOrder = new NonTerminal("inorder");
            NonTerminal orderby = new NonTerminal("orderby");

            NonTerminal userTypeCQL = new NonTerminal("usertypecql");

            NonTerminal inBreak = new NonTerminal("inbreak");

            NonTerminal inWhile = new NonTerminal("inwhile");

            NonTerminal inDoWhile = new NonTerminal("indowhile");

            NonTerminal inFor = new NonTerminal("infor");

            NonTerminal listaMap = new NonTerminal("listamap");
            NonTerminal mapvalue = new NonTerminal("mapvalue");

            NonTerminal inInsertMap = new NonTerminal("ininsertmap");
            NonTerminal inSetMap = new NonTerminal("insetmap");
            NonTerminal inRemoveMap = new NonTerminal("inremovemap");
            NonTerminal inClear = new NonTerminal("inclear");

            NonTerminal inFunction = new NonTerminal("infuncion");

            NonTerminal inReturn = new NonTerminal("inreturn");
            NonTerminal llamadaFuncion = new NonTerminal("llamadafuncion");

            NonTerminal inLog = new NonTerminal("inlog");

            NonTerminal inProcedure = new NonTerminal("inprocedure");
            NonTerminal listaParametros = new NonTerminal("listaparametros");
            NonTerminal listaDeclaracion = new NonTerminal("listadeclaracion");
            NonTerminal listaDeclaracion2 = new NonTerminal("listadeclaracion2");

            NonTerminal callProcedure = new NonTerminal("callprocedure");

            NonTerminal inAsignacionEspecial = new NonTerminal("inasignacionespecial");
            NonTerminal inMultiAsignacion = new NonTerminal("inmultiasignacion");
            NonTerminal tempExAsignacion = new NonTerminal("expresion");

            NonTerminal inContinue = new NonTerminal("incontinue");

            NonTerminal inCursor = new NonTerminal("incursor");
            NonTerminal inExpresion = new NonTerminal("expresion");
            NonTerminal inOperacionCursor = new NonTerminal("inoperacioncursor");

            NonTerminal inForEach = new NonTerminal("inforeach");

            NonTerminal inTryCatch = new NonTerminal("intrycatch");
            NonTerminal tiposExcepciones = new NonTerminal("tiposexcepciones");
            NonTerminal inThrow = new NonTerminal("inthrow");

            NonTerminal inCommit = new NonTerminal("incommit");
            NonTerminal inRollback = new NonTerminal("inrollback");

            NonTerminal inInstruccion = new NonTerminal("instruccion");

            NonTerminal inBatch = new NonTerminal("inbatch");
            NonTerminal instruccionesBatch = new NonTerminal("instruccion");
            NonTerminal instruccionesBatch2 = new NonTerminal("instrucciones");

            #endregion

            #region Gramatica

            #region Instrucciones de inicio
            inicio.Rule = instrucciones;

            instrucciones.Rule = instrucciones + inInstruccion
                               | inInstruccion
                               ;

            instruccionesBatch2.Rule = instruccionesBatch2 + instruccionesBatch
                                     | instruccionesBatch
                                     ;

            instruccionesBatch.Rule = inInsert + ";"
                                    | inUpdate + ";"
                                    | inDelete + ";"
                                    ;

            inInstruccion.Rule = use + ";"
                             | inInsertMap + ";"
                             | inSetMap + ";"
                             | inMultiAsignacion + ";"
                             | inRemoveMap + ";"
                             | inClear + ";"
                             | createDatabase + ";"
                             | declaracion + ";"
                             | declaracionA + ";"
                             | user_type + ";"
                             | asignacion + ";"
                             | ifsuperior
                             | inSwitch
                             | inDrop + ";"
                             | inTable + ";"
                             | inAlterTable + ";"
                             | inDropTable + ";"
                             | inTruncaTable + ";"
                             | inUser + ";"
                             | editUser + ";"
                             | inInsert + ";"
                             | inUpdate + ";"
                             | inDelete + ";"
                             | inSelect + ";"
                             | inBreak + ";"
                             | inWhile
                             | inDoWhile + ";"
                             | inFor
                             | inFunction
                             | inReturn + ";"
                             | inLog + ";"
                             | inProcedure
                             | inContinue + ";"
                             | inCursor + ";"
                             | inExpresion + ";"
                             | inOperacionCursor + ";"
                             | inForEach
                             | inTryCatch
                             | inThrow + ";"
                             | inCommit + ";"
                             | inRollback + ";"
                             | inBatch + ";"
                             ;
            instruccion.Rule = use + ";"
                             | inInsertMap + ";"
                             | inSetMap + ";"
                             | inMultiAsignacion + ";"
                             | inRemoveMap + ";"
                             | inClear + ";"
                             | createDatabase + ";"
                             | declaracion + ";"
                             | declaracionA + ";"
                             | asignacion + ";"
                             | ifsuperior
                             | inSwitch
                             | inDrop + ";"
                             | inTable + ";"
                             | inAlterTable + ";"
                             | inDropTable + ";"
                             | inTruncaTable + ";"
                             | inUser + ";"
                             | editUser + ";"
                             | inInsert + ";"
                             | inUpdate + ";"
                             | inDelete + ";"
                             | inSelect + ";"
                             | inBreak + ";"
                             | inWhile
                             | inDoWhile + ";"
                             | inFor
                             | inReturn + ";"
                             | inLog + ";"
                             | inContinue + ";"
                             | inCursor + ";"
                             | inExpresion + ";"
                             | inOperacionCursor + ";"
                             | inForEach
                             | inTryCatch
                             | inThrow + ";"
                             | inCommit + ";"
                             | inRollback + ";"
                             | inBatch + ";"
                             ;
            #endregion
            //--------------------------------------------------- USE ---------------------------------------------------------------------------------------

            #region use
            use.Rule = USE + ID;
            #endregion

            #region crear base de datos
            //--------------------------------------------------- BASES DE DATOS-----------------------------------------------------------------------------
            createDatabase.Rule = CREATE + DATABASE + ID
                                | CREATE + DATABASE + IF + NOT + EXISTS + ID
                                ;

            #endregion
            //---------------------------------------------------------- EXPRESIONES ------------------------------------------------------------------------
            #region expresiones
            expresion.Rule = expresion + OR + expresion
                           | expresion + AND + expresion
                           | expresion + XOR + expresion
                           | NEGACION + expresion
                           | expresion + IGUAIG + expresion
                           | expresion + DIFERENTE + expresion
                           | expresion + MAYOR + expresion
                           | expresion + MENOR + expresion
                           | expresion + MAYORIG + expresion
                           | expresion + MENORIG + expresion
                           | expresion + SUMA + expresion
                           | expresion + RESTA + expresion
                           | expresion + DIVISION + expresion
                           | expresion + POR + expresion
                           | expresion + MODULO + expresion
                           | expresion + POTENCIA + expresion
                           | RESTA + expresion
                           | COUNT + "(" + "<<" + inSelect + ">>" + ")"
                           | MIN + "(" + "<<" + inSelect + ">>" + ")"
                           | MAX + "(" + "<<" + inSelect + ">>" + ")"
                           | SUM + "(" + "<<" + inSelect + ">>" + ")"
                           | AVG + "(" + "<<" + inSelect + ">>" + ")"
                           | ToTerm("(") + expresion + ToTerm(")")
                           | ToTerm("(") + tipoVariable + ToTerm(")") + expresion
                           | NEW + ID
                           | NEW + MAP + "<" + tipoVariable2 + "," + tipoVariable2 + ">"
                           | NEW + LIST + "<" + tipoVariable2 + ">"
                           | NEW + SET + "<" + tipoVariable2 + ">"
                           | expresion + "." + ID
                           | "{" + listValues + "}"
                           | "{" + listValues + "}" + AS + ID
                           | "[" + listaMap + "]"
                           | "[" + listValues + "]"
                           | expresion + "." + SIZE + "(" + ")"
                           | ENTERO
                           | ToTerm("@") + ID
                           | ToTerm("@") + ID + INCREMENTO
                           | ToTerm("@") + ID + DECREMENTO
                           | expresion + ToTerm("?") + expresion + ToTerm(":") + expresion
                           | expresion + "." + GET + "(" + expresion + ")"
                           | expresion + "." + CONTAINS + "(" + expresion + ")"
                           | expresion + "." + LENGTH + "(" + ")"
                           | expresion + "." + TOUPPERCASE + "(" + ")"
                           | expresion + "." + TOLOWERCASE + "(" + ")"
                           | expresion + "." + STARSTWITH + "(" + expresion + ")"
                           | expresion + "." + ENDSWITH + "(" + expresion + ")"
                           | expresion + "." + SUBSTRING + "(" + expresion + "," + expresion + ")"
                           | expresion + "." + GETYEAR + "(" + ")"
                           | expresion + "." + GETMONTH + "(" + ")"
                           | expresion + "." + GETDAY + "(" + ")"
                           | expresion + "." + GETHOUR + "(" + ")"
                           | expresion + "." + GETMINUTS + "(" + ")"
                           | expresion + "." + GETSECONDS + "(" + ")"
                           | expresion + "." + MESSAGE 
                           | TODAY + "(" + ")"
                           | NOW + "(" + ")"
                           | ID + IN + expresion
                           | llamadaFuncion
                           | CADENA
                           | FECHA
                           | HORA
                           | TRUE
                           | FALSE
                           | DECIMALN
                           | ID
                           | NULL
                           | callProcedure
                           ;

            inExpresion.Rule = ToTerm("@") + ID + INCREMENTO
                             | ToTerm("@") + ID + DECREMENTO
                             | llamadaFuncion
                             | callProcedure
                             ;

           

            #endregion

            //------------------------------------------------------ TIPO DE VARIABLES --------------------------------------------------------------------------
            #region Tipo de variables
            tipoVariable.Rule = STRING
                              | INT
                              | BOOL
                              | DECIMAL
                              | DATE
                              | TIME
                              | ID
                              | COUNTER
                              | MAP
                              | LIST
                              | SET
                              | CURSOR
                              ;
            tipoVariable2.Rule = STRING
                              | INT
                              | BOOL
                              | DECIMAL
                              | DATE
                              | TIME
                              | ID
                              | COUNTER
                              | CURSOR
                              | MAP + "<" + tipoVariable2 + ToTerm(",") + tipoVariable2 + ">"
                              | LIST + "<" + tipoVariable2 + ">"
                              | SET + "<" + tipoVariable2 + ">"
                              ;
            #endregion
            //------------------------------------------------------- DECLARACION DE VARIABLE --------------------------------------------------------------------
            #region declaracion de variables
            declaracion.Rule = declaracion + "," + "@" + ID
                             | tipoVariable + "@" + ID
                             ;
            #endregion
            //--------------------------------------------------------- DECLARAR Y ASIGNAR UNA VARIABLE ------------------------------------------------------------
            #region declaracion y asignacion
            declaracionA.Rule = tipoVariable + "@" + ID + "=" + expresion
                              | declaracion + "," + "@" + ID + "=" + expresion
                              ;
            #endregion
            //---------------------------------------------------------- CREACION DE USER TYPES ---------------------------------------------------------------------
            #region creacion de usertypes
            user_type.Rule = CREATE + TYPE + IF + NOT + EXISTS + ID + "(" + lista_user_type + ")"
                           | CREATE + TYPE + ID + "(" + lista_user_type + ")"
                           ;
            #endregion
            //---------------------------------------------------------- Lista de atributos de un user_type ----------------------------------------------------------
            #region atributos de un usertype
            lista_user_type.Rule = lista_user_type + "," + ID + tipoVariable2
                                 | ID + tipoVariable2
                                 ;
            #endregion

            //--------------------------------------------------------- ASIGNACION DE VARIABLES ----------------------------------------------------------------------
            #region asignacion de variables
            asignacion.Rule = "@" + ID + ToTerm("=") + expresion

                            | "@" + ID + ToTerm("+=") + expresion
                            | "@" + ID + ToTerm("*=") + expresion
                            | "@" + ID + ToTerm("-=") + expresion
                            | "@" + ID + ToTerm("/=") + expresion
                            | asignacionA + ToTerm("=") + expresion
                            | asignacionA + ToTerm("+=") + expresion
                            | asignacionA + ToTerm("*=") + expresion
                            | asignacionA + ToTerm("-=") + expresion
                            | asignacionA + ToTerm("/=") + expresion
                            ;

            inMultiAsignacion.Rule = inAsignacionEspecial + "=" + tempExAsignacion;

            tempExAsignacion.Rule = callProcedure;

            inAsignacionEspecial.Rule = inAsignacionEspecial + "," + "@" + ID
                                       | "@" + ID;

            #endregion
            //-------------------------------------------------------- asignacion de un atributo ----------------------------------------------------------------------
            #region setear atributos
            asignacionA.Rule = expresion + "." + ID
                             ;
            #endregion





            //---------------------------------------------------------- IF SUPERIOR ------------------------------------------------------------------------
            #region if / else if / ese
            ifsuperior.Rule = insif
                            | insif + inselseif
                            | insif + inselseif + inselse
                            | insif + inselse
                            ;
            //------------------------------------------------------------- IF -----------------------------------------------------------------------------
            insif.Rule = IF + "(" + expresion + ")" + "{" + instrucciones + "}";
            //------------------------------------------------------------- ELSE IF -----------------------------------------------------------------------------
            inselseif.Rule = inselseif + ELSE + IF + "(" + expresion + ")" + "{" + instrucciones + "}"
                            | ELSE + IF + "(" + expresion + ")" + "{" + instrucciones + "}"
                            ;
            //------------------------------------------------------------- IF -----------------------------------------------------------------------------
            inselse.Rule = ELSE + "{" + instrucciones + "}";

            //------------------------------------------------------------ SWITCH -------------------------------------------------------------------------
            inSwitch.Rule = SWITCH + "(" + expresion + ")" + "{" + lisCase + inDefault + "}";
            //------------------------------------------------------------- LISTADO DE CASES ---------------------------------------------------------------
            lisCase.Rule = lisCase + inCase
                         | inCase
                         ;
            //------------------------------------------------------------- CASE ---------------------------------------------------------------------------
            inCase.Rule = CASE + expresion + ":" + "{" + instrucciones + "}";
            #endregion

            #region CQL
            //-------------------------------------------------------------- DEFAULT -----------------------------------------------------------------------
            inDefault.Rule = DEFAULT + ":" + "{" + instrucciones + "}";
            //------------------------------------------------------------- DROP DATABASE ------------------------------------------------------------------
            inDrop.Rule = DROP + DATABASE + ID
                        | DROP + DATABASE + IF + EXISTS + ID;

            //-------------------------------------------------------------- CREATE TABLE ------------------------------------------------------------------
            inTable.Rule = CREATE + TABLE + ID + "(" + defColumn + ")"
                         | CREATE + TABLE + IF + NOT + EXISTS + ID + "(" + defColumn + ")"
                         | CREATE + TABLE + ID + "(" + defColumn + "," + compPrimarias + ")"
                         | CREATE + TABLE + IF + NOT + EXISTS + ID + "(" + defColumn + "," + compPrimarias + ")"
                         ;
            //--------------------------------------------------------------- COLUMNAS ---------------------------------------------------------------------
            defColumn.Rule = defColumn + "," + ID + tipoVariable2
                           | defColumn + "," + ID + tipoVariable2 + PRIMARY + KEY
                           | ID + tipoVariable2
                           | ID + tipoVariable2 + PRIMARY + KEY
                           ;

            //---------------------------------------------------- llaves compuestas ------------------------------------------------------------------------

            compPrimarias.Rule = PRIMARY + KEY + "(" + listaPrimary + ")";

            //---------------------------------------------------------------- LISTA DE COLUMNAS QUE PUEDEN SER PRIMARIAS -----------------------------------
            listaPrimary.Rule = listaPrimary + "," + ID
                              | ID
                              ;

            //----------------------------------------------------------- ALTER TABLE ------------------------------------------------------------------------
            inAlterTable.Rule = ALTER + TABLE + ID + ADD + colTable
                              | ALTER + TABLE + ID + DROP + listaPrimary
                              ;

            //------------------------------------------------------------ LISTA DE COLUMNAS A AGREGAR ------------------------------------------------------
            colTable.Rule = colTable + "," + ID + tipoVariable2
                          | ID + tipoVariable2
                          ;

            //------------------------------------------------------------ DROP TABLE ----------------------------------------------------------------------
            inDropTable.Rule = DROP + TABLE + ID
                             | DROP + TABLE + IF + EXISTS + ID
                             ;
            //------------------------------------------------------------ TRUNCATE TABLE ------------------------------------------------------------------
            inTruncaTable.Rule = TRUNCATE + TABLE + ID;

            //------------------------------------------------------------- CREATE USER ---------------------------------------------------------------------
            inUser.Rule = CREATE + USER + ID + WITH + PASSWORD + CADENA;


            //------------------------------------------------------------ GRANT/REVOKE USER----------------------------------------------------------------
            editUser.Rule = GRANT + ID + ON + ID
                          | REVOKE + ID + ON + ID
                          ;

            //------------------------------------------------------------ INSERT INTO --------------------------------------------------------------------
            inInsert.Rule = INSERT + INTO + ID + VALUES + "(" + listValues + ")"
                          | INSERT + INTO + ID + "(" + listaPrimary + ")" + VALUES + "(" + listValues + ")";
            ;

            listValues.Rule = listValues + "," + expresion
                            | expresion
                            ;



            //---------------------------------------------------------------UPDATE--------------------------------------------------------------------------
            inUpdate.Rule = UPDATE + ID + SET + listaSet
                          | UPDATE + ID + SET + listaSet + WHERE + expresion
                          ;

            listaSet.Rule = listaSet + "," + ID + "=" + expresion
                          | listaSet + "," + userTypeCQL + "=" + expresion
                          | ID + "=" + expresion
                          | userTypeCQL + "=" + expresion
                          ;

            userTypeCQL.Rule = expresion + "." + ID
                             | expresion + "[" + expresion + "]"
                             ;

            //--------------------------------------------------------------- DELETE -------------------------------------------------------------------------
            inDelete.Rule = DELETE + FROM + ID
                          | DELETE + tipoDelete + FROM + ID
                          | DELETE + FROM + ID + WHERE + expresion
                          | DELETE + tipoDelete + FROM + ID + WHERE + expresion
                          ;
            tipoDelete.Rule = expresion + "[" + expresion + "]"
                            ;


            //---------------------------------------------------------------- SELECT -------------------------------------------------------------------------
            tiposCampos.Rule = POR
                             | listValues
                             ;

            inSelect.Rule = SELECT + tiposCampos + FROM + ID
                          | SELECT + tiposCampos + FROM + ID + inWhere
                          | SELECT + tiposCampos + FROM + ID + inLimit
                          | SELECT + tiposCampos + FROM + ID + inOrder
                          | SELECT + tiposCampos + FROM + ID + inWhere + inOrder
                          | SELECT + tiposCampos + FROM + ID + inWhere + inLimit
                          | SELECT + tiposCampos + FROM + ID + inOrder + inLimit
                          | SELECT + tiposCampos + FROM + ID + inWhere + inOrder + inLimit
                          ;


            inLimit.Rule = LIMIT + expresion;

            inWhere.Rule = WHERE + expresion;

            inOrder.Rule = inOrder + "," + orderby
                         | ORDER + BY + orderby
                         ;

            orderby.Rule = ID
                         | ID + ASC
                         | ID + DESC
                         ;
            #endregion

            #region Ciclos
            //----------------------------------------------- WHILE -----------------------------------------------------------------------------------------
            inWhile.Rule = WHILE + "(" + expresion + ")" + "{" + instrucciones + "}";

            //--------------------------------------------- DO WHILE ---------------------------------------------------------------------------------------
            inDoWhile.Rule = DO + "{" + instrucciones + "}" + WHILE + "(" + expresion + ")";

            //----------------------------------------------- FOR ------------------------------------------------------------------------------------------
            inFor.Rule = FOR + "(" + declaracionA + ";" + expresion + ";" + expresion + ")" + "{" + instrucciones + "}"
                       | FOR + "(" + asignacion + ";" + expresion + ";" + expresion + ")" + "{" + instrucciones + "}"
                       ;

            //---------------------------------------------------------------------------------------------------------------------------------------------
            inBreak.Rule = BREAK;

            inContinue.Rule = CONTINUE;
            #endregion

            #region MAp
            //----------------------------------------------- EXPRESIONES DENTRO DE UN MAP ----------------------------------------------
            listaMap.Rule = listaMap + "," + mapvalue
                          | mapvalue
                          ;

            mapvalue.Rule = expresion + ":" + expresion;

            #endregion

            #region Operacion Collections
            inInsertMap.Rule = expresion + "." + INSERT + "(" + expresion + "," + expresion + ")"
                             | expresion + "." + INSERT + "(" + expresion + ")"
                             ;

            inSetMap.Rule = expresion + "." + SET + "(" + expresion + "," + expresion + ")";

            inRemoveMap.Rule = expresion + "." + REMOVE + "(" + expresion + ")";

            inClear.Rule = expresion + "." + CLEAR + "(" + ")";
            #endregion

            #region Funciones/Procedures

            //------------------------------------------------- FUNCIONES -----------------------------------------------------------------------------------
            inFunction.Rule = tipoVariable + ID + "(" + ")" + "{" + instrucciones + "}"
                            | tipoVariable + ID + "(" + listaDeclaracion + ")" + "{" + instrucciones + "}"
                            ;

            //------------------------------------------------- RETURN --------------------------------------------------------------------------------------
            inReturn.Rule = RETURN + expresion
                          | RETURN + listValues
                          ;


            inLog.Rule = LOG + "(" + expresion + ")";

            //---------------------------------------------------- PROCEDURE ------------------------------------------------------------------------------
            inProcedure.Rule = PROCEDURE + ID + "(" + ")" + "," + "(" + ")" + "{" + instrucciones + "}"
                             | PROCEDURE + ID + "(" + listaDeclaracion + ")" + "," + "(" + ")" + "{" + instrucciones + "}"
                             | PROCEDURE + ID + "(" + ")" + "," + "(" + listaDeclaracion2 + ")" + "{" + instrucciones + "}"
                             | PROCEDURE + ID + "(" + listaDeclaracion + ")" + "," + "(" + listaDeclaracion2 + ")" + "{" + instrucciones + "}"
                             ;

            listaParametros.Rule = listaParametros + "," + "(" + listaDeclaracion + ")"
                                 | "(" + listaDeclaracion + ")"
                                 ;

            listaDeclaracion.Rule = listaDeclaracion + "," + parametros
                                  | parametros
                                  ;

            listaDeclaracion2.Rule = listaDeclaracion2 + "," + parametros
                                 | parametros
                                 ;


            parametros.Rule = tipoVariable + "@" + ID;

            callProcedure.Rule = CALL + ID + "(" + listValues + ")"
                               | CALL + ID + "(" + ")"
                               ;

            llamadaFuncion.Rule = ID + "(" + ")"
                               | ID + "(" + listValues + ")"
                               ;
            #endregion

            #region Cursor
            inCursor.Rule = CURSOR + "@" + ID + IS + inSelect
                          | CURSOR + "@" + ID + "=" + expresion;

            inOperacionCursor.Rule = OPEN + "@" + ID
                                   | CLOSE + "@" + ID
                                   ;

            inForEach.Rule = FOR + EACH + "(" + listaDeclaracion + ")" + IN + "@" + ID + "{" + instrucciones + "}";
            #endregion

            #region Try Catch

            inTryCatch.Rule = TRY + "{" + instrucciones + "}" + CATCH + "(" + tiposExcepciones + "@" + ID + ")" + "{" + instrucciones + "}";

            tiposExcepciones.Rule = ARITHMETICEXCEPTION
                                  | TYPEALREDYEXISTS
                                  | BDALREADYEXISTS
                                  | BDDONTEXISTS
                                  | USEDBEXCEPTION
                                  | TABLEALREADYEXISTS
                                  | TABLEDONTEXISTS
                                  | COUNTERTYPEXCEPTION
                                  | USERALREADYEXISTS
                                  | USERDONTEXISTS
                                  | VALUESEXCEPTION
                                  | COLUMNEXCEPTION
                                  | INDEXOUTEXCEPTION
                                  | NULLPOINTEREXCEPTION
                                  | NUMBERRETURNSEXCEPTION
                                  | FUNCTIONALREADYEXISTS
                                  | PROCEDUREALREADYEXISTS
                                  | OBJECTALREADYEXISTS
                                  | BATCHEXCEPTION
                                  | EXCEPTION
                                  ;

            inThrow.Rule = THROW + NEW + tiposExcepciones;

            #endregion

            #region Cambios fisicos

            inCommit.Rule = COMMIT;

            inRollback.Rule = ROLLBACK;

            #endregion

            #region Batch
            inBatch.Rule = BEGIN + BATCH + instruccionesBatch2 + APPLY + BATCH ;
            #endregion

            #endregion

            #region Preferencias

            RegisterOperators(1, Associativity.Left, OR);
            RegisterOperators(2, Associativity.Left, AND, XOR);
            RegisterOperators(3, Associativity.Left, IGUAIG, DIFERENTE);
            RegisterOperators(4, Associativity.Left, MAYOR, MAYORIG, MENOR, MENORIG);
            RegisterOperators(5, Associativity.Left, SUMA, RESTA);
            RegisterOperators(6, Associativity.Left, POR, DIVISION, MODULO);
            RegisterOperators(7, Associativity.Right, POTENCIA);
            RegisterOperators(8, Associativity.Right, NEGACION);
            RegisterOperators(9, Associativity.Neutral, ToTerm("("), ToTerm(")"));

            this.MarkPunctuation("(", ")",";",",");



            this.Root = inicio;
            inicio.ErrorRule = SyntaxError + inicio;
            #endregion
        }
    }
}
