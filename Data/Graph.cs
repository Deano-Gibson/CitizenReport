using System;
using System.Collections.Generic;
using System.Linq;

namespace CitizenReportWeb.Data
{
    /// <summary>
    /// Simple undirected graph for linking related service categories.
    /// Used to demonstrate graph traversal (BFS/DFS).
    /// </summary>
    public class Graph<T>
    {
        private readonly Dictionary<T, List<T>> _adjacencyList = new();

        public void AddVertex(T vertex)
        {
            if (!_adjacencyList.ContainsKey(vertex))
                _adjacencyList[vertex] = new List<T>();
        }

        public void AddEdge(T vertex1, T vertex2)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);

            // undirected edge
            _adjacencyList[vertex1].Add(vertex2);
            _adjacencyList[vertex2].Add(vertex1);
        }

        public IEnumerable<T> GetVertices() => _adjacencyList.Keys;

        /// <summary>
        /// Breadth-First Search traversal.
        /// Returns all connected vertices starting from the given source.
        /// </summary>
        public List<T> BreadthFirstSearch(T start)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();
            var result = new List<T>();

            if (!_adjacencyList.ContainsKey(start))
                return result;

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                result.Add(vertex);

                foreach (var neighbor in _adjacencyList[vertex])
                {
                    if (visited.Add(neighbor))
                        queue.Enqueue(neighbor);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                _adjacencyList.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}"));
        }
    }
}
