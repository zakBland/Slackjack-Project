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
    GameObject roundTextObject;
    GameObject roundsGroupBlockObject;

    public const int MIN_PAGE = 1; //declares constant for page minimum
    static GameObject playAgainClassObject;
    public static int currentRounds;

    // Start is called before the first frame update
    void Start()
    {
        helpGroupBlockObject.SetActive(false); //sets help screen to inactive        
        currentRounds = 1;
        Debug.Log($"rounds at start are {PlayerPrefs.GetInt("rounds")}");
        if(PlayerPrefs.GetInt("rounds") > 1)
        {
            roundsGroupBlockObject.SetActive(false);            
            PlayerPrefs.SetInt("rounds", PlayerPrefs.GetInt("rounds") - 1);

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
        leftArrowGameObject = GameObject.Find("LeftArrowButton"); //finds left arrow button
        rightArrowGameObject = GameObject.Find("RightArrowButton"); //finds right arrow button
        roundTextObject = GameObject.Find("RoundsNumberText");
        leftArrowRoundsObject = GameObject.Find("LeftRoundsButton"); //finds left arrow button
        rightArrowRoundsObject = GameObject.Find("RightRoundsButton"); //finds right arrow button
        roundsGroupBlockObject = GameObject.Find("RoundsGroupBlock");


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
        GameFunctionsScript.displayCard(card, player);

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


        //maybe disable background buttons (NOT IMPLEMENTED)
        for (int i = 0; i < buttonsBlockObject.Length; i++)
        {
            buttonsBlockObject[i].GetComponent<Button>().enabled = false;
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
            buttonsBlockObject[i].GetComponent<Button>().enabled = true;
        }

    }
    //LeaveButtonAction
    public void leaveButtonAction()
    {
        SceneManager.LoadScene("TitleScreenScene"); //loads title screen

    }

    //StandButtonAction (NOT FULLY IMPLEMENTED)
    public void standButtonAction()
    {

        Player player = MainClass.players[MainClass.currentPlayerNumber]; //finds current player

        GameFunctionsScript.showOutcome(null, player, "stand"); // shows outcome
        
        player.status = "stand"; //sets player status to stand

        //disables "you" player buttons
        GameObject standButton = GameObject.Find("StandButton"); 
        GameObject hitButton = GameObject.Find("HitButton");
        standButton.SetActive(false);
        hitButton.SetActive(false);
    }

    //LeftArrowAction (NOT IMPLEMENTED)
    public void leftArrowAction()
    {

    }

    //RightArrowAction (NOT IMPLEMENTED)
    public void rightArrowAction()
    {

    }

    public static void playAgainAction()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playAgainClassObject = GameObject.Find("PlayAgainGroupBlock");
        playAgainClassObject.SetActive(false);

    }
    public static void leaveGameButtonAction()
    {
        SceneManager.LoadScene("TitleScreenScene"); //loads title screen

    }

    public void leftArrowRoundsButtonAction()
    {
        int maxRounds = 10;

        TextMeshProUGUI roundsArrowText = roundTextObject.GetComponent<TextMeshProUGUI>();
        Image rightArrowImageObject = rightArrowRoundsObject.GetComponent<Image>();
        Button rightArrowButtonObject = rightArrowRoundsObject.GetComponent<Button>();
        Image leftArrowImageObject = leftArrowRoundsObject.GetComponent<Image>();
        Button leftArrowButtonObject = leftArrowRoundsObject.GetComponent<Button>();


        //determine which page is active
        if (currentRounds - 1 >= 1)
        {
            if (currentRounds == maxRounds)
            {
                rightArrowImageObject.color = new Color32(255, 255, 255, 255);
                rightArrowButtonObject.GetComponent<Button>().enabled = true;

            }
            
            currentRounds--;          

            if (currentRounds == 1)
            {
                leftArrowImageObject.color = new Color32(102, 94, 94, 255);
                leftArrowButtonObject.GetComponent<Button>().enabled = false;

            }

            roundsArrowText.text = currentRounds + "";
        }
    }


    public void rightArrowRoundsButtonAction()
    {
        int maxRounds = 10;

        TextMeshProUGUI roundsArrowText = roundTextObject.GetComponent<TextMeshProUGUI>();
        Image rightArrowImageObject = rightArrowRoundsObject.GetComponent<Image>();
        Button rightArrowButtonObject = rightArrowRoundsObject.GetComponent<Button>();
        Image leftArrowImageObject = leftArrowRoundsObject.GetComponent<Image>();
        Button leftArrowButtonObject = leftArrowRoundsObject.GetComponent<Button>();



        if (currentRounds + 1 <= maxRounds)
        {
            if (currentRounds == 1)
            {
                leftArrowImageObject.color = new Color32(255, 255, 255, 255);
                leftArrowButtonObject.GetComponent<Button>().enabled = true;
            }

            currentRounds++;

            if (currentRounds == maxRounds)
            {
                rightArrowImageObject.color = new Color32(102, 94, 94, 255);
                rightArrowButtonObject.GetComponent<Button>().enabled = false;

            }

            roundsArrowText.text = currentRounds + "";


        }

        
    }

    public void doneRoundsButtonAction()
    {
        PlayerPrefs.SetInt("rounds", currentRounds);
        PlayerPrefs.SetInt("setRounds", 1);
        Debug.Log($"player rounds are {currentRounds}");
        roundsGroupBlockObject.SetActive(false);
    }
}
