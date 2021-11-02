using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainClass : MonoBehaviour
{

    //add arrows to gui

    public const int DECK_SIZE = 52; //constant max card variables
    public const int SUIT_COUNT = 4; //number of suits
    public const int PIP_COUNT = 13; //number of pips
    public const int MAX_PLAYERS = 4; //max amount of players
    public static Player[] players; //array of all players
    public static List<Card> deck; //deck of cards list varaible
    public static int currentPlayerNumber; //a variable that represents which player's turn it is

    void Start()
    {
        PlayerPrefs.SetInt("continueGame", 1);
        Debug.Log("EXECUTED");
        players = new Player[PlayerPrefs.GetInt("playerCount") + 1]; //initializes the player array with max players; maybe initialize with only amount of actual players
        deck = new List<Card>(); //initialize deck array

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Player();
        }


        //initializes the name area of each player
        players[0].playerNameBlockString = "DealerCardAreaBlock";
        players[1].playerNameBlockString = "PlayerCardAreaBlock";
        
        //initializes the player turn variable
        players[0].playerNumber = 0;
        players[1].playerNumber = 1;

        //initializes player name
        players[0].playerName = "Dealer";
        players[1].playerName = "Player";

        //if total players includes Sam, add Sam, hide Jill
        if(players.Length == 3)
        {
            players[2].playerNameBlockString = "SamCardAreaBlock";
            players[2].playerNumber = 2;        
            players[2].playerName = "Sam";

            GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock");
            jillAreaObject.SetActive(false);
        }

        //else initialize Sam and Jill players
        else if(players.Length == 4)
        {
            players[2].playerNameBlockString = "SamCardAreaBlock";
            players[2].playerNumber = 2;
            players[2].playerName = "Sam";

            players[3].playerNameBlockString = "JillCardAreaBlock";
            players[3].playerNumber = 3;    
            players[3].playerName = "Jill";

        }
        //else hide Sam and Jill
        else
        {
            GameObject samAreaObject = GameObject.Find("SamCardAreaBlock");
            samAreaObject.SetActive(false);

            GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock");
            jillAreaObject.SetActive(false);
        }

        

        //sets current player number/turn to 0;
        currentPlayerNumber = 1;

        if (PlayerPrefs.GetInt("bettingEnabled") == 2)
        {
            for (int i = 1; i < players.Length; i++)
            {
                //show betBlocks
                GameObject playerBetBlockObject = GameObject.Find(players[i].playerName + "BettingAmountBlock");
                playerBetBlockObject.SetActive(true);

                //calculate bets for AI
                if (i != 1)
                {
                    AIFunction.generateBetAmount(players[i]);
                }

                if (PlayerPrefs.GetInt("playersMoney" + i) == 0) {
                    PlayerPrefs.SetInt("playersMoney" + i, 500);

                }

                players[i].playerTotalMoney = PlayerPrefs.GetInt("playersMoney" + i);

                //update text to accurately reflect this
                playerBetBlockObject = GameObject.Find(players[i].playerName + "AmountBetText");
                playerBetBlockObject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + "";
                Debug.Log(players[i].playerName + " money amount is " + players[i].playerTotalMoney);
                playerBetBlockObject = GameObject.Find(players[i].playerName + "AmountText");
                Debug.Log(playerBetBlockObject == null);
                playerBetBlockObject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + "";
                Debug.Log("DONEEEE");
            }
        }
        else
        {
            for(int i = 1; i < players.Length; i++)
            {
                GameObject playerBetBlockObject = GameObject.Find(players[i].playerName + "BettingAmountBlock");
                playerBetBlockObject.SetActive(false);
            }
        }

        PlayerPrefs.Save();
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("continueGame") == 1);
        {
            testPlayer();
        }

        AIFunction.player = players[currentPlayerNumber];
        Dealer.player = players[currentPlayerNumber];
    }

    public static void testPlayer()
    {
        //if current player isn't playing anymore, move to next player
        if(PlayerPrefs.GetInt("continueGame") == 0)
        {
            return;
        }

        if (!players[currentPlayerNumber].status.Equals("playing"))
        {
            Debug.Log(players[currentPlayerNumber].playerName);

            if (currentPlayerNumber == 0)
            {
                Debug.Log("inside results");
                return;
            }
            else if (currentPlayerNumber == players.Length - 1)
            {
                currentPlayerNumber = 0; //sets to dealer if on last AI player

            }
            else
            {
                currentPlayerNumber++; //else, increments to next AI player
            }

            Debug.Log(players[currentPlayerNumber].playerName);
            changePlayerAction(players[currentPlayerNumber]); //changes player method
        }
    }

    //changes player once they win, stand, or bust after "you" gameplay
    public static void changePlayerAction(Player player)
    {
        Debug.Log($"{player.playerName}{player.playerNumber}");
        if(player.playerNumber == 0)
        {
            Debug.Log("Starting debug");
            Dealer.dealerStart = true;
        }
        else
        {
            Debug.Log("Inside AI");
            AIFunction.startAIPlay = true;
        }
    }

}

