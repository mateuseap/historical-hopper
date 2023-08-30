using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim:MonoBehaviour{

    public float jumpForce;
    public AudioClip jumpSound;

    private Animator animacao;

    void Start(){
        animacao = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            animacao.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }
}
