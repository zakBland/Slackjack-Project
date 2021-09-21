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
        helpGroupBlockObject.SetActive(false);

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

        Card card = GameFunctionsScript.pickRandomCard(MainClass.deck);

        if(card.pip != 1)
        {
            //players[1].handTotal += card.pip;
        }
        else
        {
            //display option to play as 1 or 11
            //int chosenValue = determineAceValue();

            //if(aceValue)
        }
        GameObject hitButtonObject = GameObject.Find("HitButtonBlock");
        Button buttonObject = hitButtonObject.GetComponent<Button>();

        buttonObject.enabled = true;

       
    }

    //hitResultAction
    public void hitResultAction()
    {
        
    }


    //HelpButtonAction
    public void helpButtonAction()
    {
        Debug.Log("inside");
        // currentPage = 1;
        PlayerPrefs.SetInt("currentPage", 1);
        helpGroupBlockObject.SetActive(true);

        Debug.Log(buttonsBlockObject == null);
        /*for (int i = 0; i < buttonsBlockObject.Length; i++)
        {
            buttonsBlockObject[i].GetComponent<Button>().enabled = false;
        }
        */
      
        leftArrowGameObject.SetActive(true);
        rightArrowGameObject.SetActive(true);
        leftArrowGameObject.GetComponent<Button>().enabled = false;

        Debug.Log("How to Pages Block is null:");
        Debug.Log(howToPagesBlockObject == null);

        Debug.Log("How to Pages Block is null:");
        Debug.Log(rulesPagesBlockObject == null);

        rulesPagesBlockObject.SetActive(true);
        //(GameObject.Find("HowToPagesBlock")).SetActive(true);
        //(GameObject.Find("RulesPagesBlock")).SetActive(true);


        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        rulesPagesGameObjects[0].gameObject.SetActive(true);
    }

    public void doneButtonAction()
    {

        helpGroupBlockObject.SetActive(false);
        for(int i = 0; i < buttonsBlockObject.Length; i++)
        {
            buttonsBlockObject[i].GetComponent<Button>().enabled = true;
        }

    }
    //LeaveButtonAction
    public void leaveButtonAction()
    {

    }

    //StandButtonAction
    public void standButtonAction()
    {

    }
    
    //LeftArrowAction
    public void leftArrowAction()
    {

    }

    //RightArrowAction
    public void rightArrowAction()
    {

    }

    public void lowAceButtonAction()
    {
        MainClass.players[MainClass.currentPlayerNumber].handTotal += 1;

        for (int i = 0; i < MainClass.players[MainClass.currentPlayerNumber].playerHand.Count; i++)
        {
            if (MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue == 0)
            {
                MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue = -1;
            }

        }
        GameObject lowObject = GameObject.Find("LowAceButton");
        lowObject.SetActive(false);
    }

    public void highAceButtonAction()
    {
        MainClass.players[MainClass.currentPlayerNumber].handTotal += 11;

        for (int i = 0; i < MainClass.players[MainClass.currentPlayerNumber].playerHand.Count; i++)
        {
            if (MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue == 0) 
            {
                MainClass.players[MainClass.currentPlayerNumber].playerHand[i].aceValue = 1;
            }

        }
        GameObject lowObject = GameObject.Find("HighAceButton");
        lowObject.SetActive(false);
    }



}
