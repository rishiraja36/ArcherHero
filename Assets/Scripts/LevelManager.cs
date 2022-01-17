using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public List<GameObject> DeadEnemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        ActiveEnemies.AddRange(enemies);
        DeadEnemies = new List<GameObject>();
    }

    
}
