using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileEffect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        FindObjectOfType<PlayerMovement>().SlowDown();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        FindObjectOfType<PlayerMovement>().EscapeSlowDown();
    }
}
