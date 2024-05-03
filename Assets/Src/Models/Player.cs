using UnityEngine;

[System.Serializable]
public class Player {

    public int level;
    public SerializableVector3 position;
    public int health;

    #region helpers data
    public bool[] triangleBox;
    public float squares;
    public int goldenSquares;
    #endregion
    

    public Player(){
        level = 1;
        position = new SerializableVector3(Vector3.zero);
        health = 6;
        triangleBox = new bool[5] { false, false, false, false, false };
        squares = 100;
        goldenSquares = 0;
        
    }

}