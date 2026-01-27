using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : MonoBehaviour
{
    public static Action OnDoubleJumpUpgradePickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpgradeJump();
    }

    private void UpgradeJump()
    {
        OnDoubleJumpUpgradePickUp?.Invoke();
        Destroy(gameObject);
    }
}
