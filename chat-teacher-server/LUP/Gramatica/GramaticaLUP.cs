﻿using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cql_teacher_server.LUP.Gramatica
{
     class GramaticaLUP : Grammar
    {
        public GramaticaLUP() : base(caseSensitive: false)
        {

            

            #region Terminales

            var LOGIN = ToTerm("LOGIN");
            var LOGOUT = ToTerm("LOGOUT");
            var USER = ToTerm("USER");
            var PASS = ToTerm("PASS");
            var QUERY = ToTerm("QUERY");
            var DATA = ToTerm("DATA");
            var STRUCT = ToTerm("STRUCT");

            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var LLAVEIZQ = ToTerm("[");
            var LLAVEDER = ToTerm("]");

            #endregion

            #region ER
            var CUERPO = new RegexBasedTerminal("Cuerpo", "[^\n\\[]+");
            
            var CUERPO2 = new RegexBasedTerminal("Cuerpo", "((?!(\\[(\\+|\\-))).+)");

            IdentifierTerminal ID = new IdentifierTerminal("ID");
            #endregion


            #region No Terminales

            NonTerminal inicio = new NonTerminal("inicio");
            NonTerminal instruccion = new NonTerminal("instruccion");

            NonTerminal login = new NonTerminal("login");
            NonTerminal logout = new NonTerminal("logout");
            NonTerminal query = new NonTerminal("query");
            NonTerminal data = new NonTerminal("data");
            NonTerminal instruct = new NonTerminal("struct");

            NonTerminal expresion_cuerpo = new NonTerminal("expresion_cuerpo");


            #endregion
            
           


            #region Gramatica

            inicio.Rule = instruccion;

            instruccion.Rule = login
                             | logout
                             | query
                             | instruct
                             ;



            login.Rule = LLAVEIZQ + MAS + LOGIN + LLAVEDER + LLAVEIZQ + MAS
                        + USER + LLAVEDER + CUERPO + LLAVEIZQ + MENOS + USER + LLAVEDER + LLAVEIZQ + MAS + PASS + LLAVEDER + CUERPO +
                        LLAVEIZQ + MENOS + PASS + LLAVEDER + LLAVEIZQ + MENOS + LOGIN + LLAVEDER;


            logout.Rule = LLAVEIZQ + MAS + LOGOUT + LLAVEDER + LLAVEIZQ + MAS + USER + LLAVEDER + CUERPO + LLAVEIZQ + MENOS + USER +
                          LLAVEDER + LLAVEIZQ + MENOS + LOGOUT + LLAVEDER;


            

            data.Rule = LLAVEIZQ + MAS + DATA + LLAVEDER + expresion_cuerpo + LLAVEIZQ + MENOS + DATA + LLAVEDER;

            instruct.Rule = LLAVEIZQ + MAS + STRUCT + LLAVEDER + LLAVEIZQ + MAS + USER + LLAVEDER + CUERPO + LLAVEIZQ + MENOS + USER + LLAVEDER + LLAVEIZQ + MENOS + STRUCT + LLAVEDER;

            query.Rule = LLAVEIZQ + MAS + QUERY + LLAVEDER + LLAVEIZQ + MAS + USER + LLAVEDER + ID + LLAVEIZQ + MENOS + USER + LLAVEDER
                       + data + LLAVEIZQ + MENOS + QUERY + LLAVEDER;

            expresion_cuerpo.Rule = expresion_cuerpo + CUERPO2
                                  | CUERPO2;

            #endregion

            #region Preferencias
            this.Root = inicio;
            #endregion
        }

    }
}
