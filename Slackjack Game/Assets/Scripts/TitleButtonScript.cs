using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonScript : MonoBehaviour
{
    GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        gameObject = GameObject.Find("SettingsDisplayBlock");

    }

    //method that reacts to clicking the play button on titleScreenScene
    public void playButtonAction()
    {
        Debug.Log("Works");

        //Load game
        SceneManager.LoadScene("GameplayScene");
    }

    public void showSettingsScreen()
    {
        Debug.Log("Inside");

        //GameObject gameObject = GameObject.Find("ButtonsBlock");

        if (gameObject != null)
        {
            gameObject.SetActive(true);
            Debug.Log("InsideWorks");
        }

    }
}
