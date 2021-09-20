using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
