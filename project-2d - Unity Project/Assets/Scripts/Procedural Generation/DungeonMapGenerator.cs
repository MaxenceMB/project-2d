using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int[,] map;

    public int startingRoomX;
    public int startingRoomY;
    public int roomCount = 10;

    public DungeonRoom[] dungeonRooms;

    private void Start() {
        GenerateRoomDisposition();
        Debug.Log(printMap(map));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)){
            GenerateRoomDisposition();
            Debug.Log(printMap(map));
        }
    }

    public void PlaceStartingRoom(){
        if (width % 2 != 0){
            startingRoomX = width / 2;
        } else {
            startingRoomX = (width / 2) - 1;
        }
        if (height % 2 != 0){
            startingRoomY = height / 2;
        } else {
            startingRoomY = (height / 2) - 1;
        }
        map[startingRoomX, startingRoomY] = (int) RoomType.STARTING_ROOM;
    }

    public void GenerateRoomDisposition(){
        map = new int[height, width];
        PlaceStartingRoom();
        Vector2Int[] placedRoomsCoordinates = new Vector2Int[roomCount];
        dungeonRooms = new DungeonRoom[roomCount];
        int placedRoomsCount = 0;
        placedRoomsCoordinates[placedRoomsCount] = new Vector2Int(startingRoomX, startingRoomY);  
        
        // Generate starting room entry door direction
        int entry = Random.Range(0,4);
        PlaceRoom(startingRoomX, startingRoomY, (Direction) entry, RoomType.VOID);
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
                        Vector2Int placedRoomCoordinates = PlaceRoom(currentRoomX, currentRoomY, direction, RoomType.ENEMY_ROOM);
                        placedRoomsCount++;
                        placedRoomsCoordinates[placedRoomsCount] = placedRoomCoordinates;
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
                if (x == 0){
                    return false;
                } else if (map[x - 1, y] != 0){
                    return false;
                }
                return true;
            case Direction.RIGHT:
                if (y == width - 1){
                    return false;
                } else if (map[x, y + 1] != 0){
                    return false;
                }
                return true;
            case Direction.BOTTOM:
                if (x == height - 1){
                    return false;
                } else if (map[x + 1, y] != 0){
                    return false;
                }
                return true;
            case Direction.LEFT:
                if (y == 0){
                    return false;
                } else if (map[x, y - 1] != 0){
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
                map[x - 1, y] = roomTypeInt;
                return new Vector2Int(x - 1, y);
            case Direction.RIGHT:
                map[x, y + 1] = roomTypeInt;
                return new Vector2Int(x, y + 1);
            case Direction.BOTTOM:
                map[x + 1, y] = roomTypeInt;
                return new Vector2Int(x + 1, y);
            case Direction.LEFT:
                map[x, y - 1] = roomTypeInt;
                return new Vector2Int(x, y - 1);
        }
        return new Vector2Int(x, y);
    }

    public string printMap(int[,] mapToPrint){
        string str = "\n[";
        for (int x = 0; x < height; x++){
            str += "[";
            for (int y = 0; y < width; y++){
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


    /*

    void Start() {
       GenerateMap(); 
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            GenerateMap();
        }
    }

    public void GenerateMap(){
        map = new int[width, height];
        RandomlyFillMap();
        for (int i = 0; i < iterations; i++){
            SmoothMap();
        }
    }

    

    public void RandomlyFillMap(){
        // Using a seed to generate a different map every time
        if (useRandomSeed){
            seed = Time.time.ToString();
        }
        System.Random pseudoRandomNumber = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                if (pseudoRandomNumber.Next(0, 100) < wallDensity){
                    map[x, y] = 0;
                } else {
                    map[x, y] = 1;
                }
            }
        }
    }

    public void SmoothMap(){
        int[,] tempMap = (int[,]) map.Clone();
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                int numberOfWallNeighbours = NumberOfWallNeighbours(x, y);

                if (numberOfWallNeighbours < 4){
                    tempMap[x, y] = 0;
                } else if (numberOfWallNeighbours > 4){
                    tempMap[x, y] = 1;
                }
            }
        }
        map = tempMap;
    }

    public int NumberOfWallNeighbours(int posX, int posY){
        int numberOfWallNeighbours = 0;
        for (int x = posX - 1; x <= posX + 1; x++){
            for (int y = posY - 1; y <= posY + 1; y++){
                if (x < 0 || x >= width || y < 0 || y >= height){
                    numberOfWallNeighbours++;
                } else if (x != posX || y != posY){
                    numberOfWallNeighbours += map[x, y];
                }
            }
        }
        return numberOfWallNeighbours;
    }

    void OnDrawGizmos() {
        if (map != null){
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    if (map[x, y] == 1){
                        Gizmos.color = Color.black;
                    } else {
                        Gizmos.color = Color.white;
                    }
                    Vector3 mapPosition = new Vector3(-width/2 + x + 0.5f, 0, -height/2 + y + 0.5f);
                    Gizmos.DrawCube(mapPosition, Vector3.one);
                }
            }
        }
    }

    */

}
