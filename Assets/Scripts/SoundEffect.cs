using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect:MonoBehaviour{

    public AudioClip soundEffect;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }
    }
}
