using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGrid : MonoBehaviour {

    private int offsetX = DungeonRoom.RoomXSize + 1;
    private int offsetY = DungeonRoom.RoomYSize + 1;

    public GameObject dungeonRoomGO;

    public void StartPlacing(DungeonRoom[] dungeonRooms, int[,] dungeonMap) {/*
        for (int x = 0; x < DungeonMapGenerator.DUNGEON_HEIGHT; x++){
            for (int y = 0; y < DungeonMapGenerator.DUNGEON_WIDTH; y++){
                if (dungeonMap[x, y] > 0){
                    //Debug.Log("Placé en (" + x + ", " + y + ");");
                    GameObject dungeonRoomGOTemp = Instantiate(dungeonRoomGO);
                    dungeonRoomGOTemp.GetComponent<DungeonRoomDisplayer>().PlaceWalls(GetRoomAt(dungeonRooms, x, y));
                    Instantiate(dungeonRoomGOTemp, new Vector3(y + y * offsetX, x + x * offsetY, 0), Quaternion.identity);
                }
            }
        }*/
        GameObject dungeonRoomGOTemp = Instantiate(dungeonRoomGO);
        Instantiate(dungeonRoomGOTemp, new Vector3(0 + 0 * offsetX, 0, 0), Quaternion.identity);
        Instantiate(dungeonRoomGOTemp, new Vector3(2 + 2 * offsetX, 0, 0), Quaternion.identity);
        Instantiate(dungeonRoomGOTemp, new Vector3(3 + 3 * offsetX, 0, 0), Quaternion.identity);
        Instantiate(dungeonRoomGOTemp, new Vector3(3 + 3 * offsetX, 1 * offsetY, 0), Quaternion.identity);
    }

    public DungeonRoom GetRoomAt(DungeonRoom[] dungeonRooms, int x, int y){
        for (int i = 0; i < dungeonRooms.Length; i++){
            //Debug.Log("Dungeon X : " + dungeonRooms[i].roomX + " | Dungeon Y : "+ dungeonRooms[i].roomY + " | true X : " + x + " | true Y : " + y);
            if (dungeonRooms[i].roomX == x &&
                dungeonRooms[i].roomY == y){
                    return dungeonRooms[i];
            }
        }
        return null;
    }

}