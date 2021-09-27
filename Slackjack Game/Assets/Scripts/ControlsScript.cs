using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlsScript : MonoBehaviour
{
    GameObject helpGroupBlockObject; 
    GameObject[] buttonsBlockObject;
    GameObject leftArrowGameObject;
    GameObject rightArrowGameObject;
    GameObject leftArrowTextGameObject;
    GameObject rightArrowTextGameObject;
    GameObject[] howToPagesGameObjects;
    TextMeshProUGUI[] rulesPagesGameObjects;
    GameObject rulesPagesBlockObject;
    GameObject howToPagesBlockObject;
    public const int MIN_PAGE = 1;

    // Start is called before the first frame update
    void Start()
    {
        helpGroupBlockObject.SetActive(false); //sets help screen to inactive

    }

    void Awake()
    {
        helpGroupBlockObject = GameObject.Find("HelpGroupBlock");
        buttonsBlockObject = GameObject.FindGameObjectsWithTag("Buttons");
        howToPagesGameObjects = GameObject.FindGameObjectsWithTag("Pages");
        rulesPagesGameObjects = (GameObject.Find("RulesPagesBlock")).GetComponentsInChildren<TextMeshProUGUI>();
        rulesPagesBlockObject = GameObject.Find("RulesPagesBlock");
        howToPagesBlockObject = GameObject.Find("HowToPagesBlock");
        leftArrowGameObject = GameObject.Find("LeftArrowButton");
        rightArrowGameObject = GameObject.Find("RightArrowButton");
        leftArrowTextGameObject = GameObject.Find("BackText");
        rightArrowTextGameObject = GameObject.Find("NextText");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //HitButtonAction
    public void hitButtonAction()
    {
        Debug.Log(MainClass.deck == null);
        List<Card> deck = MainClass.deck;
        Debug.Log(deck.Count);
        Card card = GameFunctionsScript.pickRandomCard(MainClass.deck); //picks random card from MainClass deck
        // Debug.Log(card == null);

        Player player = MainClass.players[MainClass.currentPlayerNumber]; //finds current player


        //calculate total
        GameFunctionsScript.calculateTotal(card, player);

        //display card to player area
        GameFunctionsScript.displayCard(card, player);

        //display outcome
        GameFunctionsScript.showOutcome(card, player, "hit");

        //Unnecessary code for now
       /* GameObject hitButtonObject = GameObject.Find("HitButtonBlock"); // 
        Button buttonObject = hitButtonObject.GetComponent<Button>();

        buttonObject.enabled = true; */

       
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
    //LeaveButtonAction (NOT IMPLEMENTED)
    public void leaveButtonAction()
    {

    }

    //StandButtonAction (NOT IMPLEMENTED)
    public void standButtonAction()
    {

    }
    
    //LeftArrowAction (NOT IMPLEMENTED)
    public void leftArrowAction()
    {

    }

    //RightArrowAction (NOT IMPLEMENTED)
    public void rightArrowAction()
    {

    }

    //plays ace as low value
    public void lowAceButtonAction()
    {
        MainClass.players[MainClass.currentPlayerNumber].handTotal += Card.ACE_LOW; //adds low ace value to player hand total

        //sets new ace as low ace value; finds all aces
        for (int i = 0; i < MainClass.players[MainClass.currentPlayerNumber].playerHand.Count; i++)
        {
            //if ace value is not set, set it 
            if (MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue == 0)
            {
                MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue = -1;
            }

        }

        //finds and disables ace choice buttons
        /*GameObject lowObject = GameObject.Find("LowAceButton");
        lowObject.SetActive(false);
        GameObject lowObject = GameObject.Find("HighAceButton");
        lowObject.SetActive(false); */
    }

    //high ace button
    public void highAceButtonAction()
    {
        MainClass.players[MainClass.currentPlayerNumber].handTotal += Card.ACE_HIGH; //adds high ace value to player hand total

        //finds all aces; sets ace value to high ace value
        for (int i = 0; i < MainClass.players[MainClass.currentPlayerNumber].playerHand.Count; i++)
        {
            //if ace value not set, set it
            if (MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue == 0) 
            {
                MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue = 1;
            }

        }

        //finds and disables ace choice buttons
        /*GameObject lowObject = GameObject.Find("HighAceButton");
        lowObject.SetActive(false);
        GameObject lowObject = GameObject.Find("LowAceButton");
        lowObject.SetActive(false); */
    }



}
