using System;
using System.Collections.Generic;
using System.IO;

namespace Buoi0304
{
    class AdjList
    {
        bool[] visited;     // Dùng đánh dấu đỉnh đã đi qua
        int[] index;        // Dùng đánh dấu các TPLT
        int[] color;
        int[] previous;
        int inconnect;      // Dùng đếm số TPLT
        List<int> road;  //Dùng để đếm các thành phần đi qua

        LinkedList<int>[] v;
        int n;  //Số cột
        int m;  //Số dòng
        public int[,] a;

        public int Inconnect { get => inconnect; set => inconnect = value; }
        public int[,] A { get => a; set => a = value; }

        //Propeties
        public int N { get => n; set => n = value; }
        public int M { get => m; set => m = value; }
        public LinkedList<int>[] V
        {
            get { return v; }
            set { v = value; }
        }
        // Contructor
        public AdjList() { }
        public AdjList(int k)   // Khởi tạo v có k đỉnh
        {
            v = new LinkedList<int>[k];
        }
        // copy g --> đồ thị hiện tại v
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


        // Bài 1 : Duyệt đồ thị theo BFS với đỉnh xuất phát s
        public void BFS(int s)
        {
            // Khởi tạo visited[]
            visited = new bool[v.Length];
            // Khởi gáng visited[i] = false (i=0.. <visited.Lenght)
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }
            // Khai báo Queue : Queue<int> q = new Queue<int>();
            Queue<int> q = new Queue<int>();
            // Đánh dấu duyệt s (visited[s]= true)
            visited[s] = true;
            // Đưa s vào queue :s -> q
            q.Enqueue(s);
            // Lặp khi q còn chưa rỗng
            string tmp = "";
            while (q.Count > 0)
            {
                // Lấy trong q ra phần tử s : s <- q
                s = q.Dequeue();
                tmp = tmp + s;
                // Duyệt s, xuất s lên màn hình (Console.Write(s + " - ");)
                // Xét các đỉnh kề u của s : (foreach (int u in v[s]))
                foreach (int u in v[s])
                {
                    // Nếu đã duyệt u (visited[u]=true) -> bỏ qua : continue;
                    if (visited[u]) continue;
                    // Đánh dấu duyệt u (visited[u]=true)
                    visited[u] = true;
                    // Đưa u vào q : u -> q
                    q.Enqueue(u);
                }
                tmp += "";
            }

            string[] tmps = tmp.TrimEnd().Split(' ');
            Console.WriteLine("So luong dinh di tu {0} => {1} ", tmps[0], tmps.Length - 1);
            Console.Write("Cac dinh lien thong voi {0}: ", tmps[0]);
            for (int i = 1; i < tmps.Length; i++)
            {
                Console.Write(tmps[i] + "->" + " ");
            }
            Console.WriteLine();
        }

        // Bài 2 : Tìm đường đi từ đỉnh x đến y theo BFS
        public void BFS_XtoY(int x, int y)
        {
            // pre[] : lưu đỉnh nằm trước trên đường đi
            int[] pre = new int[v.Length];
            // Khởi tạo pre[] với v.Lenght phần tử

            // Gán các giá trị pre[i] = -1 với i = 0 ... <v.Lenght
            for (int i = 1; i < pre.Length; i++)
            {
                pre[i] = -1;
            }
            // khởi tạo và gán các giá trị ban đầu cho visited[]
            visited = new bool[v.Length];
            // Khai báo một queue<int> q
            Queue<int> q = new Queue<int>();
            // Đánh dấu đã duyệt x
            for (int i = 1; i < visited.Length; i++)
            {
                visited[i] = false; // set tất cả giá trị ban đầu cho tất cả đỉnh bằng false tại chưa đi qua
            }
            // Đưa x vào q
            visited[x] = true;
            q.Enqueue(x);
            // Lặp khi q chưa rỗng
            while (q.Count != 0)
            {
                // Lấy trong q ra đỉnh s : int s = q.Dequeue();
                int s = q.Dequeue();
                // Duyệt các đỉnh kề u của s : (foreach (int u in v[s]))
                foreach (int value in v[s])
                {
                    // Nếu đã duyệt u (visited[u]=true) thì bỏ qua (continue)
                    if (visited[value]) continue;
                    // Đánh dấu đã duyệt u
                    visited[value] = true;
                    // Đưa u vào q
                    q.Enqueue(value);
                    // Đặt s là đỉnh trước u : pre[u] = s;
                    pre[value] = s;
                }
            }
            // Xuất đường đi từ x đến y
            Console.WriteLine();
            int k = y;
            Stack<int> stk = new Stack<int>();
            while (pre[k] != -1)
            {
                stk.Push(k);
                k = pre[k];
            }
            Console.WriteLine();
            Console.Write(" Đường đi từ " + x + " -> " + y + " :   " + x);
            while (stk.Count > 0)
            {
                k = stk.Pop();
                Console.Write(" -> " + k);
            }
            Console.WriteLine();
        }
        // Xét tính liên thông và xác định giá trị cho visite[], index[]
        // Xác định inconnect : số thành phần liên thông (TPLT)


