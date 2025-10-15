using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0) return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (results == null || letters == null) return;

        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; ++i)
        {
            char c = letters[i];
            if (word.Contains(c)) continue;
            PermutationsChoose(results, letters, size, word + c);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count ways to climb using steps of 1,2,3 with memoization.
    /// The public signature expected by tests is CountWaysToClimb(int s).
    /// </summary>
    public static decimal CountWaysToClimb(int s)
    {
        var memo = new Dictionary<int, decimal>();
        return CountWaysToClimbMemo(s, memo);
    }

    private static decimal CountWaysToClimbMemo(int s, Dictionary<int, decimal> memo)
    {
        if (s < 0) return 0m;
        if (s == 0) return 1m; // one way to stand still / finish
        if (memo.ContainsKey(s)) return memo[s];

        decimal ways = CountWaysToClimbMemo(s - 1, memo)
                     + CountWaysToClimbMemo(s - 2, memo)
                     + CountWaysToClimbMemo(s - 3, memo);

        memo[s] = ways;
        return ways;
    }

    /// <summary>
    /// Problem 4 — Wildcard Binary Patterns
    /// Expand '*' to '0' and '1' recursively and add results.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        if (pattern == null || results == null) return;

        int idx = pattern.IndexOf('*');
        if (idx == -1)
        {
            // add concrete pattern (handles empty string)
            results.Add(pattern);
            return;
        }

        var with0 = pattern.Substring(0, idx) + '0' + pattern.Substring(idx + 1);
        var with1 = pattern.Substring(0, idx) + '1' + pattern.Substring(idx + 1);

        WildcardBinary(with0, results);
        WildcardBinary(with1, results);
    }

    /// <summary>
    /// Problem 5 — Maze
    /// Entry point called by tests: SolveMaze(results, maze)
    /// This uses backtracking to find all paths to the end.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze)
    {
        if (results == null || maze == null) return;

        var currPath = new List<(int x, int y)>();
        SolveMazeRec(results, maze, 0, 0, currPath);
    }

    private static void SolveMazeRec(List<string> results, Maze maze, int x, int y, List<(int x, int y)> currPath)
    {
        // add current position
        currPath.Add((x, y));

        // if end, record and backtrack
        if (maze.IsEnd(x, y))
        {
            // Assumes the helper AsString() extension is provided in the project as used by tests.
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // neighbors: up, down, left, right
        var moves = new (int dx, int dy)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

        foreach (var m in moves)
        {
            int nx = x + m.dx;
            int ny = y + m.dy;
            // NOTE: Maze.IsValidMove expects the path first then coordinates,
            // so pass currPath before nx, ny to match the Maze API.
            if (maze.IsValidMove(currPath, nx, ny))
            {
                SolveMazeRec(results, maze, nx, ny, currPath);
            }
        }

        // backtrack
        currPath.RemoveAt(currPath.Count - 1);
    }
}