using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprite : MonoBehaviour
{

    // NEEDS WORK

    public SpriteRenderer spriteRenderer; //sprite renderer component reference declaration
    public Sprite newSprite; // sprite/image reference declaration

    // Start is called before the first frame update
    void Start()
    {
        //GameObject gameObject = GameObject.Find("CardSlot4U"); // finds card slot
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //finds sprite renderer from gameobject above
    }

    //changes default white sprite to card value
    void ChangeSprite()
    {
        //spriteRenderer.sprite = newSprite;  //will set sprite to sprite renderer component
        
    }
}
