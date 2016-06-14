using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtracking
{
    class Program
    {
        int n;
        int[] st = new int[20];

         void tipar()
        {
            for (int i = 1; i <= n; i++)
                Console.WriteLine(st[i]);
            //cout << endl;
        }

        int valid(int k)
        {
            for (int i = 1; i <= k - 1; i++)
                if (st[i] == st[k])
                    return 0;
            return 1;
        }
        void back(int k)
        {
            for (int i = 1; i <= n; i++)
            {
                st[k] = i;
                if (valid(k)!=0)
                    if (k == n)
                        tipar();
                    else
                        back(k + 1);
            }
        }
        static void Main(string[] args)
        {
            //int[] st = new int[20];
            Console.WriteLine("n=");
            int n= Convert.ToInt32(Console.ReadLine());
            //Program.back(1);
        }
 
    }
}
