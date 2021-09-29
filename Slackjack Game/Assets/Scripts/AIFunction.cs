using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunction: MonoBehaviour
{
    public static int cardsRemaining;
    public static List<Card> deck;
    public static List<string> usedDeck;
    public static double probability;
    public static Player player;
    public static int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        deck = MainClass.deck;
        cardsRemaining = deck.Count;
        probability = 0;
        //player = MainClass.players[MainClass.currentPlayerNumber];
        usedDeck = GameFunctionsScript.usedDeck;
        difficulty = PlayerPrefs.GetInt("difficultyLevel");
    }

    // Update is called once per frame
    void Update()
    {
        usedDeck = GameFunctionsScript.usedDeck;
        
    }


    //Determine how many cards remain    
    //Determine hand total

    //Calculate probability of finding card

    public static double probabilityOfCard(Player player)
    {
        int maxValue = 21 - player.handTotal;
        int possibleCards = maxValue * 4;


        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= maxValue; j++)
            {
                if (GameFunctionsScript.usedDeck.Contains($"{i}{j}"))
                {
                    possibleCards--;
                }
            }
        }

        //calculate probability
        probability = possibleCards / (cardsRemaining * 1.0);

        return probability;
    }

    public static void AIPlay(Player player)
    {
        probability = probabilityOfCard(player);

        while (true)
        {
            if(player.handTotal == 21)
            {
                stand(player);
                break;
            }
            else if(player.handTotal > 21)
            {
                GameFunctionsScript.showOutcome(null, player, "bust");

            }
            else if ((difficulty == 0 && probability > 40) || (difficulty == 1 && probability > 65) || (difficulty == 2 && probability > 90) && player.handTotal < 21)
            {
                //hit
                hit(player);

            }
            else
            {
                stand(player);
                break;
            }
        }

    }

    
    //hit
    public static void hit(Player player)
    {
        Card card = GameFunctionsScript.pickRandomCard(MainClass.deck); ;
        GameFunctionsScript.addToDeck(player, card);
        GameFunctionsScript.showOutcome(card, player, "hit");
        GameFunctionsScript.calculateTotal(card, player);


    }

    //stand and move turn
    public static void stand(Player player)
    {
        GameFunctionsScript.showOutcome(null, player, "stand");

    }
    
}
