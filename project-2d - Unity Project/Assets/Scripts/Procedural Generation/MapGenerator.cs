using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int[,] map;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 15)]
    public int iterations = 4;

    [Range(0,100)]
    public int wallDensity;

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

    public void RandomlyFillMap(){
        // Using a seed to generate a different map every time
        if (useRandomSeed){
            seed = Time.time.ToString();
        }
        System.Random pseudoRandomNumber = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1){
                    map[x, y] = 1;
                } else if (pseudoRandomNumber.Next(0, 100) < wallDensity){
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



}
