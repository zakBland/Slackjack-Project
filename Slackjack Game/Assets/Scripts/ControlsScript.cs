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

        Card card = GameFunctionsScript.pickRandomCard();

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
    


}
