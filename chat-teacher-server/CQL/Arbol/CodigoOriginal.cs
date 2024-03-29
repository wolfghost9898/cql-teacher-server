﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.CQL.Arbol
{
    public class CodigoOriginal
    {
        ParseTreeNode raiz { set; get; }

        /*
         * CONSTRUCTOR DE LA CLASE
         * @param {raiz} arbol a analizar
         */
        public CodigoOriginal(ParseTreeNode raiz)
        {
            this.raiz = raiz;
        }


        /*

        public string instrucciones(ParseTreeNode raiz)
        {
            //------------------ instrucciones instruccion -------------
            if (raiz.ChildNodes.Count == 2)
            {
                string lista = instrucciones(raiz.ChildNodes.ElementAt(0));
                lista += "\n" + instruccion(raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
                return lista;
            }
            //------------------  instruccion -------------
            else
            {
                string res = instruccion(raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                return res;
            }
        }

        /*
         * Metodo que recorre el arbol por completo
         * @raiz es el nodo raiz del arbol a analizar
         * @return una InstruccionCQL o una LinkedList<InstruccionCQL>
         */
        /*
                public string instruccion(ParseTreeNode raiz)
                {
                    string token = raiz.Term.Name;
                    ParseTreeNode hijo = raiz;

                    switch (token)
                    {
                        //-------------------------------- USE DB ----------------------------------------------------------------
                        case "use":
                            string id = hijo.ChildNodes.ElementAt(1).Token.Text;
                            return "USE " + id;

                        // ----------------------------------- CREATE DATABASE ------------------------------------------------------
                        case "createdatabase":
                            string idB = "";

                            //--------------------------------------- CREATE DATABASE ID ------------------------------------------
                            if (hijo.ChildNodes.Count() == 3)
                            {
                                idB = hijo.ChildNodes.ElementAt(2).Token.Text.ToLower().TrimEnd().TrimStart(); 
                                return "CREATE DATABASE " + idB ;
                             }
                            //---------------------------------------- CREATE DATABASE IF NOT EXISTS ID -----------------------------------------
                            else
                            {
                                idB = hijo.ChildNodes.ElementAt(5).Token.Text.ToLower().TrimEnd().TrimStart();
                                return "CREATE DATABASE IF NOT EXISTS " + idB;
                            }


                        //------------------------------------------- Expresion ---------------------------------------------------------------
                        case "expresion":
                            return resolver_expresion(hijo)  ;

                        //----------------------------------------------- DECLARACION DE VARIABLE -----------------------------------------------
                        case "declaracion":
                            string tokend = hijo.ChildNodes.ElementAt(0).Term.Name;
                            string t = "";
                            string i = "";
                            string lista = "";

                            if (tokend.Equals("declaracion"))
                            {
                                lista = instruccion(hijo) + ",";
                                t = declaracionTipo(hijo.ChildNodes.ElementAt(0));
                            }
                            else t = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                            i = hijo.ChildNodes.ElementAt(2).Token.Text;
                            t = t.ToLower().TrimEnd().TrimStart();
                            i = i.ToLower().TrimEnd().TrimStart();
                            return t + " " + lista + i ;

                        //----------------------------------------------- DECLARACION ASIGNACION -----------------------------------------------------
                        case "declaracionA":
                            string tokenA = hijo.ChildNodes.ElementAt(0).Term.Name;
                            string tA = "";
                            string iA = "";
                            string listaTemp = "";
                            if (tokenA.Equals("declaracion"))
                            {
                                listaTemp = instruccion(hijo) + ",";
                                tA = declaracionTipo(hijo.ChildNodes.ElementAt(0));
                            }
                            else tA = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                            iA = hijo.ChildNodes.ElementAt(2).Token.Text;
                            tA = tA.ToLower().TrimEnd().TrimStart();
                            iA = iA.ToLower().TrimEnd().TrimStart();
                            return tA + " " + listaTemp + iA + " = " + resolver_expresion(hijo.ChildNodes.ElementAt(4));

                        //------------------------------------------------------ ASIGNACION DE VARIABLES --------------------------------------------------
                        case "asignacion":
                            string nameT = hijo.ChildNodes.ElementAt(0).Term.Name;
                            nameT = nameT.ToLower().TrimEnd().TrimStart();
                            string idAs = "";
                            string operaAs = "";
                            if (nameT.Equals("asignaciona"))
                            {
                                idAs = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2).Token.Text;
                                idAs = idAs.ToLower().TrimEnd().TrimStart();
                                operaAs = hijo.ChildNodes.ElementAt(1).Token.Text;
                                return resolver_expresion(hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0)) + "." + idAs + " " + operaAs + " " + resolver_expresion(hijo.ChildNodes.ElementAt(2));

                            }


                            idAs = hijo.ChildNodes.ElementAt(1).Token.Text;
                            idAs = idAs.ToLower().TrimEnd().TrimStart();



                            operaAs = hijo.ChildNodes.ElementAt(2).Token.Text;
                            operaAs = operaAs.ToLower().TrimEnd().TrimStart();
                            return "@" + idAs + "  " + operaAs + " " + resolver_expresion(hijo.ChildNodes.ElementAt(3)) ;

                        //------------------------------------------------------------- IF SUPERIOR --------------------------------------------------------------
                        case "ifsuperior":
                            //------------------------------------------------------- IF ------------------------------------------------------------------------
                            //------------------------------------------------ IF --------------------------------------------------------------------------------
                            int lif = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Line;
                            int cif = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Column;
                            string listadoInS = instrucciones(hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(3));
                            string codigoIF = "if ( " + resolver_expresion(hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1)) + "){\n" + listadoInS + "\n}";

                            //------------------------------------ IF ELSE / IF ELSE IF + ----------------------------------------------------------------------------
                            if (hijo.ChildNodes.Count() == 2)
                            {
                                string idSeparator = hijo.ChildNodes.ElementAt(1).Term.Name;
                                //----------------------------------------------- ELSE -------------------------------------------------------------------------------
                                if (idSeparator.Equals("else"))
                                {

                                    listadoInS = instrucciones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2));
                                    codigoIF += "\nelse{\n" + listadoInS + "\n}";
                                }
                                else
                                {
                                    string listatemp = resolver_if(hijo.ChildNodes.ElementAt(1));
                                    codigoIF += "\n" + listatemp;
                                }
                            }
                            //-----------------------------------------IF ELSE IF + ELSE ------------------------------------------------------------------------------
                            else if (hijo.ChildNodes.Count() == 3)
                            {
                                string listatemp = resolver_if(hijo.ChildNodes.ElementAt(1));
                                codigoIF += "\n" + listatemp;

                                listadoInS = instrucciones(hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(2));
                                codigoIF += "\nelse{\n" + listadoInS + "\n}";
                            }
                            return codigoIF;

                        //------------------------------------------------------- SWITCH --------------------------------------------------------------------------------
                        case "inswitch":
                            Expresion condicionS = resolver_expresion(hijo.ChildNodes.ElementAt(1));
                            LinkedList<Case> listadoCase = resolver_case(hijo.ChildNodes.ElementAt(3), condicionS);
                            LinkedList<InstruccionCQL> listadoR = new LinkedList<InstruccionCQL>();


                            ParseTreeNode nodeDefault = hijo.ChildNodes.ElementAt(4);
                            int lineaD = nodeDefault.ChildNodes.ElementAt(0).Token.Location.Line;
                            int columnaD = nodeDefault.ChildNodes.ElementAt(0).Token.Location.Column;
                            LinkedList<InstruccionCQL> listadoD = instrucciones(nodeDefault.ChildNodes.ElementAt(3));
                            listadoCase.AddLast(new Case(listadoD, lineaD, columnaD));


                            listadoR.AddLast(new Switch(listadoCase));
                            return listadoR;

                        //---------------------------------------------------------- DROP ------------------------------
                        case "indrop":
                            int lineaDrop;
                            int columnaDrop;
                            string idDrop;
                            Boolean flagD;
                            if (raiz.ChildNodes.Count() == 5)
                            {
                                lineaDrop = hijo.ChildNodes.ElementAt(4).Token.Location.Line;
                                columnaDrop = hijo.ChildNodes.ElementAt(4).Token.Location.Column;
                                idDrop = hijo.ChildNodes.ElementAt(4).Token.Text;
                                idDrop = idDrop.ToLower().TrimEnd().TrimStart();
                                flagD = true;
                            }
                            else
                            {
                                lineaDrop = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                                columnaDrop = hijo.ChildNodes.ElementAt(2).Token.Location.Column;
                                idDrop = hijo.ChildNodes.ElementAt(2).Token.Text;
                                idDrop = idDrop.ToLower().TrimEnd().TrimStart();
                                flagD = false;
                            }

                            LinkedList<InstruccionCQL> listaDR = new LinkedList<InstruccionCQL>();
                            listaDR.AddLast(new Drop(idDrop, lineaDrop, columnaDrop, flagD));
                            return listaDR;


                        //-------------------------------------------------------- DROP TABLE --------------------------------------------------------
                        case "indroptable":
                            LinkedList<InstruccionCQL> listaRDT = new LinkedList<InstruccionCQL>();
                            string idDT;
                            int lDT;
                            int cDT;
                            Boolean flagDT;

                            if (hijo.ChildNodes.Count() == 5)
                            {
                                idDT = hijo.ChildNodes.ElementAt(4).Token.Text;
                                idDT = idDT.ToLower().TrimEnd().TrimStart();

                                lDT = hijo.ChildNodes.ElementAt(4).Token.Location.Line;
                                cDT = hijo.ChildNodes.ElementAt(4).Token.Location.Column;

                                flagDT = true;

                            }
                            else
                            {
                                idDT = hijo.ChildNodes.ElementAt(2).Token.Text;
                                idDT = idDT.ToLower().TrimEnd().TrimStart();

                                lDT = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                                cDT = hijo.ChildNodes.ElementAt(2).Token.Location.Column;

                                flagDT = false;
                            }
                            listaRDT.AddLast(new DropTable(idDT, flagDT, lDT, cDT));
                            return listaRDT;


                        //----------------------------------------------------- TRUNCATE TABLE ------------------------------------------------------------
                        case "intruncatetable":
                            LinkedList<InstruccionCQL> listaRTT = new LinkedList<InstruccionCQL>();
                            string idTT;
                            int lTT;
                            int cTT;

                            idTT = hijo.ChildNodes.ElementAt(2).Token.Text;
                            idTT = idTT.ToLower().TrimEnd().TrimStart();

                            lTT = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            cTT = hijo.ChildNodes.ElementAt(2).Token.Location.Line;

                            listaRTT.AddLast(new TruncateTable(idTT, lTT, cTT));
                            return listaRTT;

                        //-------------------------------------------------CREATE USER--------------------------------------------------------------------------
                        case "inuser":
                            LinkedList<InstruccionCQL> listaRCU = new LinkedList<InstruccionCQL>();

                            string idCU = hijo.ChildNodes.ElementAt(2).Token.Text;
                            idCU = idCU.ToLower().TrimEnd().TrimStart();

                            string paCU = hijo.ChildNodes.ElementAt(5).Token.Text;
                            paCU = paCU.TrimStart('\"').TrimEnd('\"');

                            int lCU = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            int cCU = hijo.ChildNodes.ElementAt(2).Token.Location.Column;

                            listaRCU.AddLast(new CreateUser(idCU, paCU, lCU, cCU));

                            return listaRCU;


                        //------------------------------------------------ EDIT USER ----------------------------------------------------------------------------
                        case "edituser":
                            LinkedList<InstruccionCQL> listaEU = new LinkedList<InstruccionCQL>();

                            string operacionEU = hijo.ChildNodes.ElementAt(0).Token.Text;
                            operacionEU = operacionEU.ToLower().TrimEnd().TrimStart();

                            string uEU = hijo.ChildNodes.ElementAt(1).Token.Text;
                            uEU = uEU.ToLower().TrimEnd().TrimStart();

                            string dEU = hijo.ChildNodes.ElementAt(3).Token.Text;
                            dEU = dEU.ToLower().TrimEnd().TrimStart();

                            int lEU = hijo.ChildNodes.ElementAt(1).Token.Location.Line;
                            int cEU = hijo.ChildNodes.ElementAt(1).Token.Location.Line;

                            if (operacionEU.Equals("grant")) operacionEU = "GRANT";
                            else operacionEU = "REVOKE";

                            listaEU.AddLast(new EditUser(uEU, dEU, lEU, cEU, operacionEU));

                            return listaEU;




                        //-------------------------------------------------- INSERT VALUES --------------------------------------------------------------------------
                        case "ininsert":
                            LinkedList<InstruccionCQL> listaII = new LinkedList<InstruccionCQL>();
                            string idII;
                            int lII;
                            int cII;
                            idII = hijo.ChildNodes.ElementAt(2).Token.Text;
                            idII = idII.ToLower().TrimEnd().TrimStart();

                            lII = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            cII = hijo.ChildNodes.ElementAt(2).Token.Location.Column;
                            if (hijo.ChildNodes.Count() == 5) listaII.AddLast(new Insert(idII, listaExpresiones(hijo.ChildNodes.ElementAt(4)), "NORMAL", lII, cII));
                            else listaII.AddLast(new Insert(idII, listaExpresiones(hijo.ChildNodes.ElementAt(5)), getCompuestas(hijo.ChildNodes.ElementAt(3)), "ESPECIAL", lII, cII));


                            return listaII;


                        //-------------------------------------------------- UPDATE --------------------------------------------------------------------------------------
                        case "inupdate":
                            LinkedList<InstruccionCQL> listaUP = new LinkedList<InstruccionCQL>();
                            string idUP;
                            int lUP;
                            int cUP;

                            idUP = hijo.ChildNodes.ElementAt(1).Token.Text;
                            idUP = idUP.ToLower().TrimEnd().TrimStart();

                            lUP = hijo.ChildNodes.ElementAt(1).Token.Location.Line;
                            cUP = hijo.ChildNodes.ElementAt(1).Token.Location.Column;

                            LinkedList<SetCQL> listaSet = resolver_setcql(hijo.ChildNodes.ElementAt(3));
                            if (hijo.ChildNodes.Count() == 6) listaUP.AddLast(new Update(idUP, listaSet, resolver_expresion(hijo.ChildNodes.ElementAt(5)), lUP, cUP, "WHERE"));
                            else listaUP.AddLast(new Update(idUP, listaSet, lUP, cUP, "NORMAL"));
                            return listaUP;




                        //----------------------------------------------------- DELETE -------------------------------------------------------------------------------------
                        case "indelete":
                            LinkedList<InstruccionCQL> listaDE = new LinkedList<InstruccionCQL>();
                            int lDE;
                            int cDE;
                            Expresion objeto;
                            Expresion atributo;
                            string iDE;
                            Expresion condicion;
                            //------------------------------------- DELETE FROM ID
                            lDE = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            cDE = hijo.ChildNodes.ElementAt(2).Token.Location.Column;
                            if (hijo.ChildNodes.Count() == 3)
                            {
                                iDE = hijo.ChildNodes.ElementAt(2).Token.Text.ToLower().TrimStart().TrimEnd();
                                objeto = null;
                                atributo = null;
                                condicion = null;
                            }
                            //---------------------------------- DELETE TIPO FROM ID ---------------------------
                            else if (hijo.ChildNodes.Count() == 4)
                            {
                                iDE = hijo.ChildNodes.ElementAt(3).Token.Text.ToLower().TrimStart().TrimEnd();
                                objeto = resolver_expresion(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
                                atributo = resolver_expresion(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2));
                                condicion = null;

                            }
                            //--------------------------------- DELETE FROM ID WHERE EXPRESION ------------------------------
                            else if (hijo.ChildNodes.Count() == 5)
                            {
                                iDE = hijo.ChildNodes.ElementAt(2).Token.Text.ToLower().TrimStart().TrimEnd();
                                objeto = null;
                                atributo = null;
                                condicion = resolver_expresion(hijo.ChildNodes.ElementAt(4));
                            }
                            //------------------------------------ DELETE TIPO FROM ID WHERE EXPRESION ---------------------------------
                            else
                            {
                                iDE = hijo.ChildNodes.ElementAt(3).Token.Text.ToLower().TrimStart().TrimEnd();
                                objeto = resolver_expresion(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
                                atributo = resolver_expresion(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2));
                                condicion = resolver_expresion(hijo.ChildNodes.ElementAt(5));
                            }

                            if (condicion == null) listaDE.AddLast(new Delete(iDE, lDE, cDE, "NORMAL", objeto, atributo));
                            else listaDE.AddLast(new Delete(iDE, lDE, cDE, "WHERE", objeto, atributo, condicion));



                            return listaDE;








                        //------------------------------------------------------- SELECT -------------------------------------------------------------------------------------
                        case "inselect":
                            LinkedList<InstruccionCQL> listaSE = new LinkedList<InstruccionCQL>();
                            string idSE = hijo.ChildNodes.ElementAt(3).Token.Text;
                            idSE = idSE.ToLower().TrimEnd().TrimStart();

                            int lSE = hijo.ChildNodes.ElementAt(3).Token.Location.Line;
                            int cSE = hijo.ChildNodes.ElementAt(3).Token.Location.Column;

                            string tkSE = hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Term.Name;
                            if (hijo.ChildNodes.Count() == 5)
                            {
                                string operaSS = hijo.ChildNodes.ElementAt(4).Term.Name;
                                if (operaSS.Equals("inwhere"))
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "a",
                                        resolver_expresion(accion.ChildNodes.ElementAt(1)), null));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "a", resolver_expresion(accion.ChildNodes.ElementAt(1)), null));
                                }
                                else if (operaSS.Equals("inlimit"))
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "c",
                                       null, resolver_expresion(accion.ChildNodes.ElementAt(1))));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "c", null, resolver_expresion(accion.ChildNodes.ElementAt(1))));
                                }
                                else if (operaSS.Equals("inorder"))
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "b",
                                        getOrdenamiento(accion)));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "b", getOrdenamiento(accion)));
                                }

                            }
                            else if (hijo.ChildNodes.Count() == 6)
                            {
                                string op1 = hijo.ChildNodes.ElementAt(4).Term.Name;
                                string op2 = hijo.ChildNodes.ElementAt(5).Term.Name;
                                if (op1.Equals("inwhere") && op2.Equals("inorder"))
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                    ParseTreeNode accion2 = hijo.ChildNodes.ElementAt(5);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "ab",
                                        resolver_expresion(accion.ChildNodes.ElementAt(1)), null, getOrdenamiento(accion2)));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "ab", resolver_expresion(accion.ChildNodes.ElementAt(1)), null, getOrdenamiento(accion2)));
                                }
                                else if (op1.Equals("inwhere") && op2.Equals("inlimit"))
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                    ParseTreeNode accion2 = hijo.ChildNodes.ElementAt(5);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "ac",
                                        resolver_expresion(accion.ChildNodes.ElementAt(1)), resolver_expresion(accion2.ChildNodes.ElementAt(1))));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "ac", resolver_expresion(accion.ChildNodes.ElementAt(1)), resolver_expresion(accion2.ChildNodes.ElementAt(1))));
                                }
                                else
                                {
                                    ParseTreeNode accion = hijo.ChildNodes.ElementAt(5);
                                    ParseTreeNode accion2 = hijo.ChildNodes.ElementAt(4);
                                    if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "bc",
                                        null, resolver_expresion(accion.ChildNodes.ElementAt(1)), getOrdenamiento(accion2)));
                                    else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "bc", null, resolver_expresion(accion.ChildNodes.ElementAt(1)), getOrdenamiento(accion2)));
                                }
                            }
                            else if (hijo.ChildNodes.Count() == 7)
                            {
                                ParseTreeNode accion = hijo.ChildNodes.ElementAt(4);
                                ParseTreeNode accion2 = hijo.ChildNodes.ElementAt(6);
                                ParseTreeNode accion3 = hijo.ChildNodes.ElementAt(5);
                                if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "abc",
                                    resolver_expresion(accion.ChildNodes.ElementAt(1)), resolver_expresion(accion2.ChildNodes.ElementAt(1)), getOrdenamiento(accion3)));
                                else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "abc", resolver_expresion(accion.ChildNodes.ElementAt(1)), resolver_expresion(accion2.ChildNodes.ElementAt(1)), getOrdenamiento(accion3)));
                            }
                            else
                            {
                                if (tkSE.Equals("listvalues")) listaSE.AddLast(new Select(idSE, listaExpresiones(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0)), lSE, cSE, "none"));
                                else listaSE.AddLast(new Select(idSE, null, lSE, cSE, "none"));
                            }


                            return listaSE;



                        //------------------------------------------------------- BREAK --------------------------------------------------------------------------------------
                        case "inbreak":
                            LinkedList<InstruccionCQL> listaBR = new LinkedList<InstruccionCQL>();
                            listaBR.AddLast(new Break());
                            return listaBR;






                        //------------------------------------------------------ WHILE ----------------------------------------------------------------------------------------
                        case "inwhile":
                            LinkedList<InstruccionCQL> listaWH = new LinkedList<InstruccionCQL>();
                            int lwh = hijo.ChildNodes.ElementAt(0).Token.Location.Line;
                            int cwh = hijo.ChildNodes.ElementAt(0).Token.Location.Column;
                            LinkedList<InstruccionCQL> cuerpowh = instrucciones(hijo.ChildNodes.ElementAt(3));
                            listaWH.AddLast(new While(lwh, cwh, resolver_expresion(hijo.ChildNodes.ElementAt(1)), cuerpowh));
                            return listaWH;




                        //------------------------------------------------------ DO WHILE ---------------------------------------------------------------------------------
                        case "indowhile":
                            LinkedList<InstruccionCQL> listaDW = new LinkedList<InstruccionCQL>();
                            int ldh = hijo.ChildNodes.ElementAt(0).Token.Location.Line;
                            int cdh = hijo.ChildNodes.ElementAt(0).Token.Location.Column;
                            LinkedList<InstruccionCQL> cuerpoch = instrucciones(hijo.ChildNodes.ElementAt(2));
                            listaDW.AddLast(new While(cdh, cdh, resolver_expresion(hijo.ChildNodes.ElementAt(5)), cuerpoch));
                            return listaDW;


                        //----------------------------------------------------- FOR ---------------------------------------------------------------------------------------
                        case "infor":
                            LinkedList<InstruccionCQL> listaF = new LinkedList<InstruccionCQL>();
                            int lf = hijo.ChildNodes.ElementAt(0).Token.Location.Line;
                            int cf = hijo.ChildNodes.ElementAt(0).Token.Location.Column;

                            LinkedList<InstruccionCQL> accionF = (LinkedList<InstruccionCQL>)instruccion(hijo.ChildNodes.ElementAt(1));
                            Expresion condiF = resolver_expresion(hijo.ChildNodes.ElementAt(2));
                            Expresion asigna = resolver_expresion(hijo.ChildNodes.ElementAt(3));
                            LinkedList<InstruccionCQL> instruF = instrucciones(hijo.ChildNodes.ElementAt(5));

                            object instru = (accionF.Count() < 1) ? null : accionF.ElementAt(0);

                            listaF.AddLast(new inFor(lf, cf, instru, condiF, asigna, instruF));

                            return listaF;





                        //---------------------------------------------------- Expresion . INSERT (KEY : VALUE ) ----------------------------------------------------------------
                        case "ininsertmap":
                            LinkedList<InstruccionCQL> listaIM = new LinkedList<InstruccionCQL>();
                            int lim = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            int cim = hijo.ChildNodes.ElementAt(2).Token.Location.Column;
                            Expresion mp = resolver_expresion(hijo.ChildNodes.ElementAt(0));
                            Expresion ky = resolver_expresion(hijo.ChildNodes.ElementAt(3));
                            if (hijo.ChildNodes.Count() == 5)
                            {
                                Expresion vl = resolver_expresion(hijo.ChildNodes.ElementAt(4));
                                listaIM.AddLast(new InsertMap(mp, ky, vl, lim, cim, "MAP"));
                            }
                            else listaIM.AddLast(new InsertMap(mp, ky, null, lim, cim, "LIST"));


                            return listaIM;




                        //-------------------------------------------------------- Expresion . SET (KEY,VALUE) -------------------------------------------------------------
                        case "insetmap":
                            LinkedList<InstruccionCQL> listaSM = new LinkedList<InstruccionCQL>();
                            int lsm = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            int csm = hijo.ChildNodes.ElementAt(2).Token.Location.Column;

                            Expresion mps = resolver_expresion(hijo.ChildNodes.ElementAt(0));
                            Expresion kys = resolver_expresion(hijo.ChildNodes.ElementAt(3));
                            Expresion vls = resolver_expresion(hijo.ChildNodes.ElementAt(4));
                            listaSM.AddLast(new SetMap(mps, kys, vls, lsm, csm, "MAP"));

                            return listaSM;






                        //-------------------------------------------------------- Expresion . REMOVE ( KEY ) ----------------------------------------------------------
                        case "inremovemap":
                            LinkedList<InstruccionCQL> lisatRM = new LinkedList<InstruccionCQL>();
                            int lrm = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            int crm = hijo.ChildNodes.ElementAt(2).Token.Location.Column;

                            Expresion mpr = resolver_expresion(hijo.ChildNodes.ElementAt(0));
                            Expresion kyr = resolver_expresion(hijo.ChildNodes.ElementAt(3));

                            lisatRM.AddLast(new RemoveMap(mpr, kyr, lrm, crm));
                            return lisatRM;






                        //------------------------------------------------------ Expresion . CLEAR ( ) -------------------------------------------------------------
                        case "inclear":
                            LinkedList<InstruccionCQL> listaCL = new LinkedList<InstruccionCQL>();

                            int lcl = hijo.ChildNodes.ElementAt(2).Token.Location.Line;
                            int ccl = hijo.ChildNodes.ElementAt(2).Token.Location.Column;

                            Expresion mcl = resolver_expresion(hijo.ChildNodes.ElementAt(0));

                            listaCL.AddLast(new Clear(mcl, lcl, ccl));
                            return listaCL;

                        //-------------------------------------------------- RETURN -----------------------------------------------------------------------------
                        case "inreturn":
                            LinkedList<InstruccionCQL> listRe = new LinkedList<InstruccionCQL>();
                            listRe.AddLast(new Return(resolver_expresion(hijo.ChildNodes.ElementAt(1))));
                            return listRe;





                        //------------------------------------------------ LOG ------------------------------------------------------------------------------
                        case "inlog":
                            LinkedList<InstruccionCQL> listaLO = new LinkedList<InstruccionCQL>();
                            listaLO.AddLast(new Log(resolver_expresion(hijo.ChildNodes.ElementAt(1))));
                            return listaLO;
                    }
                    return null;


                }


                /*
                * METODO QUE BUSCA SI EXISTE YA ESA FUNCION O NO
                * @param {id} funcion a buscar (se pasa su identificador generado)
                * @return True si la encuentra False si no
                */
                /*
        public Boolean buscarFuncion(string id)
        {
            foreach (Funcion f in TablaBaseDeDatos.listaFunciones)
            {
                if (f.identificador.Equals(id)) return true;
            }
            return false;
        }

        /*
         * Metodo que obtendra todas las columnas con las cuales se ordenara
         * @param {raiz} es el sub arbol a analizar
         * @return una lista de OrderBy
         */
         /*
        private LinkedList<OrderBy> getOrdenamiento(ParseTreeNode raiz)
        {
            LinkedList<OrderBy> lista = new LinkedList<OrderBy>();
            ParseTreeNode hijo;
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = getOrdenamiento(raiz.ChildNodes.ElementAt(0));
                hijo = raiz.ChildNodes.ElementAt(1);
            }
            else hijo = raiz.ChildNodes.ElementAt(2);

            string id = hijo.ChildNodes.ElementAt(0).Token.Text;
            id = id.ToLower().TrimEnd().TrimStart();
            if (hijo.ChildNodes.Count() == 2)
            {
                Boolean asc = false;
                string token = hijo.ChildNodes.ElementAt(1).Token.Text;
                token = token.ToLower().TrimEnd().TrimStart();
                if (token.Equals("asc")) asc = true;
                lista.AddLast(new OrderBy(id, asc));
            }
            else lista.AddLast(new OrderBy(id, false));
            return lista;
        }

        /*
         * Metodo que analiza los set
         * @raiz subarbol a analizar
         * retorna una lista de la clase SETCQL
         */
         /*

        private LinkedList<SetCQL> resolver_setcql(ParseTreeNode raiz)
        {
            LinkedList<SetCQL> lista = new LinkedList<SetCQL>();
            Expresion expresion;
            int l;
            int c;
            ParseTreeNode hijo;

            //---------------------------- LISTA SET  ID/userTypeCQL = EXPRESION ----------------------------------------------------------------
            if (raiz.ChildNodes.Count() == 4)
            {
                lista = resolver_setcql(raiz.ChildNodes.ElementAt(0));
                expresion = resolver_expresion(raiz.ChildNodes.ElementAt(3));
                l = raiz.ChildNodes.ElementAt(2).Token.Location.Line;
                c = raiz.ChildNodes.ElementAt(2).Token.Location.Column;
                hijo = raiz.ChildNodes.ElementAt(1);

            }
            else
            {
                expresion = resolver_expresion(raiz.ChildNodes.ElementAt(2));
                l = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                c = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                hijo = raiz.ChildNodes.ElementAt(0);
            }
            string term = hijo.Term.Name;
            if (term.Equals("usertypecql"))
            {
                Expresion objeto = resolver_expresion(hijo.ChildNodes.ElementAt(0));
                if (hijo.ChildNodes.Count() == 4)
                {
                    Expresion valor = resolver_expresion(hijo.ChildNodes.ElementAt(2));
                    lista.AddLast(new SetCQL(valor, expresion, objeto, "USER", l, c));
                }
                else
                {
                    string id = hijo.ChildNodes.ElementAt(2).Token.Text.ToLower().TrimEnd().TrimStart();
                    lista.AddLast(new SetCQL(id, expresion, objeto, "USER", l, c));
                }


            }
            else
            {
                string id = hijo.Token.Text.ToLower().TrimEnd().TrimStart();
                lista.AddLast(new SetCQL(id, expresion, "NORMAL", l, c));
            }
            return lista;


        }



        /*
         *Metodo que recorre todos los valores a guardar en un insert
         * @raiz nodo del subarbol a recorrer
         * retonar un lista de expresiones
         */
/*
        private LinkedList<Expresion> listaExpresiones(ParseTreeNode raiz)
        {
            LinkedList<Expresion> lista = new LinkedList<Expresion>();
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = listaExpresiones(raiz.ChildNodes.ElementAt(0));
                lista.AddLast(resolver_expresion(raiz.ChildNodes.ElementAt(1)));
            }
            else lista.AddLast(resolver_expresion(raiz.ChildNodes.ElementAt(0)));
            return lista;

        }



        /*
         * Metodo que obtiene una lista de las primarias compuestas
         * @raiz nodo del sub arbol a analizar
         */

/*
        public LinkedList<string> getCompuestas(ParseTreeNode raiz)
        {
            LinkedList<string> lista = new LinkedList<string>();
            string nombre;
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = getCompuestas(raiz.ChildNodes.ElementAt(0));
                nombre = raiz.ChildNodes.ElementAt(1).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();
            }
            else
            {
                nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();
            }
            lista.AddLast(nombre);
            return lista;
        }

        /*
         * Metodo que obtiene una lista de columnas
         * @raiz nodo del sub arbol a analizar
         */
/*
        public LinkedList<Columna> getListaColumna(ParseTreeNode raiz)
        {
            LinkedList<Columna> lista = new LinkedList<Columna>();
            string token = raiz.ChildNodes.ElementAt(0).Term.Name;
            string nombre;
            string tipo;
            Boolean pk = false;
            if (token.Equals("defcolumn"))
            {
                lista = getListaColumna(raiz.ChildNodes.ElementAt(0));
                if (raiz.ChildNodes.Count() == 5) pk = true;

                nombre = raiz.ChildNodes.ElementAt(1).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();

                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(2));
                tipo = tipo.ToLower().TrimEnd().TrimStart();

            }
            else
            {
                if (raiz.ChildNodes.Count() == 4) pk = true;
                nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();

                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(1));
                tipo = tipo.ToLower().TrimEnd().TrimStart();
            }
            lista.AddLast(new Columna(nombre, tipo, pk));
            return lista;
        }

        /*
         * Metodo que obtiene una lista de columnas
         * @raiz nodo del sub arbol a analizar
         */
/*
        public LinkedList<Columna> getListaColumnaAdd(ParseTreeNode raiz)
        {
            LinkedList<Columna> lista = new LinkedList<Columna>();
            string nombre;
            string tipo;
            Boolean pk = false;
            if (raiz.ChildNodes.Count() == 3)
            {
                lista = getListaColumnaAdd(raiz.ChildNodes.ElementAt(0));

                nombre = raiz.ChildNodes.ElementAt(1).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();

                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(2));
                tipo = tipo.ToLower().TrimEnd().TrimStart();

            }
            else
            {
                nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
                nombre = nombre.ToLower().TrimEnd().TrimStart();

                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(1));
                tipo = tipo.ToLower().TrimEnd().TrimStart();
            }
            lista.AddLast(new Columna(nombre, tipo, pk));
            return lista;
        }


        /*
         * Metodo que obtiene los atributos de un Usertype
         * @raiz nodo del arbol donde se encuentran los atributos
         */
/*
        public LinkedList<Attrs> getListaUserType(ParseTreeNode raiz)
        {
            LinkedList<Attrs> lista = new LinkedList<Attrs>();
            string id = "";
            string tipo = "";
            if (raiz.ChildNodes.Count() == 3)
            {
                lista = getListaUserType(raiz.ChildNodes.ElementAt(0));
                id = raiz.ChildNodes.ElementAt(1).Token.Text;
                id = id.ToLower().TrimEnd().TrimStart();
                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(2));
            }
            else
            {
                id = raiz.ChildNodes.ElementAt(0).Token.Text;
                id = id.ToLower().TrimEnd().TrimStart();
                tipo = concatenarTipo(raiz.ChildNodes.ElementAt(1));
            }
            tipo = tipo.ToLower().TrimEnd().TrimStart();
            Attrs a = new Attrs(id, tipo);
            lista.AddLast(a);
            return lista;

        }


        /*
         * Metodo que devuelve el tipo concatenado
         * @raiz de subarbol a analizar
         * @return string de tipo completo
         */
/*
        private string concatenarTipo(ParseTreeNode raiz)
        {
            string salida = "";
            if (raiz.ChildNodes.Count() == 5) salida += "map<" + concatenarTipo(raiz.ChildNodes.ElementAt(2)) + "," + concatenarTipo(raiz.ChildNodes.ElementAt(3)) + ">";
            else
            {
                for (int i = 0; i < raiz.ChildNodes.Count(); i++)
                {
                    string name = raiz.ChildNodes.ElementAt(i).Term.Name;
                    if (name.Equals("tipovariable")) salida += concatenarTipo(raiz.ChildNodes.ElementAt(i));
                    else salida += raiz.ChildNodes.ElementAt(i).Token.Text;
                }
            }
            salida = salida.ToLower().TrimEnd().TrimStart();
            return salida;
        }


        /*
         * Metodo que resuelve las expresiones aritmeticas,logicas
         * @raiz nodo principal de la lista de expresiones
         */
/*
        public Expresion resolver_expresion(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count() == 3)
            {


                string iden = raiz.ChildNodes.ElementAt(1).Term.Name;                //------------------------------------- ID ++ / ID -- ------------------------------------------------------
                if (iden.Equals("ID"))
                {

                    string idin = raiz.ChildNodes.ElementAt(1).Token.Text;
                    idin = idin.ToLower().TrimEnd().TrimStart();


                    string accio = raiz.ChildNodes.ElementAt(2).Token.Text;
                    int ln = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    int cn = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                    if (accio.Equals("++")) return new Expresion(new Expresion(idin, "ID", ln, cn), "INCREMENTO", ln, cn, idin);
                    else return new Expresion(new Expresion(idin, "ID", ln, cn), "DECREMENTO", ln, cn, idin);

                }
                //-------------------------- [ lista de valores para maps ]-------------------------
                else if (iden.Equals("listamap"))
                {
                    int lM = raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                    int cM = raiz.ChildNodes.ElementAt(0).Token.Location.Column;
                    return new Expresion(getListaValoresMap(raiz.ChildNodes.ElementAt(1)), "LISTAMAP", lM, cM);
                }
                //----------------------------[ lista valores para list ] ------------------------------------------------------------------
                else if (iden.Equals("listvalues"))
                {
                    int lM = raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                    int cM = raiz.ChildNodes.ElementAt(0).Token.Location.Column;
                    string simbolo = raiz.ChildNodes.ElementAt(0).Token.Text;
                    if (simbolo.Equals("[")) return new Expresion(resolver_user_type(raiz.ChildNodes.ElementAt(1)), "LISTALIST", lM, cM);
                    else return new Expresion(resolver_user_type(raiz.ChildNodes.ElementAt(1)), "LISTASET", lM, cM);
                }
                //--------------------------------- ID  IN LISTA ---------------------------------------------------------
                else if (iden.Equals("IN"))
                {
                    int le = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    int ce = raiz.ChildNodes.ElementAt(1).Token.Location.Column;

                    string idI = raiz.ChildNodes.ElementAt(0).Token.Text.ToLower().TrimEnd().TrimStart();
                    Expresion listI = resolver_expresion(raiz.ChildNodes.ElementAt(2));
                    return new Expresion(listI, "IN", le, ce, idI);
                }
                else
                {
                    string toketemp = raiz.ChildNodes.ElementAt(1).Token.Text;
                    //----------------------------------- USERTYPE . ATRIBUTO---------------------------------------------------
                    if (toketemp.Equals("."))
                    {
                        string sepa = raiz.ChildNodes.ElementAt(2).Term.Name;
                        int le = raiz.ChildNodes.ElementAt(2).Token.Location.Line;
                        int ce = raiz.ChildNodes.ElementAt(2).Token.Location.Column;
                        if (sepa.Equals("ID"))
                        {
                            string opee = "ACCESOUSER";
                            string idA = raiz.ChildNodes.ElementAt(2).Token.Text;
                            idA = idA.ToLower().TrimEnd().TrimStart();
                            return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(0)), opee, le, ce, idA);
                        }
                        //--------------------------------- EXPRESION . SIZE
                        else return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(0)), "SIZE", le, ce);

                    }


                    //--------------------------------------- OPERACIONES TERNARIAS ------------------------------------------
                    string toke = raiz.ChildNodes.ElementAt(1).Token.Text;
                    int l1 = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    int c1 = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                    return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(0)), resolver_expresion(raiz.ChildNodes.ElementAt(2)), getOperacion(toke), l1, c1);


                }


            }
            else if (raiz.ChildNodes.Count() == 2)
            {
                string iden = raiz.ChildNodes.ElementAt(0).Term.Name;
                if (iden.Equals("tipovariable"))
                {
                    int l1 = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Line;
                    int c1 = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Column;
                    string tipov = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text.TrimEnd().TrimStart().ToLower();
                    return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(1)), "CONVERSION", l1, c1, tipov);
                }
                else
                {
                    string toke = raiz.ChildNodes.ElementAt(0).Token.Text;
                    string idE = raiz.ChildNodes.ElementAt(1).Term.Name;
                    if (toke.Equals("new"))
                    {
                        int ln = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                        int cn = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                        string tipoA = raiz.ChildNodes.ElementAt(1).Token.Text;
                        tipoA = tipoA.ToLower().TrimEnd().TrimStart();

                        return new Expresion("INSTANCIA", ln, cn, tipoA);
                    }
                    else if (idE.Equals("ID"))
                    {
                        string valor = raiz.ChildNodes.ElementAt(1).Token.Text;
                        int l11 = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                        int c11 = raiz.ChildNodes.ElementAt(1).Token.Location.Column;

                        return getValor("id2", valor, l11, c11);
                    }
                    int l1 = raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                    int c1 = raiz.ChildNodes.ElementAt(0).Token.Location.Column;
                    string opera = "";
                    if (toke.Equals("-")) opera = "NEGATIVO";
                    else if (toke.Equals("!")) opera = "NEGACION";
                    return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(1)), opera, l1, c1);
                }

            }
            else if (raiz.ChildNodes.Count() == 5)
            {
                string idDiferenciador = raiz.ChildNodes.ElementAt(0).Term.Name;
                string tipoNew = raiz.ChildNodes.ElementAt(1).Term.Name;
                //--------------------------------------------- OPERACION TERNARIA ---------------------------------------------------------
                if (idDiferenciador.Equals("expresion"))
                {
                    int lineaT = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    int columnaT = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    return new Expresion(resolver_expresion(raiz.ChildNodes.ElementAt(2)),
                        resolver_expresion(raiz.ChildNodes.ElementAt(4)),
                        resolver_expresion(raiz.ChildNodes.ElementAt(0)), "TERNARIO", lineaT, columnaT);
                }
                else if (tipoNew.Equals("listvalues"))
                {
                    LinkedList<Expresion> lista = resolver_user_type(raiz.ChildNodes.ElementAt(1));
                    string idAs = raiz.ChildNodes.ElementAt(4).Token.Text;
                    idAs = idAs.ToLower().TrimEnd().TrimStart();
                    int lAs = raiz.ChildNodes.ElementAt(4).Token.Location.Line;
                    int cAs = raiz.ChildNodes.ElementAt(4).Token.Location.Column;

                    return new Expresion("ASIGNACIONUSER", lAs, cAs, lista, idAs);
                }
                string tipoLista = raiz.ChildNodes.ElementAt(1).Token.Text.ToLower().TrimEnd().TrimStart();
                string tipoList = concatenarTipo(raiz.ChildNodes.ElementAt(3));
                int ltl = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                int ctl = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                if (tipoLista.Equals("list")) return new Expresion(tipoList, "NEWLIST", ltl, ctl);
                else return new Expresion(tipoList, "NEWSET", ltl, ctl);


            }
            else if (raiz.ChildNodes.Count() == 4)
            {
                string termN = raiz.ChildNodes.ElementAt(0).Term.Name;

                //--------------------------- EXPRESION . GETVALUE ( EXPRESION )
                if (termN.Equals("expresion"))
                {
                    string tokenOperacion = raiz.ChildNodes.ElementAt(2).Token.Text;
                    tokenOperacion = tokenOperacion.ToLower().TrimEnd().TrimStart();
                    int lp = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                    int cp = raiz.ChildNodes.ElementAt(1).Token.Location.Column;
                    Expresion mp = resolver_expresion(raiz.ChildNodes.ElementAt(0));
                    Expresion vl = resolver_expresion(raiz.ChildNodes.ElementAt(3));
                    if (tokenOperacion.Equals("get")) return new Expresion(mp, vl, "GETMAP", lp, cp);
                    else if (tokenOperacion.Equals("contains")) return new Expresion(mp, vl, "CONTAINS", lp, cp);



                }
                string token = raiz.ChildNodes.ElementAt(0).Token.Text;
                token = token.ToLower().TrimEnd().TrimStart();
                int lc = raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                int cc = raiz.ChildNodes.ElementAt(0).Token.Location.Column;
                string salida = "";
                if (token.Equals("count")) salida = "COUNT";
                else if (token.Equals("min")) salida = "MIN";
                else if (token.Equals("max")) salida = "MAX";
                else if (token.Equals("sum")) salida = "SUM";
                else salida = "AVG";
                LinkedList<InstruccionCQL> ins = (LinkedList<InstruccionCQL>)instruccion(raiz.ChildNodes.ElementAt(2));
                if (ins.Count() > 0) return new Expresion(ins.ElementAt(0), salida, lc, cc);
                return null;
            }
            else if (raiz.ChildNodes.Count() == 6)
            {
                int li = raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Location.Line;
                int ci = raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Location.Column;

                string tipo1 = concatenarTipo(raiz.ChildNodes.ElementAt(3));
                string tipo2 = concatenarTipo(raiz.ChildNodes.ElementAt(4));
                return new Expresion("CREARMAP", li, ci, tipo1, tipo2);
            }
            else
            {
                string toke = raiz.ChildNodes.ElementAt(0).Term.Name;
                if (toke.Equals("llamadafuncion"))
                {
                    ParseTreeNode hijo = raiz.ChildNodes.ElementAt(0);
                    string id = hijo.ChildNodes.ElementAt(0).Token.Text.ToLower().TrimEnd().TrimStart();
                    int l = hijo.ChildNodes.ElementAt(0).Token.Location.Line;
                    int c = hijo.ChildNodes.ElementAt(0).Token.Location.Column;
                    if (hijo.ChildNodes.Count() == 2)
                        return new Expresion("llamadaFuncion", l, c, listaExpresiones(hijo.ChildNodes.ElementAt(1)), id);
                    return new Expresion("llamadaFuncion", l, c, new LinkedList<Expresion>(), id);
                }
                else if (toke.Equals("expresion")) return resolver_expresion(raiz.ChildNodes.ElementAt(0));
                string valor = raiz.ChildNodes.ElementAt(0).Token.Text;
                int l1 = raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                int c1 = raiz.ChildNodes.ElementAt(0).Token.Location.Column;

                return getValor(toke, valor, l1, c1);
            }
        }


        /*
         * METODO QUE RECORRE UN SUB ARBOL BUSCANDO LA LISTA DE VALORES
         * @param {raiz} subarbol a analizar
         * @return una linkedlist tipo object con la clase KeyValue
         */
         /*
        public LinkedList<object> getListaValoresMap(ParseTreeNode raiz)
        {
            LinkedList<object> lista = new LinkedList<object>();
            ParseTreeNode hijo;
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = getListaValoresMap(raiz.ChildNodes.ElementAt(0));
                hijo = raiz.ChildNodes.ElementAt(1);
            }
            else hijo = raiz.ChildNodes.ElementAt(0);
            Expresion key = resolver_expresion(hijo.ChildNodes.ElementAt(0));
            Expresion value = resolver_expresion(hijo.ChildNodes.ElementAt(2));

            KeyValue keyValue = new KeyValue(key, value);

            lista.AddLast(keyValue);
            return lista;
        }


        /*
         * Metodo que se encarga de regresar un lista de expresiones para asignarsela a un UserType
         * @raiz nodo del arbol para recorrer
         * @return LinkedList<Expresiones> 
         */
/*
        public LinkedList<Expresion> resolver_user_type(ParseTreeNode raiz)
        {
            LinkedList<Expresion> lista = new LinkedList<Expresion>();
            ParseTreeNode hijo;
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = resolver_user_type(raiz.ChildNodes.ElementAt(0));
                hijo = raiz.ChildNodes.ElementAt(1);
            }
            else hijo = raiz.ChildNodes.ElementAt(0);
            lista.AddLast(resolver_expresion(hijo));
            return lista;

        }


        /*
         *  METODO PARA CREAR EL ARCHIVO ASTCQL.TXT
         *  @raiz el la raiz del arbol principal
         */

 
        /*
         * Metodo que devulve que operacion es
         * @raiz es el nodo a buscar su operacion
         */
        public string getOperacion(string token)
        {
            if (token.Equals("+")) return "SUMA";
            else if (token.Equals("-")) return "RESTA";
            else if (token.Equals("*")) return "MULTIPLICACION";
            else if (token.Equals("**")) return "POTENCIA";
            else if (token.Equals("%")) return "MODULO";
            else if (token.Equals("/")) return "DIVISION";
            else if (token.Equals(">")) return "MAYOR";
            else if (token.Equals("<")) return "MENOR";
            else if (token.Equals(">=")) return "MAYORIGUAL";
            else if (token.Equals("<=")) return "MENORIGUAL";
            else if (token.Equals("==")) return "IGUALIGUAL";
            else if (token.Equals("!=")) return "DIFERENTE";
            else if (token.Equals("||")) return "OR";
            else if (token.Equals("&&")) return "AND";
            else if (token.Equals("^")) return "XOR";
            return "none";
        }
        /*

        /*
         * Metodo que devuelve que tipo de valor es
         * @raiz es el nodo a buscar su valor
         */

            /*
        public Expresion getValor(string token, string valor, int l1, int c1)
        {
            token = token.ToLower().TrimEnd().TrimStart();
            string valorT = valor.ToLower().TrimEnd().TrimStart();
            if (token.Equals("entero")) return new Expresion(valor, "ENTERO", l1, c1);
            else if (token.Equals("decimal")) return new Expresion(valor, "DECIMAL", l1, c1);
            else if (token.Equals("cadena"))
            {
                valor = valor.TrimEnd('"');
                valor = valor.TrimStart('"');
                return new Expresion(valor, "CADENA", l1, c1);
            }
            else if (token.Equals("hora"))
            {
                valor = valor.TrimEnd('\'');
                valor = valor.TrimStart('\'');
                return new Expresion(valor, "HORA", l1, c1);
            }
            else if (token.Equals("fecha"))
            {
                valor = valor.TrimEnd('\'');
                valor = valor.TrimStart('\'');
                return new Expresion(valor, "FECHA", l1, c1);
            }
            else if (valorT.Equals("true") || valorT.Equals("false")) return new Expresion(valor, "BOOLEAN", l1, c1);
            else if (token.Equals("id")) return new Expresion(valor, "IDTABLA", l1, c1);
            else if (token.Equals("id2")) return new Expresion(valor, "ID", l1, c1);
            else return new Expresion(null, "NULL", l1, c1);
        }

        /*
         * Metodo que me devuelve el tipo de declaracion
         * @raiz el es nodo a recorrer del arbol
         */

        public string declaracionTipo(ParseTreeNode raiz)
        {
            string token = raiz.Term.Name;
            if (token.Equals("declaracion")) return declaracionTipo(raiz.ChildNodes.ElementAt(0));

            string t = raiz.ChildNodes.ElementAt(0).Token.Text;
            return t.ToLower().TrimEnd().TrimStart();


        }

        /*
        /*
         * Metodo que recorre el arbol y devuelve un LinkedList de IF/ELSEIF/ELSE
         * @raiz sub arbol a analizar
         */
         /*
        public LinkedList<SubIf> resolver_if(ParseTreeNode raiz)
        {
            LinkedList<SubIf> lista = new LinkedList<SubIf>();
            string token = raiz.ChildNodes.ElementAt(0).Term.Name;
            LinkedList<InstruccionCQL> cuerpo = null;
            Expresion condicion = null;
            int l = 0;
            int c = 0;
            if (token.Equals("elseif"))
            {
                lista = resolver_if(raiz.ChildNodes.ElementAt(0));
                l = raiz.ChildNodes.ElementAt(2).Token.Location.Line;
                c = raiz.ChildNodes.ElementAt(2).Token.Location.Line;
                condicion = resolver_expresion(raiz.ChildNodes.ElementAt(3));
                cuerpo = instrucciones(raiz.ChildNodes.ElementAt(5));
            }
            else
            {
                l = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                c = raiz.ChildNodes.ElementAt(1).Token.Location.Line;
                condicion = resolver_expresion(raiz.ChildNodes.ElementAt(2));
                cuerpo = instrucciones(raiz.ChildNodes.ElementAt(4));
            }

            lista.AddLast(new SubIf(condicion, cuerpo, l, c));
            return lista;
        }

        /*
         * Metodo que recorre un sub arbol y devulve un LinkedList de CASE/DEFAULT
         * @raiz subarbol a analizar
         * @condicion condicion del swithc
         */
         /*
        public LinkedList<Case> resolver_case(ParseTreeNode raiz, Expresion condicion)
        {
            LinkedList<Case> lista = new LinkedList<Case>();
            LinkedList<InstruccionCQL> cuerpo = new LinkedList<InstruccionCQL>();
            Expresion condicionG;
            int linea = 0;
            int columna = 0;
            ParseTreeNode hijo;
            if (raiz.ChildNodes.Count() == 2)
            {
                lista = resolver_case(raiz.ChildNodes.ElementAt(0), condicion);

                linea = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Location.Line;
                columna = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Location.Line;
                cuerpo = instrucciones(raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(4));
                hijo = raiz.ChildNodes.ElementAt(1);
            }
            else
            {
                linea = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Line;
                columna = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Location.Line;
                cuerpo = instrucciones(raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(4));
                hijo = raiz.ChildNodes.ElementAt(0);
            }
            condicionG = new Expresion(condicion, resolver_expresion(hijo.ChildNodes.ElementAt(1)), "IGUALIGUAL", linea, columna);
            lista.AddLast(new Case(condicionG, cuerpo, linea, columna));
            return lista;
        }


        */


    }
}
