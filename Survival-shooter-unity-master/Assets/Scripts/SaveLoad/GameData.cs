using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerHealth;
    public Vector3 playerPosition;

    public GameData()
    {
        playerHealth = 0;
        playerPosition = Vector3.zero;
    }
}
