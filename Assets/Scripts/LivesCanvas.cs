using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCanvas : MonoBehaviour
{
    public static LivesCanvas instance;

    [SerializeField] private GameObject life;
    private List<GameObject> livesInCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        livesInCanvas = new List<GameObject>();
    }




    public void StartUp(int livesGM)
    {
        for (int i = 0; i < livesGM; i++)
        {
            GameObject AddedImage = Instantiate(life, new Vector3(350 * i + 250, 90, 0), Quaternion.identity, transform);
            livesInCanvas.Add(AddedImage);
        }
    }
}
