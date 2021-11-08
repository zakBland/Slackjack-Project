using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ControlsScript : MonoBehaviour
{
    GameObject helpGroupBlockObject; //declares HelpGroupBlock object
    GameObject[] buttonsBlockObject; //declares buttonBlock object
    GameObject leftArrowGameObject; //declares left arrow object
    GameObject rightArrowGameObject; //declares right arrow object
    GameObject leftArrowRoundsObject; //declares left arrow object
    GameObject rightArrowRoundsObject; //declares right arrow object
    GameObject[] howToPagesGameObjects; //declares howToPages block object
    TextMeshProUGUI[] rulesPagesGameObjects; //declares rules pages object
    GameObject rulesPagesBlockObject; //declares rules pages block object
    GameObject howToPagesBlockObject; //declares howToPages block object
    GameObject roundTextObject; //declares roundText object
    GameObject roundsGroupBlockObject; //declares roundsGroupBlock object
    GameObject bettingGroupBlockObject; //declares bettingGroupBlock object
    GameObject leftArrowBettingObject; //declares left arrow betting object
    GameObject rightArrowBettingObject; // declares right arrow betting object
    GameObject bettingTextObject; //declares betting text object
    GameObject resultsGroupBlock; //declares resultsGroup block object
    static GameObject playAgainClassObject; //declares playAgainClass block object

    public const int MIN_PAGE = 1; //declares constant for page minimum
    public static int currentRounds; //declares current rounds variable
    public static int currentBet; //declares current bet amount variable

    //public static int cardPage; //declares 
    //public static int index;

    // Start is called before the first frame update
    void Start()
    {
        helpGroupBlockObject.SetActive(false); //sets help screen to inactive to hide it
        bettingGroupBlockObject.SetActive(false); //sets betting screen to inactive to hide it
        resultsGroupBlock.SetActive(false); //sets results screen to inactive to hide it
        currentRounds = 1; //sets current rounds amount to 1

        //index = 0;
        //cardPage = 0;

        //check to see if player set rounds amount
        if(PlayerPrefs.GetInt("rounds") > 1)
        {
            roundsGroupBlockObject.SetActive(false);  //if already set, hide round screen          
            PlayerPrefs.SetInt("rounds", PlayerPrefs.GetInt("rounds") - 1); //decrement from rounds amount to show completed round

        }

        //check to see if betting is enabled
        if (PlayerPrefs.GetInt("bettingEnabled") == 2) //if betting is enabled
        {
            currentBet = 2; //set default bet amount
            bettingGroupBlockObject.SetActive(true); //show betting screen

        }
        else
        {
            bettingGroupBlockObject.SetActive(false); //hide betting screen
        }


    }

    void Awake()
    {
        helpGroupBlockObject = GameObject.Find("HelpGroupBlock"); //finds HelpButton
        buttonsBlockObject = GameObject.FindGameObjectsWithTag("Buttons"); //Finds all buttons on game scene
        howToPagesGameObjects = GameObject.FindGameObjectsWithTag("Pages"); //finds all pages to control block
        rulesPagesGameObjects = (GameObject.Find("RulesPagesBlock")).GetComponentsInChildren<TextMeshProUGUI>(); // finds all rules pages
        rulesPagesBlockObject = GameObject.Find("RulesPagesBlock"); //finds rules block
        howToPagesBlockObject = GameObject.Find("HowToPagesBlock"); //finds howToPages block
        //leftArrowGameObject = GameObject.Find("LeftArrowButton"); //finds left arrow button
        //rightArrowGameObject = GameObject.Find("RightArrowButton"); //finds right arrow button
        roundTextObject = GameObject.Find("RoundsNumberText"); //finds rounds number text 
        leftArrowRoundsObject = GameObject.Find("LeftRoundsButton"); //finds left arrow button
        rightArrowRoundsObject = GameObject.Find("RightRoundsButton"); //finds right arrow button
        roundsGroupBlockObject = GameObject.Find("RoundsGroupBlock"); //finds rounds block/screen in scene
        bettingGroupBlockObject = GameObject.Find("BettingGroupBlock"); //finds betting block/screen in scene
        leftArrowBettingObject = GameObject.Find("LeftBetButton"); //finds left bet arrow button
        rightArrowBettingObject = GameObject.Find("RightBetButton"); //finds right bet arrow button
        bettingTextObject = GameObject.Find("BetNumberText"); //finds bet number text
        resultsGroupBlock = GameObject.Find("ResultsGroupBlock"); //finds results block/screen in scene


    }

    //HitButtonAction
    public void hitButtonAction()
    {
        List<Card> deck = MainClass.deck; //assigns reference to deck
        Card card = GameFunctionsScript.pickRandomCard(deck); //picks random card from MainClass deck
        Player player = MainClass.players[MainClass.currentPlayerNumber]; //finds current player

        //calculate total
        GameFunctionsScript.calculateTotal(card, player);

        //display card to player area
        GameFunctionsScript.displayCard(card, player, null);

        //display outcome
        if (player.handTotal > 21)
        {
            GameFunctionsScript.showOutcome(card, player, "bust"); //shows outcome
            player.status = "bust"; //sets status to bust
        }
        else
        {
            GameFunctionsScript.showOutcome(card, player, "hit"); //shows outcome
        }
      
       
    }

    //HelpButtonAction
    public void helpButtonAction()
    {
        PlayerPrefs.SetInt("currentPage", 1); //sets current page to 1;
        helpGroupBlockObject.SetActive(true); //sets help info block to active  
        leftArrowGameObject.SetActive(true); //sets left arrow active
        rightArrowGameObject.SetActive(true); //set right arrow active
        leftArrowGameObject.GetComponent<Button>().enabled = false; //disables left arrow
        rulesPagesBlockObject.SetActive(true); //sets rules page block active

        //disable background buttons
        for (int i = 0; i < buttonsBlockObject.Length; i++)
        {
            //if button in button list isn't null
            if(buttonsBlockObject[i].GetComponent<Button>() != null)
            {
                buttonsBlockObject[i].GetComponent<Button>().enabled = false; //disable button
            }
        }

        //sets how to pages inactive
        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        //sets rules pages inactive
        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        rulesPagesGameObjects[0].gameObject.SetActive(true); //sets rules first page active
    }

    //done button
    public void doneButtonAction()
    {

        helpGroupBlockObject.SetActive(false); //sets help button block to inactive

        //sets normal game buttons active
        for(int i = 0; i < buttonsBlockObject.Length; i++)
        {
            if (buttonsBlockObject[i].GetComponent<Button>() != null)
            {
                buttonsBlockObject[i].GetComponent<Button>().enabled = true;
            }
        }

    }

    //LeaveButtonAction
    public void leaveButtonAction()
    {
        SceneManager.LoadScene("TitleScreenScene"); //loads title screen
        PlayerPrefs.SetInt("rounds", 1); //resets rounds variable to 1 after leaving game

        //reset player money amounts to 500
        for (int i = 1; i < MainClass.players.Length; i++)
        {
            PlayerPrefs.SetInt("playersMoney" + i, 500);
        }

    }

    //StandButtonAction
    public void standButtonAction()
    {
        Player player = MainClass.players[MainClass.currentPlayerNumber]; //finds current player
        GameFunctionsScript.showOutcome(null, player, "stand"); // shows outcome   
        player.status = "stand"; //sets player status to stand

        //disables "you" player buttons
        GameObject standButton = GameObject.Find("StandButton"); //finds stand button
        GameObject hitButton = GameObject.Find("HitButton"); //finds hit button
        standButton.SetActive(false); //hides stand button
        hitButton.SetActive(false); // hides hit button
    }

    /*//LeftArrowAction (NOT IMPLEMENTED
    public void leftArrowAction()
    {
        if (true)//cardPage > 0 )//&& MainClass.players[1].playerHand.Count > 6)
        {
            GameObject[] playArea = GameObject.FindGameObjectsWithTag(MainClass.players[1].playerNameBlockString);

            for (int i = 0; i < 6; i++)
            {
                if (i < 5)
                {
                    playArea[i].GetComponent<Image>().sprite = playArea[i + 1].GetComponent<Image>().sprite;
                }
                else
                {
                    playArea[i].GetComponent<Image>().sprite = MainClass.players[1].playerHand[6 + index].sprite;
                    index--;
                }
            }
            cardPage--;
        }
    }
    */

    /*
    //RightArrowAction (NOT IMPLEMENTED)
    public void rightArrowAction()
    {
        if (true) //cardPage > 0 )&& MainClass.players[1].playerHand.Count > 6)
        {
            GameObject[] playArea = GameObject.FindGameObjectsWithTag(MainClass.players[1].playerNameBlockString);
            Debug.Log(playArea.Length + " is area size");
            for (int i = 4; i >= 0; i--)
            {
                /*if (i == 0)
                {
                    playArea[i].GetComponent<Image>().sprite = playArea[i + 1].GetComponent<Image>().sprite;
                }
                else
                {
                Debug.Log("Inside rightArrow. i is " + i);
                //Debug.Log(playArea[i].GetComponent<Image>().sprite == null);
                if (i - 1 < MainClass.players[1].playerHand.Count)
                {
                    playArea[i].GetComponent<Image>().sprite = MainClass.players[1].playerHand[i - 1].sprite;

                    index--;
                }
                //}
            }


            cardPage++;
        }
    }
*/

    //restarts game to replay
    public static void playAgainAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //loads game scene to reset it
        playAgainClassObject = GameObject.Find("PlayAgainGroupBlock"); //finds play again screen object
        playAgainClassObject.SetActive(false); //hides object

    }

    //leaves game and returns to title scene
    public static void leaveGameButtonAction()
    {
        SceneManager.LoadScene("TitleScreenScene"); //loads title screen
        PlayerPrefs.SetInt("rounds", 1); //resets rounds variable to 1

        //resets player money amounts
        for (int i = 1; i < MainClass.players.Length; i++)
        {
            PlayerPrefs.SetInt("playersMoney" + i, 500);
        }

    }

    //left arrow button for rounds screen
    public void leftArrowRoundsButtonAction()
    {
        int maxRounds = 10; //set max rounds to 10

        TextMeshProUGUI roundsArrowText = roundTextObject.GetComponent<TextMeshProUGUI>(); //gets text object for rounds
        Image rightArrowImageObject = rightArrowRoundsObject.GetComponent<Image>(); //get image for right arrow
        Button rightArrowButtonObject = rightArrowRoundsObject.GetComponent<Button>(); //gets button for right arrow
        Image leftArrowImageObject = leftArrowRoundsObject.GetComponent<Image>(); //gets image for left arrow
        Button leftArrowButtonObject = leftArrowRoundsObject.GetComponent<Button>(); //gets image for left arrow

        //determine which page is active
        if (currentRounds - 1 >= 1) // if current round amount - 1 is still valid number
        {
            if (currentRounds == maxRounds) //if current rounds is equal to max rounds
            {
                rightArrowImageObject.color = new Color32(255, 255, 255, 255); //change arrow to normal color (white)
                rightArrowButtonObject.GetComponent<Button>().enabled = true; //enable right arrow

            }
            
            currentRounds--; //decrement current rounds    

            if (currentRounds == 1) //if current rounds is at min possible value
            {
                leftArrowImageObject.color = new Color32(102, 94, 94, 255); //change arrow to disabled color (gray/black)
                leftArrowButtonObject.GetComponent<Button>().enabled = false; //disable left arrow

            }

            roundsArrowText.text = currentRounds + ""; //update text amount in scene
        }
    }

    //right arrow button for rounds screen
    public void rightArrowRoundsButtonAction()
    {
        int maxRounds = 10; //max rounds is 10

        TextMeshProUGUI roundsArrowText = roundTextObject.GetComponent<TextMeshProUGUI>(); //finds rounds text
        Image rightArrowImageObject = rightArrowRoundsObject.GetComponent<Image>(); //get image for right arrow
        Button rightArrowButtonObject = rightArrowRoundsObject.GetComponent<Button>(); //gets button for right arrow
        Image leftArrowImageObject = leftArrowRoundsObject.GetComponent<Image>(); //gets image for left arrow
        Button leftArrowButtonObject = leftArrowRoundsObject.GetComponent<Button>(); //gets image for left arrow

        if (currentRounds + 1 <= maxRounds) //if current rounds + 1 is less than or equal to max rounds; check to see if it is possible to increment current rounds
        {
            if (currentRounds == 1) // if current rounds equals 1
            {
                leftArrowImageObject.color = new Color32(255, 255, 255, 255); // sets left arrow to normal color (white)
                leftArrowButtonObject.GetComponent<Button>().enabled = true; //enables left arrow
            }

            currentRounds++; //increment current rounds

            if (currentRounds == maxRounds) //if current rounds equals max rounds
            {
                rightArrowImageObject.color = new Color32(102, 94, 94, 255); //sets right arrow to disabled color (black/gray)
                rightArrowButtonObject.GetComponent<Button>().enabled = false; //disables right arrow

            }

            roundsArrowText.text = currentRounds + ""; //update rounds text in scene
        }
    }

    //done rounds button
    public void doneRoundsButtonAction()
    {
        PlayerPrefs.SetInt("rounds", currentRounds); //sets rounds to currentRounds
        PlayerPrefs.SetInt("setRounds", 1); //sets setRounds to 1 (MAYBE SET TO 2)
        roundsGroupBlockObject.SetActive(false); //hides rounds block object
        //bettingGroupBlockObject.SetActive(true);
    }

    //left arrow betting button
    public void leftArrowBettingButtonAction()
    {
        int maxBet = 500;

        TextMeshProUGUI bettingArrowText = roundTextObject.GetComponent<TextMeshProUGUI>();  //finds bet text
        Image rightArrowImageObject = rightArrowRoundsObject.GetComponent<Image>(); //get image for right arrow
        Button rightArrowButtonObject = rightArrowRoundsObject.GetComponent<Button>(); //gets button for right arrow
        Image leftArrowImageObject = leftArrowRoundsObject.GetComponent<Image>(); //gets image for left arrow
        Button leftArrowButtonObject = leftArrowRoundsObject.GetComponent<Button>(); //gets image for left arrow


        //determine which page is active
        if (currentBet - 1 >= 2) // if current bet amount - 1 is still valid number
        {
            if (currentBet == maxBet) //if current bet is equal to max bet
            {
                rightArrowImageObject.color = new Color32(255, 255, 255, 255); //change arrow to normal color (white)
                rightArrowButtonObject.GetComponent<Button>().enabled = true; //enable right arrow

            }

            currentBet--; //decrement current bet  

            if (currentBet == 2) //if current bet is at min possible value
            {
                leftArrowImageObject.color = new Color32(102, 94, 94, 255); //change arrow to disabled color (gray/black)
                leftArrowButtonObject.GetComponent<Button>().enabled = false; //disable left arrow
            }

            bettingArrowText.text = currentBet + ""; //update text amount in scene
        }
    }

    //right arrow betting button
    public void rightArrowBettingButtonAction()
    {
        int maxBet = 500; //max bet is 500

        TextMeshProUGUI bettingArrowText = bettingTextObject.GetComponent<TextMeshProUGUI>(); //finds bet text
        Image rightArrowImageObject = rightArrowBettingObject.GetComponent<Image>(); //get image for right arrow
        Button rightArrowButtonObject = rightArrowBettingObject.GetComponent<Button>(); //gets button for right arrow
        Image leftArrowImageObject = leftArrowBettingObject.GetComponent<Image>(); //gets image for left arrow
        Button leftArrowButtonObject = leftArrowBettingObject.GetComponent<Button>(); //gets image for left arrow



        if (currentBet + 1 <= maxBet) //if current bet + 1 is less than or equal to max bet; check to see if it is possible to increment current bet
        {
            if (currentBet == 2) // if current bet equals 2
            {
                leftArrowImageObject.color = new Color32(255, 255, 255, 255); // sets left arrow to normal color (white)
                leftArrowButtonObject.GetComponent<Button>().enabled = true; //enables left arrow
            }

            currentBet++; //increment current bet

            if (currentBet == maxBet) //if current bet equals max bet
            {
                rightArrowImageObject.color = new Color32(102, 94, 94, 255); //sets right arrow to disabled color (black/gray)
                rightArrowButtonObject.GetComponent<Button>().enabled = false; //disables right arrow
            }

            bettingArrowText.text = currentBet + ""; //update bet text in scene
        }
    } 
    
    //done betting button
    public void doneBettingButtonAction()
    {
        MainClass.players[1].betAmount = currentBet; //sets bet about to player bet field
        GameObject playerBetText = GameObject.Find("PlayerAmountBetText"); // finds player bet text
        playerBetText.GetComponent<TextMeshProUGUI>().text = currentBet + ""; //updates/sets player bet amount to scene
        bettingGroupBlockObject.SetActive(false); //hide bet screen
    }
}
