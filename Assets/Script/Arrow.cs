using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    float xSpeed;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * arrowSpeed;

    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,0);
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
            Destroy(gameObject);
    }
}
