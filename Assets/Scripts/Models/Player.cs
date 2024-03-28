using UnityEngine;

[System.Serializable]
public class Player {

    public int level;
    public SerializableVector3 position;
    public int health;
    public bool[] triangleBox;
    public int squares;
    public int goldenSquares;
    

    public Player(){
        level = 1;
        position = new SerializableVector3(Vector3.zero);
        health = 6;
        triangleBox = new bool[5] { false, false, false, false, false };
        squares = 0;
        goldenSquares = 0;
        
    }

}