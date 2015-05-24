﻿using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Doubly-Linked List Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedList<T>
    {
        /// <summary>
        /// The Doubly-Linked List Node class.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
		internal class DLinkedListNode
        {
            public T Data { get; set; }
            public DLinkedListNode Next { get; set; }
            public DLinkedListNode Previous { get; set; }

            public DLinkedListNode()
            {
                Data = default(T);
                Next = Previous = null;
            }

            public DLinkedListNode(T dataItem)
            {
                Data = dataItem;
                Next = Previous = null;
            }
        }


        /// <summary>
        /// Instance variables.
        /// </summary>
        private DLinkedListNode _firstNode { get; set; }
        private DLinkedListNode _lastNode { get; set; }
        public int Count { private set; get; }


        /// <summary>
        /// A function that is used to update the _lastNode reference.
        /// </summary>
        private void UpdateLastNode()
        {
            var currentNode = _firstNode;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            _lastNode = currentNode;
        }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public DLinkedList()
        {
            _firstNode = null;
            _lastNode = null;
            Count = 0;
        }


        /// <summary>
        /// Determines whether this List is empty.
        /// </summary>
        /// <returns><c>true</c> if this list is empty; otherwise, <c>false</c>.</returns>
        public bool IsEmpty()
        {
            return (Count == 0);
        }


        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public T First
        {
            get
            {
                if (Count == 0)
                {
                    throw new Exception("Empty list.");
                }
                else
                {
                    return _firstNode.Data;
                }
            }
        }


        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public T Last
        {
            get
            {
                if (Count == 0)
                {
                    throw new Exception("Empty list.");
                }
                else if (_lastNode == null)
                {
                    var currentNode = _firstNode;
                    while (currentNode.Next != null)
                    {
                        currentNode = currentNode.Next;
                    }
                    _lastNode = currentNode;
                    return currentNode.Data;
                }
                else
                {
                    return _lastNode.Data;
                }
            }
        }


        /// <summary>
        /// Prepend the specified dataItem at the beginning of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public void Prepend(T dataItem)
        {
            DLinkedListNode newNode = new DLinkedListNode(dataItem);

            if (_firstNode == null)
            {
                _firstNode = _lastNode = newNode;
            }
            else
            {
                var currentNode = _firstNode;
                newNode.Next = currentNode;
                currentNode.Previous = newNode;
                _firstNode = newNode;
            }

            // Increment the count.
            ++Count;
        }


        /// <summary>
        /// Append the specified dataItem at the end of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public void Append(T dataItem)
        {
            DLinkedListNode newNode = new DLinkedListNode(dataItem);

            if (_firstNode == null)
            {
                _firstNode = _lastNode = newNode;
            }
            else
            {
                if (_lastNode == null)
                {
                    UpdateLastNode();
                }

                var currentNode = _lastNode;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;
                _lastNode = newNode;
            }

            // Increment the count.
            ++Count;
        }


        /// <summary>
        /// Inserts the dataItem at the specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public void InsertAt(T dataItem, int index)
        {
            if (index == 0)
            {
                Prepend(dataItem);
            }
            else if (index == Count)
            {
                Append(dataItem);
            }
            else if (index > 0 && index < Count)
            {
                DLinkedListNode currentNode = null;
                DLinkedListNode newNode = new DLinkedListNode(dataItem);

                // Decide from which reference to traverse the list, and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                if (index > (Count / 2))
                {
                    currentNode = this._lastNode;
                    for (int i = (Count - 1); i > index - 1; --i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;
                    for (int i = 0; i < index - 1; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;

                // Increment the count
                ++Count;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        /// <summary>
        /// Inserts the dataItem after specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public void InsertAfter(T dataItem, int index)
        {
            if (index == (Count - 1))
            {
                Append(dataItem);
            }
            else if (index >= 0 && index < Count)
            {
                DLinkedListNode currentNode = null;
                DLinkedListNode newNode = new DLinkedListNode(dataItem);

                // Decide from which reference to traverse the list, and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                if (index > (Count / 2))
                {
                    currentNode = this._lastNode;
                    for (int i = (Count - 1); i > index; --i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;
                    for (int i = 0; i < index; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;

                // Increment the count
                ++Count;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <returns>True if removed successfully, false otherwise.</returns>
        /// <param name="index">Index of item.</param>
        public void RemoveAt(int index)
        {
            // Handle index out of bound errors
            if (index >= Count || Count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            // Remove
            if (index == 0)
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                {
                    _firstNode.Previous = null;
                }

                // Decrement count.
                --Count;
            }
            else
            {
                int i = 0;
                var currentNode = _firstNode;
                while (currentNode.Next != null)
                {
                    if (i + 1 == index)
                    {
                        if (currentNode.Next.Next != null)
                        {
                            (currentNode.Next.Next).Previous = currentNode;
                        }

                        currentNode.Next = currentNode.Next.Next;

                        if (index == (Count - 1))
                        {
                            _lastNode = null;
                        }

                        // Decrement count
                        --Count;

                        break;
                    }//end-if

                    currentNode = currentNode.Next;
                }//end-while
            }//end-else
        }


        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _firstNode = _lastNode = null;
            Count = 0;
        }


        /// <summary>
        /// Get the element at the specified index
        /// </summary>
        /// <param name="index">Index of element</param>
        /// <returns>Element</returns>
        public T GetAt(int index)
        {
            if (index == 0)
            {
                return First;
            }
            else if (index == (Count - 1))
            {
                return Last;
            }
            else if (index > 0 && index < (Count - 1))
            {
                DLinkedListNode currentNode = null;

                // Decide from which reference to traverse the list, and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                if (index > (Count / 2))
                {
                    currentNode = this._lastNode;
                    for (int i = (Count - 1); i > index; --i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;
                    for (int i = 0; i < index; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                return currentNode.Data;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        /// <summary>
        /// Returns a number of elements as specified by countOfElements, starting from the specified index.
        /// </summary>
        /// <param name="index">Starting index.</param>
        /// <param name="countOfElements">The number of elements to return.</param>
        /// <returns>Doubly-Linked List of elements</returns>
        public DLinkedList<T> GetRange(int index, int countOfElements)
        {
            DLinkedListNode currentNode = null;
            DLinkedList<T> newList = new DLinkedList<T>();

            // Handle Index out of Bound errors
            if (Count == 0)
            {
                return newList;
            }
            else if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException();
            }

            // Decide from which reference to traverse the list, and then move the currentNode reference to the index
            // If index > half then traverse it from the end (_lastNode reference)
            // Otherwise, traverse it from the beginning (_firstNode refrence)
            if (index > (Count / 2))
            {
                currentNode = this._lastNode;
                for (int i = (Count - 1); i > index; --i)
                {
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                currentNode = this._firstNode;
                for (int i = 0; i < index; ++i)
                {
                    currentNode = currentNode.Next;
                }
            }

            // Append the elements to the new list using the currentNode reference
            while (currentNode != null && newList.Count <= countOfElements)
            {
                newList.Append(currentNode.Data);
                currentNode = currentNode.Next;
            }

            return newList;
        }


        /// <summary>
        /// Return an array version of this list.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];

            var currentNode = _firstNode;
            for (int i = 0; i < Count; ++i)
            {
                if (currentNode != null)
                {
                    array[i] = currentNode.Data;
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            return array;
        }


        /// <summary>
        /// Returns a System.List version of this DLList instace.
        /// </summary>
        /// <returns>System.List of elements</returns>
        public List<T> ToList()
        {
            List<T> list = new List<T>(Count);

            var currentNode = _firstNode;
            for (int i = 0; i < Count; ++i)
            {
                if (currentNode != null)
                {
                    list.Add(currentNode.Data);
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            return list;
        }


        /// <summary>
        /// Returns the list items as a readable multi--line string.
        /// </summary>
        /// <returns></returns>
        public string ToReadable()
        {
            string listAsString = string.Empty;
            int i = 0;
            var currentNode = _firstNode;

            while (currentNode != null)
            {
                listAsString = String.Format("{0}[{1}] => {2}\r\n", listAsString, i, currentNode.Data);
                currentNode = currentNode.Next;
                ++i;
            }

            return listAsString;
        }

    }

}
