using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Buoi4
{
    class AdjList
    {
        LinkedList<int>[] v;
        int n;  // Số đỉnh
        // Để đơn giản : thêm các thành phần chỉ tham gia vào giải thuật
        bool[] visited;     // Dùng đánh dấu đỉnh đã đi qua
        int[] index;        // Dùng đánh dấu các TPLT
        int[] color;
        int inconnect;      // Dùng đếm số TPLT
        //Propeties
        public int N { get => n; set => n = value; }
        public LinkedList<int>[] V { get => v; set => v = value; }
        public int Inconnect { get => inconnect; set => inconnect = value; }
        // Contructor
        public AdjList() { }
        public AdjList(int k)
        {
            v = new LinkedList<int>[k];
        }
        public AdjList(LinkedList<int>[] g)
        {
            v = g;
        }
        // Đọc file AdjList.txt --> danh sách kề v
        public void FileToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<int>();
                string st = sr.ReadLine();
                // Đặt điều kiện không phải đỉnh cô lập
                if (st != "")
                {
                    string[] s = st.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        v[i].AddLast(x);
                    }
                }
            }
            sr.Close();
        }
        // Xuất đồ thị
        public void Output()
        {
            Console.WriteLine("Đồ thị danh sách kề - số đỉnh : " + n);
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("   Đỉnh {0} ->", i);
                foreach (int x in v[i])
                    Console.Write("{0, 3}", x);
                Console.WriteLine();
            }
        }    
        // Xét tính liên thông và xác định giá trị cho visite[], index[]
        // Xác định inconnect : số thành phần liên thông (TPLT)
        public void Connected()
        {
            // inconnect : số TPLT  giá trị ban đầu = 0
            // index : lưu các đỉnh cùng một TPLT, khởi tạo index[] n phần tử
            // Khởi gán index[i] = -1, i = 0 .. < n 
            // Khởi tạo và giá trị ban đầu cho visited[i] = false, Vi = 0 .. < n          
            // Duyệt từng đỉnh i
            inconnect = 0;
            index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = -1;
            }
            visited = new bool[V.Length];
            for (int i = 0; i < V.Length; i++)
            {
                visited[i] = false;
            }
            Console.WriteLine();
            for (int i = 0; i < visited.Length; i++)
            // Nếu chưa duyệt đỉnh i (visited[i] == false)        
            {
                if (!visited[i])
                {
                    // Khởi đầu cho một TPLT mới -> tăng inconnect++
                    // Tìm và đánh dấu các đỉnh cùng TPLT, gọi hàm
                    inconnect++;
                    BFS_Connected(i);
                }
            }
            Console.WriteLine();
        }
        // Lượt duyệt mới vớt đỉnh bắt đầu: s
        public void BFS_Connected(int s)
        {
            // Sử dụng một queue cho giải thuật
            Queue<int> q = new Queue<int>();
            // Duyệt đỉnh s (visited[s] = true)
            visited[s] = true;
        
            // Đưa s vào q
            q.Enqueue(s);
            // Lặp khi queue q còn phần tử
            while (q.Count != 0) 
            {
                // Lấy từ queue q ra một phần tử -> s
                s= q.Dequeue(); 
                // gán giá trị TPLT : index[s] = inconnect;
                index[s] = inconnect;
                // Duyệt các đỉnh kề u của s (int u in v[s])
                foreach(int u in v[s])
                {
                    // Nếu u chưa duyệt (visited[u] == false)
                    if (!visited[u])
                    {
                        // Duyệt u : visited[u] = true;
                        // Đưa u vào Queue q
                        visited[u] = true;
                        q.Enqueue(u);
                    }
                }
            }
        }
        // Xuất các thành phần liên thông
        public void OutConnected()
        {
            for (int i = 1; i <= inconnect; i++)
            {
                Console.Write("  TPLT {0} : ", i);
                for (int j = 0; j < index.Length; j++)
                    if (index[j] == i)
                        Console.Write(j + " ");
                Console.WriteLine();
            }
        }
        public void RemoveEdgeX(int x)
        {
            //Duyệt từng đỉnh i của đồ thị
            //    Nếu i = x : xóa hết các phần tử trong dslk v[i]
            //    Ngược lại : xóa phần tử x trong v[i]
            for(int i=0; i<v.Length; i++)
            {
                if (i == x)
                {
                    v[i].Clear(); // Xóa hết các phần tử trong dslk v[i]
                }
                else
                {
                    v[i].Remove(x); // Xóa phần tử x trong v[i]
                }
            }
        }
        public void RemoveEdgeXY(int x, int y)
        {
            // Xóa y trong v[x]
            // Xóa x trong v[y]
            for (int i = 0; i < v.Length; i++)
            {
                v[i].Remove(x);
                v[i].Remove(y);
            }
            
            
        }
        public void GridToAdjList(string filePath)
        {
           
            // Tìm và xuất đường đi theo tọa độ
            //PathOnGrid(x, y, col);
        }
        // Đồ thị 2 phía dùng BFS. Gọi hàm : IsBipartite(0)
        public bool IsBipartite(int x)
        {
            color = new int[n];
            // Khởi tạo và gán giá trị color[i] = -1, i = 0..<n
            for(int i = 0; i < n; i++)
            {
                color[i] = -1;
            }
            // Trước khi xét, đặt color[x] = 1
            color[x] = 1;
            // Khởi tạo Queue<int> q = new Queue<int>();
            Queue<int> q = new Queue<int>();
            // bỏ x vào q
            q.Enqueue(x);
            // Lặp khi q khác rỗng
            while(q.Count!=0)
            {
                //Lấy s <- q;
                int s= q.Dequeue();
                //Nếu s thuộc v[s] -> return false;
                if (v[s].Contains(s)) return false;
                // Duyệt (u in v[s])
                foreach(int u in v[s])
                {
                    if(color[u] == -1)
                    {
                            // gán : color[u] = 1 - color[s];
                            color[u] = 1 - color[s];
                            // Bỏ u -> q;
                            q.Enqueue(u);
                    }
                    if(color[u] == color[s])
                                    return false;
                }
            }
            return true;
        }


    }
}

