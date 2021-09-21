using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class GameFunctionsScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    void Start()
    {
        AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/");
        spriteHandle.Completed += LoadSpritesWhenReady;


        List<Card> deck = MainClass.deck;

        for (int i = 1; i <= MainClass.SUIT_COUNT; i++)
        {
            for (int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                deck.Add(new Card(i, j, spriteArray[0]));
            }
        }

        
        //https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/
    }

    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck)
    {
        if(handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            spriteArray = handleToCheck.Result;
        }
    }
    //initialize deck
    /*public List<Card> initializeDeck(List<Card> deck)
    {

        deck = new List<Card>();

        for(int i = 1; i <= MainClass.SUIT_COUNT; i++)
        {
            for(int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                deck.Add(new Card(i, j, spriteArray[0]));
            }
        }

        return deck;
    }*/
    //shuffle
    public static List<Card> shuffleDeck(List<Card> deck)
    {
        
        List<Card> shuffledDeck = new List<Card>();
        
        for(int i = 0, j = deck.Count; i < j; i++)
        {
            int index = Random.Range(0, deck.Count);
            shuffledDeck.Add(deck[index]);
            deck.RemoveAt(index);
        }

        return shuffledDeck;
    }
    //deal
    public void dealCards()
    {
        List<Card> deck = MainClass.deck;
        Player[] players = MainClass.players;
        deck = shuffleDeck(deck);

        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2 /*PlayerPrefs.GetInt("playerCount")*/; j++)
            {
                addToDeck(players[i], pickRandomCard(deck));
            }
        }
    }

    //move turns

    //add to deck
    public static void addToDeck(Player player, Card card)
    {
        player.playerHand.Add(card);        
        displayCard(card, player);
        //calculateTotal(card, player);
    }
    //display cards
    public static void displayCard(Card card, Player player)
    {
        int slot = int.Parse(player.cardSlotOrder[0] + "");

        player.cardSlotOrder = player.cardSlotOrder.Substring(1);

        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString);
        areas[slot].GetComponent<SpriteRenderer>().sprite = card.sprite;

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
               /* GameObject aceButtonObject = GameObject.Find("LowAceButton");
                aceButtonObject.SetActive(true);
                aceButtonObject = GameObject.Find("HighAceButton");
                aceButtonObject.SetActive(true);*/
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

    public static string convertSuit(int suit, int j)
    {
        string suitString = "";

        switch (suit)
        {
            case 1: { suitString = "S"; break; }
            case 2: { suitString = "D"; break; }
            case 3: { suitString = "C"; break; }
            case 4: { suitString = "H"; break; }

        } 

        return $"{suitString}{j}";
    }

    /*public static Sprite getCardSprite(string card)
    {
        //Debug.Log("Sprites/cardfaces/" + card);
       // return Resources.Load<Sprite>("Assets/Resources/Sprites/cardfaces/C8");
    }*/
}

