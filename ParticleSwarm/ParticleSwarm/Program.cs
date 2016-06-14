using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarm
{
    class Program
    {
        private static List<Bijuterie> listBijuterii;
        private static List<Cromozom> pop;
        private static List<Array> cromozom; 
        static void Main(string[] args)
        {
            //numarul = numarul de categorii de bijuterii
            listBijuterii = new List<Bijuterie>();
            Console.WriteLine("Introduceti numarul de produse: ");
            int numarul = Convert.ToInt32(Console.ReadLine());
            int[] c1 = new int[numarul];
            for (int i = 0; i < numarul; i++)
            {
                //presupunem ca fiecare obiect are cantitatea <=20kg;
                Console.WriteLine("Introduceti cantitatea pentru obiect: ");
                int cantitate = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Introduceti pretul pentru produs: ");
                int pret = Convert.ToInt32(Console.ReadLine());
                //Bijuterie produs = new Bijuterie(cantitate,pret);
                listBijuterii.Add(new Bijuterie(cantitate, pret));
            }
            // dismensiune listabijuterii
            int d = listBijuterii.Count;
            for (int i = 0; i < d; i++)
            {
                Console.WriteLine(listBijuterii[i].cantitate);
                Console.WriteLine(listBijuterii[i].pret);
                Console.ReadLine();
            }
            int j = 0;
            while (j < numarul)
            {
                //generare cromozom
                for (int i = 0; i < numarul; i++)
                {
                    Random random = new Random();
                    int random1 = random.Next(0, 1);
                    c1[i] = random1;
                }
                cromozom.Add(c1);
                //calculare fitnes pentru fiecare cromozon + adaugare in lista de obiecte
                pop.Add(new Cromozom(c1, Fitness(c1)));
            }
            int max = 0; // pentru a calcula cel mai bun fitness al celei mai bune particule
            int pozitiaceamaibuna = 0; // pozitia particulei cel mai bine plasata
            int []ceamaibunparticula = new int[numarul];
            for (int i = 0; i < pop.Count; i++)
            {
                if (max < pop[i].fit)
                {
                    max = pop[i].fit;
                    pozitiaceamaibuna = i;
                    ceamaibunparticula = pop[i].c;
                }
                
            }
            // atribuire aleatoare a vitezei pentru fiecare particula
            // viteza nu poate fii mai mare decat numarul de obiecte pe care le am
            int[] viteza = new int[numarul];
            Random rand = new Random();
            //j = 0;
            for (int i = 0; i < numarul; i++)
            {
                int rando1 = rand.Next(-(numarul / 5), numarul / 5);
                viteza[i] = rando1;
                //j++;
            }
            // vectorul care va retine pozitia fiecarei particule
            int[] pozitia = new int[numarul];
            for (int i = 0; i < numarul; i++)
            {
                pozitia[i] = 0;
            }
            // pozitia fiecarei particule este egala cu pozitia + viteza sa
            //pozitia initiala a fiecarei particule
            for (int i = 0; i < numarul; i++)
            {
                //trebuie modificat bitul cromozomului
                //gen cromozom[pozitie]  poate fii 0 sau 1;
                // acest bit cand calcule viteza trebuie modificat;
                pozitia[i] = pozitia[i] + viteza[i];
            }
            /// calculul vitezei modificate in functie de cel mai bun individ ales
            int w = 1;  // factor de inertie
            int f1 = 2;  //f1 factor de invatare cognitiv
            int f2 = 1;  //f2 factor de invatare social
            Random randomize = new Random();
            int rand1 = randomize.Next();
            int rand2 = randomize.Next();
            // 
            for (int i = 0; i < numarul; i++)
            {

                viteza[i] = w*viteza[i] + f1*rand1*(DistantaManhattan(ceamaibunparticula,pop[i].c))+ f2*rand2*pozitiaceamaibuna;
                pozitia[i] = pozitia[i] + viteza[i];
            }
            //mutatie tare asupra fiecarei particule
            for (int i = 0; i < numarul; i++)
            {
                pop[i].c = mutatie(pop[i].c);
            }
           
           


        }
        public static int Fitness(int[] cromozom)
        {
            // cant = va retin cantitea totala a pachetului , pentru a ma asigura ca nu depaseste 20kg
            int fit = 0;
            int cant = 0;
            for (int i = 0; i < cromozom.Length; i++)
            {
                if (cromozom[i] == 1)
                {
                    fit = fit + listBijuterii[i].pret;
                    cant = cant + listBijuterii[i].cantitate;
                }
                if (cant > 20)
                {
                    fit = fit - listBijuterii[i].cantitate * listBijuterii[i].pret;
                }
            }
            return fit;
        }
        //distantaManhattan
        public static int DistantaManhattan(int[]v1, int[]v2)
        {
            int s1= 0,s2 = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                s1 = s1 + v1[i];
                s2 = s2 + v2[i];
            }
            return s1-s2;
        }

        // mutatie tare
        public static int[] mutatie(int[] c1)
        {
            Random random = new Random();
            int randomNumber1 = random.Next(0, 10);
            for (int i = 0; i < c1.Length; i++)
            {
                if (c1[i] == 1)
                {
                    int randomNumber2 = random.Next(0, 10);
                    if (randomNumber2 > randomNumber1)
                    {
                        c1[i] = 0;
                    }
                }
                if (c1[i] == 0)
                {
                    int randomNumber2 = random.Next(0, 10);
                    if (randomNumber2 > randomNumber1)
                    {
                        c1[i] = 1;
                    }
                }

            }
            return c1;
        }


    }
    class Bijuterie
    {
        public int cantitate { get; set; }
        public int pret { get; set; }


        public Bijuterie(int cantitate, int pret)
        {
            this.cantitate = cantitate;
            this.pret = pret;
        }
    }

    class Cromozom
    {
        public int[] c { get; set; }
        public int fit { get; set; }

        public Cromozom(int[] c, int fit)
        {
            this.c = c;
            this.fit = fit;
        }

    }
}
