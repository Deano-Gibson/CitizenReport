using System;
using System.Collections.Generic;

namespace CitizenReportWeb.Data
{
    /// <summary>
    /// Simple Min-Heap (Priority Queue) implementation for ordering service requests.
    /// Lower priority value = higher urgency (Pending < InProgress < Resolved)
    /// </summary>
    public class MinHeap<T> where T : IComparable<T>
    {
        private readonly List<T> _heap = new();

        public int Count => _heap.Count;
        public bool IsEmpty => _heap.Count == 0;

        public void Insert(T item)
        {
            _heap.Add(item);
            HeapifyUp(_heap.Count - 1);
        }

        public T ExtractMin()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            var root = _heap[0];
            _heap[0] = _heap[^1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);
            return root;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                var parent = (index - 1) / 2;
                if (_heap[index].CompareTo(_heap[parent]) >= 0)
                    break;

                (_heap[index], _heap[parent]) = (_heap[parent], _heap[index]);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            var lastIndex = _heap.Count - 1;

            while (true)
            {
                var left = index * 2 + 1;
                var right = index * 2 + 2;
                var smallest = index;

                if (left <= lastIndex && _heap[left].CompareTo(_heap[smallest]) < 0)
                    smallest = left;
                if (right <= lastIndex && _heap[right].CompareTo(_heap[smallest]) < 0)
                    smallest = right;

                if (smallest == index)
                    break;

                (_heap[index], _heap[smallest]) = (_heap[smallest], _heap[index]);
                index = smallest;
            }
        }

        public IEnumerable<T> ToList()
        {
            var copy = new List<T>(_heap);
            copy.Sort();
            return copy;
        }
    }
}
