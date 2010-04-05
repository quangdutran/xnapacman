// Guids.cs
// MUST match guids.h
using System;

namespace noname.brim
{
    static class GuidList
    {
        public const string guidbrimPkgString = "474fa3fb-8b37-4a88-8966-6153df8ce788";
        public const string guidbrimCmdSetString = "4f525d7b-e9dc-41a4-aa09-c0a33eec55d9";
        public const string guidToolWindowPersistanceString = "c92a8a94-09fe-44e2-8f6d-b12bf15d1ef2";

        public const string guidBrimLanguageService = "1A4C1A6E-B967-44f0-95EE-AEDE10244486";

        public static readonly Guid guidbrimPkg = new Guid(guidbrimPkgString);
        public static readonly Guid guidbrimCmdSet = new Guid(guidbrimCmdSetString);
        public static readonly Guid guidToolWindowPersistance = new Guid(guidToolWindowPersistanceString);
    };
}