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

    void Start()
    {
        playAgainClassObject.SetActive(false);
        dealerStart = false;

    }

    void Awake()
    {
        playAgainClassObject = GameObject.Find("PlayAgainGroupBlock");
    }

    void Update()
    {
        if (dealerStart)
        {
            StartCoroutine("dealerPlayFunction");
        }
    }


    IEnumerator dealerPlayFunction()
    {
        dealerStart = false;

        //yield return new WaitForSeconds(2);

        //reveal hidden card
        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = player.playerHand[1].sprite; ; //sets specific slot area to sprite/image

        //update hand total

        if (player.playerHand[1].aceValue == 1)
        {
            player.handTotal += Card.ACE_HIGH; //subtracts value of hidden card from dealer hand total 

        }
        else if (player.playerHand[1].aceValue == -1)
        {
            Debug.Log($"Inside low ace. Card is {player.playerHand[1].suit}{player.playerHand[1].pip} and aceValue is {player.playerHand[1].aceValue}");
            player.handTotal += player.playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
        }
        else
        {
            if (player.playerHand[1].pip >= 11)
            {
                player.handTotal += 10;
            }
            else
            {
                player.handTotal += player.playerHand[1].pip; //subtracts value of hidden card from dealer hand total 
            }
        }


        //player.handTotal += player.playerHand[1].pip; //adds to total
        GameObject playerText = GameObject.Find(player.playerName + "CountText"); //finds textbox
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + ""); //update textbox

        Debug.Log("Dealer handTotal is " + player.handTotal);
        yield return new WaitForSeconds(1.5f);

        //if total is 17 or more, stand
        if (player.handTotal >= 17)
        {
            //end game, show outcome of dealer, show results of all players
            GameFunctionsScript.showOutcome(null, player, "stand"); //show outcome
            player.status = "stand"; //update player status to stand
            Debug.Log("Dealer stands");
            yield return new WaitForSeconds(1.5f);
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
                        player.handTotal += 10;
                        loweredAce = true;
                        break;
                    }
                }

                List<Card> deck = MainClass.deck; //gets deck reference
                Card card = GameFunctionsScript.pickRandomCard(deck); //picks random card from MainClass deck

                player.playerHand.Add(card);     //adds to playerHand 
                GameFunctionsScript.showOutcome(card, player, "hit");
                GameFunctionsScript.usedDeck.Add(GameFunctionsScript.convertSuit(card.suit, card.pip)); //adds chosen card to used deck   
                //calculate total
                GameFunctionsScript.calculateTotal(card, player);

                //display card to player area
                GameFunctionsScript.displayCard(card, player, null);

                Debug.Log($"Dealer hand total is {player.handTotal}");
                yield return new WaitForSeconds(1.5f);
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

                yield return new WaitForSeconds(1.5f);

            }
            //keep hitting until 17 or above
            while (player.handTotal < 17);

            yield return new WaitForSeconds(2);


        }

        Debug.Log("Done1");
        PlayerPrefs.SetInt("continueGame", 0);
        Debug.Log("Done2");
        playAgainClassObject.SetActive(true);
        Debug.Log("Done3");            
        yield return new WaitForSeconds(2);


       

    }

}
