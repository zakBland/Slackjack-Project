using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class GameFunctionsScript : MonoBehaviour
{
    
    static GameObject standButton; //reference to stand button
    GameObject hitButton; //reference to hit button
    public Sprite[] spriteArray; //reference to card image list
    List<Card> deck; //deck variable reference from MainClass class
    public Sprite dealerCard; //dealer's hidden card reference
    public static List<string> usedDeck; //variable for all unused cards

    static bool start;
    static bool delayingDisplay;

    float timer = 0; //ignore
    bool startTimer = false; //ignore
    bool timerReached = false; //ignore

    void Start()
    {
        start = false;
        delayingDisplay = false;

        //StartCoroutine("delayDislay", start);
        //needToDisplay
           // StartCoroutine(delayDislay());

        //disable (you) player buttons
        standButton.SetActive(false);
        hitButton.SetActive(false);

        //initialized unused deck
        usedDeck = new List<string>();

        //gets reference from deck from MainClass 
        deck = MainClass.deck;

        //initializes deck
        deck = new List<Card>();


        //adds to deck of cards
        for (int i = 1, k = 0; i <= MainClass.SUIT_COUNT; i++)
        {
            for (int j = 1; j <= MainClass.PIP_COUNT; j++)
            {
                deck.Add(new Card(i, j, spriteArray[k]));
                k++; //increments variable to get card sprite from sprite array
            }
        }

    }

    void Awake()
    {
        //finds and initializes objects for (you) player buttons
        standButton = GameObject.Find("StandButton");
        hitButton = GameObject.Find("HitButton");
        
    }

    void Update()
    {
        //updates MainClass deck of any changes made to GameFunctionsScript deck reference; may not be necessary
        MainClass.deck = deck;
        delayingDisplay = start;

       /* if (start)
        {
            Debug.Log("Inside delay");
            
        }
        //IGNORE
        /*if (startTimer && !timerReached)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }*/
        
    }

    IEnumerator delayDislay()
    {
        Debug.Log("Start waiting before");

        while (!delayingDisplay)
        {

            Debug.Log("not waiting");

            yield return null;

            Debug.Log("After not waiting");

        }

        Debug.Log("before waiting 15");
        
        yield return new WaitForSeconds(15);
        /*while(start)
        {
            yield return new WaitForSeconds(10);        
            start = false;

        }*/
        /*
        while (!delayingDisplay)
        {
            Debug.Log("Not delaying");
            yield return null;
        } */


        Debug.Log("Delaying");
 //start = false;
       // delayingDisplay = false;
       // yield return new WaitForSeconds(15);

        Debug.Log("After wait");
       
    }

    //shuffles deck
    public static List<Card> shuffleDeck(List<Card> deck)
    {
        List<Card> shuffledDeck = new List<Card>(); //initializes shuffled deck to be returned

        deck = MainClass.deck;

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
        Debug.Log($"inside deal rounds are {PlayerPrefs.GetInt("rounds")}");
        Player[] players = MainClass.players; //gets reference of players from MainClass

        //enables/sets active player buttons once cards have been dealt
        standButton.SetActive(true);
        hitButton.SetActive(true);

        deck = shuffleDeck(deck); //shuffles deck

        //adds cards to players' hands (playerHand) and to their respective GUI area card clot
        for(int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < 2; j++) //temporarily just deals to two players rather than the correct amount of players in game
            {
                Debug.Log("Before adding");
                addToDeck(players[i], pickRandomCard(deck));
                start = true;
                //delayingDisplay = true;
                Debug.Log("After adding");
                /*
                //startTimer = true; (IGNORE)

                if (true)
                {

                    //timer = 0;
                    //timerReached = false;
                    
                }*/
                
            }
        }

        GameObject[] areas = GameObject.FindGameObjectsWithTag(players[0].playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = dealerCard; //sets specific slot area to sprite/image

        if (players[0].playerHand[1].aceValue == 1)
        {
            players[0].handTotal -= Card.ACE_HIGH; //subtracts value of hidden card from dealer hand total 

        }
        else if (players[0].playerHand[1].aceValue == -1)
        {
            Debug.Log($"Inside low ace. Card is {players[0].playerHand[1].suit}{players[0].playerHand[1].pip} and aceValue is {players[0].playerHand[1].aceValue}");
            players[0].handTotal -= players[0].playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
        }
        else
        {
            if (players[0].playerHand[1].pip >= 11)
            {
                players[0].handTotal -= 10;
            }
            else
            {
                players[0].handTotal -= players[0].playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
            }
        }

        Debug.Log($"Dealers cards are {players[0].playerHand[0].suit}{players[0].playerHand[0].pip} and {players[0].playerHand[1].suit}{players[0].playerHand[1].pip}");
        Debug.Log($"Dealer hand is {players[0].handTotal}");

        GameObject playerText = GameObject.Find(players[0].playerName + "CountText"); //finds reference to dealer text box
        playerText.GetComponent<TextMeshProUGUI>().text = (players[0].handTotal + ""); //updates dealer value text

        GameObject dealButtonObject = GameObject.Find("DealButton"); //finds deal button
        dealButtonObject.SetActive(false); //disables deal button


    }

    public static void testMethod()
    {
        standButton.SetActive(false);
    }

    //adds card to deck (gui and playerHand)
    public static void addToDeck(Player player, Card card)
    {

        player.playerHand.Add(card);     //adds to playerHand    
        usedDeck.Add(convertSuit(card.suit, card.pip)); //adds chosen card to used deck

        System.Threading.Timer timer = null;
        timer = new System.Threading.Timer((obj) =>
        {
            //displayCard(card, player);
            testMethod();
            timer.Dispose();
        }, null, 15, System.Threading.Timeout.Infinite);

        //Task.Delay(15).ContinueWith(t => displayCard(card, player));

        //displayCard(card, player); //displays card
        calculateTotal(card, player); //adjusts card total 

    }

    //display cards to screen
    public static void displayCard(Card card, Player player)
    {
        //(NOT FULLY IMPLEMENTED) setup for if player currently needs to display more than 6 cards
        if (player.playerHand.Count > 6 && !player.playerName.Equals("Player"))
        {
            return;
        }

        int slot = int.Parse(player.cardSlotOrder[0] + ""); //finds correct slot number

        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[slot].GetComponent<Image>().sprite = card.sprite; //sets specific slot area to sprite/image
        //start = true;

        //if current player is "you", check to see if card area display needs second page
        if (player.playerName.Equals("Player"))
        {
            if (player.playerHand.Count > 6)
            {
                //(NOT IMPLEMENTED) Show second row of cards
                /*GameObject[] zoomAreas = GameObject.FindGameObjectsWithTag("PlayerZoomCardAreaBlock2");
                zoomAreas[slot].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                zoomAreas[slot].GetComponent<Image>().sprite = card.sprite;*/
            }
            else
            {
                //display hover card
                GameObject[] zoomAreas = GameObject.FindGameObjectsWithTag("PlayerZoomCardAreaBlock");
                zoomAreas[slot].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                zoomAreas[slot].GetComponent<Image>().sprite = card.sprite;
            }

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
                        player.playerHand[i].aceValue = -1; //sets ace value to indicate it's played low
                        player.handTotal -= 10; //adjust hand total to show it's played low
                        loweredAce = true; //sets loweredAce flag to true
                        break;
                    }
                }
           
                //player busted (NOT FULLY IMPLEMENTED)

                if (!loweredAce) //not able to play low, then player bust
                {

                    //end turn, move to next player


                    player.status = "bust"; //set player status to bust


                    //disable (you) player buttons
                    if (player.playerName.Equals("Player")) //disable buttons if "you" is playing
                    {
                        GameObject standButton = GameObject.Find("StandButton");
                        GameObject hitButton = GameObject.Find("HitButton");
                        standButton.SetActive(false);
                        hitButton.SetActive(false);
                    }
                }
            }
            
        }
        //else, if card is an ace...
        else
        {
            if(player.handTotal + Card.ACE_HIGH <= 21)
            {
                player.handTotal += Card.ACE_HIGH;
                card.aceValue = 1;

                if (player.handTotal == 21)
                {
                    if (player.playerName.Equals("Player"))
                    {
                        //disable "you" player's buttons
                        GameObject standButton = GameObject.Find("StandButton");
                        GameObject hitButton = GameObject.Find("HitButton");
                        standButton.SetActive(false);
                        hitButton.SetActive(false);
                    }

                    player.status = "win"; //update player status
                }

            }

            //if playing ace high will result in bust, play low
            else if(player.handTotal + Card.ACE_HIGH > 21)
            {
                player.handTotal += Card.ACE_LOW; //adds l
                card.aceValue = -1; //sets ace type to -1(low ace)

                //if player hand total is equal to 21, player wins, move turns (NOT FULLY IMPLEMENTED)
                if (player.handTotal == 21)
                {
                    if (player.playerName.Equals("Player"))
                    {
                        //disable "you" player's buttons
                        GameObject standButton = GameObject.Find("StandButton");
                        GameObject hitButton = GameObject.Find("HitButton");
                        standButton.SetActive(false);
                        hitButton.SetActive(false);
                    }

                    player.status = "win"; //update player status
                }
            }
            //play card as high ace as default
            else
            {
                player.handTotal += Card.ACE_HIGH; //adds high ace value to hand total

                if (player.playerName.Equals("Player"))
                {
                    //disable "you" player's buttons
                    GameObject standButton = GameObject.Find("StandButton");
                    GameObject hitButton = GameObject.Find("HitButton");
                    standButton.SetActive(false);
                    hitButton.SetActive(false);
                }

                player.status = "bust";
            }
        }

        //update player handTotal text
        GameObject playerText = GameObject.Find(player.playerName + "CountText");
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + "");

    }

    //shows outcome of card draw
    public static void showOutcome(Card card, Player player, string hitOrStand)
    {
        //show for few seconds, then disappear (NOT IMPLEMENTED)

        GameObject resultsAreaTextObject = GameObject.Find("ResultText"); //finds result text reference
        GameObject resultsAreaObject = GameObject.Find("ResultImage"); //Finds result area reference

        int next = -1; //sets an initial value for next, which represents an index 

        //if on last player before dealer, move to dealer's turn; helps determine who's next
        if (player.playerNumber == MainClass.players.Length - 1)
        {
            next = 0;
        }
        else //else, increment as normal
        {
            next = player.playerNumber + 1;
        }

        //if player hits (presses hit button), show text and card drawn
        if (hitOrStand.Equals("hit"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} {hitOrStand}!"; //displays text of name and action
            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255); //shows card
            resultsAreaObject.GetComponent<Image>().sprite = card.sprite; //displays card
        }

        //if player stands (presses stand button), show text
        else if (hitOrStand.Equals("stand"))
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} stood! "; //shows player and action if  //fix to have specific name (FINISH)
            if (player.playerName.Equals("Dealer"))
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += "Game Over!"; //shows player and action if  //fix to have specific name (FINISH)

            }
            else
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += $" {MainClass.players[next].playerName} is next! "; //shows player and action if  //fix to have specific name (FINISH)
            }
            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0); //hides card

        }
        else
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} Bust!"; //shows player and action if  //fix to have specific name (FINISH)
            if (player.playerName.Equals("Dealer"))
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += " Game Over!"; //shows player and action if  //fix to have specific name (FINISH)

            }
            else
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += $" {MainClass.players[next].playerName} is next! "; //shows player and action if  //fix to have specific name (FINISH)

            }
            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0); //hides card

        }        
        
        //IMPLEMENT WAIT FOR X amount of seconds then hide outcome again

    }

    //picks random card from deck
    public static Card pickRandomCard(List<Card> deck)
    {
        Card card = deck[0]; //pulls first card from shuffled deck
        deck.RemoveAt(0); //removes from deck
        return card; //returns card
    }

    //converts suit to suitable sprite/image card name
    public static string convertSuit(int suit, int pip)
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

        return $"{suitChar}{pip}"; //returns card string
    }

}

