using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGrid : MonoBehaviour {

    private int offsetX = DungeonRoom.RoomXSize + 1;
    private int offsetY = DungeonRoom.RoomYSize + 1;

    public GameObject dungeonRoomGO;

    public void StartPlacing(DungeonRoom[] dungeonRooms, int[,] dungeonMap) {
        for (int x = 0; x < DungeonMapGenerator.DUNGEON_WIDTH; x++){
            for (int y = 0; y < DungeonMapGenerator.DUNGEON_HEIGHT; y++){
                if (dungeonMap[x, y] > 0){
                    //Debug.Log("Placé en (" + x + ", " + y + ");");
                    GameObject dungeonRoomGOTemp = Instantiate(dungeonRoomGO);
                    dungeonRoomGOTemp.GetComponent<DungeonRoomDisplayer>().PlaceWalls(GetRoomAt(dungeonRooms, x, y));
                    Instantiate(dungeonRoomGOTemp, new Vector3(x + x * offsetX, y + y * offsetY, 0), Quaternion.identity);
                }
            }
        }
    }

    public DungeonRoom GetRoomAt(DungeonRoom[] dungeonRooms, int x, int y){
        for (int i = 0; i < dungeonRooms.Length; i++){
            Debug.Log("Dungeon X : " + dungeonRooms[i].roomX + " | Dungeon Y : "+ dungeonRooms[i].roomY + " | true X : " + x + " | true Y : " + y);
            if (dungeonRooms[i].roomX == x &&
                dungeonRooms[i].roomY == y){
                    Debug.Log("Room returned");
                    return dungeonRooms[i];
            }
        }
        Debug.Log("Room not returned");
        return null;
    }

}