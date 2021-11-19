using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunction: MonoBehaviour
{
    public static int cardsRemaining; //declares cards remaining variable
    public static List<Card> deck; //delcares deck reference variable
    public static List<string> usedDeck; //declares used deck reference variable
    public static double probability; //declare probability variable
    public static int difficulty; //declaure difficulty variable
    public static bool startAIPlay; //declare start boolean to start AI gameplay variable
    public static Player player; //declare Player reference variable

    // Start is called before the first frame update
    void Start()
    {
        startAIPlay = false; //sets startAI to false
        deck = MainClass.deck; // gets reference to MainClass deck
        cardsRemaining = deck.Count; //gets deck count and sets to cardsRemaining
        probability = 0; //sets probability to 0
        usedDeck = GameFunctionsScript.usedDeck; //get and set reference from GamFunctionsScript
        difficulty = PlayerPrefs.GetInt("difficultyLevel"); //get and set difficultyLevel
    }

    // Update is called once per frame
    void Update()
    {
        usedDeck = GameFunctionsScript.usedDeck; //updates deck variable with current deck reference

        //if startAIPlay is true, start AI game play (with function)
        if (startAIPlay)
        {
            StartCoroutine("AIPlayFunction");
        }
        
    }

    //starts AI game play with delays
    IEnumerator AIPlayFunction()
    {
        startAIPlay = false; //sets start variable to false
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed") + 1.5f); // delays game for specific amount of seconds

        //while player doesn't stand, win, or bust
        while (true)
        {
            probability = probabilityOfCard(player); //get probability of getting needed card and set to probability

            Debug.Log($"probability of drawing valid card is {probability}%");
            Debug.Log($"difficulty is {PlayerPrefs.GetInt("difficultyLevel")}");
            difficulty = PlayerPrefs.GetInt("difficultyLevel"); //get new difficulty level amount

            bool loweredAce = false; // set lowered ace to false

            //if player hand total is greater than 21
            if (player.handTotal > 21)
            {
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    //if card ace value is high, play low
                    if (player.playerHand[i].aceValue == 1)
                    {
                        player.playerHand[i].aceValue = -1; //update ace value to low, if possible
                        player.handTotal -= 10; //subtract 10 to play low
                        loweredAce = true; //set lowered ace to true
                        break; //leave loop
                    }
                }
            }

            //if player hand total is greater than 21
            /*if (player.handTotal > 21)
            {
                for (int i = 0; i < player.playerHand.Count; i++)
                {
                    //
                    if ((player.handTotal > 21) && player.playerHand[i].aceValue == -1)
                    {
                        player.playerHand[i].aceValue = 1; //update ace value to high, if possible
                        player.handTotal += 10;
                        loweredAce = true;
                        break;
                    }
                }
            }*/

            //if player hand total is 21
            if (player.handTotal == 21)
            {
                stand(player); //player stands
                player.status = "win"; //sets players status to win
                Debug.Log("player won");
                break; //leave loop
            }
            //if player hand total is greater than 21
            else if (player.handTotal > 21)
            {
                GameFunctionsScript.showOutcome(null, player, "bust"); //shows outcome of player busting
                player.status = "bust"; //sets player status to bust
                Debug.Log("Player bust");
                break; //leave loop
            }
            //if at a specific difficulty, the probability is greater than specificed value, hit
            else if ((difficulty == 1 && probability > 30) || (difficulty == 2 && probability > 45) || (difficulty == 3 && probability > 55) && player.handTotal < 21) // respectively: easy, medium, hard
            {
                Debug.Log("player hit");
                hit(player); //player hits       
                yield return new WaitForSeconds(PlayerPrefs.GetFloat("gameSpeed")); //delays for specified amount of time
            }
            //else, probaility was to low, stand
            else
            {
                Debug.Log("Probability was too low. Current player stood");
                stand(player); //player stands
                break; //leave loop
            }
        }
        Debug.Log("AIEnd");
        //yield return new WaitForSeconds(1.5f);
    }

    //Calculate probability of finding card
    public static double probabilityOfCard(Player player)
    {
        int playerHandTotal = player.handTotal; //sets player hand total to variable
        
        for(int i = 0; i < player.playerHand.Count; i++)
        {
            //if an ace is played high, treat as if played low
            if(player.playerHand[i].aceValue == 1)
            {
                playerHandTotal -= 10; //subtract 10 to make playertotal be treated as if it has low ace
                break; //leave loop
            }
        }

        int maxValue = 21 - playerHandTotal; //get max value

        cardsRemaining = MainClass.deck.Count; //get deck count from MainClass
        int possibleCards = 0; //set possibleCard value to 0

        Debug.Log($"max value is {maxValue}");
        Debug.Log("AI player " + player.playerName + " hand total is " + player.handTotal);
        Debug.Log(GameFunctionsScript.usedDeck == null);

        //if maxValue is greated than 13
        if (maxValue > 13)
        {   
            //fix to set maxValue max
            possibleCards = cardsRemaining;
        }
        else
        {
            possibleCards = maxValue * 4; //get all possible card count 
        }

        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= maxValue; j++)
            {
                if (GameFunctionsScript.usedDeck.Contains($"{i}{j}"))
                {
                    Debug.Log($"{possibleCards} inside possible cards");
                    possibleCards--; //if card exist in usedDeck array, decrement from variable
                }
            }
        }

        Debug.Log(MainClass.deck == null);
        Debug.Log($"possible cards are {possibleCards} and cards remaining is {cardsRemaining}");
        probability = (possibleCards / (cardsRemaining * 1.0)) * 100.0; //calculate probability
        Debug.Log($"Prob inside is {probability}");
        Debug.Log("Difficulty is " + difficulty);
        return probability;
    } 
    
    //hit
    public static void hit(Player player)
    {
        Card card = GameFunctionsScript.pickRandomCard(MainClass.deck); //picks random card
        GameFunctionsScript.addToDeck(player, card, null); //adds to player deck
        GameFunctionsScript.usedDeck.Add(card.suit + "" + card.pip); //adds to used deck
        GameFunctionsScript.showOutcome(card, player, "hit"); //shows outcome

        Debug.Log($"The card drawn is {card.suit}{card.pip}");
    }

    //stand and move turn
    public static void stand(Player player)
    {
        GameFunctionsScript.showOutcome(null, player, "stand"); //shows outcome of character
        player.status = "stand"; //sets status to stand

    }

    //generates bet amount for AI player
    public static void generateBetAmount(Player player)
    {
        player.betAmount = Random.Range(2, PlayerPrefs.GetInt("playersMoney" + player.playerNumber) + 1); //generates random bet amount between min possible value and player current total money
    }
    
}
