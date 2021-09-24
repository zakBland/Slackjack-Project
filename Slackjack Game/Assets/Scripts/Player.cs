using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    
    public List<Card> playerHand; //declaration of player hand list
    public int handTotal; //declaration of hand total
    public string playerNameBlockString; // declaration of player area name
    public string cardSlotOrder; //declaration of card slot order
    public string playerName; // declaration of player name
    public int playerNumber; //declaration of player order

    //Player constructor
    public Player()
    {
        playerHand = new List<Card>(); //initializes player's hand
        handTotal = 0; //initializes hand total to zero
        playerNameBlockString = ""; // initializes String name of player's area
        cardSlotOrder = "231405"; //initializes the order in which cards should be displayed in gui
        playerName = ""; //Name of the player
        playerNumber = -1; //The player's turn number

    }


    
}
