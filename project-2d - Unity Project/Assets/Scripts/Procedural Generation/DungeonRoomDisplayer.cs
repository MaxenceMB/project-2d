using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoomDisplayer : MonoBehaviour {

    public Tilemap tilemap;
    public Tile wall;
    public Tile door;

    public void PlaceWalls(DungeonRoom room){
        Bounds bounds = tilemap.localBounds;
        Vector2Int topLeft = new Vector2Int((int) bounds.min.x - 1, (int) bounds.max.y);
        Vector2Int topRight = new Vector2Int((int) bounds.max.x, (int) bounds.max.y);
        Vector2Int bottomLeft = new Vector2Int((int) bounds.min.x - 1, (int) bounds.min.y - 1);
        Vector2Int bottomRight = new Vector2Int((int) bounds.max.x, (int) bounds.min.y - 1);

        if (room.isStartingRoom){
            tilemap.SetTile(new Vector3Int(0, 0, 0), door);
        }

        for (int i = bottomRight.y; i <= topLeft.y; i++){
            tilemap.SetTile(new Vector3Int(topLeft.x, i, 0), wall);
        }
        for (int i = topLeft.x; i <= topRight.x; i++){
            tilemap.SetTile(new Vector3Int(i, topRight.y, 0), wall);
        }
        for (int i = bottomRight.y; i <= topRight.y; i++){
            tilemap.SetTile(new Vector3Int(topRight.x, i, 0), wall);
        }
        for (int i = bottomLeft.x; i <= bottomRight.x; i++){
            tilemap.SetTile(new Vector3Int(i, bottomRight.y, 0), wall);
        }

        if (room.topRoom != null){
            tilemap.SetTile(new Vector3Int(0, topLeft.y, 0), door);
            tilemap.SetTile(new Vector3Int(-1, topLeft.y, 0), door);
        }
        if (room.bottomRoom != null){
            tilemap.SetTile(new Vector3Int(0, bottomLeft.y, 0), door);
            tilemap.SetTile(new Vector3Int(-1, bottomLeft.y, 0), door);
        }
        if (room.rightRoom != null){
            tilemap.SetTile(new Vector3Int(bottomRight.x, 0, 0), door);
            tilemap.SetTile(new Vector3Int(bottomRight.x, -1, 0), door);
        }
        if (room.leftRoom != null){
            tilemap.SetTile(new Vector3Int(bottomLeft.x, 0, 0), door);
            tilemap.SetTile(new Vector3Int(bottomLeft.x, -1, 0), door);
        }
    }
}


