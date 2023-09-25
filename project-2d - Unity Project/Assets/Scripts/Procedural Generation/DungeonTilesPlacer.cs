using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonTilesPlacer : MonoBehaviour {

    public Tilemap tileMap;
    public RuleTile tileRule;
    public Tile tile;
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
                    tileMap.SetTile(new Vector3Int(x, y, 0), tileRule);
                } else {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

}
