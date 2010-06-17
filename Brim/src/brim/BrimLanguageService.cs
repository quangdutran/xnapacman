using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Package;
using System.Runtime.InteropServices;
using noname.brim;

namespace Babel
{
    [ComVisible(true)]
    [Guid(GuidList.guidBrimLanguageService)]

    public class BrimLanguageService : LanguageService
    {
        public override string GetFormatFilterList()
        {
            return Babel.Configuration.FormatList;
        }
    }
}
