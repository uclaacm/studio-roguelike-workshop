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

            // if the room is not empty, ignotre it
            if (!map.Get(current.x, current.y).IsEmpty)
            {
                continue;
            }

            // Save neighboring positions:
            List<Vector2Int> selectedNeighbors = new();

            // shuffle the neighbor offsets
            ShuffleList(neighborOffsets);

            foreach (Vector2Int offset in neighborOffsets)
            {
                Vector2Int neighbor = current + offset;

                // if the neighbor is not in bounds or
                // is not empty, disregard it
                if (
                    !map.InBounds(neighbor.x, neighbor.y)
                    || !map.Get(neighbor.x, neighbor.y).IsEmpty
                )
                {
                    continue;
                }

                // count the number of filled rooms are surrounding the neighbor
                int neighboringRooms = 0;
                foreach (Vector2Int neighborOffset in neighborOffsets)
                {
                    Vector2Int neighborNeighbor = neighbor + neighborOffset;

                    if (
                        map.InBounds(neighborNeighbor.x, neighborNeighbor.y)
                        && !map.Get(neighborNeighbor.x, neighborNeighbor.y).IsEmpty
                    )
                    {
                        neighboringRooms++;
                    }
                }
                if (neighboringRooms > 1)
                {
                    continue;
                }
                selectedNeighbors.Add(neighbor);
            }

            // of the available neighbors,
            // add a random number of them to the stack (at least 1)
            int neighborsToAdd = Random.Range(1, selectedNeighbors.Count + 1);
            for(int i = 0; i < neighborsToAdd; i++)
            {
                stack.Push(selectedNeighbors[i]);
            }

            // if we are at the last room,
            // make it the boss room
            bool spawnBoss = false;
            if (numRooms >= maxRooms - 1)
            {
                spawnBoss = true;
            }

            // if we are at the minimum number of rooms,
            // stop generating rooms with a random chance
            if (numRooms >= minRooms - 1 && Random.value < failChance)
            {
                spawnBoss = true;
            }

            if (spawnBoss)
            {
                map.Get(current.x, current.y).Type = MapEntryType.BossRoom;
                // the boss is the last room, so stop iterating
                break;
            }

            MapEntryType roomType = MapEntryType.NormalRoom;
            if (current == startCell)
            {
                roomType = MapEntryType.StartRoom;
            }
            else if (Random.value < itemChance)
            {
                roomType = MapEntryType.ItemRoom;
            }
            map.Get(current.x, current.y).Type = roomType;
            numRooms++;
        }
        return map;
    }
}