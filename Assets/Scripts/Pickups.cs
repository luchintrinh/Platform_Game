using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int coinValue = 100;
    GameSession gameSes;
    bool wasCollect = false;
    private void Start()
    {
        gameSes = FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollect)
        {
            wasCollect = true;
            gameSes.AddToScore(coinValue);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
