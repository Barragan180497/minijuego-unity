using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed, jump;

    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool canJump = true, canDrop = true;
    private float height;
    private int layerFloor;
    private float moveDir;
    private Animator anim;
    public bool isDead = false;
    private bool jumping;
    private bool movement = true;
    public int hp;
    private int currentHp;

    public Image hp_UI;
    public GameObject hp_Canvas;

    Player_Controller pj;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pj = GameObject.FindObjectOfType<Player_Controller>();
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
            height = coll.bounds.extents.y + 0.05f;
            layerFloor = 1 << LayerMask.NameToLayer("Floor");
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void FixedUpdate()
    {
        /*if (isDead)
        {
            return;
        }*/
        anim.SetBool("isGround", isGround());

        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            movement = false;
            Attack();
        }
        else
        {
            movement = true;
        }

        if (jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jumping = false;
        }

        if (movement)
        {
            Move();
        }
    }

    private void OnEnable()
    {
        isDead = false;
        currentHp = hp;
        hp_UI.fillAmount = 1;
        hp_Canvas.SetActive(true);
    }

    void Attack()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        anim.SetTrigger("Attack");      
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("EnemyKnockBack", transform.position.x);
        }

        if (col.CompareTag("Espada"))
        {
            if (currentHp == hp)
            {
                hp_Canvas.SetActive(true);
            }
            currentHp--;
            if (currentHp <= 0)
            {
                Invoke("OnBecameInvisible", 1.5f);
            }
            hp_UI.fillAmount = (float)currentHp / hp;
        }
    }

    public void PlayerKnockBack(float playerPosX)
    {
        jumping = true;
        float side = Mathf.Sign(playerPosX - transform.position.x);
        rb.AddForce(Vector2.left * side * 10f, ForceMode2D.Impulse);

        movement = false;
        Invoke("Move", 1.5f);
    }

    public void PosInicialPlayer()
    {
        pj.transform.position = new Vector3(-3.6f, -0.121f, 0);
        pj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    void Move()
    {
        movement = true;
        if (isGround())
        {
            moveDir = PlayerPosX();
        }
        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
        anim.SetBool("Running", true);
        float dir = PlayerPosX();
        if (dir != 0)
        {
            transform.localScale = new Vector3(dir, 0.25f, 0.25f);
            hp_Canvas.transform.localScale = new Vector3(dir, 1, 1);
        }
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

    float PlayerPosX()
    {
        float diff = player.transform.position.x - transform.position.x;
        if (diff > 0.1f)
        {
            return 0.25f;
        }
        else if (diff < -0.1f)
        {
            return -0.25f;
        }
        else
        {
            return 0;
        }
    }

    /*string CheckPlayerYPos()
    {
        float diff = player.transform.position.y - transform.position.y;
        if (diff > 3)
        {
            return "Over";
        }
        else if (diff < -3)
        {
            return "Under";
        }
        else
        {
            return "Same";
        }
    }*/

    public bool isGround()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - height);
        bool bottom = Physics2D.Raycast(pos, -Vector2.up, 0.2f, layerFloor);
        Debug.DrawRay(pos, -Vector2.up * 0.2f, Color.red);
        return bottom;
    }

    public void OnBecameInvisible()
    {
        transform.position = new Vector3(3.6f, -0.121f, 0);
        transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
        isDead = true;
        PosInicialPlayer();
    }
    
}