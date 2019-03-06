﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private float height;
    private int layerFloor;
    private Rigidbody2D rb;
    public float speed, jump;
    private Animator anim;
    private bool canSlash = true;
    public float slashSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D coll = GetComponent<Collider2D>();
        height = coll.bounds.extents.y + 0.05f;
        layerFloor = 1 << LayerMask.NameToLayer("Floor");
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetBool("isGround", isGround());
        Move();
        if (input().y > 0 && isGround())
            rb.velocity = new Vector2(rb.velocity.x, jump);
        Attacks();
    }

    void Move()
    {
        bool running = (input().x != 0) ? true : false;
        anim.SetBool("Running", running);
        if (isGround())
            rb.velocity = new Vector2(input().x * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(input().x * speed * 0.75f, rb.velocity.y);
        float dir = (float)input().x;
        if (dir != 0)
            transform.localScale = new Vector3(dir, 0.25f, 0.25f);
    }

    void Attacks()
    {
        /*if (Input.GetMouseButtonDown(0) && canShoot)
        {
            canShoot = false;
            canSlash = false;
            StartCoroutine(Shoot());
        }*/
        if (Input.GetMouseButtonDown(0) && canSlash)
        {
            //canShoot = false;
            canSlash = false;
            StartCoroutine(Slash());
        }
    }

    /*IEnumerator Shoot()
    {
        //Instantiate  Bullet
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
        canSlash = true;
    }*/

    IEnumerator Slash()
    {
        //Instantiate  slash effect
        //check colliders
        //Do damage
        anim.SetTrigger("Slash");
        yield return new WaitForSeconds(slashSpeed);
        //canShoot = true;
        canSlash = true;
    }

    Vector2 playerPos(float xFix = 0)
    {
        return new Vector2(transform.position.x + xFix, transform.position.y - height);
    }

    public bool isGround()
    {
        bool bottom1 = Physics2D.Raycast(playerPos(), -Vector2.up, 0.2f, layerFloor);
        bool bottom2 = Physics2D.Raycast(playerPos(0.4f), -Vector2.up, 0.2f, layerFloor);
        bool bottom3 = Physics2D.Raycast(playerPos(-0.4f), -Vector2.up, 0.2f, layerFloor);
        Debug.DrawRay(playerPos(), -Vector2.up * 0.2f, Color.red);
        Debug.DrawRay(playerPos(0.4f), -Vector2.up * 0.2f, Color.red);
        Debug.DrawRay(playerPos(-0.4f), -Vector2.up * 0.2f, Color.red);
        if (bottom1 || bottom2 || bottom3)
            return true;
        else
            return false;
    }

    public Vector2 input()
    {
        Vector2 _input = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
                _input.x = -0.25f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
                _input.x = 0.25f;
        }
        else
            _input.x = 0;

        if (Input.GetKeyDown(KeyCode.Space))
            _input.y = 1;

        return _input;
    }
}