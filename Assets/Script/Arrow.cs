using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    float xSpeed;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    EnemyMovement enemy;
    void Start()
    {
        enemy = FindObjectOfType<EnemyMovement>();
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
            FindObjectOfType<GameManager>().AddScore(enemy.EnemyPoints);
        }
            Destroy(gameObject);
    }
}
