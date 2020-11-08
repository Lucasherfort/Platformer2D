using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private PlayerConfiguration playerConfiguration;
	
	private Movement movement;
	
	private void Awake () {
		playerConfiguration = Player.Instance.configPreset;
		movement = GetComponent<Movement>();
	}
	
	private void Start () {
		EventManager.Instance.PlayerOnGround += OnGround;
	}
	
	private void OnDestroy () {
		EventManager.Instance.PlayerOnGround -= OnGround;
	}
	
	private void OnGround (bool isOnGround) {
		enabled = !isOnGround;
	}
	
    // Update is called once per frame
    void FixedUpdate()
    {
        movement.velocity.y += playerConfiguration.gravityForce * Time.fixedDeltaTime;
		movement.velocity.y = Mathf.Clamp(movement.velocity.y, movement.onWall ? -playerConfiguration.maxFallOnWallSpeed : -playerConfiguration.maxFallSpeed, Mathf.Infinity);
    }


}
