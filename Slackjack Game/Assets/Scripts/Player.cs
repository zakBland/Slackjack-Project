using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Access to Deck and Card classes 
    public Card card;
    public Deck deck;

    //Value of player's hand
    public int handValue = 0;

    //Array to store player'cards
    public GameObject[] hand;

    //Index of face down card
    public int cardIndex = 0;

    //Tracking aces to dynmically change them from 1 to 11
    List<Card> acesList = new List<Card>();

    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    //Add cards to player's hand
    public int GetCard()
    {
        //Use deal to set sprite and value to cards on table
        int cardValue = deck.Deal(hand[cardIndex].GetComponent<Card>());
        
        //Show card on screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;

        //Add card value to hand total
        handValue += cardValue;

        //Check if card is an Ace
        if(cardValue ==1)
        {
            acesList.Add(hand[cardIndex].GetComponent<Card>());
        }

        //Method to check if ace should be 1 or 11
        //AceCheck(); **TO BE ADDED**

        //increase card index to next card
        cardIndex++;

        return handValue;


    }
}
