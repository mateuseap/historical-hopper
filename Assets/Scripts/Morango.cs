using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morango:MonoBehaviour{

    public GameObject collected;
    public AudioClip audioClip;
    public int score;

    private SpriteRenderer spriteDesenhado;
    private CircleCollider2D circle;

    private bool firstTime = true;

    void Start(){
        spriteDesenhado = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player" && (firstTime)){
            spriteDesenhado.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);
            AudioSource.PlayClipAtPoint(audioClip, transform.position);

            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.3f);
            firstTime = false;
        }
    }
}
