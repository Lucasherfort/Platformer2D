using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlateform : MonoBehaviour
{
    public Vector2 direction;
    public float distance;
    public float speed;
    public bool invert = false;

    private Vector2 initPos;

    private Transform playerTransform;

    private void Awake () {
        initPos = transform.position;
        direction = direction.normalized;
    }

    private void FixedUpdate () {
        Vector3 oldPos = transform.position;
        float inversion = invert ? -1 : 1;
        transform.position = Vector2.Lerp(initPos, (initPos + direction * distance), (1 + Mathf.Sin(Time.time * speed * inversion)) / 2f);

        if(playerTransform) playerTransform.position += transform.position - oldPos;
    }

    public void AffectPlayer (Transform t) {
        playerTransform = t;
    }

    public void UnaffectPlayer () {
        playerTransform = null;
    }
}
