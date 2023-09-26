using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoom : MonoBehaviour {

    public DungeonRoom topRoom;
    public DungeonRoom rightRoom;
    public DungeonRoom bottomRoom;
    public DungeonRoom leftRoom;

    public Tilemap tilemap;

    public enum RoomType{
        TOP, RIGHT, BOTTOM, LEFT,
        TOP_RIGHT, TOP_BOTTOM, TOP_LEFT, RIGHT_BOTTOM, RIGHT_LEFT, BOTTOM_LEFT,
        TOP_RIGHT_BOTTOM, RIGHT_BOTTOM_LEFT, BOTTOM_LEFT_TOP, LEFT_TOP_RIGHT,
        FULL
    }

    public RoomType type;

    public bool isStartingRoom = false;
    public Direction enteringDirection;

    public DungeonRoom(Direction direction, bool isStartingRoom = false){
        this.isStartingRoom = isStartingRoom;
        this.enteringDirection = direction;
    }
}
