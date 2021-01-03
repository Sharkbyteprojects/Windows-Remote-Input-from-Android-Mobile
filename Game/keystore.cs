using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace keystoreage
{
    public class keystore
    {
        string internpath = "";
        string rgex(string input)
        {
            return (new Regex("[^a-zA-Z0-9]")).Replace(input, "");
        }
        string getpath()
        {
            string outt = internpath;
            if (internpath == "")
            {
                internpath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "key.tmp");
            }
            return internpath;
        }
        string cached = "";
        public string keystored
        {
            set
            {
                File.WriteAllText(getpath(), value);
            }
            get
            {
                string cpath = getpath();
                if (cached == "")
                {
                    if (!File.Exists("cpath"))
                    {
                        newrandkey();
                        return keystored;
                    }
                    cached = File.ReadAllText(cpath);
                }
                return rgex(cached);
            }
        }
        private static Random random = new Random();
        string randitem()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void newrandkey()
        {
            try
            {
                string yourkey = "";
                yourkey = randitem();
                keystored = yourkey;
                Console.WriteLine(string.Format("Your Key: {0}", yourkey));
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                newrandkey();
            }
        }
        public bool stringmatch(string tocompare)
        {
            bool end = false;
            if (rgex(tocompare.ToUpper()) == keystored)
            {
                end = true;
            }
            return end;
        }
    }
}
