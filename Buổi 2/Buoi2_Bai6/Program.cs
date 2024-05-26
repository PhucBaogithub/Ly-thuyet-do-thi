using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi2_Bai6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathInp = @"../../dataset/MaTranKe2DSKe.INP";
            string pathOut = @"../../dataset/MaTranKe2DSKe.OUT";

            AdjMatrix gm = new AdjMatrix();
            gm.DocFile(pathInp);
            
            AdjList ga = AdjMatrixToAdjList(gm);
            ga.GhiFile(pathOut);


            Console.WriteLine("Complete");
            Console.ReadKey();
        }
        static AdjList AdjMatrixToAdjList(AdjMatrix gm)
        {
            AdjList ga = new AdjList();
            ga.N = gm.N;
            ga.List = new LinkedList<int>[ga.N];
            for(int i = 0; i < ga.N; i++)
            {
                ga.List[i] = new LinkedList<int>();
            }
            for(int i = 0; i < gm.N; i++)
            {
                for(int j = 0; j < gm.N; j++)
                {
                    if (gm.A[i, j] == 1)
                    {
                        ga.List[i].AddLast(j + 1);                      
                    }
                }
            }


            return ga;
        }
    }
}
