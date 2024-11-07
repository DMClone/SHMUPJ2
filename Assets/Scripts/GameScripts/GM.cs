using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM instance;

    [SerializeField] public PlaneInput _input;
    private Coroutine waveCoroutine;


    [System.Serializable]
    public class Waves
    {
        public Rounds[] rounds;
    }

    [System.Serializable]
    public class Rounds
    {
        public int enemyOneCount;
        public int enemyTwoCount;
        public int enemyThreeCount;
        [Tooltip("Time for the next round")]
        public int spawnInterval = 20;
    }

    [SerializeField]
    private Waves[] waves;

    public GameObject[] playerShips;
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject scoreText;

    public int score;
    public int enemiesAlive;
    public int enemiesKilled;
    public int currentWave = 1;
    public int currentRound = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        switch (PlayerPrefs.GetString("SelectedClass"))
        {
            case "Starter":
                Instantiate(playerShips[0]);
                break;
            case "Hunter":
                Instantiate(playerShips[1]);
                break;
            case "Builder":
                Instantiate(playerShips[2]);
                break;
            default:
                Instantiate(playerShips[0]);
                break;
        }

        _input = new PlaneInput();
        _input.UI.PauseMenu.Enable();

        waveCoroutine = StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
    }

    public void AddScore(int scoreRecieved)
    {
        if (!PlayerPlane.instance.GetComponent<UnitStats>().isDead)
        {
            score += scoreRecieved;
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        }
    }

    #region WaveLogic

    public void PowerupSpawnCheck(Vector3 enemyPosition)
    {
        switch (enemiesKilled % 50)
        {
            case 0:
            case 25:
                Instantiate(powerups[0], enemyPosition, Quaternion.identity);
                Debug.Log("Spawned upgrade: Regenerate");
                break;
            case 20:
            case 45:
                Instantiate(powerups[1], enemyPosition, Quaternion.identity);
                Debug.Log("Spawned upgrade: ShipUpgrade");
                break;
            default:
                break;
        }
    }

    IEnumerator ProgressRound(int roundInterval)
    {
        SpawnRound();
        yield return new WaitForSeconds(roundInterval);
        if (waves[currentWave - 1].rounds.Length > currentRound)
        {
            currentRound++;
            waveCoroutine = StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
        }
    }

    private void SpawnRound()
    {
        for (int i = 0; i < waves[currentWave - 1].rounds[currentRound - 1].enemyOneCount; i++)
        {
            int spawnOffset = Random.Range(-5, 5);
            Instantiate(enemies[0], new Vector3(spawnOffset, transform.position.y, 0), Quaternion.identity);
            enemiesAlive++;
        }
        for (int i = 0; i < waves[currentWave - 1].rounds[currentRound - 1].enemyTwoCount; i++)
        {
            int spawnOffset = Random.Range(-5, 5);
            Instantiate(enemies[1], new Vector3(spawnOffset, transform.position.y, 0), Quaternion.identity);
            enemiesAlive++;
        }
        for (int i = 0; i < waves[currentWave - 1].rounds[currentRound - 1].enemyThreeCount; i++)
        {
            int spawnOffset = Random.Range(-5, 5);
            Instantiate(enemies[2], new Vector3(spawnOffset, transform.position.y, 0), Quaternion.identity);
            enemiesAlive++;
        }
    }

    private void FixedUpdate()
    {
        if (waves[currentWave - 1].rounds.Length > currentRound && waves.Length >= currentWave - 1 && enemiesAlive <= 0)
        {
            currentRound++;
            StopCoroutine(waveCoroutine);
            StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
            Debug.Log("Defeated enemies too early");
        }
        else if (waves[currentWave - 1].rounds.Length == currentRound && waves.Length > currentWave && enemiesAlive <= 0)
        {
            currentWave++;
            currentRound = 1;
            StopCoroutine(waveCoroutine);
            StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
            Debug.Log("Started new wave");
        }
        else if (waves.Length == currentWave && waves[currentWave - 1].rounds.Length == currentRound)
        {
            Debug.Log("We have reached the end of all the waves");
        }
    }
    #endregion

    #region ActionToggles
    private void OnEnable()
    {
        _input.UI.PauseMenu.performed += PauseAction;
    }

    private void OnDisable()
    {
        _input.UI.PauseMenu.performed -= PauseAction;
    }
    #endregion

    public void PauseAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Paused");
        }
    }

    public void GameEnd()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + score / 50);
        StartCoroutine(EndGame());

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(1.4f);
            SceneManager.LoadScene("Menu");
        }
    }
}
