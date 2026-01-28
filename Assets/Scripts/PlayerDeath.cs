using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private string hazardTag = "Hazard";
    [SerializeField] private string killZoneTag = "KillZone";
    [SerializeField] private bool disableControlsOnDeath = true;

    private bool isDead = false;

    private PlayerMove playerMove;
    private PlayerJumper playerJumper;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerJumper = GetComponent<PlayerJumper>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other.CompareTag(hazardTag) || other.CompareTag(killZoneTag)) Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.collider.CompareTag(hazardTag) || collision.collider.CompareTag(killZoneTag)) Die();
    }

    private void Die()
    {
        isDead = true;

        if (disableControlsOnDeath)
        {
            if (playerMove != null) playerMove.enabled = false;
            if (playerJumper != null) playerJumper.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Ending.GoToEnding(Ending.EndingType.Death);
    }
}

