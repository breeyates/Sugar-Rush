using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "New Order", menuName = "Order")]
public class OrderTemplate : ScriptableObject
{
    public int orderID;
    public TextMeshProUGUI orderBasicText;
    public TextMeshProUGUI orderPowerUpText;
    public int index;
    public int BasicNum;
    public string basicTextDisplay;
    //OrderManager manager;

    //Checks if this order is on screen - im adding this in case later on card 1 always displays the same shit so i can reset it every time its removeCard is ran
    //public bool isDisplayed;


    void Start()
    {
        //manager = FindObjectOfType<OrderManager>();
        basicTextDisplay = basicTextGenerator();
        orderBasicText.text = basicTextDisplay;

    }

    public string basicTextGenerator() {
        
        BasicNum = Random.Range(0, 6);
        string text = "";

        switch (BasicNum){
            case 0:
                text ="Cake";
                break;
            case 1:
                text ="Cupcake";
                break;
            case 2:
                text ="Cookie";
                break;
            case 3:
                text ="Brownie";
                break;
            case 4:
                text ="Donut";
                break;
            case 5:
                text ="Pie";
                break;
            case 6:
                text ="Waffle";
                break;
        }

        return text;
    }

}
