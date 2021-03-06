using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] public int EnemyPoints = 100;

    Rigidbody2D myRigidbody;
    SpriteRenderer mySprite;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed,0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

}
