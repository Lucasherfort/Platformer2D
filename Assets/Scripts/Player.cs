using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    public PlayerConfiguration configPreset;

    public Vector3 spawnPos;
    private Movement movement;
    private Gravity gravity;
    private CollisionBehavior colBehavior;
    private Collider2D col;
    private SpriteRenderer rend;
    private TrailRenderer trail;
    private DeadDetection deadDetection;

    [SerializeField]
    private GameObject SpawnParticuleSystem;

    [SerializeField]
    private GameObject DeadParticuleSystem;

    private void Awake () {
        if(Instance){
            Destroy(this);
            return;
        }

        Instance = this;

        spawnPos = transform.position;
        movement = GetComponent<Movement>();
        gravity = GetComponent<Gravity>();
        colBehavior = GetComponent<CollisionBehavior>();
        col = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        deadDetection = GetComponent<DeadDetection>();
    }

    private void OnDestroy () {
        if(Instance == this) Instance = null;
    }

    public void Dead () {
        SoundBox.Instance.PlayDeadSound();

        if(GraphicsFeedbacksSettings.Instance.ActiveDeadEffect) PlayDeadParticles();

        trail.emitting = false;
        trail.Clear();
        movement.enabled = false;
        gravity.enabled = false;
        colBehavior.enabled = false;
        col.enabled = false;
        rend.enabled = false;
        deadDetection.enabled = false;

        movement.velocity = Vector2.zero;

        StartCoroutine(WaitRespawnTime());
    }

    public void Spawn () {
        SoundBox.Instance.PlayTeleportSound();

        transform.position = spawnPos;
        CameraFollow.Instance.Reset();

        if(GraphicsFeedbacksSettings.Instance.ActiveSpawnEffect) PlaySpawnParticles();

        movement.enabled = true;
        gravity.enabled = true;
        colBehavior.enabled = true;
        col.enabled = true;
        rend.enabled = true;
        deadDetection.enabled = true;
        trail.emitting = true;
        trail.Clear();
    }

    public IEnumerator WaitRespawnTime () {
        yield return new WaitForSeconds(configPreset.respawnTime);
        Spawn();
    }

    public void PlaySpawnParticles()
    {
        var PS = Instantiate(SpawnParticuleSystem,transform.position,Quaternion.identity);
        Destroy(PS,2f);
    }

    private void PlayDeadParticles()
    {
        var PS = Instantiate(DeadParticuleSystem,transform.position,Quaternion.identity);
        Destroy(PS,2f);
    }

    public void SetTrailTime (float time) {
        trail.time = time;
    }

    public void SetTrailWidth (float width) {
        trail.startWidth = width;
    }
}
