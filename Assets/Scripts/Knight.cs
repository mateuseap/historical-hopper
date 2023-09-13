using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight:MonoBehaviour{

    public Transform headPoint;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public AudioClip dyingSoundEffect;

    private bool colliding;

    private Rigidbody2D rig;
    private Animator animacao;
    private AreaEffector2D areaEffector2D;

    bool playerDestroyed = false;

    void Start(){
        rig = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D colliding){
        if(colliding.gameObject.tag == "Player"){
            float height = colliding.contacts[0].point.y-headPoint.position.y;
            if((height >= 0) && (!playerDestroyed)){
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

    void OnTriggerEnter2D(Collider2D colliding){
        if(colliding.gameObject.tag == "Player"){
            animacao.SetBool("attack", true);

            if(colliding.transform.position.x < transform.position.x && transform.localScale.x > 0f){
                transform.localScale = new Vector2(transform.localScale.x*-1f, transform.localScale.y);
            } else if(colliding.transform.position.x > transform.position.x && transform.localScale.x < 0f) {
                transform.localScale = new Vector2(transform.localScale.x*-1f, transform.localScale.y);
            }
        }
    }

    void OnTriggerExit2D(Collider2D colliding){
        if(colliding.gameObject.tag == "Player"){
            animacao.SetBool("attack", false);
        }
    }
}
