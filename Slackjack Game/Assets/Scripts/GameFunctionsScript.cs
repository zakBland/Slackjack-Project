using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class GameFunctionsScript : MonoBehaviour
{
    Sprite[] spriteArray;

    void Start()
    {
        AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/"); //loads sprites from folder into an array
        spriteHandle.Completed += LoadSpritesWhenReady; //loads the sprite for instant use

        List<Card> deck = MainClass.deck; //gets reference from deck from MainClass

        //initializes deck
        for (int i = 1; i <= MainClass.SUIT_COUNT; i++)
        {
            for (int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                deck.Add(new Card(i, j, spriteArray[0]));
            }
        }

        
        //https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/  reference for code
    }

    //Loads sprites for instant access event
    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck)
    {
        //if successful load, sets results to array
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

    //shuffles deck
    public static List<Card> shuffleDeck(List<Card> deck)
    {
        
        List<Card> shuffledDeck = new List<Card>(); //initializes shuffled deck to be returned
        
        //randomly picks index of card to add to shuffled deck
        for(int i = 0, j = deck.Count; i < j; i++)
        {
            int index = Random.Range(0, deck.Count); //set index of randomly chosen card
            shuffledDeck.Add(deck[index]); //adds to shuffled deck
            deck.RemoveAt(index); //removes chosen card so it won't be chosen again
        }

        return shuffledDeck; //returns shuffled deck
    }

    //deals cards
    public void dealCards()
    {
        List<Card> deck = MainClass.deck; //gets reference of deck from MainClass
        Player[] players = MainClass.players; //gets reference of players from MainClass

        deck = shuffleDeck(deck); //shuffles deck

        //adds cards to players' hands (playerHand) and to their respective GUI area card clot
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2 /*PlayerPrefs.GetInt("playerCount")*/; j++) //temporarily just deals to two players rather than the correct amount of players in game
            {
                addToDeck(players[i], pickRandomCard(deck));
            }
        }
    }

    //adds card to deck (gui and playerHand)
    public static void addToDeck(Player player, Card card)
    {
        player.playerHand.Add(card);     //adds to playerHand    
        displayCard(card, player); //displays card
        //calculateTotal(card, player); //adjusts card total (not implemented yet)
    }

    //display cards to screen
    public static void displayCard(Card card, Player player)
    {
        int slot = int.Parse(player.cardSlotOrder[0] + ""); //finds correct slot number

        player.cardSlotOrder = player.cardSlotOrder.Substring(1); //updates card order for player

        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[slot].GetComponent<SpriteRenderer>().sprite = card.sprite; //sets specific slot area to sprite/image

    }

    //calculate total of hand
    public static void calculateTotal(Card card, Player player)
    {
        //if the card isn't an ace...
        if(card.pip != 1)
        {
            //...and if it is a face card/10, add 10 to total
            if (card.pip >= 10)
            {
                player.handTotal += 10;
            }
            //else, add pip value to total
            else 
            {
                player.handTotal += card.pip;
            }

            //if total is above 21...
            if(player.handTotal > 21)
            {
                //if there is an ace that is played as a high, play it as low
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    if (player.playerHand[i].aceValue == 1)
                    {
                        player.playerHand[i].aceValue = -1;
                        player.handTotal -= 10;
                        break;
                    }
                }
            }
            //player busted (NOT IMPLEMENTED)
            else
            {
                //end turn, move to next player

                //disable (you) player buttons

            }
        }
        //else, if card is an ace...
        else
        {
            //if playing ace high will result in bust, play low
            if(player.handTotal + Card.ACE_HIGH > 21)
            {
                player.handTotal += Card.ACE_LOW; //adds l
                card.aceValue = -1; //sets ace type to -1(low ace)

                //if player hand total is equal to 21, player wins, move turns (NOT IMPLEMENTED)
                if (player.handTotal == 21)
                {
                   //disable "you" player's buttons
                }
            }
            //give player option of playing ace high or low (FINISH IMPLEMENTING)
            else
            {
                GameObject aceButtonObject = GameObject.Find("LowAceButton"); //finds low ace button
                aceButtonObject.SetActive(true); //sets active low ace button
                aceButtonObject = GameObject.Find("HighAceButton"); //finds high ace button
                aceButtonObject.SetActive(true); //set active high ace button
            }
        }

    }

    //shows outcome of card draw
    public static void showOutcome(Card card, Player player, string hitOrStand)
    {
        //show for few seconds, then disappear (NOT IMPLEMENT)

        GameObject resultsAreaObject = GameObject.Find("ResultsArea"); //Finds result area reference
        GameObject resultsAreaTextObject = GameObject.Find("ResultText"); //finds result text reference

        //if player hits (presses hit button), show text and card drawn
        if (hitOrStand.Equals("hit"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} {hitOrStand}!"; //displays text of name and action
            resultsAreaObject.GetComponent<Image>().sprite = card.sprite; //displays card
        }
        //if player stands (presses stand button), show text
        else
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} stood! Player is next!"; //shows player and action if  //fix to have specific name (FINISH)
            
        }

    }

    //picks random card from deck
    public static Card pickRandomCard(List<Card> deck)
    {
        Card card = deck[0]; //pulls first card from shuffled deck
        deck.RemoveAt(0); //removes from deck
        return card; //returns card
    }

    //converts suit to suitable sprite/image card name
    public static string convertSuit(int suit, int j)
    {
        char suitChar = 'a'; //initializes string variable

        //sets int to corresponding char suit value
        switch (suit)
        {
            case 1: { suitChar = 'S'; break; } //spades
            case 2: { suitChar = 'D'; break; }  // diamonds
            case 3: { suitChar = 'C'; break; } //clubs
            case 4: { suitChar = 'H'; break; } //hearts

        } 

        return $"{suitChar}{j}"; //returns card string
    }

}

