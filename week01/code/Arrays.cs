// Arrays.cs
// Implementations for Week 01: Dynamic Arrays
// Includes: MultiplesOf and RotateListRight
// The comments before each method show the step-by-step plan required by the assignment.

using System;
using System.Collections.Generic;

namespace Week01Code
{
    public static class Arrays
    {
        // Part 1: MultiplesOf
        // Plan (written as comments before the implementation):
        // 1. Validate inputs: if count <= 0, return an empty array.
        // 2. Create a result array with length equal to "count".
        // 3. Use a for-loop from i = 0 to count - 1.
        //    - For each index i, compute the (i+1)-th multiple of "start": value = start * (i + 1).
        //    - Store that value in result[i].
        // 4. Return the result array.
        // Example: MultiplesOf(3, 5) => { 3, 6, 9, 12, 15 }
        public static double[] MultiplesOf(double start, int count)
        {
            if (count <= 0)
            {
                // No multiples requested — return an empty array.
                return new double[0];
            }

            double[] result = new double[count];

            for (int i = 0; i < count; i++)
            {
                // i is zero-based, so (i + 1) gives the 1st, 2nd, ... multiple.
                result[i] = start * (i + 1);
            }

            return result;
        }


        // Part 2: RotateListRight
        // Plan (written as comments before the implementation):
        // 1. Handle edge cases:
        //    - If data is null, throw ArgumentNullException.
        //    - If data.Count is 0, just return the list (nothing to rotate).
        // 2. Normalize the amount to rotate by using modulo: amount = amount % data.Count.
        //    - If amount becomes 0 after modulo, the list does not change.
        // 3. Compute the split index: splitIndex = data.Count - amount.
        //    - The last "amount" elements form the tail that will move to the front.
        //    - The first splitIndex elements form the head that will follow the tail.
        // 4. Use GetRange to obtain the two slices: tail = data.GetRange(splitIndex, amount)
        //    and head = data.GetRange(0, splitIndex).
        // 5. Create a new list result, add the tail then the head.
        // 6. Modify the original list in-place by clearing it and AddRange(result).
        // 7. Return the (now-rotated) original list.
        // Notes:
        //  - This approach uses list slicing via GetRange and AddRange as suggested in the assignment.
        //  - The function both modifies the provided list and returns it, which covers common test
        //    expectations (some tests check the returned list, others check that the input list was changed).
        public static List<T> RotateListRight<T>(List<T> data, int amount)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int n = data.Count;
            if (n == 0)
            {
                // Nothing to do for an empty list
                return data;
            }

            // Normalize amount into the range 0..n-1
            amount = amount % n;
            if (amount == 0)
            {
                // Rotating by 0 (or a multiple of n) leaves the list unchanged
                return data;
            }

            int splitIndex = n - amount;

            // GetRange(index, count)
            List<T> tail = data.GetRange(splitIndex, amount);
            List<T> head = data.GetRange(0, splitIndex);

            List<T> result = new List<T>(n);
            result.AddRange(tail);
            result.AddRange(head);

            // Modify the original list in-place so callers see the changed list as well
            data.Clear();
            data.AddRange(result);

            // Return the rotated list (same reference as the input list)
            return data;
        }
    }
}

