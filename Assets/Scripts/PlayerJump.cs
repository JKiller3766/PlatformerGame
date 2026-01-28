using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private AudioManager audioManager;

            private Rigidbody2D rb;
    private CollisionDetection collisionDetection;
    private float lastVelocityY;
    private float jumpStartedTime;

    public float MaxFallSpeed = 12f;

    bool IsWallSliding => collisionDetection.IsTouchingFront || collisionDetection.IsTouchingBack;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
    }

    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();

        if (IsWallSliding) SetWallSlide();

        if (!collisionDetection.IsGrounded) LimitFallSpeed();
    }

    public void OnJumpStarted()
    {
        audioManager.PlaySFX(audioManager.jump);

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
        var velocity = new Vector2(rb.linearVelocity.x, GetJumpForce());
        rb.linearVelocity = velocity;
        jumpStartedTime = Time.time;
    }

    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - jumpStartedTime) / PressTimeToMaxJump);
        rb.gravityScale *= fractionOfTimePressed;
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
        bool reached = ((lastVelocityY * rb.linearVelocity.y) < 0);
        lastVelocityY = rb.linearVelocity.y;
        return reached;
    }

    private void SetWallSlide()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 
        Mathf.Max(rb.linearVelocity.y, -WallSlideSpeed));
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        rb.gravityScale = grav / 9.81f;
    }

    private void TweakGravity()
    {
        rb.gravityScale *= 1f;
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

    private void LimitFallSpeed()
    {
        if (rb.linearVelocity.y < -MaxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -MaxFallSpeed);
        }
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
