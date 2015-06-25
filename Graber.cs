using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Graber
{
    class Graber
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
            IEnumerable<string> urls = File.ReadLines(Path.Combine(gs.SaveDirect, lf.LinkFilePath));

            
            string fileName = Path.Combine(gs.SaveDirect, lf.SaveFileName);
            foreach (string url in urls)
            {
                
                
                    GraberWebClient webClient = new GraberWebClient(url, null);
                    
                    webClient.BeginDownString(o => {
                        string content = webClient.EndDownString(o);
                        string result  = GetSingleRegexMatchResult(gs, content);
                        File.AppendAllLines(fileName, new string[] { result });
                    }, null);
            }
        }

        private void Export(GrabSite gs, PagedLinkUrl plu)
        {
            string fileName = Path.Combine(gs.SaveDirect, plu.SaveFileName);
            for (int i = 0; i < plu.PageCount; i++)
            {
                GraberWebClient webClient = new GraberWebClient(string.Format(plu.Url, plu.StartPageIndex + i), null);
                string content = webClient.DownString();
                IEnumerable<string> results = GetRegexMatchResults(gs, content);
                File.AppendAllLines(fileName, results);
            }
        }
        private IEnumerable<string> GetRegexMatchResults(GrabSite gs, string content)
        {
            List<string> results = new List<string>();
            Regex reg = gs._GrabInfo.Regexs.Regexs[0];
            MatchCollection matches = reg.Matches(content);
            foreach (Match match in matches)
            {
                results.Add(gs._GrabInfo.Prefix + match.Groups[1].Value);
            }
            return results;
        }
        private string GetSingleRegexMatchResult(GrabSite gs, string content)
        {
            List<string> matchStrs = new List<string>();
            foreach (Regex reg in gs._GrabInfo.Regexs.Regexs)
            {
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
                        matchStrs.Add(group.Value);
                    }
                }
            }
            return gs._GrabInfo.Prefix + string.Join(gs._GrabInfo.Regexs.Seperate, matchStrs);
        }
    }
}
