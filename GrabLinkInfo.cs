using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graber
{
    class LinkFile {
        public string LinkFilePath { get; set; }
        public string SaveFileName { get; set; }
    }
    class PagedLinkUrl
    {
        public string Url { get; set; }
        public string SaveFileName { get; set; }
        public int StartPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    class GrabLinkInfo
    {

        public List<LinkFile> LinkFiles { get; set; }
        public List<PagedLinkUrl> PagedLinkUrls { get; set; }
        public GrabLinkInfo() {
            LinkFiles = new List<LinkFile>(); 
            PagedLinkUrls = new List<PagedLinkUrl>(); }
    }
}
