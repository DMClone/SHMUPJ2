using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndScreen : MonoBehaviour
{
    public GameObject endScreen;
    private bool screenSpawned;
    
    void Update()
    {
        if (Time.timeScale <= 0.1f && !screenSpawned)
        {

        }
    }
}
