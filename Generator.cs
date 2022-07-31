using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    internal static class Generator
    {
        private static List<string> names;
        private static List<string> surnames;
        private static Random r;
        static Generator()
        {
            names = new List<string>() {"Amelia", "Harry", "Oliver",
                                        "Emily", "Jacob", "Ava",
                                        "Mia", "William", "Sophie"};
            surnames = new List<string>() {"Adamson", "Babcock", "Eddington",
                                           "Faber", "Jackson", "Keat",
                                           "Kelly", "Laird", "MacDonald"};
            r = new Random((int)DateTime.Now.Ticks);
        }

        private static string StrGenerate(string start_str, int step)
        {
            for (int i = 0; i < step; i++)
                start_str += Convert.ToString(r.Next(0, 9));
            
            return start_str;
        }

        public static Client ClientGenerate()
        {
            return new Client(names[r.Next(0, 8)], surnames[r.Next(0, 8)], StrGenerate("+44", 10), StrGenerate(null, 4), StrGenerate(null, 6));
        }
    }
}
