using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Graber.Grab
{
    class GrabNode:GrabNodeBase
    {
        protected string ResultPrefix { get; set; }
        protected string RegexStr { get; set; }
        public GrabNode(string resultPrefix, string regexStr)
        {
            ResultPrefix = resultPrefix;
            RegexStr = regexStr;
        }
        public override object Grab(object obj)
        {
            string str = obj as string;
            Match match = Regex.Match(str, RegexStr);
            if (match != null && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
