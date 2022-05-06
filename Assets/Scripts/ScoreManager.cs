using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float score; 
    public TextMeshProUGUI scoreText;
    OrderManager orderManager;
    CardPositions positions;

    void Start()
    {
        score = 50;
        orderManager = FindObjectOfType<OrderManager>();  
        positions = FindObjectOfType<CardPositions>();
    }

    void Update()
    {
        scoreText.text = "$" + score.ToString();
    }

    //function to update score based on basic/powerup and a = selectedID b = playedID
    //Not in use
    public void updateScore(int type, int a, int b) {
        
        switch (type) {
            case 0:
                if(a == b) {
                    score += 10;
                } else {
                    if (score > 0) {
                        score -= 5;
                    }
                }
            break;

            case 1:
                if(a == b) {
                    score += 4;
                } else {
                    if (score > 0) {
                        score -= 2;
                    }
                }
            break;
        }
            
    }

}
