using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunctionsScript : MonoBehaviour
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
        player = MainClass.players[MainClass.currentPlayerNumber];
        usedDeck = GameFunctionsScript.usedDeck;
        difficulty = PlayerPrefs.GetInt("difficultyLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //Determine how many cards remain    
    //Determine hand total
    
    //Calculate probability of finding card

    public static double probabilityOfCard()
    {
        int maxValue = 21 - player.handTotal;
        int possibleCards = maxValue * 4;


        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= maxValue; j++)
            {
                if (usedDeck.Contains($"{i}{j}"))
                {
                    possibleCards--;
                }
            }
        }

        //calculate probability
        probability = possibleCards / (cardsRemaining * 1.0);

        return probability;
    }

    public static void AI_Play()
    {
        probability = probabilityOfCard();

        while (true) {
            if ((difficulty == 0 && probability > 40) || (difficulty == 1 && probability > 65) || (difficulty == 2 && probability > 90))
            {
                //hit
                hit();

            }
            else
            {
                stand();
                break;
            }
        }

    }

    //hit
    public static void hit()
    {
        Card card = GameFunctionsScript.pickRandomCard(deck);
        GameFunctionsScript.addToDeck(player, card);
        GameFunctionsScript.showOutcome(card, player, "hit");


    }

    //stand and move turn
    public static void stand()
    {
        GameFunctionsScript.showOutcome(null, player, "stand");

    }
}
