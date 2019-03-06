﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed, jump;

    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool canJump = true, canDrop = true;
    private float height;
    private int layerFloor, moveDir;
    private Animator anim;
    private bool isDead = false;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
            height = coll.bounds.extents.y + 0.05f;
            layerFloor = 1 << LayerMask.NameToLayer("Floor");
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void OnEnable()
    {
        isDead = false;
        anim.SetTrigger("Spawn");
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        anim.SetBool("isGround", isGround());
        Move();
        Attack();
        if (isGround() && canJump && CheckPlayerYPos() == "Over")
        {
            canJump = false;
            StartCoroutine(Jump());
        }
        if (isGround() && canDrop && CheckPlayerYPos() == "Under")
        {
            canDrop = false;
            StartCoroutine(Drop());
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 0.3f)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            anim.SetTrigger("Attack");
            Invoke("Die", 2);
        }
    }

    void Die()
    {
        //Add to pool
        Destroy(gameObject);
    }

    void Move()
    {
        if (isGround())
            moveDir = PlayerPosX();
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);

        int dir = PlayerPosX();
        if (dir != 0)
            transform.localScale = new Vector3(dir, 1, 1);
    }

    IEnumerator Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }

    IEnumerator Drop()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(0.4f);
        coll.enabled = true;
        yield return new WaitForSeconds(4);
        canDrop = true;
    }

    int PlayerPosX()
    {
        float diff = player.transform.position.x - transform.position.x;
        if (diff > 0.1f)
            return 1;
        else if (diff < -0.1f)
            return -1;
        else
            return 0;
    }

    string CheckPlayerYPos()
    {
        float diff = player.transform.position.y - transform.position.y;
        if (diff > 3)
            return "Over";
        else if (diff < -3)
            return "Under";
        else
            return "Same";
    }

    public bool isGround()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - height);
        bool bottom = Physics2D.Raycast(pos, -Vector2.up, 0.2f, layerFloor);
        Debug.DrawRay(pos, -Vector2.up * 0.2f, Color.red);
        return bottom;
    }
}