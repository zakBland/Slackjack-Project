using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class GameFunctionsScript : MonoBehaviour
{
    public Sprite[] spriteArray;
    List<Card> deck;
    public Sprite dealerCard;

    void Start()
    {
        //gets reference from deck from MainClass 
         deck = MainClass.deck;

        //initializes deck
        deck = new List<Card>();
        //GameObject[] objs = Resources.LoadAll<GameObject[]>($"Sprites/cardfaces/{convertSuit(2, 10)}");
        Debug.Log("Hellwohow");
        //Debug.Log(spriteArray == null);


        for (int i = 1, k = 0; i <= MainClass.SUIT_COUNT; i++)
        {
            for (int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                // AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>($"Sprites/cardfaces/{convertSuit(i, j)}"); //loads sprites from folder into an array
                //spriteHandle.Completed += LoadSpritesWhenReady; //loads the sprite for instant use
                deck.Add(new Card(i, j, spriteArray[k]));
                k++;
            }


            //https://gamedevbeginner.com/how-to-change-a-sprite-from-a-script-in-unity-with-examples/  reference for code
        }
    }

    void Update()
    {
        MainClass.deck = deck;
    }

    //Loads sprites for instant access event
    /*void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck)
    {
        //if successful load, sets results to array
        if(handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            spriteArray = handleToCheck.Result;
            Debug.Log(spriteArray.Length);
            GameObject fi = GameObject.Find("CardSlot45");
            Debug.Log(spriteArray[0] == null);
            Debug.Log(fi == null);
            fi.GetComponent<SpriteRenderer>().sprite = spriteArray[0];
        }
    } */

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
        Debug.Log(deck.Count);
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

        Player[] players = MainClass.players; //gets reference of players from MainClass

        deck = shuffleDeck(deck); //shuffles deck
        Debug.Log(deck == null);
        //adds cards to players' hands (playerHand) and to their respective GUI area card clot
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2 /*PlayerPrefs.GetInt("playerCount")*/; j++) //temporarily just deals to two players rather than the correct amount of players in game
            {
                addToDeck(players[i], pickRandomCard(deck));
            }
        }

        GameObject[] areas = GameObject.FindGameObjectsWithTag(players[0].playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = dealerCard; //sets specific slot area to sprite/image
        players[0].handTotal -= players[0].playerHand[1].pip;
        GameObject playerText = GameObject.Find(players[0].playerName + "CountText");
        playerText.GetComponent<TextMeshProUGUI>().text = (players[0].handTotal + "");

        GameObject dealButtonObject = GameObject.Find("DealButton");
        dealButtonObject.SetActive(false);

    }

    //adds card to deck (gui and playerHand)
    public static void addToDeck(Player player, Card card)
    {
        player.playerHand.Add(card);     //adds to playerHand    
        displayCard(card, player); //displays card
        calculateTotal(card, player); //adjusts card total (not implemented yet)

        Debug.Log(player.handTotal);
    }

    //display cards to screen
    public static void displayCard(Card card, Player player)
    {
        int slot = int.Parse(player.cardSlotOrder[0] + ""); //finds correct slot number


        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[slot].GetComponent<Image>().sprite = card.sprite; //sets specific slot area to sprite/image

        if (player.playerName.Equals("Player"))
        {
            GameObject[] zoomAreas = GameObject.FindGameObjectsWithTag("PlayerZoomCardAreaBlock");
            zoomAreas[slot].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            zoomAreas[slot].GetComponent<Image>().sprite = card.sprite;

        }        
        
        player.cardSlotOrder = player.cardSlotOrder.Substring(1); //updates card order for player

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
                bool loweredAce = false;

                //if there is an ace that is played as a high, play it as low
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    if (player.playerHand[i].aceValue == 1)
                    {
                        player.playerHand[i].aceValue = -1;
                        player.handTotal -= 10;
                        loweredAce = true;
                        break;
                    }
                }
           
                //player busted (NOT IMPLEMENTED)

                if (!loweredAce)
                {

                    //end turn, move to next player
                    
                    //disable (you) player buttons
                    GameObject standButton = GameObject.Find("StandButton");
                    GameObject hitButton = GameObject.Find("HitButton");
                    standButton.SetActive(false);
                    hitButton.SetActive(false);
                }
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
                    GameObject standButton = GameObject.Find("StandButton");
                    GameObject hitButton = GameObject.Find("HitButton");
                    standButton.SetActive(false);
                    hitButton.SetActive(false);

                    
                }
            }
            //give player option of playing ace high or low (FINISH IMPLEMENTING)
            else
            {
                player.handTotal += Card.ACE_HIGH;
            }
        }

        GameObject playerText = GameObject.Find(player.playerName + "CountText");
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + "");

    }

    //shows outcome of card draw
    public static void showOutcome(Card card, Player player, string hitOrStand)
    {
        //show for few seconds, then disappear (NOT IMPLEMENTED)

        GameObject resultsAreaTextObject = GameObject.Find("ResultText"); //finds result text reference
        GameObject resultsAreaObject = GameObject.Find("ResultImage"); //Finds result area reference

        //if player hits (presses hit button), show text and card drawn
        if (hitOrStand.Equals("hit"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} {hitOrStand}!"; //displays text of name and action
            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            resultsAreaObject.GetComponent<Image>().sprite = card.sprite; //displays card
        }
        //if player stands (presses stand button), show text
        else if(hitOrStand.Equals("stand"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} stood! Player is next!"; //shows player and action if  //fix to have specific name (FINISH)
            
        }
        else
        {

        }

        //IMPLEMENT WAIT FOR X amount of seconds then hide outcome again

    }

    //picks random card from deck
    public static Card pickRandomCard(List<Card> deck)
    {
        Debug.Log("Start");
        Debug.Log(deck.Count);
        Card card = deck[0]; //pulls first card from shuffled deck
        deck.RemoveAt(0); //removes from deck

        Debug.Log("Done");
        return card; //returns card
    }

    //converts suit to suitable sprite/image card name
    public static string convertSuit(int suit, int j)
    {
        char suitChar = 'a'; //initializes string variable

        //sets int to corresponding char suit value
        switch (suit)
        {
            case 1: { suitChar = 'C'; break; } //clubs
            case 2: { suitChar = 'D'; break; }  // diamonds
            case 3: { suitChar = 'H'; break; } //hearts
            case 4: { suitChar = 'S'; break; } //spades

        } 

        return $"{suitChar}{j}"; //returns card string
    }

}

