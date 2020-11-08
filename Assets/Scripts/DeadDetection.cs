using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDetection : MonoBehaviour
{
    private Player player;
    private Camera cam;

    private void Start () {
        player = Player.Instance;
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        bool dead = false;
        if(screenPos.x < 0){
            //transform.position = new Vector2(0, transform.position.y);
            dead = true;
        }else if(screenPos.x > Screen.width){
            //transform.position = new Vector2(Screen.width, transform.position.y);
            dead = true;
        }else if(screenPos.y < 0){
            //transform.position = new Vector2(transform.position.x, 0);
            dead = true;
        }else if(screenPos.y > Screen.height){
            //transform.position = new Vector2(transform.position.x, Screen.height);
            dead = true;
        }

        if(dead) player.Dead();
    }
}
