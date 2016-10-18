using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;

namespace LuceneTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            prog.PrintToken("Test's tested, objective-c");
            prog.PrintToken("Test. testing");
            prog.PrintToken("Test/test");
            prog.PrintToken("Test-test");
            prog.PrintToken("Test and test");
            prog.PrintToken("Test++, c#");
            prog.PrintToken("([Test])");
            prog.PrintToken("l'équipe + DÉVELOPPEMENT & c#, free/form.");

            Console.Read();
        }

        void PrintToken(string val)
        {
            Console.WriteLine($"\"{val}\"");
            TokenStream ts = new CustomAnalyzer().TokenStream("default", new StringReader(val));
            var termAtt = ts.GetAttribute(typeof(TermAttribute));

            while (ts.IncrementToken())
            {
                Console.WriteLine(termAtt.ToString());

            }
            Console.WriteLine("--------------------");
        }
    }

    public class MyAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream stream = new WhitespaceTokenizer(reader);
            stream = new StandardFilter(stream);
            stream = new LowerCaseFilter(stream);
            return new StopFilter(true, stream, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
        }
    }

    public class StandardAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream stream = new StandardTokenizer(Lucene.Net.Util.Version.LUCENE_29, reader);
            return stream;
        }
    }

    public class CustomAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream stream = new CustomTokenizer(reader);
            stream = new StandardFilter(stream);
            //stream = new PorterStemFilter(stream);
            //stream = new StopFilter(true, stream, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
            return stream;
        }
    }


}
