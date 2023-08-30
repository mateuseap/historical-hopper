using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude:MonoBehaviour{

    public float speed;
    public float jumpPower;
    public bool startRighToLeft = true;

    public Transform rightCollisionChecker;
    public Transform leftCollisionChecker;
    public Transform headPoint;
    public LayerMask layer;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public AudioClip dyingSoundEffect;

    private bool colliding;

    private Rigidbody2D rig;
    private Animator animacao;

    bool playerDestroyed = false;

    void Start(){
        rig = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
        if(!startRighToLeft){
            speed *= -1f;
            transform.localScale = new Vector2(transform.localScale.x*-1f, transform.localScale.y);
        }
    }

    void Update(){
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCollisionChecker.position, leftCollisionChecker.position, layer);

        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x*-1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D colliding){
        if(colliding.gameObject.tag == "Player"){
            float height = colliding.contacts[0].point.y-headPoint.position.y;
            if((height >= 0) && (!playerDestroyed)){
                colliding.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                speed = 0;
                animacao.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                AudioSource.PlayClipAtPoint(dyingSoundEffect, transform.position);
                Destroy(gameObject, 0.25f);
            }else{
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(colliding.gameObject);
            }
        }
    }
}
