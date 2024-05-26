using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT6TH
{
    internal class AdjList
    {
        LinkedList<int>[] list;
        LinkedList<int> Topo;
        int n;
        bool[] diqua;
        int[] index;
        int inconnect;      // Đếm các thành phần liên thông
        int[] previous;
        int[] color;        // Use to color

        public int N { get => n; set => n = value; }
        public LinkedList<int>[] List { get => list; set => list = value; }
        public int Inconnect { get => inconnect; set => inconnect = value; }

        public AdjList() { }
        public AdjList(int k)
        {
            list = new LinkedList<int>[k];

        }
        public AdjList(LinkedList<int>[] newLinkedList)
        {
            list = newLinkedList;
        }
        public void FileToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine()) + 1;
            list = new LinkedList<int>[n];
            for (int i = 1; i < n; i++)
            {
                list[i] = new LinkedList<int>();
                string st = sr.ReadLine();
                if (st != "")
                {
                    string[] s = st.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        list[i].AddLast(x);
                    }
                }
            }
            sr.Close();
        }
        public void Xuat()
        {
            int count = n - 1;
            Console.WriteLine("Số đỉnh: " + count);
            for (int i = 1; i < list.Length; i++)
            {
                Console.Write("{0} ->", i);
                foreach (int x in list[i])
                {
                    Console.Write("{0, 3}", x);
                }
                Console.WriteLine();
            }
        }
        public void DegV(string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath);
            Console.WriteLine("Bậc của các đỉnh :");
            for (int i = 0; i < n; i++)
            {
                int deg = 0;
                deg += list[i].Count;
                Console.WriteLine("{0} : {1}", i, deg);
                sw.WriteLine(i + " : " + deg);
            }
            sw.Close();
        }

        public void Bipartite()
        {
            diqua = new bool[n];
            for (int i = 1; i < n; i++)
            {
                diqua[i] = false;
            }
            color = new int[n];
            for (int i = 1; i < n; i++)
            {
                color[i] = -1;
            }
            color[0] = 1;
            if (IsBipartite(1) == true)
            {
                Console.WriteLine("Đồ thị 2 phía");
            }
            else
            {
                Console.WriteLine("Không phải Đồ thị 2 phía");
            }
        }

        public bool IsBipartite(int s)
        {
            foreach (int u in list[s])
            {
                if (diqua[u] == false)
                {
                    diqua[u] = true;
                    color[u] = 1 - color[s];
                    if (!IsBipartite(u))
                    {
                        return false;
                    }
                }
                else if (color[u] == color[s])
                {
                    return false;
                }
            }
            return true;
        }

        public void DFSTestChuTrinh()
        {
            color = new int[n];
            for (int i = 1; i < n; i++)
            {
                color[i] = 0;
            }
            for (int i = 1; i < n; i++)
            {
                if (DFSCheckChuTrinh(i))
                {
                    Console.WriteLine("  Đồ thị có chu trình");
                    return;
                }
            }
            Console.WriteLine("  Đồ thị không có chu trình");
        }

        public bool DFSCheckChuTrinh(int s)
        {
            color[s] = 1;
            foreach (int u in list[s])
            {    
                if (color[u] == 0)
                {
                    DFSCheckChuTrinh(u);
                }
                else if (color[u] == 1)
                {
                    return true;
                }
            }
            color[s] = 2;
            return false;
        }
        public void TopoSort()
        {
            diqua = new bool[n];
            for (int i = 1; i < n; i++)
            {
                diqua[i] = false;
            }
            Topo = new LinkedList<int>();
            for (int i = 1; i < n; i++)
            {
                if (diqua[i] == false)
                {
                    TopoCheck(i);
                }
            }
            foreach (int x in Topo)
            {
                Console.Write(" " + x);
            }

        }
        // Đệ qui DFS
        public void TopoCheck(int s)
        {
            diqua[s] = true;
            foreach (int u in list[s])
            {
                if (diqua[u] == false)
                {
                    TopoCheck(u);
                }
            }
            Topo.AddFirst(s);
        }
    }
}
