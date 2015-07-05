using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Graber
{
    internal class GraberConfig
    {
        public static List<GrabSite> CreateGrabSite(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            //XDocument xDoc = new XDocument();

            doc.Load(fileName);

            XmlNode root = doc.DocumentElement;
            XmlNodeList siteNodes = root.SelectNodes("sites/site");
            List<GrabSite> gss = new List<GrabSite>();
            foreach (XmlNode siteNode in siteNodes)
            {
                GrabSite gs = new GrabSite();
                XmlNode saveDirectNode = siteNode.SelectSingleNode("saveDirect");
                gs.SaveDirect = saveDirectNode.InnerText ?? string.Empty;
                XmlNode linkNode = siteNode.SelectSingleNode("linkInfo");
                InitLinkInfo(gs._GrabLinkInfo, linkNode);
                XmlNode grabInfoNode = siteNode.SelectSingleNode("linkInfo/grabInfo");
                InitGrabInfo(gs._GrabInfo, grabInfoNode);
                gss.Add(gs);
            }

            return gss;
        }

        private static void InitLinkInfo(GrabLinkInfo linkInfo, XmlNode linkInfoNode)
        {
            XmlNodeList linkNodes = linkInfoNode.SelectNodes("links/linkFile");
            if (linkNodes.Count > 0)
            {
                foreach (XmlNode node in linkNodes)
                {
                    LinkFile lf = new LinkFile();
                    XmlNode linkFileNode = node.SelectSingleNode("linkFilePath");
                    lf.LinkFilePath = linkFileNode.InnerText ?? string.Empty;
                    XmlNode saveFileNode = node.SelectSingleNode("saveFileName");
                    lf.SaveFileName = saveFileNode.InnerText ?? string.Empty;
                    linkInfo.LinkFiles.Add(lf);
                }
            }
            linkNodes = linkInfoNode.SelectNodes("links/linkUrlPaged");
            foreach (XmlNode node in linkNodes)
            {
                PagedLinkUrl plu = new PagedLinkUrl();
                XmlNode urlNode = node.SelectSingleNode("url");
                plu.Url = urlNode.InnerText ?? string.Empty;
                XmlNode saveFileNode = node.SelectSingleNode("saveFileName");
                plu.SaveFileName = saveFileNode.InnerText ?? string.Empty;
                XmlNode startPageIndexNode = node.SelectSingleNode("startPageIndex");
                plu.StartPageIndex = Convert.ToInt32(startPageIndexNode.InnerText ?? string.Empty);
                XmlNode pageCountNode = node.SelectSingleNode("pageCount");
                plu.PageCount = Convert.ToInt32(pageCountNode.InnerText ?? string.Empty);
                linkInfo.PagedLinkUrls.Add(plu);
            }
        }

        private static void InitGrabInfo(GrabInfo grabInfo, XmlNode grabNode)
        {
            XmlNode prefixNode = grabNode.SelectSingleNode("prefix");
            grabInfo.Prefix = prefixNode.InnerText ?? string.Empty;
            XmlNode regexsNode = grabNode.SelectSingleNode("regexs");
            XmlNode seperateNode = regexsNode.SelectSingleNode("seperate");
            grabInfo.Regexs.Seperate = seperateNode.InnerText ?? string.Empty;
            XmlNodeList regexNodes = regexsNode.SelectNodes("regex");
            foreach (XmlNode node in regexNodes)
            {
                grabInfo.Regexs.Regexs.Add(new Regex(node.InnerText ?? string.Empty));
            }
        }
    }
}