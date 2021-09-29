using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClass : MonoBehaviour
{

    //add arrows to gui
    //add soft hand button options
    //add card total currently
    //change color of name for current player
    //show how softhand is played

    public const int DECK_SIZE = 52; //constant max card variables
    public const int SUIT_COUNT = 4; //number of suits
    public const int PIP_COUNT = 13; //number of pips
    public const int MAX_PLAYERS = 4; //max amount of players
    public static Player[] players; //array of all players
    public static List<Card> deck; //deck of cards list varaible
    public static int currentPlayerNumber; //a variable that represents which player's turn it is

    void Start()
    {
        players = new Player[PlayerPrefs.GetInt("playerCount") + 1]; //initializes the player array with max players; maybe initialize with only amount of actual players
        deck = new List<Card>(); //initialize deck array

        for(int i = 0; i < players.Length; i++)
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

        if(players.Length == 3)
        {
            players[2].playerNameBlockString = "SamCardAreaBlock";
            players[2].playerNumber = 2;        
            players[2].playerName = "Sam";

            GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock");
            jillAreaObject.SetActive(false);
        }

        else if(players.Length == 4)
        {
            players[3].playerNameBlockString = "JillCardAreaBlock";
            players[3].playerNumber = 3;    
            players[3].playerName = "Jill";

        }
        else
        {
            GameObject samAreaObject = GameObject.Find("SamCardAreaBlock");
            samAreaObject.SetActive(false);

            GameObject jillAreaObject = GameObject.Find("JillCardAreaBlock");
            jillAreaObject.SetActive(false);
        }
        

        //sets current player number/turn to 0;
        currentPlayerNumber = 1;
        
        
        
    }

    void Update()
    {
        if (!players[currentPlayerNumber].status.Equals("playing"))
        {
            if(currentPlayerNumber == players.Length - 1)
            {
                currentPlayerNumber = 0;
            }
            else
            {
                currentPlayerNumber++;
            }
            changePlayerAction(players[currentPlayerNumber]);
        }

    }

    public static void changePlayerAction(Player player)
    {
        if(player.playerNumber == 0)
        {
            Dealer.dealerPlay(player);
        }
        else
        {
           AIFunction.AIPlay(player);
        }
    }

}

