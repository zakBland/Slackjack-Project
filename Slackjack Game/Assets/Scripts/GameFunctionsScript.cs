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
    static GameObject gameOverTextObject;
    static GameObject leaveButtonObject;
    static GameObject playAgainButtonObject;


    static bool start; //declares start boolean for starting game
    static bool startDelay; //declares start boolean for starting delays in game
    static Card cardRoutine; //card reference declaration for coroutine
    static Player playerRoutine; //player reference declaration for coroutine

    void Start()
    {
        

        startDelay = false; //sets startDelay to false
        cardRoutine = null; //sets cardRoutine to null
        playerRoutine = null; //sets playerRoutine to null
        start = false; //sets start to false

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

        gameOverTextObject.SetActive(false); //hides gameOver text 
    }

    void Awake()
    {
        //finds and initializes objects for (you) player buttons
        standButton = GameObject.Find("StandButton");
        hitButton = GameObject.Find("HitButton");

        gameOverTextObject = GameObject.Find("GameOverText"); //finds gameOver button
        leaveButtonObject = GameObject.Find("LeaveGameButton"); //finds leaveGame button
        playAgainButtonObject = GameObject.Find("PlayAgainButton"); //finds playAgain button
    }

    void Update()
    {
        MainClass.deck = deck; //updates MainClass deck of any changes made to GameFunctionsScript deck reference; may not be necessary

        //if start is true, start coroutine "delayDisplay"
        if (start)
        {
            StartCoroutine("delayDislay");
        }

    }

    //coroutine that starts game and includes delays
    IEnumerator delayDislay()
    {
        Player[] players = MainClass.players; //gets reference of players from MainClass
        start = false; //sets start to false
        GameObject dealButtonObject = GameObject.Find("DealButton"); //finds deal button
        dealButtonObject.SetActive(false); //disables deal button
        
        deck = MainClass.deck; // gets deck reference from MainClass
        deck = shuffleDeck(deck); //shuffles deck

        //adds cards to players' hands (playerHand) and to their respective GUI area card clot
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < players.Length; j++) //temporarily just deals to two players rather than the correct amount of players in game
            {
                addToDeck(players[j], pickRandomCard(deck), dealerCard);
                yield return new WaitForSeconds(0.6f); //waits specific amount of time before dealing next card
            }
        }

        //enables/sets active player buttons once cards have been dealt
        standButton.SetActive(true);
        hitButton.SetActive(true);

        //if hidden card is an ace and is played high, subtract high value
        if (players[0].playerHand[1].aceValue == 1)
        {
            players[0].handTotal -= Card.ACE_HIGH; //subtracts value of hidden card from dealer hand total 

        }
        //if hidden card is an ace and is played low, subtract low value
        else if (players[0].playerHand[1].aceValue == -1)
        {
            players[0].handTotal -= players[0].playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
        }
        //else subtract normal pip value from dealer hand total
        else
        {
            if (players[0].playerHand[1].pip >= 11) //if face card
            {
                players[0].handTotal -= 10; //subtract 10
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

        GameObject resultsAreaTextObject = GameObject.Find("ResultText"); //finds result text reference
        resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = ""; //displays text of name and action
    }

    //shuffles deck
    public static List<Card> shuffleDeck(List<Card> deck)
    {
        List<Card> shuffledDeck = new List<Card>(); //initializes shuffled deck to be returned
        deck = MainClass.deck; //gets new reference of deck from MainClass

        //randomly picks index of card to add to shuffled deck
        for(int i = 0, j = deck.Count; i < j; i++)
        {
            int index = Random.Range(0, deck.Count); //set index of randomly chosen card
            shuffledDeck.Add(deck[index]); //adds to shuffled deck
            deck.RemoveAt(index); //removes chosen card so it won't be chosen again
        }

        return shuffledDeck; //returns shuffled deck
    }

    //signals to start game
    public void dealCards()
    {
        start = true; //sets start to true
    }

    //adds card to deck (gui and playerHand)
    public static void addToDeck(Player player, Card card, Sprite dealerCard)
    {

        player.playerHand.Add(card); //adds to playerHand    
        usedDeck.Add(card.suit + "" + card.pip); //adds chosen card to used deck     
        displayCard(card, player, dealerCard); //displays card
        cardRoutine = card; //sets cardRoutine to current card reference
        playerRoutine = player; //sets playerRoutine to current player reference
        calculateTotal(card, player); //adjusts card total 
    }

    //display cards to screen
    public static void displayCard(Card card, Player player, Sprite dealerCard)
    {
        //(NOT FULLY IMPLEMENTED) setup for if player currently needs to display more than 6 cards
        if (player.playerHand.Count > 6 && !player.playerName.Equals("Player"))
        {
            return;
        }

        int slot = int.Parse(player.cardSlotOrder[0] + ""); //finds correct slot number

        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player

        //if current player is dealer and card to be displayed is hidden card
        if (player.playerName.Equals("Dealer") && slot == 3)
        {
            areas[slot].GetComponent<Image>().sprite = dealerCard; //sets specific slot area to sprite/image

        }
        else
        {
            areas[slot].GetComponent<Image>().sprite = card.sprite; //sets specific slot area to sprite/image

        }

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
                //GameObject[] zoomAreas = GameObject.FindGameObjectsWithTag("PlayerZoomCardAreaBlock");
                //zoomAreas[slot].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //zoomAreas[slot].GetComponent<Image>().sprite = card.sprite;
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
                bool loweredAce = false; //sets loweredAce to false

                //if there is an ace that is played as a high, play it as low
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    if (player.playerHand[i].aceValue == 1)
                    {
                        player.playerHand[i].aceValue = -1; //sets ace value to indicate it's played low
                        player.handTotal -= 10; //adjust hand total to show it's played low
                        loweredAce = true; //sets loweredAce flag to true
                        break; //if ace value is adjusted, leave
                    }
                }
           
                //player busted
                if (!loweredAce) //not able to play low, then player bust
                {
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
            //if playing ace high doesn't make player bust, play high
            if(player.handTotal + Card.ACE_HIGH <= 21)
            {
                player.handTotal += Card.ACE_HIGH; //adds high ace to player total
                card.aceValue = 1; //sets ace card to high ace value

                //if player total is 21, player wins
                if (player.handTotal == 21)
                {
                    /*if (player.playerName.Equals("Player")) //if current player is "you", disable hit and stand buttons
                    {
                        //disable "you" player's buttons
                        GameObject standButton = GameObject.Find("StandButton"); //finds stand button
                        GameObject hitButton = GameObject.Find("HitButton"); //finds hit button

                        //if found...
                        if (standButton != null && hitButton != null)
                        {
                            standButton.SetActive(false); //hide stand button
                            hitButton.SetActive(false); //hide hit button
                        }
                    }

                    player.status = "win"; //update player status to win
                    showOutcome(null, player, "stand"); //show outcome of calculation
                    */
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
                    /*
                    if (player.playerName.Equals("Player"))
                    {
                        //disable "you" player's buttons
                        GameObject standButton = GameObject.Find("StandButton");
                        GameObject hitButton = GameObject.Find("HitButton");
                        standButton.SetActive(false);
                        hitButton.SetActive(false);
                    }

                    player.status = "win"; //update player status to win
                    */
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

                player.status = "bust"; //updates player status to bust/lose
            }
        }

        //if current player is dealer and second (hidden) card is being calculated, skip to hide actual value
        if(player.playerName.Equals("Dealer") && player.playerHand.Count == 2) 
        {
            return;
        }

        //update player handTotal text
        GameObject playerText = GameObject.Find(player.playerName + "CountText"); //finds player count text
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + ""); // updates text to current handTotal value
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
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} stood! "; //shows player and action

            if (player.playerName.Equals("Dealer"))
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += "Game Over!"; //adds "game over", if dealer finished

            }
            else
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += $" {MainClass.players[next].playerName} is next! "; //shows player and action
            }

            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0); //hides card

        }
        else
        {
            resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text = $"{player.playerName} Bust!"; //shows player and action

            if (player.playerName.Equals("Dealer"))
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += " Game Over!"; //adds "game over", if dealer finished

            }
            else
            {
                resultsAreaTextObject.GetComponent<TextMeshProUGUI>().text += $" {MainClass.players[next].playerName} is next! "; //shows player and action 


            }
            resultsAreaObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0); //hides card
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

    //calculates end game results and displays to screen
    public static void calculateResults(Player[] players)
    {
        int dealerScore = players[0].handTotal; //sets dealer score to dealerScore variable

        //for each player, excluding the dealer
        for(int i = 1; i < players.Length; i++)
        {
            //if player hand total is greater than deal and less than or equal to 21
            if(players[i].handTotal > dealerScore && players[i].handTotal <= 21)
            {
                //win; collect money
                players[i].playerTotalMoney += players[i].betAmount; //adds bet from player total money
                players[i].status = "Win"; //set player status to win       
            }
            //if player hand total is greater than 21 or less than dealer score and dealer score is less than or equal to 21
            else if(players[i].handTotal > 21 || (players[i].handTotal < dealerScore && dealerScore <= 21) )
            {
                //bust or less than dealer; lose money 
                players[i].playerTotalMoney -= players[i].betAmount; //subtracts bet from player total money
                players[i].status = "Lose"; //set player status to lose
            }
            //if player total equals dealer score and dealer score is less than or equal to 21
            else if (players[i].handTotal == dealerScore && dealerScore <= 21)
            {
                //tie, no payout
                players[i].status = "Tie"; //set player status to tiw
            }
            //if player score is less than or equal to 21 and dealer score is greater than 21
            else if(players[i].handTotal <= 21 && dealerScore > 21)
            {
                //win, dealer bust; win money
                players[i].playerTotalMoney += players[i].betAmount; //adds bet from player total money
                players[i].status = "win"; //set player status to win
            }

            if (players[i].playerTotalMoney < 2)
            {
                if (i == 1) continue; //skip player (you)

                PlayerPrefs.SetInt("playersMoney" + i, -1); //set playerMoney to -1 to indicate removing the player
                PlayerPrefs.SetString("removedPlayerList", i + ""); //adds player to removed player list
                Debug.Log(players[i].playerName + " has " + players[i].playerTotalMoney + " total money. PlayerPrefs money AI1 is" + PlayerPrefs.GetInt("playersMoney" + 2) + " and AI2 is " + PlayerPrefs.GetInt("playersMoney" + 3));
            }
            else
            {
                PlayerPrefs.SetInt("playersMoney" + i, players[i].playerTotalMoney); //set playerMoney to new current value
            }
            PlayerPrefs.Save(); //save PlayerPrefs values
        }

        //if player has no more money....
        if(PlayerPrefs.GetInt("playersMoney1") == -1)
        {
            leaveButtonObject.transform.position = new Vector3(-0.5f, -0.5f, 0); //center leave button
            playAgainButtonObject.SetActive(false); //hide playAgain button
            gameOverTextObject.SetActive(true); //show gameOver text
        }
    }

}

