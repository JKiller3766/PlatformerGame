using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsPlatform;
    [SerializeField]
    private LayerMask WhatIsRoof;
    [SerializeField]
    private LayerMask WhatIsWall;

    [SerializeField]
    private Transform GroundCheckPoint;
    [SerializeField]
    private Transform FrontCheckPoint;
    [SerializeField]
    private Transform BackCheckPoint;
    [SerializeField]
    private Transform RoofCheckPoint;

    public Transform CurrentPlatform;

    private float checkRadius = 0.15f;
    private bool wasGrounded;

    [SerializeField]
    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded || isPlatformGround; } }

    [SerializeField]
    private bool isTouchingFront;
    public bool IsTouchingFront { get { return isTouchingFront; } }

    [SerializeField]
    private bool isTouchingBack;
    public bool IsTouchingBack { get { return isTouchingBack; } }

    [SerializeField]
    private bool isPlatformGround;
    public bool IsPlatForm { get { return isPlatformGround; } }

    [SerializeField]
    private bool isTouchingRoof;
    public bool IsTouchingRoof { get { return isTouchingRoof; } }

    [SerializeField]
    private float distanceToGround;
    public float DistanceToGround { get { return distanceToGround; } }

    [SerializeField]
    private float groundAngle;
    public float GroundAngle { get { return groundAngle; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, checkRadius);
        Gizmos.DrawWireSphere(FrontCheckPoint.position, checkRadius);
        Gizmos.color = Color.white;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        CheckPlatformed();
        CheckFront();
        CheckBack();
        CheckRoof();
    }

    private void CheckFront()
    {
        var colliders = Physics2D.OverlapCircleAll(FrontCheckPoint.position, checkRadius, WhatIsWall);

        isTouchingFront = (colliders.Length > 0);
    }

    private void CheckBack()
    {
        var colliders = Physics2D.OverlapCircleAll(BackCheckPoint.position, checkRadius, WhatIsWall);

        isTouchingBack = (colliders.Length > 0);
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position, checkRadius, WhatIsRoof);

        isTouchingRoof = (colliders.Length > 0);
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, checkRadius, WhatIsGround);

        isGrounded =  (colliders.Length > 0);
    }

    private void CheckPlatformed()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, checkRadius, WhatIsPlatform);

        isPlatformGround = (colliders.Length > 0);
        if (isPlatformGround) CurrentPlatform = colliders[0].transform;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, 100, WhatIsGround);

        distanceToGround = hit.distance;
        groundAngle = Vector2.Angle(hit.normal,new Vector2(1,0));
    }
}
