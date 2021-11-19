using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Dealer : MonoBehaviour
{
    //public static GameObject playAgainClassObject; 
    public static bool dealerStart; //declares bool variable to indicate start of dealerPlay
    public static Player player; //declares reference to current player
    public static GameObject resultsGroupBlock; //declares reference to resultsGroup in Scene

    void Start()
    {
        //playAgainClassObject.SetActive(false);
        dealerStart = false;  //sets variable to false
        resultsGroupBlock.SetActive(false); //hides resultsGroupBlock
    }

    void Awake()
    {
        //playAgainClassObject = GameObject.Find("PlayAgainGroupBlock");
        resultsGroupBlock = GameObject.Find("ResultsGroupBlock"); //finds reference to ResultsGroupBlock
    }

    void Update()
    {
        //if time to start dealer play
        if (dealerStart)
        {
            StartCoroutine("dealerPlayFunction"); //executes coroutine with dealerPlayFunction function
        }
    }

    //
    IEnumerator dealerPlayFunction()
    {
        dealerStart = false; //sets dealer start variable to false

        //yield return new WaitForSeconds(2);

        //reveal hidden card
        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = player.playerHand[1].sprite; ; //sets specific slot area to sprite/image

        //if dealer hidden card is played high, update hand total to reflect it
        if (player.playerHand[1].aceValue == 1)
        {
            player.handTotal += Card.ACE_HIGH; //subtracts value of hidden card from dealer hand total 

        }
        //if dealer hidden card is played low, update hand total to reflect it
        else if (player.playerHand[1].aceValue == -1)
        {
            player.handTotal += player.playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
        }
        //else add normal pip value to dealer total
        else
        {
            //if face card, add 10
            if (player.playerHand[1].pip >= 11)
            {
                player.handTotal += 10;
            }
            //else add normal pip value
            else
            {
                player.handTotal += player.playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
            }
        }

        GameObject playerText = GameObject.Find(player.playerName + "CountText"); //finds textbox
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + ""); //update textbox

        Debug.Log("Dealer handTotal is " + player.handTotal);
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed"));  //delays game for specific amount of time

        //if total is 17 or more, stand
        if (player.handTotal >= 17)
        {
            //end game, show outcome of dealer, show results of all players
            GameFunctionsScript.showOutcome(null, player, "stand"); //show outcome
            player.status = "stand"; //update player status to stand
            Debug.Log("Dealer stands");
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed")); // delays game for specific amount of time
        }

        //else, hit
        else
        {
            do
            {
                //check if hand has ace
                bool loweredAce = false;

                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    //if so, make sure to play as high, if it will total less than 22 and greater than 16
                    if (player.playerHand[i].aceValue == -1 && (player.handTotal + Card.ACE_HIGH >= 16) && (player.handTotal + Card.ACE_HIGH <= 21))
                    {
                        player.playerHand[i].aceValue = 1; //update ace value to high, if possible
                        player.handTotal += 10; //adds 10 to hand total 
                        loweredAce = true; //sets lowered ace to true
                        break;
                    }
                }

                List<Card> deck = MainClass.deck; //gets deck reference
                Card card = GameFunctionsScript.pickRandomCard(deck); //picks random card from MainClass deck

                player.playerHand.Add(card);     //adds to playerHand 
                GameFunctionsScript.showOutcome(card, player, "hit"); //show outome
                GameFunctionsScript.usedDeck.Add(GameFunctionsScript.convertSuit(card.suit, card.pip)); //adds chosen card to used deck   
                GameFunctionsScript.calculateTotal(card, player); //calculate total
                GameFunctionsScript.displayCard(card, player, null); //display card to player are

                Debug.Log($"Dealer hand total is {player.handTotal}");
                yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed")); //waits for a specific amount of seconds

                //if player bust...
                if (player.handTotal > 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "bust"); //show outcome
                    player.status = "bust"; //update player status to bust
                    break;

                }
                //if player wins...
                else if (player.handTotal == 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand"); //show outcome
                    player.status = "win"; //update status to win
                    break;
                }
                //if player hand it greater than or equal to 17...
                else if (player.handTotal >= 17)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand"); //show outcome
                    player.status = "stand"; //set status to stand
                    break;
                }     
                else
                {
                 //   GameFunctionsScript.showOutcome(card, player, "hit"); //show outcome
                }

                yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed")); //delays the game for specified amount of secconds

            }
            //keep hitting until 17 or above
            while (player.handTotal < 17);

            yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed") + 0.5f); //delays the game for specified amount of secconds
        }

        Debug.Log("Done1");
        PlayerPrefs.SetInt("continueGame", 0); //sets continueGame to false
        Debug.Log("Done2");
        //playAgainClassObject.SetActive(true);
        Debug.Log("Done3");
        GameFunctionsScript.calculateResults(MainClass.players); //calculates results at end of game
            
        Player[] players = MainClass.players; //gets references to all players
        GameObject gameobject; //declares gameObject variable

        resultsGroupBlock.SetActive(true); //shows resultsGroupBlock

        for (int i = 0; i < 4; i++)
        {
            //if dealer...
            if(i == 0)
            {
                gameobject = GameObject.Find(players[i].playerName + "NameText"); //finds reference to NameText
                gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName; //sets text to playerName
                gameobject = GameObject.Find(players[i].playerName + "TotalText"); //finds reference to TotalText
                gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + ""; //sets text to handTotal
                gameobject = GameObject.Find(players[i].playerName + "BetText"); //finds reference to BetText
                gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                gameobject = GameObject.Find(players[i].playerName + "ScoreText"); //finds reference to ScoreText
                gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player

            }
            //else if current player...
            else if (i < players.Length)
            {
                //if player(you)...
                if (i == 1)
                {
                    gameobject = GameObject.Find(players[i].playerName + "NameText"); //finds reference to NameText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName; //sets text to playerName
                    gameobject = GameObject.Find(players[i].playerName + "TotalText"); //finds reference to TotalText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")"; //sets text to handTotal and player game status

                    //if betting is enabled....
                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find(players[i].playerName + "BetText"); //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + ""; //sets text to betAmount
                        gameobject = GameObject.Find(players[i].playerName + "ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + ""; //sets text to current player money
                    }
                    //else if not enabed....
                    else
                    {
                        gameobject = GameObject.Find(players[i].playerName + "BetText"); //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                        gameobject = GameObject.Find(players[i].playerName + "ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                    }
                }
                //else if AI1....
                else if(i == 2)
                {
                    gameobject = GameObject.Find("AI1NameText"); //finds reference to NameText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName; //sets text to playerName
                    gameobject = GameObject.Find("AI1TotalText"); //finds reference to TotalText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")"; //sets text to handTotal and player game status

                    //if betting is enabled...
                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find("AI1BetText");  //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + ""; //sets text to betAmount
                        gameobject = GameObject.Find("AI1ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + ""; //sets text to current player money
                    }
                    //else if not enabled...
                    else
                    {
                        gameobject = GameObject.Find("AI1BetText"); //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                        gameobject = GameObject.Find("AI1ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                    }
                }
                //else if AI2
                else
                {
                    gameobject = GameObject.Find("AI2NameText"); //finds reference to NameText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName; //sets text to playerName
                    gameobject = GameObject.Find("AI2TotalText"); //finds reference to TotalText
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")"; //sets text to handTotal and player game status

                    //if betting is enabled....
                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find("AI2BetText"); //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + ""; //sets text to betAmount
                        gameobject = GameObject.Find("AI2ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + ""; //sets text to current player money
                    }
                    //else if betting is not enabled....
                    else
                    {
                        gameobject = GameObject.Find("AI2BetText"); //finds reference to BetText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                        gameobject = GameObject.Find("AI2ScoreText"); //finds reference to ScoreText
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-"; //sets text to dash to indicate inactive player
                    }
                }
            }
            //if player isn't in game, hide player sections
            else
            {
                string name = "";

                if(MainClass.removedPlayers.Length != 0 && "S".Equals(MainClass.removedPlayers[i - 2] + ""))
                {
                    name = "AI1";
                }
                else if ((players.Length == 3 && players[2].playerName.Equals("Sam")) || "J".Equals(MainClass.removedPlayers[i -2] + ""))
                {
                    name = "AI2";
                }
              

                gameobject = GameObject.Find(name + "NameText"); //finds reference to NameText
                gameobject.GetComponent<TextMeshProUGUI>().text = ""; //sets text to blank
                gameobject = GameObject.Find(name + "TotalText"); //finds reference to TotalText
                gameobject.GetComponent<TextMeshProUGUI>().text = ""; //sets text to blank
                gameobject = GameObject.Find(name + "BetText"); //finds reference to BetText
                gameobject.GetComponent<TextMeshProUGUI>().text = ""; //sets text to blank
                gameobject = GameObject.Find(name + "ScoreText"); //finds reference to ScoreText
                gameobject.GetComponent<TextMeshProUGUI>().text = ""; //sets text to blank
            }
        }

        yield return new WaitForSeconds(2); //delays game for specified amount of time
    }

}
