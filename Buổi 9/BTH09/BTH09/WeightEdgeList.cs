using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BTH09
{
    class WeightEdgeList
    {
        List<Tuple<int, int, int>> g; // Graph represented as a list of tuples (vertex1, vertex2, weight)
        int n; // Number of vertices in the graph
        int m; // Number of edges in the graph
        List<Tuple<int, int, int>> tree; // Minimum Spanning Tree (MST) for both Kruskal and Prim
        bool[,] connected; // Used for Kruskal's algorithm to mark connected components
        bool[] label; // Used for Prim's algorithm to mark connected vertices

        // Properties
        public int N { get => n; set => n = value; }
        public int M { get => m; set => m = value; }
        public List<Tuple<int, int, int>> G { get => g; set => g = value; }

        // Constructor
        public WeightEdgeList()
        {
            g = new List<Tuple<int, int, int>>();
        }

        // Read graph from a file
        public void FileToWeightEdgeList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string[] s = sr.ReadLine().Split();
            n = int.Parse(s[0]); // Read number of vertices
            m = int.Parse(s[1]); // Read number of edges

            // Read each edge and add it to the graph
            for (int i = 0; i < m; i++)
            {
                s = sr.ReadLine().Split();
                Tuple<int, int, int> e = new Tuple<int, int, int>(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
                g.Add(e);
            }
            sr.Close(); // Close the StreamReader
        }

        // Kruskal's algorithm to find the minimum spanning tree
        public void Kruskal()
        {
            g.Sort((x, y) => x.Item3.CompareTo(y.Item3)); // Sort edges by weight

            connected = new bool[n, n]; // Initialize connected array
            tree = new List<Tuple<int, int, int>>(); // Initialize tree

            foreach (var edge in g)
            {
                if (tree.Count == n - 1) // Stop when the tree has n-1 edges (MST property)
                    break;

                int v1 = edge.Item1;
                int v2 = edge.Item2;

                if (!IsConnected(v1, v2)) // Check if adding this edge creates a cycle
                {
                    tree.Add(edge); // Add the edge to the MST
                    Union(v1, v2); // Merge the connected components of v1 and v2
                }
            }

            Console.WriteLine("Cây khung nhỏ nhất theo thuật toán Kruskal:");
            ShowTree();
        }

        // Prim's algorithm to find the minimum spanning tree
        public void Prim(int u)
        {
            label = new bool[n]; // Initialize label array to mark connected vertices
            label[u] = true; // Mark the starting vertex u as visited
            tree = new List<Tuple<int, int, int>>(); // Initialize the minimum spanning tree

            while (tree.Count < n - 1) // Loop until the tree has n-1 edges
            {
                var e = Dmin(); // Find the minimum edge (u, v) where u is in Vt and v is not
                tree.Add(e); // Add the edge to the minimum spanning tree
                label[e.Item1] = true; // Mark both vertices of the edge as visited
                label[e.Item2] = true;
            }


            ShowTree();
        }

        // Find the minimum edge (u, v) where u is in Vt and v is not
        private Tuple<int, int, int> Dmin()
        {
            Tuple<int, int, int> emin = new Tuple<int, int, int>(int.MaxValue, int.MaxValue, int.MaxValue);
            foreach (var edge in g)
            {
                // Check if both vertices of the edge are in Vt
                if (label[edge.Item1] && !label[edge.Item2] || !label[edge.Item1] && label[edge.Item2])
                {
                    // Update emin if the weight of the current edge is smaller
                    if (edge.Item3 < emin.Item3)
                        emin = edge;
                }
            }
            return emin;
        }

        // Check if vertices v1 and v2 are in the same connected component
        private bool IsConnected(int v1, int v2)
        {
            return connected[v1, v2];
        }

        // Merge the connected components of vertices v1 and v2
        private void Union(int v1, int v2)
        {
            // Implementation of Union-Find algorithm
            // Here, we simply mark v1 and v2 as connected
            connected[v1, v2] = true;
            connected[v2, v1] = true;
        }

        // Display the minimum spanning tree
        public void ShowTree()
        {
            int totalWeight = 0; // Total weight of the minimum spanning tree

            foreach (var edge in tree)
            {
                Console.WriteLine($" ({edge.Item1}, {edge.Item2}) = {edge.Item3}");
                totalWeight += edge.Item3; // Accumulate edge weights
            }

            Console.WriteLine($"ĐỘ dài= {totalWeight}");
        }

    }
}
