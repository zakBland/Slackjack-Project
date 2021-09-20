using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverImageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject card;
    public bool isOver = false;

    void Start()
    {       
        Debug.Log(card == null);
        card = GameObject.Find("PCardSlot3");
        card.GetComponent<Image>().enabled = false;
        //card.SetActive(false);

    }

    void Awake()
    {
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        isOver = true;

        card.GetComponent<Image>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        isOver = false;
        card.GetComponent<Image>().enabled = false;
    }
}
