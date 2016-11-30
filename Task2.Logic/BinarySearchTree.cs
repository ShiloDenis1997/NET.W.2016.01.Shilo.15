using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Class provides functionallity of binary search tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BinarySearchTree<T> : ICollection<T>
    {
        /// <summary>
        /// Count of elements 
        /// </summary>
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        /// <summary>
        /// Custom comparer can be provided
        /// </summary>
        private readonly IComparer<T> comparer;

        /// <summary>
        /// Root of the tree
        /// </summary>
        private Node<T> root;

        /// <summary>
        /// Initializes new instance of <see cref="BinarySearchTree{T}"/>
        /// </summary>
        /// <param name="comparer">If not specified, default comparer
        /// will be used</param>
        /// <exception cref="BinarySearchTreeException">Throws if type <see cref="T"/>
        /// neither implements <see cref="IComparable{T}"/> 
        /// nor <see cref="IComparable"/></exception>
        public BinarySearchTree(IComparer<T> comparer = null)
        {
            if (ReferenceEquals(comparer, null))
                ValidateType();
            root = null;
            Count = 0;
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Initializes new instance of <see cref="BinarySearchTree{T}"/>
        /// </summary>
        /// <param name="comparison"></param>
        /// <exception cref="ArgumentNullException">Throws if comparison is null
        /// </exception>
        public BinarySearchTree(Comparison<T> comparison)
            : this(Comparer<T>.Create(comparison))
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="BinarySearchTree{T}"/>
        /// with specified <paramref name="items"/>
        /// </summary>
        /// <param name="comparer">If not specified, default comparer
        /// will be used</param>
        /// <param name="items">Items for binary tree</param>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="items"/>
        /// parameter is null</exception>
        /// <exception cref="BinarySearchTreeException">Throws if type <see cref="T"/>
        /// neither implements <see cref="IComparable{T}"/> 
        /// nor <see cref="IComparable"/> and <paramref name="comparer"/> is null</exception>
        public BinarySearchTree(IEnumerable<T> items, IComparer<T> comparer = null)
            : this(comparer)
        {
            if (ReferenceEquals(items, null))
                throw new ArgumentNullException($"{nameof(items)} is null");
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Adds new item to current <see cref="BinarySearchTree{T}"/>
        /// </summary>
        /// <param name="item">item to add</param>
        public void Add(T item)
        {
            if (ReferenceEquals(root, null))
            {
                root = new Node<T> {Value = item};
                Count++;
                return;
            }
            Node<T> unusedParent;
            Node<T> parentNode = FindPotentialParent(item, out unusedParent);
            int compareResult = comparer.Compare(item, parentNode.Value);
            Node<T> newNode = new Node<T> { Value = item };
            if (compareResult >= 0)
            {
                parentNode.Right = newNode;
            }
            else
            {
                parentNode.Left = newNode;
            }
            Count++;
        }

        /// <summary>
        /// Removes all elements from current tree
        /// </summary>
        public void Clear()
        {
            Count = 0;
            root = null;
        }

        /// <summary>
        /// Checks if <paramref name="item"/> is in current tree
        /// </summary>
        /// <param name="item">item to search</param>
        /// <returns>true if <paramref name="item"/> is in current tree,
        /// otherwise returns false</returns>
        public bool Contains(T item)
        {
            if (ReferenceEquals(root, null))
                return false;
            Node<T> unusedParent;
            Node<T> founded = FindItem(item, out unusedParent);
            if (ReferenceEquals(founded, null))
                return false;
            return true;
        }

        /// <summary>
        /// Copies elements to array in order that <see cref="GetEnumerator"/> does
        /// </summary>
        /// <param name="array">destination array</param>
        /// <param name="arrayIndex">start destination index</param>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="array"/>
        /// is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if 
        /// <paramref name="arrayIndex"/> is out of range</exception>
        /// <exception cref="ArgumentException">Throws if there are no space in
        /// <paramref name="array"/> to keep current tree starting from 
        /// <paramref name="arrayIndex"/></exception>
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
                throw new ArgumentException
                    ($"There are not enought space in {nameof(array)} " +
                     $"starting from {nameof(arrayIndex)}");
            foreach (var item in this)
                array[arrayIndex++] = item;
        }

        /// <summary>
        /// Trying to remove <paramref name="item"/> from the current tree
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if <paramref name="item"/> successfully removed from 
        /// the current tree. Otherwise returns false</returns>
        public bool Remove(T item)
        {
            if (root == null)
                return false;
            Node<T> parentNode;
            Node<T> foundedNode = FindItem(item, out parentNode);
            if (ReferenceEquals(foundedNode, null))
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
                if (ReferenceEquals(foundedNode, parentNode.Left))
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
            if (ReferenceEquals(root, foundedNode))
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
                parentSwapNode.Left = currentNodeToSwap.Right;
            else
                parentSwapNode.Right = currentNodeToSwap.Right;
            currentNodeToSwap.Left = foundedNode.Left;
            currentNodeToSwap.Right = foundedNode.Right;
            
            Count--;
            return true;
        }

        /// <summary>
        /// Returns an enumerator to enumerate tree elements in
        /// preorder order
        /// </summary>
        public IEnumerator<T> GetPreorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            foreach (var item in GetPreorderEnumerator(root))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns an enumerator to enumerate tree elements in
        /// inorder order
        /// </summary>
        public IEnumerator<T> GetInorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            foreach (var item in GetInorderEnumerator(root))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns an enumerator to enumerate tree elements in
        /// postorder order
        /// </summary>
        public IEnumerator<T> GetPostorderEnumerator()
        {
            if (ReferenceEquals(root, null))
                yield break;
            foreach (var item in GetPostorderEnumerator(root))
            {
                yield return item;
            }
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

        /// <summary>
        /// Finds potential parent
        /// of <paramref name="item"/>
        /// </summary>
        /// <param name="item">item to find</param>
        /// <param name="parent">parent of the founded item</param>
        /// <returns>Reference to founded parent</returns>
        private Node<T> FindPotentialParent(T item, out Node<T> parent)
        {
            parent = null;
            if (ReferenceEquals(root, null))
                return root;
            Node<T> current = root;
            int compareResult = comparer.Compare(item, current.Value);
            while (true)
            {
                if (compareResult >= 0)
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
        /// Finds a node with item
        /// of <paramref name="item"/>
        /// </summary>
        /// <param name="item">item to find</param>
        /// <param name="parent">parent of the founded item</param>
        /// <returns>Reference to founded node or null if not found</returns>
        private Node<T> FindItem(T item, out Node<T> parent)
        {
            parent = null;
            if (ReferenceEquals(root, null))
                return null;
            Node<T> current = root;
            int compareResult = comparer.Compare(item, current.Value);
            while (true)
            {
                if (compareResult == 0)
                    return current;
                if (compareResult > 0)
                {
                    if (ReferenceEquals(current.Right, null))
                        return null;
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    if (ReferenceEquals(current.Left, null))
                        return null;
                    parent = current;
                    current = current.Left;
                }
                compareResult = comparer.Compare(item, current.Value);
            }
        }

        /// <summary>
        /// Returns enumerator which enumerates elements of the tree in preorder order
        /// </summary>
        /// <param name="current">Must be not null</param>
        private IEnumerable<T> GetPreorderEnumerator(Node<T> current)
        {
            yield return current.Value;
            if (!ReferenceEquals(current.Left, null))
                foreach (var item in GetPreorderEnumerator(current.Left))
                {
                    yield return item;
                }
            if (!ReferenceEquals(current.Right, null))
                foreach (var item in GetPreorderEnumerator(current.Right))
                {
                    yield return item;
                }
        }

        /// <summary>
        /// Returns enumerator which enumerates elements of the tree in inorder order
        /// </summary>
        /// <param name="current">Must be not null</param>
        private IEnumerable<T> GetInorderEnumerator(Node<T> current)
        {
            if (!ReferenceEquals(current.Left, null))
                foreach (var item in GetInorderEnumerator(current.Left))
                {
                    yield return item;
                }
            yield return current.Value;
            if (!ReferenceEquals(current.Right, null))
                foreach (var item in GetInorderEnumerator(current.Right))
                {
                    yield return item;
                }
        }

        /// <summary>
        /// Returns enumerator which enumerates elements of the tree in postorder order
        /// </summary>
        /// <param name="current">Must be not null</param>
        private IEnumerable<T> GetPostorderEnumerator(Node<T> current)
        {
            if (!ReferenceEquals(current.Left, null))
                foreach (var item in GetPostorderEnumerator(current.Left))
                {
                    yield return item;
                }
            if (!ReferenceEquals(current.Right, null))
                foreach (var item in GetPostorderEnumerator(current.Right))
                {
                    yield return item;
                }
            yield return current.Value;
        }

        /// <summary>
        /// Validates if elements of type <see cref="T"/> can be compared
        /// </summary>
        /// <exception cref="BinarySearchTreeException">Throws if type <see cref="T"/>
        /// neither implements <see cref="IComparable{T}"/> 
        /// nor <see cref="IComparable"/></exception>
        private void ValidateType()
        {
            Type[] interfaces = typeof(T).GetInterfaces();
            if (!interfaces.Contains(typeof(IComparable<T>)) && !interfaces.Contains(typeof(IComparable)))
                throw new BinarySearchTreeException
                    ($"Type {nameof(T)} does not implements IComparable or IComparable<{nameof(T)}>");
        }

        /// <summary>
        /// Node of the tree
        /// </summary>
        /// <typeparam name="TValue">Type of value which contains in node</typeparam>
        private class Node<TValue>
        {
            public TValue Value { get; set; }
            public Node<TValue> Left { get; set; }
            public Node<TValue> Right { get; set; }
        }
    }
}
