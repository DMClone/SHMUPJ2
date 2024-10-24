using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject[] enemies;

    public int enemiesAlive;
    public int currentWave = 1;
    public int currentRound = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _input = new PlaneInput();
        _input.UI.PauseMenu.Enable();

        waveCoroutine = StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
    }

    #region WaveLogic
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

    public void Start()
    {
        RefreshLiveCanvas();
    }

    public void RefreshLiveCanvas()
    {
        LivesCanvas.instance.StartUp(PlayerPlane.instance.gameObject.GetComponent<UnitStats>().health);
    }

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

}
