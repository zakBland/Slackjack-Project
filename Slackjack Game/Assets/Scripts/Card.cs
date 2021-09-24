using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //For the purposes of blackjack all we care about is the value of 
    //a card and not its suit or rank, so we will only be storing values
    //and sprite of card
    public int value = 0;

    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValueOfCard(int newValue)
    {
        value = newValue;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    //Sets card to display back and sets value to zero
    public void ResetCard()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Deck").GetComponent<Deck>().GetCardBack();
        value = 0;
    }
}
