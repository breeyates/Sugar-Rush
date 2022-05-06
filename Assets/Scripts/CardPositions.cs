using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositions : MonoBehaviour
{


    //Holds all Basic Cards
    public List<Card> basics;
    public bool[] availBasicSlots;
    public Transform[] basicPositions;
    public List<Card> basicsDisplayed;

    //Holds all power ups cards
    public List<Card> powerUps;
    public bool[] availPowerUpSlots;
    public Transform[] powerUpPositions;
    public List<Card> powerUpsDisplayed;

    //Holds basic card played ----TEST----
    public List<Card> playedCards;
    public bool[] availPlayedSlots;
    public Transform[] playedPositions;

    //Holds basic cards played
    public Card basicPlayed = null;
    public bool availPlayedBasicSlot;
    public Transform playedBasicPosition;

    //Holds power up cards played
    public List<Card> playedPowerUps;
    public List<int> playedPowerUpIDs;
    public bool[] availPlayedPUSlots;
    public Transform[] playedPUPositions;

    OrderManager orderManager;
    ScoreManager scoreManager;
    Animator cardAnimations;
    AudioSource soundfx;

    //I need to make an array for powerups of boolean values, each of which holds if that card is played.
    //This is how i can tell if these are the correct cards
    //Then add each card when clicked to the array and check it once it is served

    public void Start() {
        orderManager = FindObjectOfType<OrderManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        cardAnimations = GetComponent<Animator>();
        soundfx = GetComponent<AudioSource>();
    }


    //Draw a basic Card
    public void DisplayBasics() {

        Card newCard = basics[Random.Range(0, basics.Count)];
        

        for(int i = 0; i < basicPositions.Length; i++) {

            if (availBasicSlots[i] == true) {
                soundfx.Play();
                newCard.cardAnimations.Play("draw", 0, 0);
                newCard.gameObject.SetActive(true);
                newCard.played = i;
                newCard.transform.position = basicPositions[i].position;
                newCard.transform.rotation = basicPositions[i].rotation;
                newCard.transform.localScale = basicPositions[i].localScale;
                newCard.hasBeenClicked = false;
                basics.Remove(newCard);
                availBasicSlots[i] = false;
                basicsDisplayed.Add(newCard);
                return;
            }

            
        }

    }

    //Draw a Power Up Card
    public void DisplayPowerUps() {

        Card newCard = powerUps[Random.Range(0, powerUps.Count)];

        for(int i = 0; i < powerUpPositions.Length; i++) {

            if (availPowerUpSlots[i] == true) {
                soundfx.Play();
                newCard.cardAnimations.Play("draw", 0, 0);
                newCard.gameObject.SetActive(true);
                newCard.played = i;
                newCard.transform.position = powerUpPositions[i].position;
                newCard.transform.rotation = powerUpPositions[i].rotation;
                newCard.transform.localScale = powerUpPositions[i].localScale;
                newCard.hasBeenClicked = false;
                powerUps.Remove(newCard);
                availPowerUpSlots[i] = false;
                powerUpsDisplayed.Add(newCard);
                return;

            }

        }

    }


    //Serve Cards
    public void serveCards() {

        //SEND CARDS
        if(basicPlayed != null && orderManager.OrderSelected != null) {
            
            //BASICS
            basics.Add(basicPlayed);
            basicPlayed.gameObject.SetActive(false);
            availPlayedBasicSlot = true;

            //POWER UPS
            if (playedPowerUps.Count >= 0) {

                foreach (Card card in playedPowerUps) {
                    powerUps.Add(card);
                    card.gameObject.SetActive(false);
                }

            for(int i = 0; i < availPlayedPUSlots.Length; i++) {
                availPlayedPUSlots[i] = true;
            }

            //---------ORDER MANAGEMENT / SCORE---------
            
            if(orderManager.OrderSelected != null) {
                //BASIC CARD
                if(orderManager.selectedBasicID == basicPlayed.getID()) {
                    scoreManager.score += 10;
                    //Debug.Log("Added 10 points for basic card");
                } else {
                    if(scoreManager.score > 0) {
                        scoreManager.score -= 5;
                        //Debug.Log("Wrong basic card -5");

                        if(scoreManager.score < 0) {
                            scoreManager.score = 0;
                        }
                    } else {
                        scoreManager.score += 0;
                    }
                }

                //POWER UPS
                foreach (int i in orderManager.selectedPowerUpIDs) {
                    if (playedPowerUpIDs.Contains(i)) {
                        scoreManager.score += 3;
                        //Debug.Log("Added 3 points for power up");
                    } else {
                        if(scoreManager.score > 0) {
                            scoreManager.score -= 2;
                            //Debug.Log("Minus 2 points for wrong power up");

                            if(scoreManager.score < 0) {
                                scoreManager.score = 0;
                            }

                        } else {
                            scoreManager.score += 0;
                        }
                    }
    
                }

               orderManager.OrderSelected.isServed = true; 
            }

            //CLEARING EVERYTHING
            playedPowerUps.Clear();
            playedPowerUpIDs.Clear();
            basicPlayed = null;

            //remove the order
            StartCoroutine(orderManager.removeOrder(orderManager.OrderSelected, orderManager.OrderSelected.index, 0.5f));
            Debug.Log("Serving completed. Order: " + orderManager.OrderSelected.index.ToString());

        }

        }
    }

    //Reroll Basics **IN PROGRESS*
    public void rerollBasics() {

        if(scoreManager.score >= 2) {

                if(basicsDisplayed.Count > 0) {

                if(basicsDisplayed.Count >= 0) {
                    foreach (Card card in basicsDisplayed) {
                        basics.Add(card);
                        card.gameObject.SetActive(false);

                        //Subtract from score based on how many rerolled
                        if(scoreManager.score > 0) {
                            scoreManager.score -= 2;
                            Debug.Log("-$2 for each Basic Reroll");

                            if(scoreManager.score < 0) {
                                scoreManager.score = 0;
                                return;
                            }

                        } else {
                            scoreManager.score += 0;
                        }

                    }

                    for(int i = 0; i < availBasicSlots.Length; i++) {
                        availBasicSlots[i] = true;
                    }

                    
                }

                //Clear list
                basicsDisplayed.Clear();
 
            } else {
                Debug.Log("Cannot reroll! Nothing is displayed!");
            }

        } else {
            Debug.Log("Cannot reroll! Not enough money");
        }

    }

    //Reroll Powerups
    public void rerollPowerUps() {

        if(scoreManager.score >= 1) {

                if(powerUpsDisplayed.Count > 0) {

                if(powerUpsDisplayed.Count >= 0) {
                    foreach (Card card in powerUpsDisplayed) {
                        powerUps.Add(card);
                        card.gameObject.SetActive(false);

                        //Subtract from score based on how many rerolled
                        if(scoreManager.score > 0) {
                            scoreManager.score -= 1;
                            Debug.Log("-$1 for each Power Up Reroll");

                            if(scoreManager.score < 0) {
                                scoreManager.score = 0;
                                return;
                            }

                        } else {
                            scoreManager.score += 0;
                        }

                    }

                    for(int i = 0; i < availPowerUpSlots.Length; i++) {
                        availPowerUpSlots[i] = true;
                    }

                    
                }

                //Clear list
                powerUpsDisplayed.Clear();
 
            } else {
                Debug.Log("Cannot reroll! Nothing is displayed!");
            }

        } else {
            Debug.Log("Cannot reroll! Not enough money");
        }
    }


    //Throw Away cards played
    public void TrashOrder() {
        //Remove basic played
        if(basicPlayed != null) {
            basicPlayed.gameObject.SetActive(false);
            basics.Add(basicPlayed);
            availPlayedBasicSlot = true;
        }

        if(playedPowerUps.Count >= 0) {
            foreach (Card card in playedPowerUps) {
                powerUps.Add(card);
                card.gameObject.SetActive(false);
            }

            for(int i = 0; i < availPlayedPUSlots.Length; i++) {
                availPlayedPUSlots[i] = true;
            }

        }


        //Clear Lists
        playedPowerUps.Clear();
        playedPowerUpIDs.Clear();
        basicPlayed = null;

    }
    

   




}
