using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverImageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //FINISH LATER
    GameObject card; //declaration of card to be shown
    string slotName; //declaration of slot name area for a player where the card will be shown
    public GameObject cardName;

    void Start()
    {
       // slotName = MainClass.players[1].playerNameBlockString; //finds block area for player 1's cards
        card = GameObject.Find(cardName.name); //finds cards
        card.GetComponent<Image>().enabled = false; //set zoomed card to false/hidden

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
            card.GetComponent<Image>().enabled = true; //shows card if mouse is hovering over card
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        card.GetComponent<Image>().enabled = false; //hides card if mouse isn't hovering over card
    }
}
