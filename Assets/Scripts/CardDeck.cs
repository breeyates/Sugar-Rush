using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{

    public List<Card> basicCardDeck = new List<Card> ();
    public List<Card> powerUpCardDeck = new List<Card> ();

    // Start is called before the first frame update
    void Start()
    {
        basicCardDeck.Add(new Card(0, "Cake", 0));
        basicCardDeck.Add(new Card(1, "Cupcake", 0));
        basicCardDeck.Add(new Card(2, "Cookie", 0));
        basicCardDeck.Add(new Card(3, "Donut", 0));
        basicCardDeck.Add(new Card(4, "Pie", 0));
        basicCardDeck.Add(new Card(5, "Waffle", 0));

        //POWER UPS
        powerUpCardDeck.Add(new Card(6, "Whisk", 1));
        powerUpCardDeck.Add(new Card(7, "Sugar", 2));

        powerUpCardDeck.Add(new Card(8, "Chocolate Flavor", 3));
        powerUpCardDeck.Add(new Card(9, "Strawberry Flavor", 3));
        powerUpCardDeck.Add(new Card(10, "Red Velvet Flavor", 3));
        powerUpCardDeck.Add(new Card(11, "Cookies and Cream Flavor", 3));

        powerUpCardDeck.Add(new Card(12, "Vanilla Frosting", 4));
        powerUpCardDeck.Add(new Card(13, "Chocolate Frosting", 4));
        powerUpCardDeck.Add(new Card(14, "Strawberry Frosting", 4));
        powerUpCardDeck.Add(new Card(15, "Blueberry Frosting", 4));

        powerUpCardDeck.Add(new Card(16, "Vanilla Extract", 5));
        powerUpCardDeck.Add(new Card(17, "Almond Flour", 6));
        powerUpCardDeck.Add(new Card(18, "Butter", 7));

        powerUpCardDeck.Add(new Card(19, "Chocolate Chips", 8));
        powerUpCardDeck.Add(new Card(20, "Powdered Sugar", 8));
        powerUpCardDeck.Add(new Card(21, "Fruit", 8));
        powerUpCardDeck.Add(new Card(22, "Sprinkles", 8));
        powerUpCardDeck.Add(new Card(23, "Birthday Candles", 8));
    }

}