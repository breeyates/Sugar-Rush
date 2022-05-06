using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoBehaviour
{

    //Orders
    public List<Order> Orders;
    public bool[] availOrderSlots;
    public Transform[] orderPositions;
    public List<Order> ordersDisplayed;

    public float difficulty;
    public float time1;

    //SELECTED VALUES
    public Order OrderSelected;
    public int selectedBasicID;
    public List<int> selectedPowerUpIDs;
    public int powerUpAmount;

    ScoreManager scoreManager;

    Coroutine removingOrder;


    public void Update() {             
        difficulty = ((Time.time + 300) / 30) - 10;

    }

     public void Start() {   
         FirstOrderCountdown();
         InvokeRepeating("createOrder", 0.0f, (6.0f - difficulty));
  
         scoreManager = FindObjectOfType<ScoreManager>();
       
    }
  
    //Wait for FIRST order
    public IEnumerator FirstOrderCountdown() {
        yield return new WaitForSeconds(5.0f);   
            //InvokeRepeating("createOrder", 0.0f, (8.0f));  
    }


    public void createOrder() {

        for(int i = 0; i < orderPositions.Length; i++) {

            if(availOrderSlots[i] == true) {
                Order newOrder = Orders[i];
                newOrder.gameObject.SetActive(true);
                newOrder.transform.position = orderPositions[i].position;
                newOrder.transform.rotation = orderPositions[i].rotation;
                availOrderSlots[i] = false;
                newOrder.index = i;
                newOrder.isDisplayed = true;
                newOrder.isServed = false;
                ordersDisplayed.Add(newOrder);

                Debug.Log("----------Order created: " + i.ToString() +  "-----------");

                //This is removing orders if they are served, it doesn't reset - keeps the time of the previous order

                //removingOrder = StartCoroutine(removeOrder(newOrder, i, 41.0f));
                //StopCoroutine(removingOrder);
               
                if(newOrder.orderTimer <= 0) {
                    Debug.Log("Time ran out");
                    deleteOrder(newOrder, i);
                }
                                
                return;

            }

        }
    }

    public void deleteOrder(Order order, int i) {
        order.gameObject.SetActive(false);
        ordersDisplayed.Remove(order);
        availOrderSlots[i] = true;
        order.isDisplayed = false; 
        order.orderTimer = 40.0f; 
        Orders.Add(order);

        //Clear
        OrderSelected = null;
        selectedBasicID = -1;
        selectedPowerUpIDs.Clear();
        powerUpAmount = -1;

        if(!order.isServed){
            if(scoreManager.score > 0) {
                scoreManager.score -= 4;

                if(scoreManager.score < 0) {
                    scoreManager.score = 0;
                }
            Debug.Log("Order not served. Order: " + i.ToString());
            }
        } else {
            Debug.Log("Order served:  " + i.ToString());
            //isServed = false;
        }

    }

    //manually delete
    public IEnumerator removeOrder(Order order, int i, float time) {
        
        yield return new WaitForSeconds(time);
            order.gameObject.SetActive(false);
            ordersDisplayed.Remove(order);
            availOrderSlots[i] = true;
            order.isDisplayed = false; 
            order.orderTimer = 40.0f; 
            Orders.Add(order);

            //Clear
            OrderSelected = null;
            selectedBasicID = -1;
            selectedPowerUpIDs.Clear();
            powerUpAmount = -1;

            if(!order.isServed){
                if(scoreManager.score > 0) {
                    scoreManager.score -= 4;

                    if(scoreManager.score < 0) {
                        scoreManager.score = 0;
                    }
                Debug.Log("Order not served. Order: " + i.ToString());
                }
            } else {
                Debug.Log("Order served:  " + i.ToString());
                //isServed = false;
            }
        

    }

    public void ClearOrder() {
        //CLEAR SELECTED
        OrderSelected = null;
        selectedBasicID = -1;
        selectedPowerUpIDs.Clear();
        powerUpAmount = -1;
        Debug.Log("Order cleared");
    }


}
