using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace Graber
{
    internal class Graber
    {
        private List<GrabSite> gss = new List<GrabSite>();

        public void LoadFromConfigFile(string fileName)
        {
            gss = GraberConfig.CreateGrabSite(fileName);
        }

        public void Grab()
        {
            foreach (var gs in gss)
            {
                if (!string.IsNullOrEmpty(gs.SaveDirect) && !Directory.Exists(gs.SaveDirect))
                {
                    Directory.CreateDirectory(gs.SaveDirect);
                }
                foreach (PagedLinkUrl plu in gs._GrabLinkInfo.PagedLinkUrls)
                {
                    Export(gs, plu);
                }
                foreach (LinkFile lf in gs._GrabLinkInfo.LinkFiles)
                {
                    Export(gs, lf);
                }
            }
        }

        private void Export(GrabSite gs, LinkFile lf)
        {
            IEnumerable<string> urls = File.ReadAllLines(Path.Combine(gs.SaveDirect, lf.LinkFilePath));

            string fileName = Path.Combine(gs.SaveDirect, lf.SaveFileName);
            object _objLock = new object();
            foreach (string url in urls)
            {
                GraberWebClient webClient = new GraberWebClient(url, null);
                webClient.BeginDownString((o) =>
                {
                    string content = webClient.EndDownString(o);
                    string result = GetSingleRegexMatchResult(gs, content);
                    lock (_objLock)
                    {
                        File.AppendAllText(fileName, result + "\r\n");
                    }
                }, null);
            }
        }

        private void Export(GrabSite gs, PagedLinkUrl plu)
        {
            string fileName = Path.Combine(gs.SaveDirect, plu.SaveFileName);
            for (int i = 0; i < plu.PageCount; i++)
            {
                GraberWebClient webClient = new GraberWebClient(string.Format(plu.Url, i + 1), null);
                string content = webClient.DownString();
                string[] results = GetRegexMatchResults(gs, content);
                File.AppendAllText(fileName, string.Join("\r\n", results) + "\r\n");
            }
        }

        private string[] GetRegexMatchResults(GrabSite gs, string content)
        {
            List<string> results = new List<string>();
            Regex reg = gs._GrabInfo.Regexs.Regexs[0];
            MatchCollection matches = reg.Matches(content);
            foreach (Match match in matches)
            {
                results.Add(gs._GrabInfo.Prefix + match.Groups[1].Value);
            }
            return results.ToArray();
        }

        private string GetSingleRegexMatchResult(GrabSite gs, string content)
        {
            List<string> matchStrs = new List<string>();
            List<Regex> regexs = gs._GrabInfo.Regexs.Regexs;
            for (int ireg = 0; ireg < regexs.Count; ireg++)
            {
                Regex reg = regexs[ireg];
                Match match = reg.Match(content);
                if (match.Groups.Count == 1)
                {
                    matchStrs.Add(string.Empty);
                }
                else
                {
                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        Group group = match.Groups[i];
                        string result = group.Value.Trim();
                        result = GrabResultProcessor.Process(result);
                        matchStrs.Add(result);
                    }
                }
            }
            return gs._GrabInfo.Prefix + string.Join(gs._GrabInfo.Regexs.Seperate, matchStrs.ToArray());
        }
    }
}