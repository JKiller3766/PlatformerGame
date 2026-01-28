using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerJumper : MonoBehaviour
{
    public float JumpHeight;
    public float DistanceToMaxHeight;
    public float SpeedHorizontal;
    public float PressTimeToMaxJump;
    [SerializeField] public bool DoubleJumpEnabled = false;
    public bool SingleJump = false;

    public float WallSlideSpeed = 1;
    public ContactFilter2D filter;

    private Rigidbody2D rigidbody;
    private CollisionDetection collisionDetection;
    private float lastVelocityY;
    private float jumpStartedTime;

    bool IsWallSliding => collisionDetection.IsTouchingFront || collisionDetection.IsTouchingBack;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
    }

    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();
        if (IsWallSliding) SetWallSlide();
    }

    public void OnJumpStarted()
    {
        if ((collisionDetection.IsGrounded) || (collisionDetection.IsTouchingFront) || (collisionDetection.IsTouchingBack))
        {
            Jump();
            SingleJump = true;
        } 
        else {
            if (DoubleJumpEnabled && SingleJump)
            {
                Jump();
                SingleJump = false;
            }
        }
    }

    public void Jump()
    {
        SetGravity();
        var velocity = new Vector2(rigidbody.linearVelocity.x, GetJumpForce());
        rigidbody.linearVelocity = velocity;
        jumpStartedTime = Time.time;
    }

    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - jumpStartedTime) / PressTimeToMaxJump);
        rigidbody.gravityScale *= fractionOfTimePressed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float h = -GetDistanceToGround() + JumpHeight;
        Vector3 start = transform.position + new Vector3(-1, h, 0);
        Vector3 end = transform.position + new Vector3(1, h, 0);
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }

    private bool IsPeakReached()
    {
        bool reached = ((lastVelocityY * rigidbody.linearVelocity.y) < 0);
        lastVelocityY = rigidbody.linearVelocity.y;
        return reached;
    }

    private void SetWallSlide()
    {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 
        Mathf.Max(rigidbody.linearVelocity.y, -WallSlideSpeed));
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        rigidbody.gravityScale = grav / 9.81f;
    }

    private void TweakGravity()
    {
        rigidbody.gravityScale *= 1.2f;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D[] hit = new RaycastHit2D[3];
        Physics2D.Raycast(transform.position, Vector2.down, filter, hit, 10);
        return hit[0].distance;
    }

    private void OnEnable()
    {
        JumpUpgrade.OnJumpUpgradePickUp += UpgradeJump;
        DoubleJumpUpgrade.OnDoubleJumpUpgradePickUp += EnableDoubleJump;
    }

    private void OnDisable()
    {
        JumpUpgrade.OnJumpUpgradePickUp -= UpgradeJump;
        DoubleJumpUpgrade.OnDoubleJumpUpgradePickUp -= EnableDoubleJump;
    }

    private void UpgradeJump()
    {
        JumpHeight *= 1.5f;
    }

    private void EnableDoubleJump()
    {
        DoubleJumpEnabled = true;
    }
}
