using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomPlacer : MonoBehaviour
{
    public GameObject DoorB;
    public GameObject DoorP;
    public GameObject DoorM;
    public GameObject DoorC;
    public GameObject canvas;

    public Room[] RoomPrefabs;
    public Room StartingRoom;

    public Room[,] spawnedRooms;
    public personage pers;
    public float timer;
    private void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        int k = Random.Range(6, 12);
        for (int i = 0; i < k; i++)
        {
            PlaceOneRoom(k - i);
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
                    {

                        if (spawnedRooms[x - 1, y].CompareTag("Boss"))
                        {
                            spawnedRooms[x, y].DoorL = Instantiate(DoorB, new Vector3(x * 50 - 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        }
                        if (spawnedRooms[x - 1, y].CompareTag("Monster"))
                        {
                            spawnedRooms[x, y].DoorL = Instantiate(DoorM, new Vector3(x * 50 - 23.05f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, 90.0f));
                        }
                        if (spawnedRooms[x - 1, y].CompareTag("Prize"))
                        {
                            spawnedRooms[x, y].DoorL = Instantiate(DoorP, new Vector3(x * 50 - 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, 90.0f));
                        }
                        if (spawnedRooms[x - 1, y].CompareTag("Clear"))
                        {
                            spawnedRooms[x, y].DoorL = Instantiate(DoorC, new Vector3(x * 50 - 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, 90.0f));
                        }
                        spawnedRooms[x, y].DoorL.SetActive(true);
                        spawnedRooms[x, y].DoorL.tag = "DoorL";
                    }
                    else
                        spawnedRooms[x, y].DoorL = null;


                    if (y > 0 && spawnedRooms[x, y - 1] != null)
                    {

                        if (spawnedRooms[x, y - 1].CompareTag("Boss"))
                        {
                            spawnedRooms[x, y].DoorD = Instantiate(DoorB, new Vector3(x * 50, y * 50 - 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, 90.0f));
                        }
                        if (spawnedRooms[x, y - 1].CompareTag("Monster"))
                        {
                            spawnedRooms[x, y].DoorD = Instantiate(DoorM, new Vector3(x * 50, y * 50 - 12.6f, -1), Quaternion.Euler(0.0f, 0.0f, 180.0f));
                        }
                        if (spawnedRooms[x, y - 1].CompareTag("Prize"))
                        {
                            spawnedRooms[x, y].DoorD = Instantiate(DoorP, new Vector3(x * 50, y * 50 - 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, 180.0f));
                        }
                        if (spawnedRooms[x, y - 1].CompareTag("Clear"))
                        {
                            spawnedRooms[x, y].DoorD = Instantiate(DoorC, new Vector3(x * 50, y * 50 - 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, 180.0f));
                        }
                        spawnedRooms[x, y].DoorD.SetActive(true);
                        spawnedRooms[x, y].DoorD.tag = "DoorD";
                    }
                    else
                        spawnedRooms[x, y].DoorD = null;
                    if (x < maxX && spawnedRooms[x + 1, y] != null)
                    {

                        if (spawnedRooms[x + 1, y].CompareTag("Boss"))
                        {
                            spawnedRooms[x, y].DoorR = Instantiate(DoorB, new Vector3(x * 50 + 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, 180.0f));
                        }
                        if (spawnedRooms[x + 1, y].CompareTag("Monster"))
                        {
                            spawnedRooms[x, y].DoorR = Instantiate(DoorM, new Vector3(x * 50 + 23.05f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, -90.0f));
                        }
                        if (spawnedRooms[x + 1, y].CompareTag("Prize"))
                        {
                            spawnedRooms[x, y].DoorR = Instantiate(DoorP, new Vector3(x * 50 + 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, -90.0f));
                        }
                        if (spawnedRooms[x + 1, y].CompareTag("Clear"))
                        {
                            spawnedRooms[x, y].DoorR = Instantiate(DoorC, new Vector3(x * 50 + 22.4f, y * 50, -1), Quaternion.Euler(0.0f, 0.0f, -90.0f));
                        }
                        spawnedRooms[x, y].DoorR.SetActive(true);
                        spawnedRooms[x, y].DoorR.tag = "DoorR";
                    }
                    else
                        spawnedRooms[x, y].DoorR = null;
                    if (y < maxY && spawnedRooms[x, y + 1] != null)
                    {

                        if (spawnedRooms[x, y + 1].CompareTag("Boss"))
                        {
                            spawnedRooms[x, y].DoorU = Instantiate(DoorB, new Vector3(x * 50, y * 50 + 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, -90.0f));

                        }
                        if (spawnedRooms[x, y + 1].CompareTag("Monster"))
                        {
                            spawnedRooms[x, y].DoorU = Instantiate(DoorM, new Vector3(x * 50, y * 50 + 12.6f, -1), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        }
                        if (spawnedRooms[x, y + 1].CompareTag("Prize"))
                        {
                            spawnedRooms[x, y].DoorU = Instantiate(DoorP, new Vector3(x * 50, y * 50 + 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        }
                        if (spawnedRooms[x, y + 1].CompareTag("Clear"))
                        {
                            spawnedRooms[x, y].DoorU = Instantiate(DoorC, new Vector3(x * 50, y * 50 + 11.94f, -1), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        }
                        spawnedRooms[x, y].DoorU.SetActive(true);
                        spawnedRooms[x, y].DoorU.tag = "DoorU";

                    }
                    else
                        spawnedRooms[x, y].DoorU = null;

                }
            }
        }
        pers.spawnedRooms = spawnedRooms;

    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            canvas.SetActive(true);
            Time.timeScale = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector3(position.x, position.y, 0) * 50;
        spawnedRooms[position.x, position.y] = newRoom;

        if (i % 3 == 0)
        {
            newRoom.tag = "Prize";
        }
        else
            newRoom.tag = "Monster";
        if (i == 1)
            newRoom.tag = "Boss";



    }



}
