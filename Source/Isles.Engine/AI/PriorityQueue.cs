using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Isles.AI
{
    #region PriorityQueue
    /// <summary>
    /// Use min heap to implement a priority queue
    /// </summary>
    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Internal queue elements.
        /// Use array doubling approach to implement dynamic set.
        /// The first element is not used for easy index generation.
        /// </summary>
        T[] data;

        /// <summary>
        /// Actual data length
        /// </summary>
        int length;

        /// <summary>
        /// Gets priority queue element count
        /// </summary>
        public int Count
        {
            get { return length; }
        }

        /// <summary>
        /// Gets priority queue capacity
        /// </summary>
        public int Capacity
        {
            get { return data.Length; }
        }

        /// <summary>
        /// Gets whether the queue is empty
        /// </summary>
        public bool Empty
        {
            get { return length == 0; }
        }

        /// <summary>
        /// Retrive the minimun (top) element without removing it
        /// </summary>
        public T Top
        {
            get { return data[1]; }
        }

        /// <summary>
        /// Creates a priority queue
        /// </summary>
        public PriorityQueue()
        {
            data = new T[16];
        }

        /// <summary>
        /// Creates a priority queue to hold n elements
        /// </summary>
        /// <param name="capacity"></param>
        public PriorityQueue(int capacity)
        {
            data = new T[(capacity + 2) & 0xFFFFFE];
        }

        /// <summary>
        /// Clear the priority queue
        /// </summary>
        public void Clear()
        {
            length = 0;
        }

        /// <summary>
        /// Adds an element to the queue
        /// </summary>
        public void Add(T element)
        {
            int i, x;

            // When we running out of cache
            if (length >= data.Length - 1)
            {
                // Double the size of the cache
                int capacity = data.Length << 1;
                T[] newData = new T[capacity];

                // Copy to the new buffer
                for (i = 1; i <= length; i++)
                    newData[i] = data[i];

                data = newData;
            }

            // Bubble up the heap
            i = ++length;
            while (i > 0)
            {
                x = i >> 1;
                if (x > 0 && element.CompareTo(data[x]) < 0)
                {
                    data[i] = data[x];
                    i = x;
                }
                else
                    break;
            }

            // Assign the new element
            data[i] = element;
        }

        /// <summary>
        /// Remove and retrieve the minimun (top) element
        /// </summary>
        public T Pop()
        {
            if (length <= 0)
                throw new InvalidOperationException();

            // Make use of the first element here
            T top = data[1];
            FixHeap(data, 1, length - 1, data[length--]);
            return top;
        }

        /// <summary>
        /// Increase the priority of a given node
        /// </summary>
        public void IncreasePriority(int index, T newValue)
        {
            int x;

            // Bubble up the heap
            while (index > 0)
            {
                x = index >> 1;
                if (x > 0 && newValue.CompareTo(data[x]) < 0)
                {
                    data[index] = data[x];
                    index = x;
                }
                else
                    break;
            }

            // Assign the new element
            data[index] = newValue;
        }

        /// <summary>
        /// Fix the heap
        /// </summary>
        /// <param name="elements">Array of elements to be fixed</param>
        /// <param name="i">Root index of the subtree</param>
        /// <param name="n">Subtree size</param>
        /// <param name="k">Element to be add as the root</param>
        static void FixHeap(T[] E, int i, int n, T k)
        {
            int x, min;
            while (i <= n)
            {
                x = i << 1;         /* Left subtree */
                if (x > n)
                    break;
                else if (x == n)   /* No right subtree */
                    min = x;
                else
                    min = (E[x].CompareTo(E[x + 1]) < 0) ? x : x + 1;

                if (E[min].CompareTo(k) < 0)
                {
                    E[i] = E[min];  /* Sink if k is bigger */
                    i = min;
                }
                else
                {
                    break;          /* Otherwise fix is done */
                }
            }

            E[i] = k;
        }

        /// <summary>
        /// Get priority queue enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 1; i <= length; i++)
                yield return data[i];
        }

        /// <summary>
        /// Get priority queue enumerator, nongeneric version
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
    #endregion

    #region IndexedPriorityQueue
    /// <summary>
    /// Use min heap to implement a priority queue.
    /// Used to implement Dijkstra's algorithm.
    /// </summary>
    /// <remarks>
    /// The size of the indexed priority queue is fixed.
    /// </remarks>
    public class IndexedPriorityQueue
    {
        /// <summary>
        /// Internal queue elements.
        /// The first element is not used for easy index generation.
        /// </summary>
        int[] data;

        /// <summary>
        /// Keep track of the position of individual item in the heap.
        /// E.g. index[3] = 5 means that data[5] = 3;
        /// </summary>
        int[] index;

        /// <summary>
        /// Cost of each item
        /// </summary>
        float[] costs;

        /// <summary>
        /// Actual data length
        /// </summary>
        int count;

        /// <summary>
        /// Gets element index array
        /// </summary>
        public int[] Index
        {
            get { return index; }
        }

        /// <summary>
        /// Gets priority queue element count
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Gets priority queue capacity
        /// </summary>
        public int Capacity
        {
            get { return data.Length; }
        }

        /// <summary>
        /// Gets whether the queue is empty
        /// </summary>
        public bool Empty
        {
            get { return count == 0; }
        }

        /// <summary>
        /// Retrive the minimun (top) element without removing it
        /// </summary>
        public int Top
        {
            get { return data[1]; }
        }

        /// <summary>
        /// Creates a priority queue to hold n elements
        /// </summary>
        /// <param name="capacity"></param>
        public IndexedPriorityQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException();

            data = new int[capacity];
            costs = new float[capacity];
            index = new int[capacity];

            Reset();
        }

        /// <summary>
        /// Reset the priority queue
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < costs.Length; i++)
            {
                costs[i] = 0;
                index[i] = -1;
            }
        }

        /// <summary>
        /// Clear the priority queue
        /// </summary>
        public void Clear()
        {
            count = 0;
        }

        /// <summary>
        /// Adds an element to the queue
        /// </summary>
        public void Add(int element, float cost)
        {
            int i, x;

            // Bubble up the heap
            i = ++count;
            while (i > 0)
            {
                x = i >> 1;
                if (x > 0 && cost < costs[x])
                {
                    costs[i] = costs[x];
                    data[i] = data[x];
                    index[data[x]] = i;
                    i = x;
                }
                else
                    break;
            }

            // Assign the new element
            costs[i] = cost;
            data[i] = element;
            index[element] = i;
        }

        /// <summary>
        /// Remove and retrieve the minimun (top) element
        /// </summary>
        public int Pop()
        {
            if (count <= 0)
                throw new InvalidOperationException();

            // Make use of the first element here
            int top = data[1];
            index[top] = 0;
            FixHeap(1, count - 1, data[count], costs[count]);
            count--;
            return top;
        }

        /// <summary>
        /// Increase the priority of a given node
        /// </summary>
        public void IncreasePriority(int element, float cost)
        {
            int x, i;

            // Check to see if the element is in the heap
            i = index[element];
            if (i <= 0)
                return;

            // Bubble up the heap
            while (i > 0)
            {
                x = i >> 1;
                if (x > 0 && cost < costs[x])
                {
                    costs[i] = costs[x];
                    data[i] = data[x];
                    index[data[x]] = i;
                    i = x;
                }
                else
                    break;
            }

            // Assign the new element
            costs[i] = cost;
            data[i] = element;
            index[element] = i;
        }

        /// <summary>
        /// Fix the heap
        /// </summary>
        /// <param name="elements">Array of elements to be fixed</param>
        /// <param name="i">Root index of the subtree</param>
        /// <param name="n">Subtree size</param>
        /// <param name="k">Element to be add as the root</param>
        void FixHeap(int i, int n, int k, float cost)
        {
            int x, min;
            while (i <= n)
            {
                x = i << 1;         /* Left subtree */
                if (x > n)
                    break;
                else if (x == n)   /* No right subtree */
                    min = x;
                else
                    min = (costs[x] < costs[x + 1]) ? x : x + 1;

                if (costs[min] < cost)
                {
                    costs[i] = costs[min];
                    data[i] = data[min];  /* Sink if k is bigger */
                    index[data[min]] = i;
                    i = min;
                }
                else
                {
                    break;          /* Otherwise fix is done */
                }
            }

            costs[i] = cost;
            data[i] = k;
            index[k] = i;
        }
    }
    #endregion
}