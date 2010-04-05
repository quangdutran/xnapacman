using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Package;
using Babel.Parser;
using Microsoft.VisualStudio.TextManager.Interop;


namespace Babel
{
    public static partial class Configuration
    {
        public const string Name = "ManagedMyC";
        public const string Extension = ".myc";
        public const string FormatList = "My C File (*.myc)\n*.myc";

        static CommentInfo myCInfo;
        public static CommentInfo MyCommentInfo { get { return myCInfo; } }

        static Configuration()
        {
            myCInfo.BlockEnd = "*/";
            myCInfo.BlockStart = "/*";
            myCInfo.LineStart = "??";
            myCInfo.UseLineComments = false;

            // default colors - currently, these need to be declared
            CreateColor("Keyword", COLORINDEX.CI_BLUE, COLORINDEX.CI_USERTEXT_BK);
            CreateColor("Comment", COLORINDEX.CI_DARKGREEN, COLORINDEX.CI_USERTEXT_BK);
            CreateColor("Identifier", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK);
            CreateColor("String", COLORINDEX.CI_RED, COLORINDEX.CI_USERTEXT_BK);
            CreateColor("Number", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK);
            CreateColor("Text", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK);

            TokenColor error = CreateColor("Error", COLORINDEX.CI_RED, COLORINDEX.CI_USERTEXT_BK, false, true);

            //
            // map tokens to color classes
            //
            ColorToken((int)Tokens.KWIF, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWELSE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWWHILE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWFOR, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWCONTINUE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWBREAK, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWRETURN, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWEXTERN, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWSTATIC, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWAUTO, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWINT, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWVOID, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWSTATIC, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWAUTO, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.KWINT, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.NUMBER, TokenType.Literal, TokenColor.String, TokenTriggers.None);

            ColorToken((int)'(', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
            ColorToken((int)')', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
            ColorToken((int)'{', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
            ColorToken((int)'}', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);

            //// Extra token values internal to the scanner
            ColorToken((int)Tokens.LEX_ERROR, TokenType.Text, error, TokenTriggers.None);
            ColorToken((int)Tokens.LEX_COMMENT, TokenType.Text, TokenColor.Comment, TokenTriggers.None);

        }
    }
}
