using System.Collections;
using System.Collections.Generic;
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
    }

    [SerializeField]
    private Waves[] waves;


    public int enemiesAlive;
    public int currentWave;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _input = new PlaneInput();
        _input.UI.PauseMenu.Enable();
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

    private void FixedUpdate()
    {
        if (currentWave == 0)
        {
            Debug.Log("5 sec");
            currentWave++;
        }
        else if (currentWave == 1)
        {
            Debug.Log("10 sec");
            currentWave++;
        }
    }
}
