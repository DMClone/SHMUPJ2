using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM : MonoBehaviour
{
    public static GM instance;

    [SerializeField] public PlaneInput _input;


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

        StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
    }

    IEnumerator ProgressRound(int roundInterval)
    {
        SpawnRound();
        yield return new WaitForSeconds(roundInterval);
        if (waves[currentWave - 1].rounds.Length > currentRound)
        {
            currentRound++;
            StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
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
    }

    public void Start()
    {
        LivesCanvas.instance.StartUp(PlayerPlane.instance.gameObject.GetComponent<UnitStats>().health);
    }

    private void OnEnable()
    {
        _input.UI.PauseMenu.performed += PauseAction;
    }

    private void OnDisable()
    {
        _input.UI.PauseMenu.performed -= PauseAction;
    }

    public void PauseAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Paused");
        }
    }

    private void Update()
    {
        if (waves[currentWave - 1].rounds.Length == currentRound && waves.Length >= currentWave && enemiesAlive <= 0)
        {
            currentWave++;
            currentRound = 1;
            StartCoroutine(ProgressRound(waves[currentWave - 1].rounds[currentRound - 1].spawnInterval));
        }
        else if (waves.Length == currentWave && waves[currentWave - 1].rounds.Length == currentRound)
        {
            Debug.Log("We have reached the end of all the waves");
        }
    }
}
