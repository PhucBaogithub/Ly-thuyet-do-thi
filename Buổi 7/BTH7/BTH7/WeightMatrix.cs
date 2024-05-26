using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTH7
{
    internal class WeightMatrix
    {
        private int n;
        private int[,] array;

        int[] pre;
        int[] dist;
        bool[] visited;
        int[,] floyPrevious;
        int[,] floyDist;

        public int N { get => n; set => n = value; }
        public int[,] Array { get => array; set => array = value; }
        public int[] Pre { get => pre; set => pre = value; }
        public int[] Dist { get => dist; set => dist = value; }
        public bool[] Visited { get => visited; set => visited = value; }
        public int[,] FloyPrevious { get => floyPrevious; set => floyPrevious = value; }
        public int[,] FloyDist { get => floyDist; set => floyDist = value; }

        public WeightMatrix() { }

        public WeightMatrix(int newWeightMatrix)
        {
            n = newWeightMatrix;
            array = new int[n, n];
        }

        public void fileToWeightMatrix(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            n = int.Parse(reader.ReadLine());
            array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] s = reader.ReadLine().Split();
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        array[i, j] = 0;
                    }
                    else
                    {
                        array[i, j] = int.Parse(s[j]) > 0 ? int.Parse(s[j]) : int.MaxValue;
                    }
                }
            }
            reader.Close();
        }

        public void Output()
        {
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : " + n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 0; i < n; i++) Console.Write(" {0}", i);
            Console.WriteLine(); Console.WriteLine(" " + new string('-', 6 * n));
            for (int i = 0; i < n; i++)
            {
                Console.Write(" {0} |", i);
                for (int j = 0; j < n; j++)
                    if (array[i, j] < int.MaxValue) Console.Write(" {0, 3}", array[i, j]);
                    else Console.Write(" {0, 3}", "_");
                Console.WriteLine();
            }
        }

        public int findMinIndex()
        {
            int minIndex = 0;
            int min = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (visited[i] == false && dist[i] < min)
                {
                    min = dist[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }

        public void Dijkstra(int s)
        {
            pre = new int[n];
            dist = new int[n];
            visited = new bool[n];
            // hàm này dùng để xác lập các giá trị ban đầu cho các biến
            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                visited[i] = false;
                pre[i] = s;
            }
            dist[s] = 0;
            for (int i = 0; i < n; i++)
            {
                int u = findMinIndex();
                visited[u] = true;
                for (int v = 0; v < n; v++)
                {
                    if (!visited[v] && array[u, v] != int.MaxValue)
                        if (dist[u] != int.MaxValue && dist[u] + array[u, v] < dist[v])
                        {
                            dist[v] = dist[u] + array[u, v];
                            pre[v] = u;
                        }
                }
            }
        }
        public void PrintDijkstra(int s)
        {
            Console.WriteLine("Shortest path from " + s + " to remaining verteces :");
            Console.Write("Vertex : ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(" " + i);
            }
            Console.WriteLine();
            Console.WriteLine(" " + new string('-', 3 * n));
            Console.Write("previous vertex : ");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0, 3}", pre[i]);
            }
            Console.WriteLine();
            Console.Write("distance : ");
            for (int i = 0; i < n; i++)
            {
                if (dist[i] < int.MaxValue)
                {
                    Console.Write("{0, 3}", dist[i]);
                }
                else
                {
                    Console.Write("{0, 3}", "x");
                }
            }
        }

        public int MinRouteXY(int x, int y)
        {
            Dijkstra(x);
            Stack<int> st = new Stack<int>();
            int k = y;
            st.Push(k);
            while (pre[k] != x)
            {
                k = pre[k];
                st.Push(k);
            }
            Console.WriteLine();
            Console.Write("Đường đi ngắn nhất từ {0} đến {1} : {2} ->", x, y, x);
            while (st.Count > 0)
            {
                k = st.Pop();
                Console.Write(" {0} ->", k);
            }
            Console.WriteLine(" độ dài : " + dist[y]);
            return dist[y];
        }

        public void MinRouteXYZ(int x, int y, int z)
        {
            int distanceXY = MinRouteXY(x, y);
            int distanceYZ = MinRouteXY(y, z);
            int distanceXZ = distanceXY + distanceYZ;
            Console.WriteLine("Khoảng cách từ X đến Z: " + distanceXZ);
        }

        public void Floyd()
        {
            floyDist = new int[n, n];
            floyPrevious = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    floyDist[i, j] = array[i, j];
                    floyPrevious[i, j] = i;
                }
            }
            for (int i = 0; i < n; i++)
            {
                floyDist[i, i] = 0;
            }
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if ((floyDist[i, j] > floyDist[i, k] + floyDist[k, j]) && floyDist[i, k] < int.MaxValue && floyDist[k, j] < int.MaxValue)
                        {
                            floyDist[i, j] = floyDist[i, k] + floyDist[k, j];
                            floyPrevious[i, j] = floyPrevious[k, j];
                        }
                    }
                }
            }
            //Outdp();
        }

        public void OutDToP()
        {
            Console.WriteLine("distance : ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(" " + floyDist[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("previous : ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(" " + floyPrevious[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Floyd_RouteXY(int x, int y)
        {
            Stack<int> st = new Stack<int>();
            int k = y;
            st.Push(k);
            while (floyPrevious[x, k] != x)
            {
                k = floyPrevious[x, k];
                st.Push(k);
            }
            Console.WriteLine();
            Console.Write("Khoảng cách ngắn nhất từ {0} đến {1}: {2}", x, y, x);
            while (st.Count > 0)
            {
                k = st.Pop();
                Console.Write(" {0} -> ", k);
            }
            Console.WriteLine("Độ dài : " + floyDist[x, y]);
        }
    }
}
