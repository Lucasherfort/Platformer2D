using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsFeedbacksSettings : MonoBehaviour
{
    public static GraphicsFeedbacksSettings Instance {get; private set;}

    public GameObject MainMenu = null;

    public bool ActivePlayerAnimation {get; set;} = true;
    public bool ActivePlayerTrail {get; set;} = true;
    public bool ActiveDeadEffect {get; set;} = true;
    public bool ActiveSpawnEffect {get; set;} = true;

    public float JiggleSpeed {get; set;} = 5;
    public float JiggleScaleVariation {get; set;} = 0.25f;
    public float BounceJumpScaleVariation {get; set;} = 2.6f;
    public float BounceJumpSpeed {get; set;} = 2.5f;

    private void Awake () {
        if(Instance){
            Destroy(this);
            return;
        }

        Instance = this;

        gameObject.SetActive(false);
    }

    private void OnDestroy () {
        if(Instance == this) Instance = null;
    }

    public void Show () {
        MainMenu.SetActive(false);
        gameObject.SetActive(true);
    }

    public void UnShow () {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
