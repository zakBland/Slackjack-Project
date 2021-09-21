using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public List<Card> playerHand;
    public int handTotal;
    public string playerNameBlockString;
    public string cardSlotOrder;
    public string playerName;
    public int playerNumber;

    //Player[] players;

    public Player()
    {
        playerHand = new List<Card>();
        handTotal = 0;
        playerNameBlockString = "";
        cardSlotOrder = "231405";
        playerName = "";
        playerNumber = -1;

    }


    
}
