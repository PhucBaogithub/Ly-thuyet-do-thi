using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi2_Bai6
{
    internal class AdjList
    {
        private int n;
        private LinkedList<int>[] list;
        public int N { get => n; set => n = value; }
        public LinkedList<int>[] List { get => list; set => list = value; }
        public AdjList()
        {

        }
        public AdjList(int n)
        {
            list = new LinkedList<int>[n];
        }
        public AdjList(LinkedList<int>[] list)
        {
            this.list = list;
        }
        public void GhiFile(string pathOut)
        {
            StreamWriter sw = new StreamWriter(pathOut);
            sw.WriteLine(n);           
            for(int i = 0; i < n; i++)
            {
                foreach(var item in list[i])
                {
                    sw.Write(item + " ");
                }
                sw.WriteLine();
            }
            
            sw.Close();
        }
    }
}
