using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlatform;
    [SerializeField]
    private LayerMask whatIsRoof;
    [SerializeField]
    private LayerMask whatIsWall;

    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private Transform frontCheckPoint;
    [SerializeField]
    private Transform backCheckPoint;
    [SerializeField]
    private Transform roofCheckPoint;

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
        Gizmos.DrawWireSphere(groundCheckPoint.position, checkRadius);
        Gizmos.DrawWireSphere(frontCheckPoint.position, checkRadius);
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
        var colliders = Physics2D.OverlapCircleAll(frontCheckPoint.position, checkRadius, whatIsWall);

        isTouchingFront = (colliders.Length > 0);
    }

    private void CheckBack()
    {
        var colliders = Physics2D.OverlapCircleAll(backCheckPoint.position, checkRadius, whatIsWall);

        isTouchingBack = (colliders.Length > 0);
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(roofCheckPoint.position, checkRadius, whatIsRoof);

        isTouchingRoof = (colliders.Length > 0);
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheckPoint.position, checkRadius, whatIsGround);

        isGrounded =  (colliders.Length > 0);
    }

    private void CheckPlatformed()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheckPoint.position, checkRadius, whatIsPlatform);

        isPlatformGround = (colliders.Length > 0);
		
        if (isPlatformGround) CurrentPlatform = colliders[0].transform;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, 100, whatIsGround);

        distanceToGround = hit.distance;
		
        groundAngle = Vector2.Angle(hit.normal,new Vector2(1,0));
    }
}
