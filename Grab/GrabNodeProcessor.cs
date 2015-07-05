using System;
using System.Collections.Generic;
using System.Text;

namespace Graber.Grab
{
    public abstract class GrabNodeProcessor:GrabNodeBase
    {
        protected GrabNodeBase _innerNode;
        public GrabNodeProcessor(GrabNodeBase innerNode)
        {
            _innerNode = innerNode;
        }
    }
}
