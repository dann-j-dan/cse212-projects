using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new Node(value);

        // If the list is empty, set head and tail to the new node and return
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        // Otherwise append after current tail if available (use _tail for O(1))
        if (_tail != null)
        {
            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
            return;
        }

        // Fallback: traverse from head (defensive)
        Node current = _head;
        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode;
        newNode.Prev = current;
        _tail = newNode;
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // Empty list
        if (_head == null)
            return;

        // Single node
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
            return;
        }

        // If tail is available, unlink it
        if (_tail != null)
        {
            var prev = _tail.Prev;
            if (prev != null)
            {
                prev.Next = null;
                _tail = prev;
            }
            else
            {
                // defensive fallback
                _head = null;
                _tail = null;
            }
            return;
        }

        // Fallback: traverse to second-to-last
        Node current = _head;
        while (current.Next != null && current.Next.Next != null)
        {
            current = current.Next;
        }

        current.Next = null;
        _tail = current;
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        if (_head == null)
            return;

        if (_head.Data == value)
        {
            // remove head
            if (_head == _tail)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _head = _head.Next;
                if (_head != null) _head.Prev = null;
            }
            return;
        }

        Node? current = _head;
        while (current != null && current.Next != null)
        {
            if (current.Next.Data == value)
            {
                var toRemove = current.Next;
                current.Next = toRemove.Next;
                if (toRemove.Next != null)
                {
                    toRemove.Next.Prev = current;
                }
                else
                {
                    // removed last node
                    _tail = current;
                }
                return;
            }
            current = current.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? current = _head;
        while (current != null)
        {
            if (current.Data == oldValue)
                current.Data = newValue;
            current = current.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable<int> Reverse()
    {
        var stack = new Stack<int>();
        Node? current = _head;
        while (current != null)
        {
            stack.Push(current.Data);
            current = current.Next;
        }
        while (stack.Count > 0)
            yield return stack.Pop();
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}