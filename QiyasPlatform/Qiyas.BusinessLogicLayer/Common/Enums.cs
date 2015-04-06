using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qiyas.BusinessLogicLayer.Enums
{
    public static class Enums
    {
        
        public enum FeedFormat
        {
            Rss,
            Atom
        }
        public struct FeedFormatMimeType
        {
            public const String Rss20 = "application/rss+xml";
            public const String Atom10 = "application/atom+xml";
        }

    }
}
