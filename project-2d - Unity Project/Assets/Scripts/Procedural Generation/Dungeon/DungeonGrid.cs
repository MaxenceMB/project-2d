using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGrid : MonoBehaviour {

    private int offsetX = DungeonRoom.RoomXSize + 4;
    private int offsetY = DungeonRoom.RoomYSize + 4;

    public GameObject[] dungeonRoomGO;

    public void PlaceAllRooms(DungeonRoom[] dungeonRooms, int[,] dungeonMap) {
        GameObject dungeonRoomGOTemp = null;
        for (int x = 0; x < DungeonMapGenerator.DUNGEON_WIDTH; x++){
            for (int y = 0; y < DungeonMapGenerator.DUNGEON_HEIGHT; y++){
                if (dungeonMap[x, y] > 0){
                    dungeonRoomGOTemp = Instantiate(dungeonRoomGO[0]);
                    dungeonRoomGOTemp.GetComponent<DungeonRoomDisplayer>().PlaceWalls(GetRoomAt(dungeonRooms, x, y));
                    Instantiate(dungeonRoomGOTemp, new Vector3(x * offsetX, y * offsetY, 0), Quaternion.identity);
                }
            }
        }
        DungeonRoom farthestRoom = GetFarthestRoom(dungeonRooms);
        dungeonRoomGOTemp = Instantiate(dungeonRoomGO[1]);
        dungeonRoomGOTemp.GetComponent<DungeonRoomDisplayer>().PlaceWalls(farthestRoom);
        Instantiate(dungeonRoomGOTemp, new Vector3(farthestRoom.roomX * offsetX, farthestRoom.roomY * offsetY, 0), Quaternion.identity);
    }

    public DungeonRoom GetRoomAt(DungeonRoom[] dungeonRooms, int x, int y){
        for (int i = 0; i < dungeonRooms.Length; i++){
            if (dungeonRooms[i].roomX == x &&
                dungeonRooms[i].roomY == y){
                    return dungeonRooms[i];
            }
        }
        return null;
    }

    public DungeonRoom GetFarthestRoom(DungeonRoom[] dungeonRooms){
        int maxDistance = 0;
        DungeonRoom farthestRoom = dungeonRooms[0];
        foreach (DungeonRoom room in dungeonRooms){
            if (room.distanceFromStartingRoom > maxDistance){
                maxDistance = room.distanceFromStartingRoom;
                farthestRoom = room;
            }
        }
        return farthestRoom;
    }
}