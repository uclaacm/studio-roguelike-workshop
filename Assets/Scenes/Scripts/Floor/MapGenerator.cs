using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float FAIL_CHANCE = 0.5f;

    static void ShuffleList(List<Vector2Int> l){
        for(int i = l.Count - 1; i >= 1; i--){
            int swap = Random.Range(0, i + 1);
            (l[i], l[swap]) = (l[swap], l[i]);
        }
    }

    /// <summary>
    /// Generate random map with given parameters. <br></br>
    /// Returns map with between minRooms and maxRooms; failChance determines the chance of stopping early. <br></br>
    /// One garunteed spawn room and boss room, rest are normal or item, determined by itemChance. <br></br>
    /// </summary>
    public static Map RandomMap(int width, int height, int minRooms, int maxRooms, float failChance, float itemChance)
    {
        Map map = new(width, height);
        // to get a cell in the map, use map.Get(cell.x, cell.y)
        // to get or set the cell type, use map.Get(cell.x, cell.y).Type
        // eg. map.get(cell.x, cell.y).Type = MapEntryType.NormalRoom;

        List<Vector2Int> neighborOffsets = new()
        {
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0),
        };

        // you can use Vector2Int to represent the location of a cell

        // start from a random cell
        Vector2Int startCell = new(
            Random.Range(0, width),
            Random.Range(0, height)
        );

        // start the BFS from the start room
        // this represents the list of "unexplored rooms"
        Stack<Vector2Int> stack = new();
        stack.Push(startCell);

        int numRooms = 0;
        // while there are unexplored rooms
        while (stack.Count > 0)
        {
            // get the unexplored room
            Vector2Int current = stack.Pop();

            // code here
        }
        return map;
    }
}