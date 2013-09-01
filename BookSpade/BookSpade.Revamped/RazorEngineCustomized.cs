﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace BookSpade.Revamped
{
    public class RazorEngineCustomized : RazorViewEngine
    {
        private static string[] PartialViewFormats = new[] {
            "~/Views/{1}/Partial Views/{0}.cshtml",
            "~/Views/Shared/Partial Views/{0}.cshtml"
        };

        public RazorEngineCustomized()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(PartialViewFormats).ToArray(); 
        }
    }
}