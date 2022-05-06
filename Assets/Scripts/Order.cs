using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    public int orderID;
    public TextMeshProUGUI orderBasicText;
    public TextMeshProUGUI orderPowerUpText;
    public TextMeshProUGUI  timerText;
    public float orderTimer;
    public float difficulty;
    public int index;

    public int BasicNum;
    public int powerUpNum;
    
    public int powerUpAmount;
    public List<int> powerUpNums;

    public string basicTextDisplay;
    public string powerUpTextDisplay;
    OrderManager manager;

    public bool isServed; 
    public bool isEnabled = false;
    public string test = "change";
    public bool isDisplayed;

    public AudioSource soundfx;


    //SPRITES
    public Sprite normalSprite;
    public Sprite selectedSprite;

    void Start()
    {
        manager = FindObjectOfType<OrderManager>();  
        this.gameObject.SetActive(false);
        soundfx = GetComponent<AudioSource>();

    }

    void Update() {

        timerText.text = orderTimer.ToString("0");

        //Timer based on difficulty
        difficulty = ((Time.time + 300) / 30) - 10;

        if(this != manager.OrderSelected) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
        } else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }

        //Expires order
        if(this.orderTimer <= 0) {
            Debug.Log("Time ran out");
           manager.deleteOrder(this, this.index);
        }
        
        

    }

    //Timer function
    public IEnumerator StartCountdown(float timer = 40.0f) {

        if(isEnabled) {
            orderTimer = timer;
            while(orderTimer > 0) {
                yield return new WaitForSeconds(1.0f);
                    orderTimer--;
            }
        }
        

    }

    void OnDisable() {
        orderBasicText.text = "";
        orderPowerUpText.text = "";
        BasicNum = -1;
        powerUpNum = -1;
        powerUpNums.Clear();
        powerUpAmount = -1;
        isEnabled = false;
        orderTimer = 40.0f;
        timerText.text = "";
    }

    void OnEnable() {

        isEnabled = true;

        StartCoroutine(StartCountdown());

        basicTextDisplay = basicTextGenerator();
        orderBasicText.text = basicTextDisplay;

        powerUpTextDisplay = powerUpTextGenerator();
        orderPowerUpText.text = powerUpTextDisplay;

        /*
        if(this.orderTimer <= 0) {
           manager.removeOrder(this, index, 0.0f);
        }
        */

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

    public string powerUpTextGenerator() {

        powerUpAmount = Random.Range(0,3);
        
        string text = "";

        for(int i = 0; i < powerUpAmount; i++) { 
            
            powerUpNum = Random.Range(7, 24);
            powerUpNums.Add(powerUpNum);

            switch (powerUpNum){
                case 7:
                    //Almond Flour
                    text += "Almond Flour\n";
                    break;
                case 8:
                    //Birthday Candles
                    text += "For a birthday\n";
                    break;
                case 9:
                    //Blueberry Icing
                    text +="Blueberry Icing\n";
                    break;
                case 10:
                    //Butter
                    text +="Butter\n";
                    break;
                case 11:
                    //Chocolate Chips
                    text +="Chocolate Chips\n";
                    break;
                case 12:
                    //Chocolate Flavoring
                    text +="Chocolating Flavor\n";
                    break;
                case 13:
                    //Chocolate Icing
                    text +="Chocolate Icing\n";
                    break;
                case 14:
                    //Cookies and Cream Flavoring
                    text +="Cookies and Cream\n";
                    break;
                case 15:
                    //Fruit
                    text +="Fruit\n";
                    break;
                case 16:
                    //Powdered Sugar
                    text +="Powdered Sugar\n";
                    break;
                case 17:
                    //Red Velvet Flavoring
                    text +="Lemon Flavor\n";
                    break;
                case 18:
                    //Sprinkles
                    text +="Sprinkles\n";
                    break;
                case 19:   
                    //Strawberry Flavoring
                    text +="Strawberry Flavor\n";
                    break;
                case 20:
                    //Strawberry Icing
                    text +="Strawberry Icing\n";
                    break;
                case 21:
                    //Sugar
                    text +="Sugar\n";
                    break;
                case 22:
                    //Vanilla Extract
                    text +="Vanilla Extract\n";
                    break;
                case 23:
                    //Vanilla Icing
                    text +="Vanilla Icing\n";
                    break;
                case 24:
                    //Whisk
                    text +="Whisked!\n";
                    break;
            }
        }

        return text;
    }


    public void OnMouseDown() {

        soundfx.Play();
        manager.OrderSelected = this;
        manager.selectedBasicID = BasicNum;  
        //powerUpnum to see amount
        manager.powerUpAmount = powerUpAmount;
        //powerupNums array to get all values   
        manager.selectedPowerUpIDs = powerUpNums; 

        //CHANGE SPRITE
        this.gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        
    }
}
