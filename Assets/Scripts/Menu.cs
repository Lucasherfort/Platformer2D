using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject PlayerUI;

    [SerializeField]
    private GameObject CanvasPause;
    private bool firstTimePlay = false;

    private void Start()
    {

        InputManager.Input.UI.Pause.performed += Pause;
        CanvasPause.SetActive(false);
        Player.SetActive(false);
        PlayerUI.SetActive(true);
    }

    public void Play()
    {
        PlayerUI.SetActive(false);
        Player.SetActive(true);
        CanvasPause.SetActive(true);

        if(!firstTimePlay)
        {
            firstTimePlay = true;
            if(GraphicsFeedbacksSettings.Instance.ActiveSpawnEffect) Player.GetComponent<Player>().PlaySpawnParticles();            
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Pause(InputAction.CallbackContext cbc)
    {
        Time.timeScale = 0.0f;
        Player.SetActive(false);
        PlayerUI.SetActive(true);
    }

    public void CanvasPauseEvent()
    {
        Time.timeScale = 0.0f;
        Player.SetActive(false);
        PlayerUI.SetActive(true);
    }
}
