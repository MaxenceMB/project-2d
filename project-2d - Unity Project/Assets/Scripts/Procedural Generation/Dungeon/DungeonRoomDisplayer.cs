using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class DungeonRoomDisplayer : MonoBehaviour {

    public DungeonRoom room;

    public Tilemap groundTilemap;
    public Tilemap wallsTilemap;
    public Tilemap doorsTilemap;

    public Tile[] topWall;
    public Tile[] rightWall;
    public Tile[] bottomWall;
    public Tile[] leftWall;

    public Tile[] topDoor;
    public Tile[] rightDoor;
    public Tile[] bottomDoor;
    public Tile[] leftDoor;

    public Tile[] corners;

    Vector2Int topLeft;
    Vector2Int topRight;
    Vector2Int bottomLeft;
    Vector2Int bottomRight;

    public void ConfigureRoom(){
        wallsTilemap.transform.position = groundTilemap.transform.position;
        doorsTilemap.transform.position = groundTilemap.transform.position;

        // Setting up the tilemap
        Bounds bounds = groundTilemap.localBounds;
        topLeft = new Vector2Int((int) bounds.min.x - 1, (int) bounds.max.y);
        topRight = new Vector2Int((int) bounds.max.x, (int) bounds.max.y);
        bottomLeft = new Vector2Int((int) bounds.min.x - 1, (int) bounds.min.y - 1);
        bottomRight = new Vector2Int((int) bounds.max.x, (int) bounds.min.y - 1);

        PlaceEnteringDoor(room);

        // Placing walls ---
        // Top wall
        for (int i = topLeft.x; i <= topRight.x; i++){
            wallsTilemap.SetTile(new Vector3Int(i, topRight.y, 0), topWall[0]);
            wallsTilemap.SetTile(new Vector3Int(i, topRight.y + 1, 0), topWall[1]);
        }

        // Right wall
        for (int i = bottomRight.y; i <= topRight.y; i++){
            wallsTilemap.SetTile(new Vector3Int(topRight.x, i, 0), rightWall[0]);
            wallsTilemap.SetTile(new Vector3Int(topRight.x + 1, i, 0), rightWall[1]);
        }

        // Bottom wall
        for (int i = bottomLeft.x; i <= bottomRight.x; i++){
            wallsTilemap.SetTile(new Vector3Int(i, bottomRight.y, 0), bottomWall[0]);
            wallsTilemap.SetTile(new Vector3Int(i, bottomRight.y - 1, 0), bottomWall[1]);
        }

        // Left wall
        for (int i = bottomRight.y; i <= topLeft.y; i++){
            wallsTilemap.SetTile(new Vector3Int(topLeft.x, i, 0), leftWall[0]);
            wallsTilemap.SetTile(new Vector3Int(topLeft.x - 1, i, 0), leftWall[1]);
        }

        // Placing doors ---
        // Top door
        if (room.topRoom != null){
            doorsTilemap.SetTile(new Vector3Int(-1, topLeft.y, 0), topDoor[0]);
            doorsTilemap.SetTile(new Vector3Int(0, topLeft.y, 0), topDoor[1]);
            doorsTilemap.SetTile(new Vector3Int(-1, topLeft.y + 1, 0), topDoor[2]);
            doorsTilemap.SetTile(new Vector3Int(0, topLeft.y + 1, 0), topDoor[3]);
            wallsTilemap.SetTile(new Vector3Int(-1, topLeft.y, 0), null);
            wallsTilemap.SetTile(new Vector3Int(0, topLeft.y, 0), null);
            wallsTilemap.SetTile(new Vector3Int(-1, topLeft.y + 1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(0, topLeft.y + 1, 0), null);
        }

        // Bottom door
        if (room.bottomRoom != null){
            doorsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y, 0), bottomDoor[0]);
            doorsTilemap.SetTile(new Vector3Int(0, bottomLeft.y, 0), bottomDoor[1]);
            doorsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y - 1, 0), bottomDoor[2]);
            doorsTilemap.SetTile(new Vector3Int(0, bottomLeft.y - 1, 0), bottomDoor[3]);
            wallsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y, 0), null);
            wallsTilemap.SetTile(new Vector3Int(0, bottomLeft.y, 0), null);
            wallsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y - 1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(0, bottomLeft.y - 1, 0), null);
        }

        // Right door
        if (room.rightRoom != null){
            doorsTilemap.SetTile(new Vector3Int(bottomRight.x, 0, 0), rightDoor[0]);
            doorsTilemap.SetTile(new Vector3Int(bottomRight.x, -1, 0), rightDoor[1]);
            doorsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, 0, 0), rightDoor[2]);
            doorsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, -1, 0), rightDoor[3]);
            wallsTilemap.SetTile(new Vector3Int(bottomRight.x, 0, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomRight.x, -1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, 0, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, -1, 0), null);
        }

        // Left door
        if (room.leftRoom != null){
            doorsTilemap.SetTile(new Vector3Int(bottomLeft.x, 0, 0), leftDoor[0]);
            doorsTilemap.SetTile(new Vector3Int(bottomLeft.x, -1, 0), leftDoor[1]);
            doorsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, 0, 0), leftDoor[2]);
            doorsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, -1, 0), leftDoor[3]);
            wallsTilemap.SetTile(new Vector3Int(bottomLeft.x, 0, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomLeft.x, -1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, 0, 0), null);
            wallsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, -1, 0), null);
        }

        // Placing corners ---
        // Top left 
        wallsTilemap.SetTile(new Vector3Int(topLeft.x, topLeft.y, 0), corners[0]);
        wallsTilemap.SetTile(new Vector3Int(topLeft.x - 1, topLeft.y + 1, 0), corners[1]);

        // Top right 
        wallsTilemap.SetTile(new Vector3Int(topRight.x, topRight.y, 0), corners[2]);
        wallsTilemap.SetTile(new Vector3Int(topRight.x + 1, topRight.y + 1, 0), corners[3]);

        // Bottom left 
        wallsTilemap.SetTile(new Vector3Int(bottomLeft.x, bottomLeft.y, 0), corners[4]);
        wallsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, bottomLeft.y - 1, 0), corners[5]);

        // Bottom right 
        wallsTilemap.SetTile(new Vector3Int(bottomRight.x, bottomRight.y, 0), corners[6]);
        wallsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, bottomRight.y - 1, 0), corners[7]);
    }

    public void PlaceSpawnPoint(){

    }

    public void PlaceEnteringDoor(DungeonRoom room){
        if (room.isStartingRoom){
            switch (room.enteringDirection){
                case Direction.TOP:
                    doorsTilemap.SetTile(new Vector3Int(-1, topLeft.y, 0), topDoor[0]);
                    doorsTilemap.SetTile(new Vector3Int(0, topLeft.y, 0), topDoor[1]);
                    doorsTilemap.SetTile(new Vector3Int(-1, topLeft.y + 1, 0), topDoor[2]);
                    doorsTilemap.SetTile(new Vector3Int(0, topLeft.y + 1, 0), topDoor[3]);
                    break; 
                case Direction.RIGHT:
                    doorsTilemap.SetTile(new Vector3Int(bottomRight.x, 0, 0), rightDoor[0]);
                    doorsTilemap.SetTile(new Vector3Int(bottomRight.x, -1, 0), rightDoor[1]);
                    doorsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, 0, 0), rightDoor[2]);
                    doorsTilemap.SetTile(new Vector3Int(bottomRight.x + 1, -1, 0), rightDoor[3]);
                    break;
                case Direction.BOTTOM:
                    doorsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y, 0), bottomDoor[0]);
                    doorsTilemap.SetTile(new Vector3Int(0, bottomLeft.y, 0), bottomDoor[1]);
                    doorsTilemap.SetTile(new Vector3Int(-1, bottomLeft.y - 1, 0), bottomDoor[2]);
                    doorsTilemap.SetTile(new Vector3Int(0, bottomLeft.y - 1, 0), bottomDoor[3]);
                    break;
                case Direction.LEFT:
                    doorsTilemap.SetTile(new Vector3Int(bottomLeft.x, 0, 0), leftDoor[0]);
                    doorsTilemap.SetTile(new Vector3Int(bottomLeft.x, -1, 0), leftDoor[1]);
                    doorsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, 0, 0), leftDoor[2]);
                    doorsTilemap.SetTile(new Vector3Int(bottomLeft.x - 1, -1, 0), leftDoor[3]);
                    break;
            }
        }
    }
}


