using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi5TH
{
    internal class AdjList
    {
        LinkedList<int>[] list;
        List<int> duongdi;
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
            Console.WriteLine("So dinh: " + count);
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
                deg+= list[i].Count;
                Console.WriteLine("{0} : {1}", i, deg);
                sw.WriteLine(i + " : " + deg);
            }
            sw.Close();
        }
        public void _DFS(int s)
        {
            duongdi = new List<int>();
            diqua = new bool[n];
            for (int i = 1; i < n; i++) diqua[i] = false;
            if (diqua[s] == false)
            {
                DFS(s);
            }
            XuatDFS(s);
        }
        public void DFS(int s)
        {
            diqua[s] = true;
            duongdi.Add(s);
            foreach (int u in list[s])
            {
                if (diqua[u] == false)
                {
                    DFS(u);
                }
            }
        }
        public void DFSfromXtoY(int x, int y)
        {
            if (list[x].Count == 0 || list[y].Count == 0)
            {
                Console.WriteLine("Khong co duong");
                return;
            }
            diqua = new bool[n];
            for (int i = 1; i < n; i++) // Set các giá trị của visited thành false hết
            {
                diqua[i] = false;
            }
            previous = new int[n];
            for (int i = 1; i < n; i++) // Set giá trị cho mảng
            {
                previous[i] = -1;
            }
            RecurseDFSXtoY(x);
            if (previous[y] == -1)
            {
                Console.WriteLine("No path" + y);
            }
            else
            {
                XuatXtoY(x, y);
            }
        }
        public void RecurseDFSXtoY(int s)
        {
            diqua[s] = true;
            foreach (int u in list[s])
                if (diqua[u] == false)
                {
                    previous[u] = s;
                    RecurseDFSXtoY(u);
                }
        }
        public void XuatXtoY(int x, int y)
        {
            Stack<int> st = new Stack<int>();
            int flag = y;
            while (previous[flag] != -1)
            {
                st.Push(flag);
                flag = previous[flag];
            }
            Console.WriteLine();
            Console.Write(x + " -> " + y + " :   " + x);
            while (st.Count > 0)
            {
                flag = st.Pop();
                Console.Write(" -> " + flag);
            }
            Console.WriteLine();
        }
        public void StackDFS(int s)
        {
            Stack<int> stack = new Stack<int>();
            diqua[s] = true;
            stack.Push(s);
            while (stack.Count != 0)
            {
                s = stack.Pop();
                index[s] = inconnect;
                foreach (int u in list[s])
                {
                    if (diqua[u] == false)
                    {
                        diqua[u] = true;
                        stack.Push(u);
                    }
                }
            }
        }
        public void XuatDFS(int s)
        {
            foreach (var item in duongdi)
            {
                if (item != s)
                {
                    Console.Write(item + " ");
                }
            }
        }
        public void OutConnected()
        {
            for (int i = 1; i <= inconnect; i++)
            {
                Console.Write("TPLT {0}: ", i);
                for (int j = 0; j < index.Length; j++)
                {
                    if (index[j] == i)
                    {
                        Console.Write(j + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void Connected()
        {
            inconnect = 0;
            index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = -1;
            }
            diqua = new bool[n];
            for (int i = 0; i < diqua.Length; i++)
            {
                diqua[i] = false;
            }
            for (int i = 1; i < diqua.Length; i++)
            {
                if (!diqua[i])
                {
                    inconnect++;
                    StackDFS(i);
                }
            }
        }

    }
}
