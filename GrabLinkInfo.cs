using System.Collections.Generic;

namespace Graber
{
    internal class LinkFile
    {
        public string LinkFilePath { get; set; }

        public string SaveFileName { get; set; }
    }

    internal class PagedLinkUrl
    {
        public string Url { get; set; }

        public string SaveFileName { get; set; }

        public int StartPageIndex { get; set; }

        public int PageCount { get; set; }
    }
    internal class GrabLinkInfo
    {
        public List<LinkFile> LinkFiles { get; set; }

        public List<PagedLinkUrl> PagedLinkUrls { get; set; }

        public GrabLinkInfo()
        {
            LinkFiles = new List<LinkFile>();
            PagedLinkUrls = new List<PagedLinkUrl>();
        }
    }
}