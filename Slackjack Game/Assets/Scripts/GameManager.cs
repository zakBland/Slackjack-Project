using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Buttons
    public Button dealBtn_prototype;
    public Button standBtn_prototype;
    public Button hitBtn_prototype;
    public Button shuffleBtn_prototype;

    //Access to Player Classes *For now just player and dealer*
    public Player humanPlayer;
    public Player dealer;

    // Start is called before the first frame update
    void Start()
    {
        //Adding listeners
        dealBtn_prototype.onClick.AddListener(() => DealClicked());
        standBtn_prototype.onClick.AddListener(() => StandClicked());
        hitBtn_prototype.onClick.AddListener(() => HitClicked());
        shuffleBtn_prototype.onClick.AddListener(() => ShuffleClicked());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deck Methods
    private void ShuffleClicked()
    {
        throw new NotImplementedException();
    }

    private void HitClicked()
    {
        throw new NotImplementedException();
    }

    private void StandClicked()
    {
        throw new NotImplementedException();
    }

    private void DealClicked()
    {
        GameObject.Find("Deck").GetComponentInParent<Deck>().Shuffle();
        humanPlayer.StartHand();
        dealer.StartHand();
    }
}
