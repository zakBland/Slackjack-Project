using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Dealer : MonoBehaviour
{
    static GameObject playAgainClassObject;

    void Start()
    {
        playAgainClassObject.SetActive(false);

    }

    void Awake()
    {
        playAgainClassObject = GameObject.Find("PlayAgainGroupBlock");

    }

    public static void dealerPlay(Player player)
    {
        //reveal hidden card
        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = player.playerHand[1].sprite; ; //sets specific slot area to sprite/image

        //update hand total
        player.handTotal += player.playerHand[1].pip; //adds to total
        GameObject playerText = GameObject.Find(player.playerName + "CountText"); //finds textbox
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + ""); //update textbox


        //if total is 17 or more, stand
        if (player.handTotal >= 17)
        {
            //end game, show outcome of dealer, show results of all players
            GameFunctionsScript.showOutcome(null, player, "stand"); //show outcome
            player.status = "stand"; //update player status to stand
            Debug.Log("Dealer stands");
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

                //calculate total
                GameFunctionsScript.calculateTotal(card, player);

                //display card to player area
                GameFunctionsScript.displayCard(card, player);

                Debug.Log($"Dealer hand total is {player.handTotal}");

                //display outcome
                if (player.handTotal > 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "bust"); //show outcome
                    player.status = "bust"; //update player status to bust
                    break;

                }
                else if(player.handTotal >= 17)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand");
                    player.status = "stand";
                    break;
                }
                else if(player.handTotal == 21)
                {
                    GameFunctionsScript.showOutcome(null, player, "stand");
                    player.status = "win";
                    break;
                }
                else
                {
                    GameFunctionsScript.showOutcome(card, player, "hit"); //show outcome
                }

                // if player.handTotal == 21 (NOT IMPLEMENTED)
                
            }
            //keep hitting until 17 or above
            while (player.handTotal < 17);

            //MainClass.continueGame = false;
        }            
        PlayerPrefs.SetInt("continueGame", 0);

        playAgainClassObject.SetActive(true);

    }
}
