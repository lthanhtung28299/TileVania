using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinAudio;
    [SerializeField] int pointsForcointPickup = 100;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameManager>().AddScore(pointsForcointPickup);
            AudioSource.PlayClipAtPoint(coinAudio,Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }
}
