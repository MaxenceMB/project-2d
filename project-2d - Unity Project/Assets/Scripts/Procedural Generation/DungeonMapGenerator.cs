using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour {

    public enum Direction{
        TOP = 0, RIGHT = 1, BOTTOM = 2, LEFT = 3
    }

    public int width;
    public int height;
    public int[,] map;/*

    public string seed;
    public bool useRandomSeed;

    [Range(0, 15)]
    public int iterations = 4;

    [Range(0,100)]
    public int wallDensity;*/

    public int startingRoomX;
    public int startingRoomY;
    public int roomCount = 6;

    private void Start() {
        map = new int[width, height];
        printMap(map);
        GenerateRoomDisposition();
        printMap(map);
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
        map[startingRoomX, startingRoomY] = 1;
    }

    public void GenerateRoomDisposition(){
        PlaceStartingRoom();
        Vector2Int[] placedRoomsCoordinates = new Vector2Int[roomCount];
        int placedRooms = 0;
        placedRoomsCoordinates[placedRooms] = new Vector2Int(startingRoomX, startingRoomY);   
        int n = 0;
        int currentRoom = 0;
        while (placedRooms < roomCount && n < 150){
            int currentRoomX = placedRoomsCoordinates[currentRoom].x;
            int currentRoomY = placedRoomsCoordinates[currentRoom].y;
            Debug.Log("Iteration " + n + " | New room coordinates : (" + currentRoomX + ", " + currentRoomY + ")");
            int neighborRoomsCount = Random.Range(1,4);
            // Placing neighborRoomsCount amount of rooms around the current room
            for (int i = 0; i < neighborRoomsCount; i++){
                if (placedRooms == roomCount - 1){
                    break;
                }
                Direction direction = (Direction) i;
                if (CanPlaceRoom(currentRoomX, currentRoomY, direction)){
                    Debug.Log("Iteration " + n + " | Room placed in " + direction.ToString() + " of (" + currentRoomX + ", " + currentRoomY);
                    Vector2Int placedRoomCoordinates = PlaceRoom(currentRoomX, currentRoomY, direction);
                    //Debug.Log("Iteration " + n + " | New room coordinates : (" + placedRoomCoordinates.x + ", " + placedRoomCoordinates.y + ")");
                    placedRooms++;
                    placedRoomsCoordinates[placedRooms] = placedRoomCoordinates;
                }
            }
            currentRoom++;
            n++;
        }
        
    }

    public bool CanPlaceRoom(int x, int y, Direction direction){
        switch (direction){
            case Direction.TOP:
                if (y == height - 1){
                    return false;
                } else if (map[x, y + 1] == 1){
                    return false;
                }
                return true;
            case Direction.RIGHT:
                if (x == width - 1){
                    return false;
                } else if (map[x + 1, y] == 1){
                    return false;
                }
                return true;
            case Direction.BOTTOM:
                if (y == 0){
                    return false;
                } else if (map[x, y -1] == 1){
                    return false;
                }
                return true;
            case Direction.LEFT:
                if (x == 0){
                    return false;
                } else if (map[x - 1, y] == 1){
                    return false;
                }
                return true;
        }
        return false;
    }

    public Vector2Int PlaceRoom(int x, int y, Direction direction){
        switch (direction){
            case Direction.TOP:
                map[x, y + 1] = 1;
                return new Vector2Int(x, y + 1);
            case Direction.RIGHT:
                map[x + 1, y] = 1;
                return new Vector2Int(x + 1, y);
            case Direction.BOTTOM:
                map[x, y - 1] = 1;
                return new Vector2Int(x, y - 1);
            case Direction.LEFT:
                map[x - 1, y] = 1;
                return new Vector2Int(x - 1, y);
        }
        return new Vector2Int(x, y);
    }

    public void printMap(int[,] mapToPrint){
        string str = "\n[";
        for (int x = 0; x < width; x++){
            str += "[";
            for (int y = 0; y < height; y++){
                str += mapToPrint[x, y] + " ";
            }
            str += "]\n";
        }
        Debug.Log(str);
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
