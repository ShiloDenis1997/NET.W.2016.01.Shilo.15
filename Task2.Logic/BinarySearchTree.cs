using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public class BinarySearchTree<T> : ICollection<T>
    {
        public int Count { get; private set; }
        public virtual bool IsReadOnly => false;

        private readonly IComparer<T> comparer;
        private Node<T> root;

        public BinarySearchTree(IComparer<T> comparer = null)
        {
            root = null;
            Count = 0;
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public BinarySearchTree(Comparison<T> comparison)
            : this(Comparer<T>.Create(comparison))
        {
        }

        public BinarySearchTree(IEnumerable<T> items, IComparer<T> comparer = null)
            : this(comparer)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public bool Add(T item)
        {
            if (ReferenceEquals(root, null))
            {
                root = new Node<T> {Value = item};
                Count++;
                return true;
            }
            Node<T> unusedParent;
            Node<T> parentNode = FindItem(item, out unusedParent);
            int compareResult = comparer.Compare(item, parentNode.Value);
            if (compareResult == 0)
                return false;
            Node<T> newNode = new Node<T> { Value = item };
            if (compareResult > 0)
            {
                parentNode.Right = newNode;
            }
            else
            {
                parentNode.Left = newNode;
            }
            Count++;
            return true;
        }

        public void Clear()
        {
            Count = 0;
            root = null;
        }

        public bool Contains(T item)
        {
            Node<T> unusedParent;
            Node<T> foundedNode = FindItem(item, out unusedParent);
            if (comparer.Compare(foundedNode.Value, item) == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Copies elements to array in order that <see cref="GetEnumerator"/> does
        /// </summary>
        /// <param name="array">destination array</param>
        /// <param name="arrayIndex">start destination index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException($"{nameof(array)} is null");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException
                    ($"{nameof(arrayIndex)} is less than zero");
            if (arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException
                    ($"{nameof(arrayIndex)} is greater than {nameof(array)} size");
            if (Count > array.Length - arrayIndex)
                throw new ArgumentOutOfRangeException
                    ($"There are not enought space in {nameof(array)} " +
                     $"starting from {nameof(arrayIndex)}");
            foreach (var item in this)
                array[arrayIndex++] = item;
        }

        public bool Remove(T item)
        {
            if (root == null)
                return false;
            Node<T> parentNode;
            Node<T> foundedNode = FindItem(item, out parentNode);
            if (comparer.Compare(foundedNode.Value, item) != 0)
                return false;
            //if removing node has not left son
            if (ReferenceEquals(foundedNode.Left, null))
            {
                //if removing node is a root
                if (ReferenceEquals(foundedNode, root))
                {
                    root = foundedNode.Right;
                    Count--;
                    return true;
                }
                if (ReferenceEquals(foundedNode, parentNode.Left))
                {
                    parentNode.Left = foundedNode.Right;
                }
                else
                {
                    parentNode.Right = foundedNode.Right;
                }
                Count--;
                return true;
            }
            //if removing node has not right son
            if (ReferenceEquals(foundedNode.Right, null))
            {
                if (ReferenceEquals(foundedNode, root))
                {
                    root = foundedNode.Left;
                    Count--;
                    return true;
                }
                if (foundedNode == parentNode.Left)
                {
                    parentNode.Left = foundedNode.Left;
                }
                else
                {
                    parentNode.Right = foundedNode.Left;
                }
                Count--;
                return true;
            }
            Node<T> currentNodeToSwap = foundedNode.Right;
            Node<T> parentSwapNode = foundedNode;
            while (!ReferenceEquals(currentNodeToSwap.Left, null))
            {
                parentSwapNode = currentNodeToSwap;
                currentNodeToSwap = currentNodeToSwap.Left;
            }
            if (ReferenceEquals(parentNode, null))
            {
                root = currentNodeToSwap;
            }
            else if (ReferenceEquals(parentNode.Left, foundedNode))
            {
                parentNode.Left = currentNodeToSwap;
            }
            else
            {
                parentNode.Right = currentNodeToSwap;
            }
            if (ReferenceEquals(parentSwapNode.Left, currentNodeToSwap))
            {
                parentSwapNode.Left = currentNodeToSwap.Right;
            }
            else
            {
                parentSwapNode.Right = currentNodeToSwap.Right;
            }
            currentNodeToSwap.Left = foundedNode.Left;
            currentNodeToSwap.Right = foundedNode.Right;
            Count--;
            return true;
        }


        public IEnumerator<T> GetPreorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            Stack<Node<T>> nodesStack = new Stack<Node<T>>(new[] {root});
            while (nodesStack.Count != 0)
            {
                Node<T> current = nodesStack.Pop();
                yield return current.Value;
                if (!ReferenceEquals(current.Right, null))
                    nodesStack.Push(current.Right);
                if (!ReferenceEquals(current.Left, null))
                    nodesStack.Push(current.Left);
            }
        }

        public IEnumerator<T> GetInorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            Stack<Node<T>> leftStack = new Stack<Node<T>>(new[] {root});
            Stack<Node<T>> centerStack = new Stack<Node<T>>();
            Stack<Node<T>> rightStack = new Stack<Node<T>>();
            while (true)
            {
                Node<T> current;
                if (leftStack.Count != 0)
                {
                    current = leftStack.Pop();
                    if (!ReferenceEquals(current.Left, null))
                    {
                        leftStack.Push(current.Left);
                        centerStack.Push(current);
                    }
                    else
                    {
                        yield return current.Value;
                        if (!ReferenceEquals(current.Right, null))
                            rightStack.Push(current.Right);
                    }
                }
                else if (rightStack.Count != 0)
                {
                    current = rightStack.Pop();
                    if (!ReferenceEquals(current.Left, null))
                    {
                        centerStack.Push(current);
                        leftStack.Push(current.Left);
                    }
                    else
                    {
                        yield return current.Value;
                        if (current.Right != null)
                            rightStack.Push(current.Right);
                    }
                }
                else if (centerStack.Count != 0)
                {
                    current = centerStack.Pop();
                    yield return current.Value;
                    if (current.Right != null)
                        rightStack.Push(current.Right);
                }
                else
                    break;
            }
        }

        public IEnumerator<T> GetPostorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            Stack<Node<T>> nodesStack = new Stack<Node<T>>(new[] {root});
            Stack<Node<T>> result = new Stack<Node<T>>();
            while (nodesStack.Count != 0)
            {
                Node<T> current = nodesStack.Pop();
                result.Push(current);
                if (!ReferenceEquals(current.Left, null))
                    nodesStack.Push(current.Left);
                if (!ReferenceEquals(current.Right, null))
                    nodesStack.Push(current.Right);
            }
            while (result.Count != 0)
                yield return result.Pop().Value;
        }

        /// <summary>
        /// GetEnumerator enumerates tree in preorder order
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return GetPreorderEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        private Node<T> FindItem(T item, out Node<T> parent)
        {
            parent = null;
            if (ReferenceEquals(root, null))
                return root;
            Node<T> current = root;
            int compareResult;
            try
            {
                compareResult = comparer.Compare(item, current.Value);
            }
            catch (Exception ex)
            {
                throw new BinarySearchTreeException("Cannot compare two items", ex);
            }
            while (true)
            {
                if (compareResult == 0)
                    return current;
                if (compareResult > 0)
                {
                    if (ReferenceEquals(current.Right, null))
                        return current;
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    if (ReferenceEquals(current.Left, null))
                        return current;
                    parent = current;
                    current = current.Left;
                }
                compareResult = comparer.Compare(item, current.Value);
            }
        }

        /// <summary>
        /// Node of the tree
        /// </summary>
        /// <typeparam name="TValue">Type of value which contains in node</typeparam>
        protected class Node<TValue>
        {
            public TValue Value { get; set; }
            public Node<TValue> Left { get; set; }
            public Node<TValue> Right { get; set; }
        }
    }
}
