using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoom {

    public static int X_SIZE = 10;
    public static int Y_SIZE = 6;

    public int roomID;

    public DungeonRoom topRoom = null;
    public DungeonRoom rightRoom = null;
    public DungeonRoom bottomRoom = null;
    public DungeonRoom leftRoom = null;

    public int roomX;
    public int roomY;

    public Tilemap tilemap;

    public RoomType type;

    public bool isStartingRoom = false;
    public Direction enteringDirection;

    public int distanceFromStartingRoom = 0;

    public DungeonRoom(int x, int y, bool isStartingRoom = false){
        this.roomX = x;
        this.roomY = y;
        this.isStartingRoom = isStartingRoom;
    }

    public void SetEntryDoor(Direction direction){
        if (isStartingRoom){
            this.enteringDirection = direction;
        }
    }

    public void SetNeighborRoom(DungeonRoom room, Direction direction){
        switch ((int) direction){
            case 0:
                this.topRoom = room;
                room.bottomRoom = this;
                break;
            case 1:
                this.rightRoom = room;
                room.leftRoom = this;
                break;
            case 2: 
                this.bottomRoom = room;
                room.topRoom = this;
                break;
            case 3:
                this.leftRoom = room;
                room.rightRoom = this;
                break;
        }
    }
}
