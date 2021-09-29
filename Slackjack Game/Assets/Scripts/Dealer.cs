using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Dealer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void dealerPlay(Player player)
    {
        //reveal hidden card
        GameObject[] areas = GameObject.FindGameObjectsWithTag(player.playerNameBlockString); //finds card slot areas for a player
        areas[3].GetComponent<Image>().sprite = player.playerHand[1].sprite; ; //sets specific slot area to sprite/image

        //update hand total
        player.handTotal += player.playerHand[1].pip;
        GameObject playerText = GameObject.Find(player.playerName + "CountText");
        playerText.GetComponent<TextMeshProUGUI>().text = (player.handTotal + "");


        //if total is 17 or more, stand
        if (player.handTotal >= 17)
        {
            //end game, show outcome of dealer, show results of all players
            GameFunctionsScript.showOutcome(null, player, "stand");
            player.status = "stand";
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
                        player.playerHand[i].aceValue = 1;
                        player.handTotal += 10;
                        loweredAce = true;
                        break;
                    }
                }

                List<Card> deck = MainClass.deck;
                Card card = GameFunctionsScript.pickRandomCard(deck); //picks random card from MainClass deck

                //calculate total
                GameFunctionsScript.calculateTotal(card, player);

                //display card to player area
                GameFunctionsScript.displayCard(card, player);

                //display outcome

                if (player.handTotal > 21)
                {
                    GameFunctionsScript.showOutcome(card, player, "bust");
                    player.status = "bust";


                }
                else
                {
                    GameFunctionsScript.showOutcome(card, player, "hit");
                }
                
            }
            //keep hitting until 17 or above
            while (player.handTotal < 17);
            
            

        }
    }
}
