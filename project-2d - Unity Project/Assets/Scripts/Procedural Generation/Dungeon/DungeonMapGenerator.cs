using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour {

    public static int DUNGEON_WIDTH = 10;
    public static int DUNGEON_HEIGHT = 6;
    public int[,] map;

    private int startingRoomX;
    private int startingRoomY;
    public int roomCount = 10;
    private int placedRoomsCount;

    public DungeonRoom[] dungeonRooms;
    
    public DungeonGrid dungeonGrid;

    private void Start() {
        GenerateRoomDisposition();
        Debug.Log(printMap(map));
        dungeonGrid.PlaceAllRooms(dungeonRooms, map);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)){
            GenerateRoomDisposition();
            Debug.Log(printMap(map));
        }
    }

    public void PlaceStartingRoom(){
        if (DUNGEON_WIDTH % 2 == 0){
            startingRoomX = DUNGEON_WIDTH / 2;
        } else {
            startingRoomX = (DUNGEON_WIDTH / 2) - 1;
        }
        if (DUNGEON_HEIGHT % 2 == 0){
            startingRoomY = DUNGEON_HEIGHT / 2;
        } else {
            startingRoomY = (DUNGEON_HEIGHT / 2) - 1;
        }
        map[startingRoomX, startingRoomY] = (int) RoomType.STARTING_ROOM;
    }

    public void GenerateRoomDisposition(){
        map = new int[DUNGEON_WIDTH, DUNGEON_HEIGHT];
        PlaceStartingRoom();
        Vector2Int[] placedRoomsCoordinates = new Vector2Int[roomCount];
        dungeonRooms = new DungeonRoom[roomCount];
        placedRoomsCount = 0;
        placedRoomsCoordinates[placedRoomsCount] = new Vector2Int(startingRoomX, startingRoomY);  
        
        // Generate starting room entry door direction
        int entry = Random.Range(0,4);
        PlaceRoom(startingRoomX, startingRoomY, (Direction) entry, RoomType.VOID);
        dungeonRooms[placedRoomsCount] = new DungeonRoom(startingRoomX, startingRoomY, true);
        dungeonRooms[placedRoomsCount].SetEntryDoor((Direction) entry);
        // Start placing rooms around other rooms starting from the starting room 
        int iteration = 0;
        int currentRoom = 0;
        while (placedRoomsCount < roomCount && currentRoom < roomCount && iteration < 150){
            int currentRoomX = placedRoomsCoordinates[currentRoom].x;
            int currentRoomY = placedRoomsCoordinates[currentRoom].y;
            int[] freeRoomLocations = PlacableRoomsLocations(currentRoomX, currentRoomY);
            
            // Generate number of rooms to spawn around the current room
            int roomSpawnChances = Random.Range(0,100);
            int neighborRoomsCount;
            switch (roomSpawnChances){
                case int n when n < 40:
                    neighborRoomsCount = 1;
                    break;
                case int n when n < 80:
                    neighborRoomsCount = 2;
                    break;
                default:
                    neighborRoomsCount = 3;
                    break;   
            }
            // Clamp the room count, if can't place said amount around the current room, 
            // to max placable rooms around the current room
            neighborRoomsCount = Mathf.Min(neighborRoomsCount, CountPlacableRooms(freeRoomLocations));

            // Placing neighborRoomsCount amount of rooms around the current room
            bool canPlaceMore = true;
            int intDirection = Random.Range(0,4);
            for (int i = 0; i < neighborRoomsCount && canPlaceMore; i++){
                if (placedRoomsCount == roomCount - 1){
                    canPlaceMore = false;
                } else {
                    while (freeRoomLocations[intDirection] != 0){
                        intDirection = (intDirection + 1) % 4;
                    }
                    Direction direction = (Direction) intDirection;
                    if (CanPlaceRoom(currentRoomX, currentRoomY, direction)){
                        Debug.Log("iteration : " + iteration + " | placedrooms : " + placedRoomsCount + " | current room " + currentRoom + " (" + currentRoomX + ", " + currentRoomY + ")");
                        Vector2Int placedRoomCoordinates = PlaceRoom(currentRoomX, currentRoomY, direction, RoomType.ENEMY_ROOM);
                        placedRoomsCount++;
                        placedRoomsCoordinates[placedRoomsCount] = placedRoomCoordinates;
                        dungeonRooms[placedRoomsCount] = new DungeonRoom(placedRoomCoordinates.x, placedRoomCoordinates.y, false)
                        {
                            distanceFromStartingRoom = dungeonRooms[currentRoom].distanceFromStartingRoom + 1
                        };
                        dungeonRooms[currentRoom].SetNeighborRoom(dungeonRooms[placedRoomsCount], direction);
                    }
                    intDirection = (intDirection + 1) % 4;
                }
            }
            currentRoom++;
            iteration++;
        }    
    }

    public bool CanPlaceRoom(int x, int y, Direction direction){
        switch (direction){
            case Direction.TOP:
                if (y == DUNGEON_HEIGHT - 1){
                    return false;
                } else if (map[x, y + 1] != 0){
                    return false;
                }
                return true;
            case Direction.RIGHT:
                if (x == DUNGEON_WIDTH - 1){
                    return false;
                } else if (map[x + 1, y] != 0){
                    return false;
                }
                return true;
            case Direction.BOTTOM:
                if (y == 0){
                    return false;
                } else if (map[x, y - 1] != 0){
                    return false;
                }
                return true;
            case Direction.LEFT:
                if (x == 0){
                    return false;
                } else if (map[x - 1, y] != 0){
                    return false;
                }
                return true;
        }
        return false;
    }

    public Vector2Int PlaceRoom(int x, int y, Direction direction, RoomType roomType){
        int roomTypeInt = (int) roomType;
        switch (direction){
            case Direction.TOP:
                map[x, y + 1] = roomTypeInt;
                return new Vector2Int(x, y + 1);
            case Direction.RIGHT:
                map[x + 1, y] = roomTypeInt;
                return new Vector2Int(x + 1, y);
            case Direction.BOTTOM:
                map[x, y - 1] = roomTypeInt;
                return new Vector2Int(x, y - 1);
            case Direction.LEFT:
                map[x - 1, y] = roomTypeInt;
                return new Vector2Int(x - 1, y);
        }
        return new Vector2Int(x, y);
    }

    public string printMap(int[,] mapToPrint){
        string str = "\n[";
        for (int y = DUNGEON_HEIGHT - 1; y >= 0; y--){
            str += "[";
            for (int x = 0; x < DUNGEON_WIDTH; x++){
                str += mapToPrint[x, y] + " ";
            }
            str += "]\n";
        }
        return str;
    }

    public int[] PlacableRoomsLocations(int x, int y){
        int[] placableRooms = new int[4];
        for (int i = 0; i < 4; i++){
            Direction direction = (Direction) i;
            if (CanPlaceRoom(x, y, direction)){
                placableRooms[i] = 0;
            } else {
                placableRooms[i] = 1;
            }
        }
        return placableRooms;
    }

    public int CountPlacableRooms(int[] placableRooms){
        int count = 0;
        for (int i = 0; i < 4; i++){
            if (placableRooms[i] == 0){
                count++;
            }
        }
        return count;
    }
}
