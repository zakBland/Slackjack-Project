using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainClass : MonoBehaviour
{
    public const int DECK_SIZE = 52; //constant max card variables
    public const int SUIT_COUNT = 4; //number of suits
    public const int PIP_COUNT = 13; //number of pips
    public const int MAX_PLAYERS = 4; //max amount of players
    public static Player[] players; //array of all players
    public static List<Card> deck; //deck of cards list varaible
    public static int currentPlayerNumber; //a variable that represents which player's turn it is

    void Start()
    {
        PlayerPrefs.SetInt("continueGame", 1); //sets continueGame to 1
        players = new Player[PlayerPrefs.GetInt("playerCount") + 1]; //initializes the player array with max players; maybe initialize with only amount of actual players
        deck = new List<Card>(); //initialize deck array

        //find out if betting player is out of money; if so, remove from game
        int newPlayerLength = PlayerPrefs.GetInt("playerCount") + 1; //sets newPlayerLength to current players amount
        string removedPlayers = ""; //initializes removed player

        for(int i = 0; i < players.Length; i++)
        {
            if (i == 0 || i == 1) continue; //if on player(you) or dealer, skip
            
            //if player ran out of money
            if(PlayerPrefs.GetInt("playersMoney" + i) == -1)
            {
                newPlayerLength--; //decrement length
                removedPlayers += "" + i; //add player index to removedPlayers string
            }
        }

        players = new Player[newPlayerLength]; //sets new player length after removing a player


        //initializes player array with default constructor
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Player();
        }


        //initializes the name area of each player for default players
        players[0].playerNameBlockString = "DealerCardAreaBlock";
        players[1].playerNameBlockString = "PlayerCardAreaBlock";
        
        //initializes the player turn variable for default players
        players[0].playerNumber = 0;
        players[1].playerNumber = 1;

        //initializes player name for default players
        players[0].playerName = "Dealer";
        players[1].playerName = "Player";

        //if total players includes Sam, add Sam, hide Jill
        if(players.Length == 3)
        {
            if (removedPlayers.Length == 1 && removedPlayers[0] == '3')
            {
                players[2].playerNameBlockString = "SamCardAreaBlock"; //initializes the name area 
                players[2].playerNumber = 2; // initializes player number
                players[2].playerName = "Sam"; //initializes name

                GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock"); //finds jill player block
                jillAreaObject.SetActive(false); //hides block object
            }
            else
            {
                players[2].playerNameBlockString = "JillCardAreaBlock"; //initializes the name area
                players[2].playerNumber = 2; //initializes the player number
                players[2].playerName = "Jill"; //initializes name

                GameObject samAreaObject = GameObject.Find("SamCardAreaBlock"); //finds jill player block
                samAreaObject.SetActive(false); //hides block object
            }
        }

        //else initialize Sam and Jill players
        else if(players.Length == 4)
        {
            players[2].playerNameBlockString = "SamCardAreaBlock"; //initializes the name area
            players[2].playerNumber = 2; //initializes the player number
            players[2].playerName = "Sam"; //initializes name

            players[3].playerNameBlockString = "JillCardAreaBlock"; //initializes the name area
            players[3].playerNumber = 3; //initializes the player number
            players[3].playerName = "Jill"; //initializes name
        }

        //else hide Sam and Jill
        else
        {
            GameObject samAreaObject = GameObject.Find("SamCardAreaBlock"); //finds sam player object
            samAreaObject.SetActive(false); //hides block object

            GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock"); //finds jill player object
            jillAreaObject.SetActive(false); //hides block object
        }

        currentPlayerNumber = 1; //sets current player number/turn to 0;

        //if betting is enabled
        if (PlayerPrefs.GetInt("bettingEnabled") == 2)
        {
            //for each player, not including the dealer (player[0])
            for (int i = 1; i < players.Length; i++)
            {
                //show betBlocks
                GameObject playerBetBlockObject = GameObject.Find(players[i].playerName + "BettingAmountBlock"); //finds player bet block
                playerBetBlockObject.SetActive(true); //shows block object

                //calculate bets for AI
                if (i != 1) //if AI player
                {
                    AIFunction.generateBetAmount(players[i]); //generates bet amount for AI
                }

                //sets player money amount
                if (PlayerPrefs.GetInt("playersMoney" + i) == 0) 
                {
                    PlayerPrefs.SetInt("playersMoney" + i, 500); //sets player money to 0
                    players[i].playerTotalMoney = PlayerPrefs.GetInt("playersMoney" + i);
                }

                players[i].playerTotalMoney = PlayerPrefs.GetInt("playersMoney" + i); //sets playerMoney to player money field in Player object

                //update text to accurately reflect this
                playerBetBlockObject = GameObject.Find(players[i].playerName + "AmountBetText"); //finds player bet amount text
                playerBetBlockObject.GetComponent<TextMeshProUGUI>().text = players[i].betAmount + ""; //gets text object and sets to bet amount
                playerBetBlockObject = GameObject.Find(players[i].playerName + "AmountText"); //find money amount text for player
                playerBetBlockObject.GetComponent<TextMeshProUGUI>().text = players[i].playerTotalMoney + ""; //gets text object and sets player money to text
            }
        }
        else
        {
            //if betting isn't enabled
            for(int i = 1; i < players.Length; i++)
            {
                GameObject playerBetBlockObject = GameObject.Find(players[i].playerName + "BettingAmountBlock"); //find all betting blocks
                playerBetBlockObject.SetActive(false); //hide block objects
            }
        }

        PlayerPrefs.Save(); //save changes to PlayerPref values
        
    }

    void Update()
    {
        //if game is still active, check to see which player is next
        if (PlayerPrefs.GetInt("continueGame") == 1);
        {
            testPlayer();
        }

        //this code could cause issues later; might need to specify when to set these variables
        AIFunction.player = players[currentPlayerNumber]; //sets current player reference for player variable in AIFunction class
        Dealer.player = players[currentPlayerNumber]; //sets current player reference for player variable in Dealer class;
    }

    //checks to see which player is next
    public static void testPlayer()
    {
        //if game is over (continueGame == 0), skip testing player
        if(PlayerPrefs.GetInt("continueGame") == 0)
        {
            return;
        }
        
        //if current player isn't playing anymore, move to next player
        if (!players[currentPlayerNumber].status.Equals("playing"))
        {
            //if current player is dealer, skip
            if (currentPlayerNumber == 0)
            {
                return;
            }
            else if (currentPlayerNumber == players.Length - 1) //if last AI player
            {
                currentPlayerNumber = 0; //sets dealer to be next (last) if on last AI player

            }
            else
            {
                currentPlayerNumber++; //else, increments to next AI player
            }

            changePlayerAction(players[currentPlayerNumber]); //changes player method
        }
    }

    //changes player once they win, stand, or bust after "you" gameplay
    public static void changePlayerAction(Player player)
    {
        //if player is dealer
        if(player.playerNumber == 0)
        {
            Dealer.dealerStart = true; //starts dealer play
        }
        else
        {
            AIFunction.startAIPlay = true; //starts AI play
        }
    }

}