        public void Connected()
        {
            // inconnect : số TPLT  giá trị ban đầu = 0
            inconnect = 0;
            // index : lưu các đỉnh cùng một TPLT, khởi tạo index[] n phần tử
            index = new int[n];
            // Khởi gán index[i] = -1, i = 0 .. < n 
            for (int i = 0; i < n; i++)
            {
                index[i] = -1;
            }
            // Khởi tạo và giá trị ban đầu cho visited[i] = false, Vi = 0 .. < n
            visited = new bool[n];
            // Duyệt từng đỉnh i
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }
            for (int i = 0; i < visited.Length; i++)
            // Nếu chưa duyệt đỉnh i (visited[i] == false)        
            {
                if (visited[i] == false)
                {
                    // Khởi đầu cho một TPLT mới -> tăng inconnect++
                    inconnect++;
                    // Tìm và đánh dấu các đỉnh cùng TPLT, gọi hàm
                    BFS_Connected(i);
                    //BFS_Connected(i);
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
                s = q.Dequeue();
                // gán giá trị TPLT : index[s] = inconnect;
                index[s] = inconnect;
                // Duyệt các đỉnh kề u của s (int u in v[s])
                foreach (int value in v[s])
                {
                    // Nếu u chưa duyệt (visited[u] == false)
                    if (visited[value] == false)
                    {
                        // Duyệt u : visited[u] = true;
                        visited[value] = true;
                        // Đưa u vào Queue q
                        q.Enqueue(value);
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
            for (int i = 0; i < n; i++)
            {
                //Nếu i = x : xóa hết các phần tử trong dslk v[i]
                if (i == x)
                {
                    v[i].Clear();
                }
                else
                {
                    //Ngược lại : xóa phần tử x trong v[i]
                    v[i].Remove(x);
                }
            }
        }

        public void RemoveEdgeXY(int x, int y)
        {
            // Xóa y trong v[x]
            v[x].Remove(y);
            // Xóa x trong v[y]
            v[y].Remove(x);
        }

        public bool IsBipartite(int x)
        {
            // Khởi tạo và gán giá trị color[i] = -1, i = 0..<n
            color = new int[n];
            for (int i = 0; i < n; i++)
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
            while (q.Count != 0)
            {
                //Lấy s <- q;
                int s = q.Dequeue();
                //Nếu s thuộc v[s] -> return false;
                if (v[s].Contains(s)) return false;
                // Duyệt (u in v[s])
                foreach (int u in v[s])
                {
                    //Nếu(color[u] == -1)
                    if (color[u] == -1)
                    {
                        // gán : color[u] = 1 - color[s];
                        color[u] = 1 - color[s];
                        // Bỏ u -> q;
                        q.Enqueue(u);
                    }
                    //Ngược lại Nếu(color[u] == color[s])
                    if (color[u] == color[s])
                        return false;
                }
            }
            return true;
        }

        // Đọc file Grid.txt -> tạo đồ thị tương ứng và 2 đỉng x, y
        public void GridToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            // Xác định dòng, cột và số đỉnh n
            string[] s = sr.ReadLine().Split();
            int row = int.Parse(s[0]);
            int col = int.Parse(s[1]);
            n = row * col;
            // Khởi tạo v[]
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
                v[i] = new LinkedList<int>();
            // Xác định đỉnh đầu x và đỉnh đến y
            s = sr.ReadLine().Split();
            int x = int.Parse(s[1]) + int.Parse(s[0]) * col;
            int y = int.Parse(s[3]) + int.Parse(s[2]) * col;

            // đọc Grid.txt ra ma trận a
            int[,] a = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < col; j++)
                    a[i, j] = int.Parse(s[j]);
            }
            // Xuất ma trận a
            Console.WriteLine("  Ma trận lưới a[] : ");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    if (a[i, j] == 1)
                    {
                        //Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  " + a[i, j]);
                        //Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.Write("  " + a[i, j]);
                Console.WriteLine();
            }

            // Đọc a[] ra AdjList
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (a[i, j] == 1)
                    {
                        int k = j + i * col;
                        if (i > 0 && a[i - 1, j] == 1)
                        {
                            if (v[k].Contains(k - col) == false) v[k].AddLast(k - col);
                            if (v[k - col].Contains(k) == false) v[k - col].AddLast(k);
                        }
                        if (j > 0 && a[i, j - 1] == 1)
                        {
                            if (v[k].Contains(k - 1) == false) v[k].AddLast(k - 1);
                            if (v[k - 1].Contains(k) == false) v[k - 1].AddLast(k);
                        }
                        if (j < col - 1 && a[i, j + 1] == 1)
                        {
                            if (v[k].Contains(k + 1) == false) v[k].AddLast(k + 1);
                            if (v[k + 1].Contains(k) == false) v[k + 1].AddLast(k);
                        }
                        if (i < row - 1 && a[i + 1, j] == 1)
                        {
                            if (v[k].Contains(k + col) == false) v[k].AddLast(k + col);
                            if (v[k + col].Contains(k) == false) v[k + col].AddLast(k);
                        }
                    }
                }
            // Tìm và xuất đường đi
            PathOnGrid(x, y, row, col, a);
        }
        // Tìm đường đi trên đồ thị và Xuất lộ trình trên lưới
        public void PathOnGrid(int x, int y, int row, int col, int[,] a)
        {
            // pre[] : lưu đỉnh nằm trước trên đường đi
            int[] pre = new int[v.Length];
            for (int i = 0; i < v.Length; i++) pre[i] = -1;

            // khởi tạo các giá trị cho visited
            visited = new bool[v.Length];
            for (int i = 0; i < visited.Length; i++) visited[i] = false;

            Queue<int> q = new Queue<int>();
            visited[x] = true;
            q.Enqueue(x);
            while (q.Count != 0)
            {
                int s = q.Dequeue();
                foreach (int u in v[s])
                {
                    if (visited[u]) continue;
                    visited[u] = true;
                    q.Enqueue(u);
                    pre[u] = s;
                }
            }
            // Xuất đường đi từ x đến y theo tọa độ
            Console.WriteLine("  Tìm đường đi");
            Console.ReadLine(); Console.WriteLine();
            int k = y;
            Stack<int> stk = new Stack<int>(); // dùng xuất tọa độ
            Stack<int> st = new Stack<int>(); // dùng xuất tô màu

            // bỏ k vào stack
            while (k != -1)
            {
                stk.Push(k); st.Push(k);
                k = pre[k];
            }
            Console.WriteLine();
            // Xuất theo tọa độ

            Console.WriteLine("  Đường đi từ ({0}, {1}) -> ({2}, {3}) : " + $"Số đỉnh của đồ thị: {stk.Count}", (x / col), x % col, (y / col), y % col, (x / col), x % col);
            Console.Write("    ({0}, {1}) ", (x / col), x % col);
            while (stk.Count > 0)
            {
                k = stk.Pop();
                Tuple<int, int> e = new Tuple<int, int>(k / col, k % col);
                Console.Write(" -> ({0}, {1})", e.Item1, e.Item2);
            }
            // Xuất theo tô màu đường đi
            Console.WriteLine();
            st.Push(x);
            for (int i = 0; i < row; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < col; j++)
                {
                    k = j + i * col;
                    if (st.Contains(k))
                    {
                        //Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.Write("  " + a[i, j]);
                        //Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (!st.Contains(k) && a[i, j] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  " + a[i, j]);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  " + a[i, j]);
                    }
                }
            }

            Console.WriteLine();
        }

        //Bài 9 duyệt dường đi bằng thuật toán DFS
        public void TraverseDFS(int s)
        {
            road = new List<int>();
            visited = new bool[n];
            for (int i = 1; i < n; i++) visited[i] = false;
            if (visited[s] == false)
            {
                DFS(s);
            }
            XuatDFS(s);
        }
        public void DFS(int s)
        {
            visited[s] = true;
            road.Add(s);
            foreach (int u in v[s])
            {
                if (visited[u] == false)
                {
                    DFS(u);
                }
            }
        }
        public void XuatDFS(int s)
        {
            Console.Write(s + "-");
            foreach (var item in road)
            {
                if (item != s)
                {
                    Console.Write(item + "-");
                }
            }
        }

        //Bài 10 tìm đường đi từ 2 đỉnh từ thuật toán DFS
        public void DFSfromXtoY(int x, int y)
        {
            if (v[x].Count == 0 || v[y].Count == 0)
            {
                Console.WriteLine("Không có đường đi!");
                return;
            }
            visited = new bool[n];
            for (int i = 1; i < n; i++) // Set các giá trị của visited thành false hết
            {
                visited[i] = false;
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
            visited[s] = true;
            foreach (int u in v[s])
                if (visited[u] == false)
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

        //Bài 11 Xét tính liên thông của đồ thị
        public void StackDFS(int s)
        {
            Stack<int> stack = new Stack<int>();
            visited[s] = true;
            stack.Push(s);
            while (stack.Count != 0)
            {
                s = stack.Pop();
                index[s] = inconnect;
                foreach (int u in v[s])
                {
                    if (visited[u] == false)
                    {
                        visited[u] = true;
                        stack.Push(u);
                    }
                }
            }
        }
        public void OutConnectedDFS()
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
        public void ConnectedDFS()
        {
            inconnect = 0;
            index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = -1;
            }
            visited = new bool[n];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }
            for (int i = 1; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    inconnect++;
                    StackDFS(i);
                }
            }
        }
    }
}
