using UnityEngine;

[System.Serializable]
public class Square {

    public int level;
    public SerializableVector3 position;
    public int health;
    public bool[] puzzle;
    public int goldenSquares;
    public int helperSquares;

    public Square(){
        level = 1;
        position = new SerializableVector3(Vector3.zero);
        health = 6;
        puzzle = new bool[5] { false, false, false, false, false };
        goldenSquares = 0;
        helperSquares = 0;
    }

}