using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxcollider;
    private AudioManager theAudio;

    public int nextMove;
    public int NextMV =3;
    public bool isOndamaged = false;

    public string enemyDieSound;

    void Awake()
    {
        theAudio = FindObjectOfType<AudioManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        Think();
 //     Invoke("Think", 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y );
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1f, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1f, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        // Set Next Active
        nextMove = Random.Range(-1 , 2);

        if (nextMove == -1)
            nextMove = NextMV * -1;
        else if (nextMove == 1)
            nextMove = NextMV;
        else nextMove = 0;

        // Sprite Animation
       // anim.SetInteger("WalkSpeed", nextMove);

        // Flip Sprite
        if(nextMove != 0)
        spriteRenderer.flipX = nextMove < 0;

        // Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove < 0;

        if (!isOndamaged)
        {
            CancelInvoke();
            Invoke("Think", 2);
        }
    }

    public void OnDamaged()
    {
        isOndamaged = true;
        theAudio.Play(enemyDieSound);

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);  //Sprite Alpha       
        spriteRenderer.flipY = true;  //Sprite Flip Y       
        boxcollider.enabled = false;  //Collider Disable      
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);   //Die Effect Jump
        Invoke("DeActive", 1f);   // Destroy                               
    }
    void DeActive()
    {
       this.gameObject.SetActive(false);
      //  Destroy(gameObject);
    }
}
