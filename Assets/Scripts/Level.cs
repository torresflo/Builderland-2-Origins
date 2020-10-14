using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
    public TextAsset tiles;
    public TextAsset physics;
    public GameObject sky;
    public int columns;
    public int rows;

    public string getTiles()
    {
        return tiles.text;
    }

    public string getPhysics()
    {
        return physics.text;
    }
}
