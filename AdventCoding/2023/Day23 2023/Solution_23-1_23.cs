#pragma warning disable CS8602

using System.Drawing;

public class Solution_23_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        char[][] maze = Input_23_23.input
                                   .Split('\n')
                                   .Select(line => line.ToCharArray())
                                   .ToArray();

        int mazeHeight = maze.Length;
        int mazeWidth = maze[0].Length;

        var target = new Point(mazeWidth-2, mazeHeight-1);

        var pathStack = new Stack<List<Point>>();
        pathStack.Push(new List<Point> {new(1,0), new(1,1)});

        int maxDistance = 0;


        while (pathStack.Count > 0) {
            
            Console.Write($"\rStacksize: {pathStack.Count}");

            var currentPath = pathStack.Pop();

            var nextMoves = GetNextPossibleMoves(maze, currentPath);

            while (true) {

                if (currentPath.Last() == target) {
                    var distance = currentPath.Count-1;
                    if (distance > maxDistance)
                        maxDistance = distance;
                    break;
                }

                while (nextMoves.Count == 1) {
                    currentPath.Add(nextMoves[0]);

                    if (currentPath.Last() == target) {
                        var distance = currentPath.Count-1;
                        if (distance > maxDistance)
                            maxDistance = distance;
                        break;
                    }

                    nextMoves = GetNextPossibleMoves(maze, currentPath);
                }

                if (currentPath.Last() == target) {
                    var distance = currentPath.Count-1;
                    if (distance > maxDistance)
                        maxDistance = distance;
                    break;
                }

                if (nextMoves.Count == 0) {
                    break;
                }

                if (nextMoves.Count > 1) {
                    
                    for (int i=1; i<nextMoves.Count; i++) {
                        pathStack.Push(currentPath.Append(nextMoves[i]).ToList());
                    }

                    currentPath.Add(nextMoves[0]);

                    if (currentPath.Last() == target) {
                        var distance = currentPath.Count-1;
                        if (distance > maxDistance)
                            maxDistance = distance;
                        break;
                    }

                    nextMoves = GetNextPossibleMoves(maze, currentPath);
                }

                if (currentPath.Last() == target) {
                    var distance = currentPath.Count-1;
                    if (distance > maxDistance)
                        maxDistance = distance;
                    break;
                }
            }
        }
 
        Console.WriteLine($"Done! steps: {maxDistance}");
    }

    private List<Point> GetNextPossibleMoves(char[][] maze, List<Point> path) {

        var currentPosition = path[^1];

        var mazeChar = maze[currentPosition.Y][currentPosition.X];

        if (mazeChar == '>') {
            var nextPotentialPosition = new Point(currentPosition.X+1, currentPosition.Y);
            if (path.Contains(nextPotentialPosition) || !CanStepOn(maze, nextPotentialPosition))
                return new List<Point>();
            else 
                return new List<Point> { nextPotentialPosition };
        }

        if (mazeChar == '<') {
            var nextPotentialPosition = new Point(currentPosition.X-1, currentPosition.Y);
            if (path.Contains(nextPotentialPosition) || !CanStepOn(maze, nextPotentialPosition))
                return new List<Point>();
            else 
                return new List<Point> { nextPotentialPosition };
        }

        if (mazeChar == 'v') {
            var nextPotentialPosition = new Point(currentPosition.X, currentPosition.Y+1);
            if (path.Contains(nextPotentialPosition) || !CanStepOn(maze, nextPotentialPosition))
                return new List<Point>();
            else 
                return new List<Point> { nextPotentialPosition };
        }

        if (mazeChar == '^') {
            var nextPotentialPosition = new Point(currentPosition.X, currentPosition.Y-1);
            if (path.Contains(nextPotentialPosition) || !CanStepOn(maze, nextPotentialPosition))
                return new List<Point>();
            else 
                return new List<Point> { nextPotentialPosition };
        }

        var resultList = new List<Point>();

        var potentialDownMove = new Point(currentPosition.X, currentPosition.Y+1);
        if (!path.Contains(potentialDownMove) && CanStepOn(maze, potentialDownMove))
            resultList.Add(potentialDownMove);
        
        var potentialUpMove = new Point(currentPosition.X, currentPosition.Y-1);
        if (!path.Contains(potentialUpMove) && CanStepOn(maze, potentialUpMove))
            resultList.Add(potentialUpMove);

        var potentialLeftMove = new Point(currentPosition.X-1, currentPosition.Y);
        if (!path.Contains(potentialLeftMove) && CanStepOn(maze, potentialLeftMove))
            resultList.Add(potentialLeftMove);

        var potentialRightMove = new Point(currentPosition.X+1, currentPosition.Y);
        if (!path.Contains(potentialRightMove) && CanStepOn(maze, potentialRightMove))
            resultList.Add(potentialRightMove);

        return resultList;
    }

    private bool CanStepOn(char[][] maze, Point position) {
        return maze[position.Y][position.X] != '#';
    }


}