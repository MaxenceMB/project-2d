using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoomsPlacer : MonoBehaviour {

    public Tilemap tileMap;
    public Tile[] tile;
    public DungeonMapGenerator mapGenerator;

    public void Start(){
        StartCoroutine(PlaceTiles());
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(PlaceTiles());
        }
    }

    IEnumerator PlaceTiles(){
        yield return null;
        for (int x = 0; x < mapGenerator.width; x++){
            for (int y = 0; y < mapGenerator.height; y++){
                if (mapGenerator.map[x, y] == 1){
                    tileMap.SetTile(new Vector3Int(x, y, 0), tile[0]);
                } else if (mapGenerator.map[x, y] == 2) {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tile[1]);
                } else if (mapGenerator.map[x, y] == 3) {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tile[2]);
                } else if (tileMap.HasTile(new Vector3Int(x, y, 0))){
                    tileMap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

}
