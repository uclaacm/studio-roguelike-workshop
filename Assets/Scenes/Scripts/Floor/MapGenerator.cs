using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float FAIL_CHANCE = 0.5f;

    /// <summary>
    /// Generate random map with given parameters. <br></br>
    /// Returns map with between minRooms and maxRooms; failChance determines the chance of stopping early. <br></br>
    /// One garunteed spawn room and boss room, rest are normal or item, determined by itemChance. <br></br>
    /// </summary>
    public static Map RandomMap(int width, int height, int minRooms, int maxRooms, float failChance, float itemChance)
    {
        // Init map, random start point, and stack for BFS:
        Map newMap = new Map(width, height);
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        int numRooms = 0;
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(x, y));
        // BFS:
        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            // Check if room already exists:
            if (!newMap.Get(current.x, current.y).IsEmpty)
            {
                // Debug.Log("Skipping existing room at position (" + current.x + ", " + current.y + ")");
                continue;
            }
            // Save neighboring positions:
            List<Vector2Int> temp = new List<Vector2Int>();
            // Randomize the order which we check neighbors:
            List<Vector2Int> randomizedOffsets = new List<Vector2Int>(NeighborOffsets);
            randomizedOffsets.Shuffle();
            foreach (Vector2Int offset in randomizedOffsets)
            {
                // A valid neighbor is empty and itself has at most one neighbor:
                Vector2Int neighbor = current + offset;
                if (!newMap.InBounds(neighbor.x, neighbor.y) || !newMap.Get(neighbor.x, neighbor.y).IsEmpty)
                {
                    continue;
                }
                int neighboringRooms = 0;
                foreach (Vector2Int neighborOffset in NeighborOffsets)
                {
                    Vector2Int neighborNeighbor = neighbor + neighborOffset;
                    if (newMap.InBounds(neighborNeighbor.x, neighborNeighbor.y) && !newMap.Get(neighborNeighbor.x, neighborNeighbor.y).IsEmpty)
                    {
                        neighboringRooms++;
                    }
                }
                if (neighboringRooms > 1)
                {
                    // Debug.Log("Skipping room at position (" + neighbor.x + ", " + neighbor.y + ") due to too many neighboring rooms");
                    continue;
                }
                temp.Add(neighbor);
            }
            // Remove a random amount of neighbors, the resulting list should have between [1, Temp.Count):
            int numToRemove = Random.Range(0, temp.Count);
            temp.RemoveRange(temp.Count - numToRemove, numToRemove);
            // Add valid neighbors to stack:
            foreach (Vector2Int element in temp)
            {
                stack.Push(element);
            }
            // End requirements (spawn boss room):
            bool spawnBoss = false;
            if (numRooms >= maxRooms - 1)
            {
                // Max room requirement:
                // Debug.Log("Reached maximum number of rooms, spawning boss");
                spawnBoss = true;
            }
            if (numRooms >= minRooms - 1 && Random.value < failChance)
            {
                // Min room requirement:
                // Debug.Log("Spawning boss early due to fail chance");
                spawnBoss = true;
            }

            // (Finally) add room to map:
            bool firstRoom = (numRooms == 0);
            if (firstRoom)
            {
                newMap.Get(current.x, current.y).Type = MapEntryType.StartRoom;
                // Debug.Log("Added start room at position (" + current.x + ", " + current.y + ")");
            }
            else if (spawnBoss)
            {
                newMap.Get(current.x, current.y).Type = MapEntryType.BossRoom;
                // Debug.Log("Added boss room at position (" + current.x + ", " + current.y + ")");
                break;
            }
            else
            {
                MapEntryType type = (Random.value < itemChance) ? MapEntryType.ItemRoom : MapEntryType.NormalRoom;
                newMap.Get(current.x, current.y).Type = type;
                // Debug.Log("Added " + type.ToString() + " room at position (" + current.x + ", " + current.y + ")");
            }
            numRooms++;
        }
        return newMap;
    }


    private static Vector2Int[] NeighborOffsets = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
    };

}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
