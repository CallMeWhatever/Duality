using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    PlayerMovement player; 

    [SerializeField] TextMeshProUGUI timerText;
    // Update is called once per frame
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }
    
    
    void Update(){
        if (player.elapsedTime > 0){
            player.elapsedTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(player.elapsedTime / 60);
            int seconds = Mathf.FloorToInt(player.elapsedTime % 60);
            int millisecons = Mathf.FloorToInt(player.elapsedTime);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else{
            timerText.text = string.Empty;
            player.elapsedTime = -1.0f;
            player.TimerFlip();
        }
    }
}
