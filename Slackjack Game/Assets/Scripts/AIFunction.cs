using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunction: MonoBehaviour
{
    public static int cardsRemaining;
    public static List<Card> deck;
    public static List<string> usedDeck;
    public static double probability;
    //public static Player player;
    public static int difficulty;
    public static bool startAIPlay;
    public static Player player;

    // Start is called before the first frame update
    void Start()
    {
        startAIPlay = false;

        Debug.Log(MainClass.deck == null);

        deck = MainClass.deck;
        Debug.Log(deck == null);
        cardsRemaining = deck.Count;
        probability = 0;
        usedDeck = GameFunctionsScript.usedDeck;
        difficulty = PlayerPrefs.GetInt("difficultyLevel");
    }

    // Update is called once per frame
    void Update()
    {
        usedDeck = GameFunctionsScript.usedDeck;
        Debug.Log("StartAIPlay is " + startAIPlay);
        if (startAIPlay)
        {
            StartCoroutine("AIPlayFunction");
            Debug.Log("Inside coroutine");
        }
        
    }

    IEnumerator AIPlayFunction()
    {
        Debug.Log("inside AIFunction");
        startAIPlay = false;
        yield return new WaitForSeconds(3f);


        while (true)
        {
            probability = probabilityOfCard(player);

            Debug.Log($"probability of drawing valid card is {probability}%");
            Debug.Log($"difficulty is {PlayerPrefs.GetInt("difficultyLevel")}");
            difficulty = PlayerPrefs.GetInt("difficultyLevel");

            bool loweredAce = false;

            if (player.handTotal > 21)
            {
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    //if so, make sure to play as high, if it will total less than 22 and greater than 16
                    if ((player.handTotal > 21) && player.playerHand[i].aceValue == -1)
                    {
                        player.playerHand[i].aceValue = 1; //update ace value to high, if possible
                        player.handTotal += 10;
                        loweredAce = true;
                        break;
                    }
                }
            }

            if (player.handTotal == 21)
            {
                stand(player);
                Debug.Log("player won");
                break;
            }
            else if (player.handTotal > 21)
            {
                GameFunctionsScript.showOutcome(null, player, "bust");
                player.status = "bust";
                Debug.Log("Player bust");
                break;

            }
            else if ((difficulty == 1 && probability > 30) || (difficulty == 2 && probability > 45) || (difficulty == 3 && probability > 55) && player.handTotal < 21) // e: 40 m: 65 h: 90
            {
                Debug.Log("player hit");
                //hit
                hit(player);            
                yield return new WaitForSeconds(1.5f);


            }
            else
            {
                Debug.Log("Probability was too low. Current player stood");
                stand(player);
                break;
            }


        }
        Debug.Log("AIEnd");
        //yield return new WaitForSeconds(1.5f);

    }

    //Determine how many cards remain    
    //Determine hand total

    //Calculate probability of finding card

    public static double probabilityOfCard(Player player)
    {

        int playerHandTotal = player.handTotal;
        
        for(int i = 0; i < player.playerHand.Count; i++)
        {
            if(player.playerHand[i].aceValue == 1)
            {
                Debug.Log("inside where AI prob ace is high");

                playerHandTotal -= 10;
                break;
            }
        }

        int maxValue = 21 - playerHandTotal;

        cardsRemaining = MainClass.deck.Count;
        int possibleCards = 0;

        Debug.Log($"max value is {maxValue}");
        Debug.Log("AI player " + player.playerName + " hand total is " + player.handTotal);
        Debug.Log(GameFunctionsScript.usedDeck == null);
        if (maxValue > 13)
        {   
            //fix to set maxValue max
            possibleCards = cardsRemaining;
        }
        else
        {
            possibleCards = maxValue * 4;

        }

        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= maxValue; j++)
            {
                if (GameFunctionsScript.usedDeck.Contains($"{i}{j}"))
                {
                    Debug.Log($"{possibleCards} inside possible cards");
                    possibleCards--;
                }
            }
        }

        //calculate probability
        Debug.Log(MainClass.deck == null);
        Debug.Log($"possible cards are {possibleCards} and cards remaining is {cardsRemaining}");
        probability = (possibleCards / (cardsRemaining * 1.0)) * 100.0;
        Debug.Log($"Prob inside is {probability}");
        Debug.Log("Difficulty is " + difficulty);
        return probability;
    } 
    
    //hit
    public static void hit(Player player)
    {
        Card card = GameFunctionsScript.pickRandomCard(MainClass.deck); 
        GameFunctionsScript.addToDeck(player, card, null);
        GameFunctionsScript.usedDeck.Add(card.suit + "" + card.pip);
        GameFunctionsScript.showOutcome(card, player, "hit");

        Debug.Log($"The card drawn is {card.suit}{card.pip}");
    }

    //stand and move turn
    public static void stand(Player player)
    {
        GameFunctionsScript.showOutcome(null, player, "stand");
        player.status = "stand";

    }

    public static void generateBetAmount(Player player)
    {
        int bet = Random.Range(2, player.playerTotalMoney + 1);
        player.betAmount = bet;

    }
    
}
