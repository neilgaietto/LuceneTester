using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace LuceneTester
{
    public class CustomTokenizer : MyCharTokenizer
    {
        public CustomTokenizer(TextReader in_Renamed) : base(in_Renamed)
        {
        }

        public CustomTokenizer(AttributeSource source, TextReader in_Renamed) : base(source, in_Renamed)
        {
        }

        public CustomTokenizer(AttributeFactory factory, TextReader in_Renamed) : base(factory, in_Renamed)
        {
        }
        protected override bool IsTokenChar(char c)
        {
            return (char.IsLetterOrDigit(c) || c == '+' || c == '#' || c == '\'' || c == '-');
        }
        protected override char Normalize(char c)
        {
            return base.Normalize(char.ToLower(c));
        }
    }
}
