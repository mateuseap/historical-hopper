using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour{

    public float speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    public AudioClip jumpSound;
    public FixedJoystick moveJoystick;

    private Rigidbody2D rig;
    private Animator animacao;

    bool fanIsOn;

    void Start(){
        rig = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    void Update(){
        Move();
        Jump();
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal")/*moveJoystick.Horizontal*/, 0f, 0f);
        transform.position += movement*Time.deltaTime*speed; //Move o personagem em uma posição

        //float movement = Input.GetAxis("Horizontal");
        //rig.velocity = new Vector2(movement*speed, rig.velocity.y);

        if(Input.GetAxis("Horizontal")/*moveJoystick.Horizontal*/ > 0f){
            animacao.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }else if(Input.GetAxis("Horizontal")/*moveJoystick.Horizontal*/ == 0f){
            animacao.SetBool("walk", false);
        }else if(Input.GetAxis("Horizontal")/*moveJoystick.Horizontal*/ < 0f){
            animacao.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && !fanIsOn){
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                animacao.SetBool("jump", true);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }else{
                if(doubleJump){
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                    doubleJump = false;
                }
            }
        }
    }

    public void JumpButton(){
        if(!fanIsOn){
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                animacao.SetBool("jump", true);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }else{
                if(doubleJump){
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = false;
            animacao.SetBool("jump", false);
        }
        if(collision.gameObject.tag == "spike"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "saw"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.layer == 10){
            fanIsOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.layer == 10){
            fanIsOn = false;
        }
    }
}
