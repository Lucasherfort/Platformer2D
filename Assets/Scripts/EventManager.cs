using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EventManager
{
    private static EventManager instance;
    public static EventManager Instance {
        get {
            if(instance == null){
                instance = new EventManager();
            }

            return instance;
        }
    }

    private SpriteRenderer checkpointRend = null;

    //Event List
    public Action<bool> PlayerOnGround;
    public Action<GameObject, Vector3> PlayerCollideWithGameObject;

    public void Arrive (GameObject checkpoint) {
        //int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //SceneManager.LoadScene(nextIndex < SceneManager.sceneCountInBuildSettings ? nextIndex : 0);

        SpriteRenderer newCheckpointRend = checkpoint.GetComponent<SpriteRenderer>();

        if(newCheckpointRend == checkpointRend) return;
        if(checkpointRend) checkpointRend.color = Color.magenta;
        checkpointRend = newCheckpointRend;
        
        SoundBox.Instance.PlayCheckPointSound();
        checkpointRend.color = Color.green;

        Player.Instance.spawnPos = checkpoint.transform.position + Vector3.up * 2;
    }
}
