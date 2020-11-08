using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private PlayerConfiguration playerConfiguration;

    private Movement movement;
    private bool onWallTolerance = false;
     private Coroutine onWallToleranceCoroutine;
    private TrailRenderer trail;

    [HideInInspector]
    public int jumpLeft;
    [HideInInspector]
    public bool onGround;

    private bool canCanceledYVelocity = false;

    [HideInInspector]
    public bool jumpActivated = false;
    private bool jumpActivatedButCancelled = false;
    private Coroutine jumpActivationCoroutine;

    private bool onGroundTolerance = false;
    private Coroutine onGroundToleranceCoroutine;
	
	private void Awake () {
        playerConfiguration = Player.Instance.configPreset;
		movement = GetComponent<Movement>();
        trail = GetComponent<TrailRenderer>();
	}

    private void Start()
    {
        InputManager.Input.Player.Jump.performed += JumpDown;
        InputManager.Input.Player.Jump.canceled += JumpUp;

        EventManager.Instance.PlayerOnGround += onCollide;
    }

    private void onCollide(bool isOnGround)
    {
        if(isOnGround){
            SoundBox.Instance.PlayHitGroundSound();

            jumpLeft = playerConfiguration.nbJump;
            trail.startColor = Color.red;
            canCanceledYVelocity = false;
            if(jumpActivated) JumpDown(new InputAction.CallbackContext());
        }else{
            if(jumpLeft == playerConfiguration.nbJump) jumpLeft--;
        }

        onGround = isOnGround;
    }

    private void Update () {
        //TODO Bon pas ouf mais c'est pour gagner du temps
        // à placer dans un setter si projet poursuivie
        if(onWallTolerance && jumpActivated){
            JumpDown(new InputAction.CallbackContext());
            jumpActivated = false;
            onWallTolerance = false;
            if(onWallToleranceCoroutine != null) StopCoroutine(OnWallToleranceCoroutine());
        }

        if(movement.onWall && onWallToleranceCoroutine != null){
            onWallTolerance = true;
            onWallToleranceCoroutine = null;
        }

        if(!movement.onWall && onWallToleranceCoroutine == null){
            onWallToleranceCoroutine = StartCoroutine(OnWallToleranceCoroutine());
        }

        if(onGroundTolerance && jumpActivated){
            JumpDown(new InputAction.CallbackContext());
            jumpActivated = false;
            onGroundTolerance = false;
            if(onGroundToleranceCoroutine != null) StopCoroutine(OnGroundToleranceCoroutine());
        }

        if(onGround && onGroundToleranceCoroutine != null){
            onGroundTolerance = true;
            onGroundToleranceCoroutine = null;
        }

        if(!onGround && onGroundToleranceCoroutine == null){
            onGroundToleranceCoroutine = StartCoroutine(OnGroundToleranceCoroutine());
        }
    }
 
    private void OnDestroy()
    {
        InputManager.Input.Player.Jump.performed -= JumpDown;
        InputManager.Input.Player.Jump.canceled -= JumpUp;

        EventManager.Instance.PlayerOnGround -= onCollide;
    }

    private void JumpDown(InputAction.CallbackContext context)
    {
        if(onWallTolerance){
            movement.velocity.y = playerConfiguration.jumpForce * 0.8f;
            movement.velocity.x = playerConfiguration.jumpForce * movement.onWallSens;
            movement.inertiaModifier = -playerConfiguration.inertiaSoustractor;

            SoundBox.Instance.PlayJumpSound();

            return;
        }

        if(jumpLeft > 0){
            if(!onGroundTolerance) --jumpLeft;
            movement.velocity.y = playerConfiguration.jumpForce;

            if(jumpActivatedButCancelled) movement.velocity.y /= playerConfiguration.jumpCancelVelocityDivisor;

            if(jumpLeft == 0){
                trail.startColor = Color.gray;
                SoundBox.Instance.PlayJumpSecondSound();
                canCanceledYVelocity = false;
            }else{
                SoundBox.Instance.PlayJumpSound();
                canCanceledYVelocity = true;
            }

            jumpActivated = false;
        }else{
            if(jumpActivated) StopCoroutine(jumpActivationCoroutine);
            jumpActivationCoroutine = StartCoroutine(JumpToleranceCoroutine());
        }

        jumpActivatedButCancelled = false;
    }

    private void JumpUp(InputAction.CallbackContext context)
    {
        if(jumpActivated) jumpActivatedButCancelled = true;

        if(canCanceledYVelocity && movement.velocity.y > 0){
            movement.velocity.y /= playerConfiguration.jumpCancelVelocityDivisor;
        }
    }

    private IEnumerator JumpToleranceCoroutine () {
        jumpActivated = true;
        yield return new WaitForSeconds(playerConfiguration.jumpActivationTimeToleranceInSurface);
        jumpActivated = false;
    }

    private IEnumerator OnWallToleranceCoroutine () {
        yield return new WaitForSeconds(playerConfiguration.jumpActivationTimeToleranceOutSurface);
        onWallTolerance = false;
    }

    private IEnumerator OnGroundToleranceCoroutine () {
        yield return new WaitForSeconds(playerConfiguration.jumpActivationTimeToleranceOutSurface);
        onGroundTolerance = false;
    }
}
