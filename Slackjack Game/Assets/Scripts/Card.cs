using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card 
{
    //add accessors soon
    public int pip;
    public int suit;
    public Sprite sprite;
    public int aceValue;
    public const int ACE_LOW = 1;
    public const int ACE_HIGH = 11;
    

    public Card(int suit, int pip, Sprite sprite)
    {
        this.pip = pip;
        this.suit = suit;
        this.sprite = null;
        aceValue = 0;
    }

    


    
}
