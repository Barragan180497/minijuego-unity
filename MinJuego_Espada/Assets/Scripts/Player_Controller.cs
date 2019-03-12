using System.Collections;
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
    public bool isDead = false;
    Enemy pc;
    private bool movement = true;
    private SpriteRenderer spr;
    private bool jumping;
    public bool derecha = false;
    public bool izquierda = false;
    public bool saltar = false;
    public bool atacar = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D coll = GetComponent<Collider2D>();
        height = coll.bounds.extents.y + 0.05f;
        layerFloor = 1 << LayerMask.NameToLayer("Floor");
        anim = GetComponent<Animator>();
        pc = GameObject.FindObjectOfType<Enemy>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        anim.SetBool("isGround", isGround());
        Move();
        if (input().y > 0 && isGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        Attacks();

        if (jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * 6.5f, ForceMode2D.Impulse);
            jumping = false;
        }
    }

    public void OnBecameInvisible()
    {
        transform.position = new Vector3(-3.6f, -0.121f, 0);
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        isDead = true;
        PosInicialEnemy();
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        jumping = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb.AddForce(Vector2.left * side * 7f, ForceMode2D.Impulse);
    }

    public void PosInicialEnemy()
    {
        pc.transform.position = new Vector3(3.6f, -0.121f, 0);
        pc.transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
    }

    void Move()
    {
        bool running = (input().x != 0) ? true : false;
        anim.SetBool("Running", running);
        if (isGround())
        {
            rb.velocity = new Vector2(input().x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(input().x * speed * 0.75f, rb.velocity.y);
        }
        float dir = (float)input().x;
        if (dir != 0)
        {
            transform.localScale = new Vector3(dir, 0.25f, 0.25f);
        }
    }

    void Attacks()
    {
        /*if (Input.GetMouseButtonDown(0) && canSlash)
        {
            //canShoot = false;
            canSlash = false;
            StartCoroutine(Slash());
        }*/
        if (atacar && canSlash)
        {
            canSlash = false;
            StartCoroutine(Slash());
        }
    }

    IEnumerator Slash()
    {
        //Instantiate  slash effect
        //check colliders
        //Do damage
        anim.SetTrigger("Slash");
        yield return new WaitForSeconds(slashSpeed);
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
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 input()
    {
        Vector2 _input = Vector2.zero;
        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
            {
                _input.x = -0.25f;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
            {
                _input.x = 0.25f;
            }
        }
        else
        {
            _input.x = 0;
        }*/
        if (derecha)
        {
            if (!izquierda)
            {
                _input.x = 0.25f;
            }
        }
        else if (izquierda)
        {
            if (!derecha)
            {
                _input.x = -0.25f;
            }
        }
        else
        {
            _input.x = 0;
        }

        if (saltar)
        {
            _input.y = 1;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            _input.y = 1;
        }*/

        return _input;
    }
}