using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card 
{

    public int pip; //pip value declaration
    public int suit; //suit value declaration
    public Sprite sprite; //card image/sprite reference
    public int aceValue; //if card is an ace, is it played high or low
    public const int ACE_LOW = 1; //constant value of low ace
    public const int ACE_HIGH = 11; //constant value of high ace
    

    public Card(int suit, int pip, Sprite sprite)
    {
        this.pip = pip; //initializes pip
        this.suit = suit; //initializes suit
        this.sprite = sprite; //initializes sprite
        aceValue = 0; //sets ace value to 0
    }

    


    
}
