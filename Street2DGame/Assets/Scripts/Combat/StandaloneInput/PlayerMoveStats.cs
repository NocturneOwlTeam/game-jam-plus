using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerMovement", menuName = "Scriptable Objects/Player Movement")]
public class PlayerMoveStats : ScriptableObject
{
    [Header("Movement")]
    public float speed = 10f;

    public float sprintSpeed = 16f;

    public float accelerationSpeed = 13f;

    public float deaccelerationSpeed = 16f;

    public bool usesStamina = false;

    [Header("Jump")]
    public float jumpForce = 13f;

    public float accelerationAirSpeed = 13f;

    public float deaccelerationAirSpeed = 16f;

    [Range(0, 0.99f)]
    public float jumpCutMultiplier = 0.4f;

    public float coyoteTime = 0.15f;

    public float jumpBufferTime = 0.1f;

    public float gravityScale = 1f;

    public float customGravityMultiplier = 2f;

    public float maxFallSpeed = 50f;

    public bool canDoubleJump = false;

    //Can be used for the infinite jump too.
    public float doubleJumpDelay = 0.1f;

    //Like Metroid's Space Jump power up
    public bool canInfiniteJump = false;

    //TODO: complete this, preferably after the states refactor, to make it more versatile.
    [Header("Dash")]
    public bool canDash = false;

    public void ResetStats()
    {
        //Horizontal movement
        speed = 10f;
        sprintSpeed = 16f;
        accelerationSpeed = 13f;
        deaccelerationSpeed = 16f;
        usesStamina = false;

        //Jump stats
        jumpForce = 13f;
        accelerationAirSpeed = 13f;
        deaccelerationAirSpeed = 16f;
        jumpCutMultiplier = 0.4f;
        coyoteTime = 0.15f;
        jumpBufferTime = 0.1f;
        gravityScale = 1f;
        customGravityMultiplier = 2f;
        maxFallSpeed = 50f;
        canDoubleJump = false;
        doubleJumpDelay = 0.1f;
        canInfiniteJump = false;
    }
}