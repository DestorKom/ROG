using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditorInternal.VersionControl.ListControl;

public class RoomPlacer : MonoBehaviour
{

    public Room[] RoomPrefabs;
    public Room StartingRoom;

    private Room[,] spawnedRooms;

    private void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        int k = Random.Range(6, 12);
        for (int i = 0; i <k; i++)
        {

            PlaceOneRoom(k-i);
        }
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] != null)
                {

                    if (x > 0 && spawnedRooms[x - 1, y] != null)
                        spawnedRooms[x, y].DoorL.active = true;
                    if (y > 0 && spawnedRooms[x, y - 1] != null)
                        spawnedRooms[x, y].DoorD.active = true;
                    if (x < maxX && spawnedRooms[x + 1, y] != null)
                        spawnedRooms[x, y].DoorR.active = true;
                    if (y < maxY && spawnedRooms[x, y + 1] != null)
                        spawnedRooms[x, y].DoorU.active = true;

                }
            }
        }


    }
    

    

    private void PlaceOneRoom(int i)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
               
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }
      
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        if (i == 1)
            newRoom.tag = "Boss";
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector3(position.x, position.y, 0) * 50;
        spawnedRooms[position.x, position.y] = newRoom;

    }

    


}
