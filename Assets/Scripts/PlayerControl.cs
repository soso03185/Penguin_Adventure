using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public string JumpSound;
    public string hpUpSound;

    public float maxSpeed = 0f;    
    public int jumpPower;
    public int jumpmax;
    public int jumpcount = 0;
    public int OldHP_Index = 0;

    public DialogueManager manager;
    public GameManager gameManager;

    private AudioManager theAudio;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    public CapsuleCollider2D capsulecollider;
    public Restart restart;

    Vector3 dirVec;
    Animator anim;

    void Awake()
    {
        theAudio = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        trailRenderer = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();      
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();                
    }

    private void Start()
    {
        FadeFalse();
        Invoke("FadeTrue",0.2f);
    }

    void Update()
    {
        InCamera();
           
        Move();

        Jump();

        if(rigid.velocity.y <= 0) // if(anim.GetBool("isJumping"))
            Down();

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.3f, rigid.velocity.y);
            anim.SetBool("isWalking", false);
            Debug.Log("이동 키 누름");
        }
        // Sprite X flip
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

    }

    void FixedUpdate()
    {        
        // Ray
        Debug.DrawRay(rigid.position, dirVec * 1f, new Color(0, 1, 0)); // 보여주기만 하는 것
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {       
            
        }
    }

    void Move()
    {
        //Move By Key Control      
        float h = manager.talking ? 0 : Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * 60 * h, ForceMode2D.Impulse);

        //대화 중 이동금지
        if (manager.talking)  
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);

        //Direction
        if (this.rigid.velocity.x > 0)
            dirVec = Vector3.right;
        if (this.rigid.velocity.x < 0)
            dirVec = Vector3.left;

        //Right Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            anim.SetBool("isWalking", true);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        //Left Max Speed
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            anim.SetBool("isWalking", true);
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    void Jump()
    {        
        if (Input.GetKeyDown(KeyCode.X) && jumpcount < jumpmax && !manager.talking)
        {
            anim.SetBool("isJumping", true);
            jumpcount++;
            theAudio.Play(JumpSound);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void Down()
    {
        anim.SetBool("isDowning", true);
        anim.SetBool("isJumping", false);
        Debug.DrawRay(rigid.position, Vector3.down * 1f, new Color(0, 1, 0)); 
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Platform"));

        if (rayHit.collider != null)
        {
            anim.SetBool("isDowning", false);
            jumpcount = 0;
            /* 
             if (rayHit.distance < 0.7f)
              {
                  jumpcount = 0;
                  anim.SetBool("isDowning", false);

              // 닿은거 이름 표시
              //  Debug.Log(rayHit.collider.name);
              }
            */
        }
        else
            jumpcount = 1;
    }

    void InCamera()
    {   
        // Camera In Player
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.x < 0f) worldpos.x = 0f;
   //   if (worldpos.y < 0f) worldpos.y = 0f;
   //   if (worldpos.x > 1f) worldpos.x = 1f;
        if (worldpos.y > 1f) worldpos.y = 1f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }

    void FadeTrue()
    {
        trailRenderer.enabled = true;
        manager.talking = false;
    }
    void FadeFalse()
    {
        trailRenderer.enabled = false;
        manager.talking = true;
    }
    void OnAttack(Transform enemy)
    {
        // Reaction Force
        rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);

        //Enenmy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hp_Up")
        {
            theAudio.Play(hpUpSound);
            bool isHp_Up = collision.gameObject.name.Contains("HP_Up");

            if (!isHp_Up)
            {
                OldHP_Index++;
            }
            collision.gameObject.SetActive(false);
        }


        if (collision.gameObject.tag == "Note_A")
        {
            gameManager.AllHp_Index--;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Saw")
        {
            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //Sprite Flip Y
            spriteRenderer.flipY = true;
            //Collider Disable
            capsulecollider.enabled = false;
            //Die Effect Jump
            rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            restart.isDie = true;
        }

        // Fade Out - Don't Move
        if (collision.gameObject.tag == "Fade")
        {
            Debug.Log("Fade에 닿음");
            gameManager.StageChangeInvoke();
            trailRenderer.enabled = false;
            manager.talking = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Fade Out - Do Move
        if (collision.gameObject.tag == "Fade")
        {
            Invoke("FadeTrue", 2f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("닿음");
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            //Damaged  
            else
            {
                //Sprite Alpha
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                //Sprite Flip Y
                spriteRenderer.flipY = true;
                //Collider Disable
                capsulecollider.enabled = false;
                //Die Effect Jump
                rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                restart.isDie = true;
            }
        }


        Portal block = collision.gameObject.GetComponent<Portal>();

        if (collision.collider.tag == "Portal")
        {
            Vector3 anotherPortalPos = block.portal.transform.position;
            Vector3 warpPos = new Vector3(anotherPortalPos.x, anotherPortalPos.y + 0.6f, anotherPortalPos.z);
            transform.position = warpPos;
        }
    }

}
