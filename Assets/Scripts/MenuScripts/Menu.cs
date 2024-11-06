using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Update() {
        if (PlayerPrefs.HasKey("SelectedClass"))
        {
            Debug.Log(PlayerPrefs.GetString("SelectedClass"));
        }
    }
}
