using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int ID;
    public string name;
    public int type;
    public int played;

    public Transform space;
    public Animator cardAnimations;

    private AudioSource soundfx;

    public bool hasBeenClicked;

    CardPositions positions;

    private void Start() {
        positions = FindObjectOfType<CardPositions>();
        cardAnimations = GetComponent<Animator>();
        gameObject.SetActive(false);
        soundfx = GetComponent<AudioSource>();
    }

    public Card(int ID, string name, int type) {
        this.ID = ID;
        this.name = name;
        this.type = type;
    }

    public int getID() {
        return ID;
    }

    public string getName() {
        return name;
    }

    public int getType() {
        return type;
    }



    private void OnMouseDown() {

        if (!hasBeenClicked) {

            soundfx.Play();
            
             if(type == 0) {

                if(positions.availPlayedBasicSlot == true) {
                    cardAnimations.Play("played", 0, 0);
                    this.transform.position = positions.playedBasicPosition.position;
                    this.transform.rotation = positions.playedBasicPosition.rotation;
                    this.transform.localScale = positions.playedBasicPosition.localScale;                   
                    positions.availPlayedBasicSlot = false;
                    positions.basicPlayed = this;
                    hasBeenClicked = true;
                    positions.availBasicSlots[played] = true;
                    positions.basicsDisplayed.Remove(this);

                }

            } else if (type == 1) {
                //Power ups
                //Move card into played card position
                for(int i = 0; i < positions.playedPUPositions.Length; i++) {

                    if (positions.availPlayedPUSlots[i] == true) {

                        cardAnimations.Play("played", 0, 0);
                        this.transform.position = positions.playedPUPositions[i].position;
                        this.transform.rotation = positions.playedPUPositions[i].rotation;
                        this.transform.localScale = positions.playedPUPositions[i].localScale;
                        positions.availPlayedPUSlots[i] = false;
                        positions.playedPowerUps.Add(this);
                        positions.playedPowerUpIDs.Add(this.getID());
                        hasBeenClicked = true;
                        positions.availPowerUpSlots[played] = true;
                        positions.powerUpsDisplayed.Remove(this);
                        return;
                    }
            }


        } 
        
    }
    }

    void moveToPlayedPile() {
        positions.playedCards.Add(this);
        gameObject.SetActive(false);
    }



}
