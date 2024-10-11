using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM : MonoBehaviour
{
    public static GM instance;

    [SerializeField] public PlaneInput _input;

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
}
