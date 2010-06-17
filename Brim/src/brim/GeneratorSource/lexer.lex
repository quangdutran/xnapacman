%using Babel.ParserGenerator;
%using Babel;
%using Babel.Parser;

%namespace Babel.Lexer


%x COMMENT

%{
         int GetIdToken(string txt)
         {
            switch (txt[0])
            {
                case 'b':
                    if (txt.Equals("break")) return (int)Tokens.KWBREAK;
                    break;
                case 'c':
                    if (txt.Equals("continue")) return (int)Tokens.KWCONTINUE;
                    break;
                case 'e':
                    if (txt.Equals("else")) return (int)Tokens.KWELSE;
                    else if (txt.Equals("extern")) return (int)Tokens.KWEXTERN;
                    break;
                case 'f':
                    if (txt.Equals("for")) return (int)Tokens.KWFOR;
                    break;
                case 'i':
                    if (txt.Equals("if")) return (int)Tokens.KWIF;
                    else if (txt.Equals("int")) return (int)Tokens.KWINT;
                    break;
                 case 'r':
                    if (txt.Equals("return")) return (int)Tokens.KWRETURN;
                    break;
                case 's':
                    if (txt.Equals("static")) return (int)Tokens.KWSTATIC;
                    break;
                case 'v':
                    if (txt.Equals("void")) return (int)Tokens.KWVOID;
                    break;
                case 'w':
                    if (txt.Equals("while")) return (int)Tokens.KWWHILE;
                    break;
                default: 
                    break;
            }
            return (int)Tokens.IDENTIFIER;
       }
       
       internal void LoadYylval()
       {
           yylval.str = tokTxt;
           yylloc = new LexLocation(tokLin, tokCol, tokLin, tokCol + tokLen);
       }
       
       public override void yyerror(string s, params object[] a)
       {
           if (handler != null) handler.AddError(s, tokLin, tokCol, tokLin, tokCol + tokLen);
       }
%}


White0          [ \t\r\f\v]
White           {White0}|\n

CmntStart    \/\*
CmntEnd      \*\/
ABStar       [^\*\n]*

%%

[a-zA-Z_][a-zA-Z0-9_]*    { return GetIdToken(yytext); }
[0-9]+                    { return (int)Tokens.NUMBER; }

;                         { return (int)';';    }
,                         { return (int)',';    }
\(                        { return (int)'(';    }
\)                        { return (int)')';    }
\{                        { return (int)'{';    }
\}                        { return (int)'}';    }
=                         { return (int)'=';    }
\^                        { return (int)'^';    }
\+                        { return (int)'+';    }
\-                        { return (int)'-';    }
\*                        { return (int)'*';    }
\/                        { return (int)'/';    }
\!                        { return (int)'!';    }
==                        { return (int)Tokens.EQ;  }
\!=                       { return (int)Tokens.NEQ;   }
\>                        { return (int)Tokens.GT; }
\>=                       { return (int)Tokens.GTE;    }
\<                        { return (int)Tokens.LT;     }
\<=                       { return (int)Tokens.LTE;    }
\&                        { return (int)'&';    }
\&\&                      { return (int)Tokens.AMPAMP; }
\|                        { return (int)'|';    }
\|\|                      { return (int)Tokens.BARBAR; }
\.                        { return (int)'.';    }

{CmntStart}{ABStar}\**{CmntEnd} { return (int)Tokens.LEX_COMMENT; } 
{CmntStart}{ABStar}\**          { BEGIN(COMMENT); return (int)Tokens.LEX_COMMENT; }
<COMMENT>\n                     |                                
<COMMENT>{ABStar}\**            { return (int)Tokens.LEX_COMMENT; }                                
<COMMENT>{ABStar}\**{CmntEnd}   { BEGIN(INITIAL); return (int)Tokens.LEX_COMMENT; }

{White0}+                  { return (int)Tokens.LEX_WHITE; }
\n                         { return (int)Tokens.LEX_WHITE; }
.                          { yyerror("illegal char");
                             return (int)Tokens.LEX_ERROR; }

%{
                      LoadYylval();
%}

%%

/* .... */
