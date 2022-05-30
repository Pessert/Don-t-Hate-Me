using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed, jumpPower;
    public LayerMask groundLayer;

    [SerializeField] private Sprite[] playerSprites; // 0 = Default, 1 = Jumping, 2 = Going Down, 3 = Die
    [SerializeField] private ParticleSystem ps_Die, ps_CoinGet;

    [HideInInspector] public Rigidbody2D rb;

    private int maxJumpCount = 2;
    private int jumpCount;
    private BoxCollider2D bCol;
    private SpriteRenderer sr;
    private Vector2 minFootPos, maxFootPos;
    private bool isGrounded, isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bCol = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) { Jump(); }

            if (isGrounded && rb.velocity.y <= 0f)
            {
                sr.sprite = playerSprites[0];
                jumpCount = maxJumpCount;
            }

            if (!isGrounded)
            {
                if (rb.velocity.y > 0.1f) { sr.sprite = playerSprites[1]; }
                else if (rb.velocity.y < 0.1f) { sr.sprite = playerSprites[2]; }
            }

            Vector2 tempV = transform.localScale;

            float f = Input.GetAxis("Horizontal");

            tempV.x = (f > 0 ? 1 : -1);

            if (f != 0) { transform.localScale = tempV; }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            float h = Input.GetAxis("Horizontal");

            Bounds bounds = bCol.bounds;
            minFootPos = new Vector2(bounds.min.x + 0.1f, bounds.min.y);
            maxFootPos = new Vector2(bounds.max.x - 0.1f, bounds.min.y);
            isGrounded = Physics2D.OverlapArea(minFootPos, maxFootPos, groundLayer);

            rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(minFootPos, maxFootPos);
    }

    private void Jump()
    {
        if (jumpCount > 0)
        {
            SoundManager.Instance.sfx_Jump.Play();
            jumpCount--;
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    private void Die()
    {
        if (isDead) return;

        SoundManager.Instance.sfx_Die.Play();
        isDead = true;
        ps_Die.Play();
        rb.velocity = Vector2.zero;
        sr.sprite = playerSprites[3];

        StartCoroutine(GameManager.Instance.GameOverRoutine());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Trap"))
        {
            Die();
        }
        else if (col.CompareTag("Trap_Wall"))
        {
            col.gameObject.SetActive(false);
        }
        else if (col.CompareTag("Potal"))
        {
            if (!GameObject.FindWithTag("Coin")) { GameManager.Instance.StageClear(); }
        }
        else if (col.CompareTag("Jumper"))
        {
            SoundManager.Instance.sfx_Jumper.Play();
            Jumper jumper = col.GetComponent<Jumper>();
            jumper.GetJumper();
            if (jumpCount != 2) { jumpCount++; }
        }
        else if (col.CompareTag("Coin"))
        {
            SoundManager.Instance.sfx_Coin.Play();
            col.gameObject.SetActive(false);
            ps_CoinGet.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded)
        {
            SoundManager.Instance.sfx_Landing.Play();
        }
    }
}
