using System;
using System.Collections.Generic;
using System.Text;
using Babel;

namespace Babel
{
    public partial class Resolver : IASTResolver
    {
        #region IASTResolver Members

        public IList<Declaration> FindCompletions(object result, int line, int col)
        {
            return new List<Babel.Declaration>();
        }

        public IList<Declaration> FindMembers(object result, int line, int col)
        {
            return new List<Babel.Declaration>();
        }

        public string FindQuickInfo(object result, int line, int col)
        {
            return string.Empty;
        }

        public IList<Method> FindMethods(object result, int line, int col, string name)
        {
            return new List<Babel.Method>();
        }

        #endregion
    }
}
