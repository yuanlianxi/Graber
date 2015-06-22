using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Graber
{
    class GrabRegexs {
        public string Seperate { get; set; }
        
        public List<Regex> Regexs { get; set; }
        public GrabRegexs() { Regexs = new List<Regex>(); }

    }
    class GrabInfo
    {
        public string Prefix { get; set; }
        public GrabRegexs Regexs { get; set; }
        public GrabInfo() { Regexs = new GrabRegexs(); }
    }
}
