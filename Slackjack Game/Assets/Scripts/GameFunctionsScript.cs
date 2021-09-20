using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public static class GameFunctionsScript
{

    //initialize deck
    private static List<Card> initializeDeck()
    {
        List<Card> deck = new List<Card>();

        for(int i = 1; i <= MainClass.SUIT_COUNT; i++)
        {
            for(int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                deck.Add(new Card(i, j, convertSuit(i, j)));
            }
        }

        return deck;
    }
    //shuffle
    public static  List<Card> shuffleDeck()
    {
        List<Card> deck = initializeDeck();
        List<Card> shuffledDeck = new List<Card>();
        
        for(int i = 0; i < deck.Count; i++)
        {
            int index = Random.Range(0, deck.Count - i - 1);
            shuffledDeck.Add(deck[index]);
            deck.RemoveAt(index);
        }

        return shuffledDeck;
    }
    //deal
    public static void dealCards()
    {
        MainClass.deck = shuffleDeck();

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < PlayerPrefs.GetInt("playerCount"); j++)
            {
                addToDeck(MainClass.players[j], GameFunctionsScript.pickRandomCard(MainClass.deck));
            }
        }
    }

    //move turns

    //add to deck
    public static void addToDeck(Player player, Card card)
    {
        Debug.Log("BEfore question");
        Debug.Log(card == null);
        player.playerHand.Add(card);
        displayCard(card, player);
        calculateTotal(card, player);
    }
    //display cards
    public static void displayCard(Card card, Player player)
    {
        int slot = int.Parse(player.cardSlotOrder[0] + "");

        player.cardSlotOrder = player.cardSlotOrder.Substring(1);

        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString);
        areas[slot].GetComponent<Image>().sprite = card.sprite;

    }

    //calculate total of hand
    public static int calculateTotal(Card card, Player player)
    {
        if(card.pip != 1)
        {
            if (card.pip >= 10)
            {
                player.handTotal += 10;
            }
            else 
            {
                player.handTotal += card.pip;
            }

            if(player.handTotal + 10 > 21)
            {
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    if (player.playerHand[i].aceValue == 1)
                    {
                        player.playerHand[i].aceValue = -1;
                        break;
                    }


                }
            }
        }
        else
        {
            if(player.handTotal + Card.ACE_HIGH > 21)
            {
                player.handTotal += 1;
                card.aceValue = -1;
            }
            else
            {
                GameObject aceButtonObject = GameObject.Find("LowAceButton");
                aceButtonObject.SetActive(true);
                aceButtonObject = GameObject.Find("HighAceButton");
                aceButtonObject.SetActive(true);
            }
        }

        return player.handTotal;
    }

    //showOutcome

    public static void showOutcome(Card card, Player player, string hitOrStand)
    {
        //show for few seconds, then disappear

        GameObject resultsAreaObject = GameObject.Find("ResultsArea");
        GameObject resultsAreaTextObject = GameObject.Find("ResultText");

        if (hitOrStand.Equals("hit"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} {hitOrStand}!";
            resultsAreaObject.GetComponent<Image>().sprite = card.sprite;
        }
        else
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} stood! ";

        }

    }

    //pickRandomCard
    public static Card pickRandomCard(List<Card> deck)
    {
        Card card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    public static Sprite convertSuit(int suit, int j)
    {
        string suitString = "";

        switch (suit)
        {
            case 1: { suitString = "S"; break; }
            case 2: { suitString = "D"; break; }
            case 3: { suitString = "C"; break; }
            case 4: { suitString = "H"; break; }
            
        }

        return Resources.Load<Sprite>("Sprites/" + suitString + "" + j);
    }
}
