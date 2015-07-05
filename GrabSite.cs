namespace Graber
{
    internal class GrabSite
    {
        public string SaveDirect { get; set; }

        public GrabLinkInfo _GrabLinkInfo { get; set; }

        public GrabInfo _GrabInfo { get; set; }

        public GrabSite()
        {
            _GrabLinkInfo = new GrabLinkInfo();
            _GrabInfo = new GrabInfo();
        }
    }
}