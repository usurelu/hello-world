using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AE
{
    class Program
    {
        static List<Bijuterie> listBijuterii;
        public static List<Cromozom> pop;
        public static List<Array> incrucisare_rez; 

        static void Main(string[] args)
        {
            //numarul = numarul de categorii de bijuterii
            listBijuterii = new List<Bijuterie>();
            Console.WriteLine("Introduceti numarul de produse: ");
            int numarul = Convert.ToInt32(Console.ReadLine());
            int[] c1 = new int[numarul];
            int[] c2 = new int[numarul];
            int[] c3 = new int[numarul];
            int[] c4 = new int[numarul];
            int[] c5 = new int[numarul];
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
            for(int i=0; i<d; i++)
            {
                //Console.WriteLine(listBijuterii[i].cantitate);
                //Console.WriteLine(listBijuterii[i].pret);
                //Console.ReadLine();
            }
            // daca pe pozitia i este 0 inseamna ca produsul nu face parte din posibilul pachet
            // daca pe pozitia i este 1 inseamna ca produsul face parte din posibilul pachet
            for (int i = 0; i < numarul; i++)
            {
                Random random = new Random();
                int random1 = random.Next(0, 2);
                int random2 = random.Next(0, 2);
                int random3 = random.Next(0, 2);
                int random4 = random.Next(0, 2);
                int random5 = random.Next(0, 2);
                c1[i] = random1;
                c2[i] = random2;
                c3[i] = random3;
                c4[i] = random4;
                c5[i] = random5;
             }
            //fitList = new List<Int64>();
            pop = new List<Cromozom>();
            pop.Add(new Cromozom(c1, Fitness(c1)));
            pop.Add(new Cromozom(c2, Fitness(c2)));
            pop.Add(new Cromozom(c3, Fitness(c3)));
            pop.Add(new Cromozom(c4, Fitness(c4)));
            pop.Add(new Cromozom(c5, Fitness(c5)));
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(pop[i].fit);
            }
            //selectie prin turnir
            int[] cromozom1 = turnir(pop);
            int[] cromozom2 = turnir(pop);
            //incrucisare 
            incrucisare_rez = incrucisare(cromozom1, cromozom2);
            //mutatie tare;  
            for (int i = 0; i < incrucisare_rez.Count; i++)
            {
                mutatie(incrucisare_rez[i] as int[]);
            }
            int max = 0;
            for (int i = 0; i < numarul; i++)
            {
                if (max < pop[i].fit)
                {
                    max = pop[i].fit;
                }            
            }
            Console.WriteLine(max);
            Console.ReadLine();
        }

        public static int Fitness(int []cromozom)
        {
            // cant = va retin cantitea totala a pachetului , pentru a ma asigura ca nu depaseste 20kg
            //fitnesu se stabileste in functie de pretul obiectului 
            // pretul fiecarui obiect 
            int fit = 0;
            int cant = 0;
            for (int i = 0; i < cromozom.Length; i++)
            {
                if (cromozom[i] == 1)
                {
                    fit = fit +listBijuterii[i].pret;
                    cant = cant + listBijuterii[i].cantitate;
                }
                if (cant > 20)
                {
                    fit = fit - listBijuterii[i].cantitate*listBijuterii[i].pret;
                }
            }
            return fit;
        }
        // turnir prin selectie 
        public static int[] turnir(List<Cromozom> pop)
        {
            Random random = new Random();
            int randomNumber1 = random.Next(0, pop.Count);
            int randomNumber2 = random.Next(0, pop.Count);
            while (randomNumber1 == randomNumber2)
            {
                randomNumber2 = random.Next(0, pop.Count);
            }

            if (pop[randomNumber1].fit > pop[randomNumber2].fit)
            {
                return pop[randomNumber1].c;
            }
            else
            {
                return pop[randomNumber2].c;
            }
        }

        public static List<Array> incrucisare(int[]c1 , int[] c2)
        {
            // se primesc 2 cromozomi
            // se stabileste un punct de taietura random
            // si se combina prima jumatate a primului cromozom cu a 2 juamatate a celui de al doilea
            // si prima jumatate a celui de al doilea cu a 2 jumatate a primului cromozom
            List<Array> rezultat;
            int[] rez1 = new int[100000];
            int[] rez2 = new int[100000];
            Random random = new Random();
            int randomNumber1 = random.Next(0, c1.Length);
            int j = 0;
            for (int i =0; i < randomNumber1; i++)
            {
                rez1[j] = c1[i];
                j++;
            }
            for (int i= randomNumber1; i < c1.Length; i++)
            {
                rez1[j] = c2[i];
                j++;
            }
            j = 0;
            for (int i=0; i<randomNumber1 ; i++)
            {
                rez2[j] = c2[i];
                j++;
            }
            for (int i = randomNumber1; i< c1.Length; i++)
            {
                rez2[j] = c1[i];
                j++;
            }
            rezultat = new List<Array>();
            rezultat.Add(rez1);
            rezultat.Add(rez2);
            return rezultat;
        }

        public static int[] mutatie(int [] c1)
        {
            Random random = new Random();
            int randomNumber1 = random.Next(0, 10);
            for (int i = 0; i<c1.Length; i++)
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
