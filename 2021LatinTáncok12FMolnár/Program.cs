using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2021LatinTáncok12FMolnár
{
    class Program
    {

        class Adat
        {
            
            /* public static List<Adat> lista = new List<Adat>();
            public Adat(string t, string l, string f)
            {
                tipus = t;
                lany = l;
                fiu = f;
                lista.Add(this);
            }
            */

            public string tipus;
            public string lany;
            public string fiu;
        }

        static void Main(string[] args)
        {
            /*
            string teljesfajlszovege = File.ReadAllText("tancrend.txt", Encoding.Default); // a fájl tartalma egy gigászi stringben
            string[] teljesfajlszovegetombben = File.ReadAllLines("tancrend.txt", Encoding.Default); // ez egy 174 elemű tömb lesz!

            foreach (string sor in teljesfajlszovegetombben)
            {
                // ...
            }
            */

            /* StreamReader f = new StreamReader("tancrend.txt", Encoding.Default);

            f.Close();*/

            List<Adat> l = new List<Adat>();

            using (StreamReader f = new StreamReader("tancrend.txt", Encoding.Default))
            {
                while(!f.EndOfStream)
                {
                    Adat a = new Adat(); // némi abúzusnak tűnik a memóriával, de a C# compilere első simításként rögtön hátrarakja ezeket. Cserébe szép marad a scope!
                    a.tipus = f.ReadLine();
                    a.lany = f.ReadLine();
                    a.fiu = f.ReadLine();
                    l.Add(a); /// nagyon könnyű elfelejteni!

                    /* l.Add(new Adat { 
                        tipus = f.ReadLine(), 
                        lany = f.ReadLine(), 
                        fiu = f.ReadLine() });
                    */
                    // new Adat(f.Readline(), f.Readline(), f.Readline());
                }

            } // így nem kell bezárni a fájlt, mert a } jelnél egyből bezárja. StreamWriter-nél, tehát fájlba íráskor különösen jól jön!

            Console.WriteLine(l.Count);

            // Console.WriteLine(Adat.lista.Count); // konstruktoros beolvasás statikus listával

            /* kitérő a dátumokra
            DateTime d = new DateTime(1, 2, 3, 4, 5, 6); // hat argumentum! próbálgasd!
            DateTime.Parse("2021.04.14 13:38:17"); // van valahogy ez így is, de ennek utána kell nézni, Excel-szerű formatcode-okkal lehet babrálni!

            TimeSpan t = new TimeSpan(1, 2, 3); // óra-perc-másodperc, a műveletekre zárt!
            var ta = t + t;
            var i = d - d; // viszont a dátumok műveletei timespaneket adnak, ne ijedj meg tőle!
            */

            Console.WriteLine($"2. feladat: Az első tánc neve: {l[0].tipus}, az utolsó tánc neve pedig: {l.Last().tipus}");
            Console.WriteLine($"3. feladat: a samba-t táncolók száma: ");
            Console.WriteLine(l.Where(x => x.tipus == "samba").ToList().Count);
            Console.WriteLine(l.Where(x => x.tipus == "samba").Count());
            Console.WriteLine(l.Count(x => x.tipus == "samba"));

            foreach (Adat adat in l.Where(x => x.lany == "Vilma"))
            {
                Console.WriteLine(adat.tipus);
            }

            Console.WriteLine("Adjon meg egy táncnevet!");
            string usertanc = Console.ReadLine();

            int vilmatancai = l.Count(x => x.tipus == usertanc && x.lany == "Vilma");

            if (vilmatancai>0)
            {
                foreach (Adat item in l.Where(x => x.tipus == usertanc && x.lany == "Vilma")) // nem emlékeztem rá, hogy táncolhat-e Vilma egy táncot több partnerrel is... ha nem, akkor First(...) elég lett volna!
                {
                    Console.WriteLine($"A {usertanc} bemutatóján Vilma párja {item.fiu} volt.");
                }
            }
            else
            {
                Console.WriteLine($"Vilma nem táncolt {usertanc} - t.");
            }

            /* 
            List<int> szamok = new List<int> { 3, 4, 5 };
            foreach (var item in szamok.Select(x => x * x))
            {
                Console.WriteLine(item);
            }
            */

            // ismétlődések kiszűrése Distincttel, ez kicsit sql-esebb...
            foreach (var item in l.Select(x => x.lany).Distinct())
            {
                Console.WriteLine(item);
            }

            // ismétlődések kiszűrése HashSet-tel: ez halmazfogalmat ad vissza, ami jól jöhet, ha utána tovább kéne dolgozni az adatokkal.
            Console.WriteLine("--------------------------------------------");
            foreach (var item in l.Select(x => x.lany).ToHashSet())
            {
                Console.WriteLine(item);
            }

            // a vessző megoldása, hogy ne legyen utolsó, I. változat, stringbe gyűjtünk, utólag törljünk, kiírjuk:
            string s = "Lányok: ";

            foreach (var item in l.Select(x => x.lany).Distinct())
            {
                s += item + ", ";
            }

            Console.WriteLine(s.Substring(0,s.Length-2));


            // a vessző megoldása, hogy ne legyen utolsó, II. változat, egyből kiírunk mindent az utolsó előttig, az utolsót külön kezeljük
            List<string> lanyok = l.Select(x => x.lany).Distinct().ToList();

            Console.Write("Lányok: ");
            for (int i = 0; i < lanyok.Count-1; i++)
            {
                Console.Write(lanyok[i]+", ");
            }

            Console.WriteLine(lanyok.Last());


            Dictionary<string, int> szótár = new Dictionary<string, int>();

            /*
            szótár["blabla"] = 4;
            szótár["blabla"]++;
            Console.WriteLine(szótár["blabla"]);
            */

            foreach (Adat adat in l)
            {
                if (szótár.ContainsKey(adat.fiu))
                {
                    szótár[adat.fiu]++;
                }
                else
                {
                    szótár[adat.fiu] = 1;
                }
            }

            Console.WriteLine("-------------------");

            // Szótár kiírása a kulcsokon való lépdeléssel:
            foreach (string kulcs in szótár.Keys)
            {
                Console.WriteLine($"{kulcs}: {szótár[kulcs]} db");
            }

            Console.WriteLine("-------------------");

            // Szótár kiírása a párokon való lépdeléssel

            foreach (KeyValuePair<string, int> pár in szótár)
            {
                Console.WriteLine($"{pár.Key}: {pár.Value} db");
            }

            Console.WriteLine("-------------------");
            // lezseren...
            foreach (var pár in szótár)
            {
                Console.WriteLine($"{pár.Key}: {pár.Value} db");
            }

            Console.WriteLine("7. feladat: Ez(ek) a fiú(k) szerepeltek a legtöbbször:");
            int m = szótár.Max(x => x.Value);
            foreach (var par in szótár.Where(p => p.Value == m))
            {
                Console.WriteLine(par.Key);
            }



        }
    }
}
