using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BT1
{
    internal class AdjList
    {
        LinkedList<int>[] list;
        int n;
        bool[] diQua;
        int[] index;
        int inconnect;

        public LinkedList<int>[] List { get => list; set => list = value; }
        public int N { get => n; set => n = value; }
        public int Inconnect { get => inconnect; set => inconnect = value; }

        public AdjList() { }
        public AdjList(int k)
        {
            list = new LinkedList<int>[k];
        }

        public AdjList(LinkedList<int>[] newList)
        {
            list = newList;
        }

        public void FileToAdjList(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            n = int.Parse(reader.ReadLine()) + 1;
            list = new LinkedList<int>[n];
            for (int i = 1; i < n; i++)
            {
                list[i] = new LinkedList<int>();
                string value = reader.ReadLine();
                if (value != "")
                {
                    string[] s = value.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        list[i].AddLast(x);
                    }
                }
            }
            reader.Close();
        }

        public void Output()
        {
            int m = n - 1;
            for (int i = 1; i < list.Length; i++)
            {
                Console.Write("Dinh {0} => ", i);
                foreach (int value in list[i])
                {
                    Console.Write("{0, 3}", value);
                }
                Console.WriteLine();
            }
        }

        public void BFS(int start)
        {
            diQua = new bool[list.Length]; // khởi tạo cho các giá trị của mảng đánh dấu

            for (int i = 1; i < diQua.Length; i++)
            {
                diQua[i] = false; // set cho tất cả các giá trị chưa đi qua = false
            }

            Queue<int> queue = new Queue<int>();

            diQua[start] = true; // đánh dấu là đã duyệt s

            queue.Enqueue(start);

            string tmp = ""; // tạo một chuỗi để lưu kết quả
            while (queue.Count != 0)
            {
                start = queue.Dequeue();
                tmp = tmp + start;
                foreach (int value in list[start]) // xét các phần tử liền kề với nó
                {
                    if (diQua[value] == true)
                    {
                        continue;
                    }
                    diQua[value] = true;
                    queue.Enqueue(value);
                }
                tmp += " ";
            }

            string[] tmps = tmp.TrimEnd().Split(' ');
            Console.WriteLine("So luong dinh di tu {0} => {1} ", tmps[0], tmps.Length - 1);
            Console.Write("Cac dinh lien thong voi {0}: ", tmps[0]);
            for (int i = 1; i < tmps.Length; i++)
            {
                Console.Write(tmps[i] + " ");
            }
            Console.WriteLine();
        }

        public void BFS_X_to_Y(int x, int y)
        {
            int[] dinhNamTruoc = new int[list.Length]; // Mảng này dùng để lưu các đỉnh nằm trước trên đường đi
            for (int i = 1; i < dinhNamTruoc.Length; i++)
            {
                dinhNamTruoc[i] = -1;
            }

            diQua = new bool[list.Length];
            for (int i = 1; i < diQua.Length; i++)
            {
                diQua[i] = false; // set tất cả giá trị ban đầu cho tất cả đỉnh bằng false tại chưa đi qua
            }

            Queue<int> queue = new Queue<int>();

            diQua[x] = true; // set cho vị trí x ban đầu đã đi qua rồi tại vì bắt đầu từ x

            queue.Enqueue(x); // Queue vào trước ra trước, theo BFS thì thuật toán tìm kiếm sẽ lan rộng ra ( khi cho node vào thì nó sẽ tìm các node liền kề với nó )

            while (queue.Count != 0)
            {
                int s = queue.Dequeue(); // tạo một biến để chứa một đỉnh được dequeue ra
                foreach (int value in list[s]) // check những cạnh liền kề với đỉnh đó
                {
                    if (diQua[value] == true) // khi node này đã được đi qua rồi thì bỏ qua
                    {
                        continue;
                    }
                    diQua[value] = true; //nếu như là false thì khi đi qua sẽ set cho nó bằng true
                    queue.Enqueue(value); //Enqueue lại
                    dinhNamTruoc[value] = s; // 
                }
            }

            //Thao tác xuất đường đi từ x => y

            Console.WriteLine();
            int k = y;

            Stack<int> stack = new Stack<int>();

            while (dinhNamTruoc[k] != -1)
            {
                stack.Push(k);
                k = dinhNamTruoc[k];
            }
            Console.WriteLine();
            Console.Write("Duong di tu " + x + " => " + y + ": " + x);
            while (stack.Count != 0)
            {
                k = stack.Pop();
                Console.Write("-> " + k);
            }
            Console.WriteLine();
        }

        public void Connected()
        {
            inconnect = 0; // inconnect ở đây là số lượng các thành phần liên thông trong đồ thị nhập vào
            index = new int[n]; // index dùng để lưu các đỉnh có cùng thành phần liên thông
            for (int i = 0; i < n; i++)
            {
                index[i] = -1;
            }

            // khởi tạo các giá trị cho diQua

            diQua = new bool[n];
            for (int i = 0; i < diQua.Length; i++)
            {
                diQua[i] = false;
            }

            for (int i = 1; i < diQua.Length; i++)
            {
                if (diQua[i] == false)
                {
                    //khởi đầu cho 1 thành phần liên thông mới
                    inconnect++;
                    BFS_Connected(i); // function này đi tìm các đỉnh nằm trong vùng liên thông với đỉnh i
                }
                Console.WriteLine();
            }

        }

        public void BFS_Connected(int s)
        {
            Queue<int> queue = new Queue<int>();
            diQua[s] = true;
            queue.Enqueue(s);
            while (queue.Count != 0)
            {
                s = queue.Dequeue();
                index[s] = inconnect;
                foreach (int value in list[s])
                {
                    if (diQua[value] == false)
                    {
                        diQua[value] = true;
                        queue.Enqueue(value);
                    }
                }
            }
        }

        public void Output_Connected() //hàm này dùng để xuất ra các thành phần liên thông
        {
            for (int i = 1; i <= inconnect; i++)
            {
                Console.WriteLine("TPLT {0} ", i);
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
    }
}
