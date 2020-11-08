using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionBehavior : MonoBehaviour
{
    public float repulseScale = 0.55f;
    public float detectionDistance = 0.5f;

	private Movement movement;
    private Collider2D ownedCollider;
	
    private bool onGround = false;

    private MovablePlateform mobileMoveWith;
    private Tapis tapisMoveWith;
	
	private void Awake () {
			movement = GetComponent<Movement>();
            ownedCollider = GetComponent<Collider2D>();
	}

    private void FixedUpdate () {
        Vector2 velocity = movement.velocity;
        //Vector2 nextMovement = velocity * Time.fixedDeltaTime;
        Vector2 pos = transform.position;

        /*RaycastHit2D[] hits = new RaycastHit2D[10];
        int numHit = ownedCollider.Cast(nextMovement, hits, nextMovement.magnitude); 

        /*if(numHit != 0){
            RaycastHit2D firstHit = hits[0];
            if(Mathf.Abs(firstHit.normal.x) == 1){
                velocity.x = 0;
                pos.x = firstHit.point.x + 0.51f * (firstHit.normal.x > 0 ? 1 : -1);
            }
            if(Mathf.Abs(firstHit.normal.y) == 1){
                velocity.y = 0;
                pos.y = firstHit.point.y + 0.51f * (firstHit.normal.y > 0 ? 1 : -1);
            }
        }*/

        bool onWall = false;
        int onWallSens = movement.onWallSens;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        bool hit = false;
        if(velocity.x > 0 && ownedCollider.Cast(Vector2.right, hits, detectionDistance) != 0 && hits[0].normal == Vector2.left){
            velocity.x = 0;
            pos.x = hits[0].point.x - repulseScale;
            onWall = true;
            onWallSens = -1;
            hit = true;
        }
        if(velocity.x < 0 && ownedCollider.Cast(Vector2.left, hits, detectionDistance) != 0 && hits[0].normal == Vector2.right){
            velocity.x = 0;
            pos.x = hits[0].point.x + repulseScale;
            onWall = true;
            onWallSens = 1;
            hit = true;
        }
        if(velocity.y > 0 && ownedCollider.Cast(Vector2.up, hits, detectionDistance) != 0 && hits[0].normal == Vector2.down){
            velocity.y = 0;
            pos.y = hits[0].point.y - repulseScale;
            hit = true;
        }

        bool groundUnder = ownedCollider.Cast(Vector2.down, hits, detectionDistance) != 0;
        if(velocity.y < 0 && groundUnder && hits[0].normal == Vector2.up){
            pos.y = hits[0].point.y + repulseScale;
            velocity.y = 0;
            hit = true;
        }

        movement.onWall = onWall;
        movement.onWallSens = onWallSens;
        movement.velocity = velocity;
        transform.position = pos;

        if(hit) CollideWithGameObject(hits[0].collider.gameObject, hits[0].normal);
    }

    private void CollideWithGameObject (GameObject obj, Vector3 normal){
        EventManager.Instance.PlayerCollideWithGameObject?.Invoke(obj, normal);
        
        if(obj.tag == "Goal") {
            EventManager.Instance.Arrive(obj);
        }
    }

    private void Update () {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        bool groundUnder = ownedCollider.Cast(Vector2.down, hits, detectionDistance) != 0;
        if(!onGround && groundUnder){
            GameObject obj = hits[0].collider.gameObject;
            if(obj.tag == "ReboundArea" || (obj.transform.position - transform.position).y > 0) return;

            onGround = true;
            EventManager.Instance.PlayerOnGround?.Invoke(true);
            if(mobileMoveWith) mobileMoveWith.UnaffectPlayer();
            if(tapisMoveWith) tapisMoveWith.UnaffectPlayer();

            if(obj.tag == "Mobile"){
                mobileMoveWith = obj.GetComponent<MovablePlateform>();
                mobileMoveWith.AffectPlayer(transform);
            }else if(obj.tag == "Tapis"){
                tapisMoveWith = obj.GetComponent<Tapis>();
                tapisMoveWith.AffectPlayer(transform);
            }
            
        }else if(onGround && !groundUnder){
            onGround = false;
            EventManager.Instance.PlayerOnGround?.Invoke(false);
            if(mobileMoveWith) mobileMoveWith.UnaffectPlayer();
            if(tapisMoveWith) tapisMoveWith.UnaffectPlayer();
        }
    }

    private bool NormalIsGround (Vector3 normal){
        return normal == Vector3.up;
    }
}
