/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // TODO Problem 4 - ADD YOUR CODE HERE
    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        // FILL IN CODE
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        // FILL IN CODE
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        // FILL IN CODE
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        // FILL IN CODE
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }

    public static (int, int) MoveLeft((int, int) position, Dictionary<(int, int), (bool left, bool right, bool up, bool down)> maze)
    {
        if (maze == null || !maze.ContainsKey(position)) return position;
        if (maze[position].left) return (position.Item1 - 1, position.Item2);
        return position;
    }

    public static (int, int) MoveRight((int, int) position, Dictionary<(int, int), (bool left, bool right, bool up, bool down)> maze)
    {
        if (maze == null || !maze.ContainsKey(position)) return position;
        if (maze[position].right) return (position.Item1 + 1, position.Item2);
        return position;
    }

    public static (int, int) MoveUp((int, int) position, Dictionary<(int, int), (bool left, bool right, bool up, bool down)> maze)
    {
        if (maze == null || !maze.ContainsKey(position)) return position;
        if (maze[position].up) return (position.Item1, position.Item2 + 1);
        return position;
    }

    public static (int, int) MoveDown((int, int) position, Dictionary<(int, int), (bool left, bool right, bool up, bool down)> maze)
    {
        if (maze == null || !maze.ContainsKey(position)) return position;
        if (maze[position].down) return (position.Item1, position.Item2 - 1);
        return position;
    }
}