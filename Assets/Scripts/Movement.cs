using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerConfiguration playerConfiguration;

	[HideInInspector]
	public Vector2 velocity;

    public bool onWall = false;
    public int onWallSens = 0;
    
    public float inertiaModifier = 0;

    private float horizontal;

    private void Awake () {
        playerConfiguration = Player.Instance.configPreset;
    }

    void Start()
    {
        InputManager.Input.Player.Move.performed += Move;
    }

    void OnDestroy () {
        InputManager.Input.Player.Move.performed -= Move;
    }

    private void Move (InputAction.CallbackContext cbc) {
        horizontal = cbc.ReadValue<Vector2>().x;
    }

    public void FixedUpdate () {
        velocity.x += horizontal * playerConfiguration.speed * ((playerConfiguration.inertiaSoustractor + inertiaModifier) * (1 / playerConfiguration.inertiaSoustractor));
        velocity.x /=  (playerConfiguration.inertia + inertiaModifier);
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.fixedDeltaTime;

        inertiaModifier = Mathf.Lerp(inertiaModifier, 0, Time.fixedDeltaTime / playerConfiguration.inertiaSoustractingTime);
    }
}
