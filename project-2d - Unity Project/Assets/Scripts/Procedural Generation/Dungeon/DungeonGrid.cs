using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonGrid : MonoBehaviour {

    private int offsetX = DungeonRoom.X_SIZE + 4;
    private int offsetY = DungeonRoom.Y_SIZE + 4;

    public GameObject[] dungeonRoomGO;

    private GameObject[] placedRooms;
    private int placedRoomsCount = 0;

    public int currentRoom;

    public void PlaceAllRooms(DungeonRoom[] dungeonRooms, int[,] dungeonMap) {
        placedRooms = new GameObject[dungeonRooms.Length];

        for (int i = 0; i < dungeonRooms.Length; i++){
            if (dungeonRooms[i] == GetFarthestRoom(dungeonRooms)){
                placedRooms[placedRoomsCount] = Instantiate(dungeonRoomGO[1]);
            } else {
                placedRooms[placedRoomsCount] = Instantiate(dungeonRoomGO[0]);
            }
            DungeonRoomDisplayer roomDisplayer = placedRooms[placedRoomsCount].GetComponent<DungeonRoomDisplayer>();
            roomDisplayer.room = dungeonRooms[i];
            roomDisplayer.roomID = dungeonRooms[i].roomID;
            roomDisplayer.ConfigureRoom();
            Instantiate(placedRooms[placedRoomsCount], new Vector3(dungeonRooms[i].roomX * offsetX, dungeonRooms[i].roomY * offsetY, 0), Quaternion.identity);
            placedRoomsCount++;
        }
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