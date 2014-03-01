using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling;
using StructureMap;
using Twitta.Website.Logic;

namespace Twitta.Website.Models
{
    public class TweetProcessor
    {
        private readonly List<string> _stopwords =
            "-,*,&amp;,a,able,about,across,after,all,almost,also,am,among,an,and,any,are,as,at,be,because,been,but,by,can,cannot,could,dear,did,do,does,either,else,ever,every,for,from,get,got,had,has,have,he,her,hers,him,his,how,however,i,if,in,into,is,it,its,just,least,let,like,likely,may,me,might,most,must,my,neither,no,nor,not,of,off,often,on,only,or,other,our,own,rather,said,say,says,she,should,since,so,some,than,that,the,their,them,then,there,these,they,this,tis,to,too,twas,us,wants,was,we,were,what,when,where,which,while,who,whom,why,will,with,would,yet,you,your"
                .Split(',').ToList();

        public Dictionary<string, int> WordCountStats(string tweets)
        {
            //every word in all tweets found
            string txt = tweets;
            //Convert the string into an array of words spliting on comma and space
            string[] source = txt.ToLowerInvariant().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            //get a distinct list of those words

            var wordcountlist = new Dictionary<string, int>();
            foreach (var word in source)
            {
                if (_stopwords.Contains(word))
                    continue;
                if (!wordcountlist.ContainsKey(word))
                {
                    wordcountlist.Add(word, 1);
                }
                else
                {
                    wordcountlist[word] = wordcountlist[word] + 1;
                }
            }
            return wordcountlist;
        }

        public Dictionary<string, int> WordCountStats(List<string> tweets)
        {
            var profiler = MiniProfiler.Current;
            //every word in all tweets found
            string txt = tweets.Aggregate(string.Empty, (current, t) => current + t);

            string[] source = txt.ToLowerInvariant().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            //get a distinct list of those words

            var wordcountlist = new Dictionary<string, int>();
            foreach (var word in source)
            {
                if (_stopwords.Contains(word))
                    continue;
                if (!wordcountlist.ContainsKey(word))
                {
                    wordcountlist.Add(word, 1);
                }
                else
                {
                    wordcountlist[word] = wordcountlist[word] + 1;
                }
            }
            return wordcountlist;

        }

        
    }
}