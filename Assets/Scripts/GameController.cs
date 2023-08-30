using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController:MonoBehaviour{

    public int totalScore;
    public int setScore;
    public Text scoreText;
    public GameObject gameOver;
    public AudioClip gameOverSoundEffect;

    public static GameController instance;

    void Start(){
        instance = this;
    }
    
    public void UpdateScoreText(){
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSoundEffect, transform.position);
    }

    public void RestartGame(string lvlName){
        SceneManager.LoadScene(lvlName);
    }

    public bool CheckScore(){
        if(setScore == totalScore){
            return true;
        }else{
            return false;
        }
    }
}
