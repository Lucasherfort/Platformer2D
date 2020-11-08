using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance {get; private set;}

    public float speed = 1;

    private Transform player;

    private void Awake () {
        if(Instance){
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnDestroy () {
        if(Instance == this) Instance = null;
    }

    private void Start () {
        player = Player.Instance.transform;
    }

    private void FixedUpdate () {
        Vector3 nextPos = new Vector3(player.position.x, player.position.y < 0 ? 0 : player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, nextPos, Time.fixedDeltaTime * speed);
    }

    public void Reset () {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
} 
