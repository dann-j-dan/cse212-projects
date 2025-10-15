using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    private Node? _root;

    /// <summary>
    /// Insert a new node in the BST.
    /// </summary>
    public void Insert(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_root is null)
        {
            _root = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            _root.Insert(value);
        }
    }

    /// <summary>
    /// Check to see if the tree contains a certain value
    /// </summary>
    /// <param name="value">The value to look for</param>
    /// <returns>true if found, otherwise false</returns>
    public bool Contains(int value)
    {
        return _root != null && _root.Contains(value);
    }

    /// <summary>
    /// Yields all values in the tree
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the BST
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var numbers = new List<int>();
        TraverseForward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }

    /// <summary>
    /// Iterate backward through the BST.
    /// </summary>
    public IEnumerable<int> Reverse()
    {
        var numbers = new List<int>();
        TraverseBackward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseBackward(Node? current, List<int> values)
    {
        if (current is null)
            return;

        // Right first (largest values first)
        TraverseBackward(current.Right, values);
        values.Add(current.Data);
        TraverseBackward(current.Left, values);
    }

    /// <summary>
    /// Get the height of the tree
    /// </summary>
    public int GetHeight()
    {
        if (_root is null)
            return 0;
        return _root.GetHeight();
    }

    public override string ToString()
    {
        return "<Bst>{" + string.Join(", ", this) + "}";
    }

    /// <summary>
    /// Insert middle elements of a sorted list to create a balanced tree.
    /// Placed inside the class as a helper static method.
    /// </summary>
    public static void InsertMiddle(BinarySearchTree tree, List<int> values, int first, int last)
    {
        if (first > last)
            return;

        int mid = (first + last) / 2;
        tree.Insert(values[mid]);

        InsertMiddle(tree, values, first, mid - 1);
        InsertMiddle(tree, values, mid + 1, last);
    }

    /// <summary>
    /// Create a balanced BST from a sorted list/sequence.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(List<int> values)
    {
        var tree = new BinarySearchTree();
        if (values == null || values.Count == 0) return tree;

        InsertMiddle(tree, values, 0, values.Count - 1);
        return tree;
    }

    // Overload: IEnumerable<int>
    public static BinarySearchTree CreateTreeFromSortedList(IEnumerable<int> values)
    {
        if (values == null) return new BinarySearchTree();
        return CreateTreeFromSortedList(values.ToList());
    }

    // Overload: array
    public static BinarySearchTree CreateTreeFromSortedList(int[] values)
    {
        if (values == null) return new BinarySearchTree();
        return CreateTreeFromSortedList((IEnumerable<int>)values);
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}