using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configuration/PlayerConfiguration", order = 0)]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Horizontal Movement")]
    [Min(0)]
    public float speed = 5;
    [Range(1, 2)]
    public float inertia = 1.3f;

    [Header("Gravity Physics")]
    public float gravityForce = -50f;
    [Min(0)]
	public float maxFallSpeed = 15f;

    [Header("Jump")]
    [Min(0)]
    public int nbJump = 2;
    [Min(0)]
    public float jumpForce = 17;
    [Min(0)]
    public float jumpCancelVelocityDivisor = 3;
    [Min(0)]
    public float jumpActivationTimeToleranceInSurface = 0.15f;
    public float jumpActivationTimeToleranceOutSurface = 0.15f;

    [Header("Wall")]
    [Min(0)]
	public float maxFallOnWallSpeed = 5f;

    [Header("Bounce Plateform")]
    [Min(0)]
    public float reboundCoefficientFail = 5;
    [Min(0)]
    public float reboundCoefficientSucces = 20;
    [Range(0, 1)]
    public float inertiaSoustractor = 0.3f;
    public float inertiaSoustractingTime = 1f;

    [Header("Respawn")]
    public float respawnTime = 1;


}
