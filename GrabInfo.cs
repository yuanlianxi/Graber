using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Graber
{
    internal class GrabRegexs
    {
        public string Seperate { get; set; }

        public List<Regex> Regexs { get; set; }

        public GrabRegexs()
        {
            Regexs = new List<Regex>();
        }
    }

    internal class GrabInfo
    {
        public string Prefix { get; set; }

        public GrabRegexs Regexs { get; set; }

        public GrabInfo()
        {
            Regexs = new GrabRegexs();
        }
    }
}