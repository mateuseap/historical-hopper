using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint:MonoBehaviour{

    public string levelName;
    public AudioClip nextLevelSoundEffect;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            if(GameController.instance.CheckScore()){
                AudioSource.PlayClipAtPoint(nextLevelSoundEffect, transform.position);
                Invoke("LoadLevel", 0.6f);
            }
        }
    }

    void LoadLevel(){
        SceneManager.LoadScene(levelName);
    }
}
