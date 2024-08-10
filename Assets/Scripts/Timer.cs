using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    PlayerMovement player; 

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float elapsedTime = 5;
    // Update is called once per frame
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }
    
    
    void Update(){
        elapsedTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime/60);
        int seconds = Mathf.FloorToInt(elapsedTime%60);
        int millisecons = Mathf.FloorToInt(elapsedTime);
        timerText.text = string.Format("{0:0}:{1:00}",minutes,seconds);
        if (elapsedTime <= 0){
            elapsedTime = 30.0f;
            player.FlipWorld();
        }
    }
}
