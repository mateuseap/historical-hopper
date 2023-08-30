using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw:MonoBehaviour{
    
    public float speed;
    public float moveTime;
    public bool directionRight = true; //Se directionRight for true, a serra começa indo para direita, caso seja false, começa indo para a esquerda

    private float timer;

    void Update(){

        if(directionRight){
            transform.Translate(Vector2.right*speed*Time.deltaTime);
        }else{
            transform.Translate(Vector2.left*speed*Time.deltaTime);
        }


        timer += Time.deltaTime;
        if(timer >= moveTime){
            directionRight = !directionRight;
            timer = 0f;
        }
    }
}
