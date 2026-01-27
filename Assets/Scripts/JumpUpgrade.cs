using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpgrade : MonoBehaviour
{
    public static Action OnJumpUpgradePickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpgradeJump();
    }

    private void UpgradeJump()
    {
        OnJumpUpgradePickUp?.Invoke();
        Destroy(gameObject);
    }
}
