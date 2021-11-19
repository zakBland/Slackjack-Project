using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Dealer : MonoBehaviour
{
    public static GameObject playAgainClassObject;
    public static bool dealerStart;
    public static Player player;
    public static GameObject resultsGroupBlock;

    void Start()
    {
        //playAgainClassObject.SetActive(false);
        dealerStart = false;       
        resultsGroupBlock.SetActive(false);
    }

    void Awake()
    {
        //playAgainClassObject = GameObject.Find("PlayAgainGroupBlock");
        resultsGroupBlock = GameObject.Find("ResultsGroupBlock");
    }

    void Update()
    {
        if (dealerStart)
        {
            StartCoroutine("dealerPlayFunction");
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
            Debug.Log($"Inside low ace. Card is {player.playerHand[1].suit}{player.playerHand[1].pip} and aceValue is {player.playerHand[1].aceValue}");
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

        //player.handTotal += player.playerHand[1].pip; //adds to total
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
                        loweredAce = true;
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

                //display outcome
                if (player.handTotal > 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "bust"); //show outcome
                    player.status = "bust"; //update player status to bust
                    break;

                }
                else if (player.handTotal == 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand");
                    player.status = "win";
                    break;
                }
                else if (player.handTotal >= 17)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand");
                    player.status = "stand";
                    break;
                }     
                else
                {
                 //   GameFunctionsScript.showOutcome(card, player, "hit"); //show outcome
                }

                yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed"));

            }
            //keep hitting until 17 or above
            while (player.handTotal < 17);

            yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed") + 0.5f);


        }

        Debug.Log("Done1");
        PlayerPrefs.SetInt("continueGame", 0);
        Debug.Log("Done2");
        //playAgainClassObject.SetActive(true);
        Debug.Log("Done3");
        GameFunctionsScript.calculateResults(MainClass.players);
            
        Player[] players = MainClass.players;
        GameObject gameobject;

        resultsGroupBlock.SetActive(true);

        GameObject gameObject;

        for (int i = 0; i < 4; i++)
        {
            if(i == 0)
            {
                gameobject = GameObject.Find(players[i].playerName + "NameText");
                gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName;
                gameobject = GameObject.Find(players[i].playerName + "TotalText");
                gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + "";  
                gameobject = GameObject.Find(players[i].playerName + "BetText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                gameobject = GameObject.Find(players[i].playerName + "ScoreText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                
            }
            else if (i < players.Length)
            {
                if (i == 1)
                {
                    gameobject = GameObject.Find(players[i].playerName + "NameText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName;
                    gameobject = GameObject.Find(players[i].playerName + "TotalText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")";
                   

                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find(players[i].playerName + "BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + "";
                        gameobject = GameObject.Find(players[i].playerName + "ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + "";
                    }
                    else
                    {
                        gameobject = GameObject.Find(players[i].playerName + "BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                        gameobject = GameObject.Find(players[i].playerName + "ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                    }
                }
                else if(i == 2)
                {
                    gameobject = GameObject.Find("AI1NameText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName;
                    gameobject = GameObject.Find("AI1TotalText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")";
                    

                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find("AI1BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + "";
                        gameobject = GameObject.Find("AI1ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + "";
                    }
                    else
                    {
                        gameobject = GameObject.Find("AI1BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                        gameobject = GameObject.Find("AI1ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                    }
                }
                else
                {
                    gameobject = GameObject.Find("AI2NameText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerName;
                    gameobject = GameObject.Find("AI2TotalText");
                    gameobject.GetComponent<TextMeshProUGUI>().text = players[i].handTotal + " (" + players[i].status + ")";
                    

                    if (PlayerPrefs.GetInt("bettingEnabled") == 2)
                    {
                        gameobject = GameObject.Find("AI2BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + "";
                        gameobject = GameObject.Find("AI2ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + "";
                    }
                    else
                    {
                        gameobject = GameObject.Find("AI2BetText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                        gameobject = GameObject.Find("AI2ScoreText");
                        gameobject.GetComponent<TextMeshProUGUI>().text = "-";
                    }
                }
            }
            else
            {
<<<<<<< HEAD
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
=======
                gameobject = GameObject.Find(players[i].playerName + "NameText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "";
                gameobject = GameObject.Find(players[i].playerName + "TotalText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "";
                gameobject = GameObject.Find(players[i].playerName + "BetText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "";
                gameobject = GameObject.Find(players[i].playerName + "ScoreText");
                gameobject.GetComponent<TextMeshProUGUI>().text = "";
>>>>>>> parent of 176130d (Update 5?)
            }
        }

        yield return new WaitForSeconds(2);


       

    }

}
