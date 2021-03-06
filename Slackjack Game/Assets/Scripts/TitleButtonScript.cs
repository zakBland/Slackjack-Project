using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleButtonScript : MonoBehaviour
{
    //comment out code
    GameObject settingsGameObject;
    GameObject helpGameObject;
    GameObject playGameObject;
    GameObject settingsBlockGameObject;
    GameObject leftArrowGameObject;
    GameObject rightArrowGameObject;
    GameObject leftArrowTextGameObject;
    GameObject rightArrowTextGameObject;
    GameObject optionalDisplayGameObject;
    GameObject helpBlockGameObject;
    GameObject[] howToPagesGameObjects;
    TextMeshProUGUI[] rulesPagesGameObjects;
    GameObject rulesPagesBlockObject;
    GameObject howToPagesBlockObject;
 
    int whichPage; // 0 == rules, 1 == howTo

    // Start is called before the first frame update
    void Start()
    {
        optionalDisplayGameObject.SetActive(false);
        PlayerPrefs.SetInt("currentPage", 1);
        
        settingsBlockGameObject.SetActive(false);
        helpBlockGameObject.SetActive(false);
        
        //MIN_PAGE = 1;
        whichPage = 0;

        //default player settings
        PlayerPrefs.SetInt("soundEnabled", 0); //no == 0, yes == 1
        PlayerPrefs.SetInt("bettingEnabled", 0); //0 == false, 1 == true
        PlayerPrefs.SetInt("playerCount", 1); //min 1, max 3
        PlayerPrefs.SetInt("difficultyLevel", 1); // 0 == beginner, 1 == normal, 2 == expert 
    }

    void Awake()
    {
        howToPagesGameObjects = GameObject.FindGameObjectsWithTag("Pages");
        rulesPagesGameObjects = (GameObject.Find("RulesPagesBlock")).GetComponentsInChildren<TextMeshProUGUI>();
        rulesPagesBlockObject = GameObject.Find("RulesPagesBlock");
        howToPagesBlockObject= GameObject.Find("HowToPagesBlock");
        

        settingsBlockGameObject = GameObject.Find("SettingsGroupBlock");
        helpBlockGameObject = GameObject.Find("HelpGroupBlock");
        settingsGameObject = GameObject.Find("SettingsButton");
        playGameObject = GameObject.Find("PlayButton");
        helpGameObject = GameObject.Find("HelpButton");
        optionalDisplayGameObject = GameObject.Find("OptionalDisplay");
        leftArrowGameObject = GameObject.Find("LeftArrowButton");
        rightArrowGameObject = GameObject.Find("RightArrowButton");
        leftArrowTextGameObject = GameObject.Find("BackText");
        rightArrowTextGameObject = GameObject.Find("NextText");


    }

    //method that reacts to clicking the play button on titleScreenScene
    public void playButtonAction()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void settingsButtonAction()
    {
        if (optionalDisplayGameObject != null)
        {
            PlayerPrefs.SetInt("currentPage", 1);

            optionalDisplayGameObject.SetActive(true);
            settingsGameObject.GetComponent<Button>().enabled = false;
            helpGameObject.GetComponent<Button>().enabled = false;
            playGameObject.GetComponent<Button>().enabled = false;
            settingsBlockGameObject.SetActive(true);
            helpBlockGameObject.SetActive(false);


        }
    }

    //help button
    public void helpButtonAction()
    {

        if (optionalDisplayGameObject != null)
        {
            PlayerPrefs.SetInt("currentPage", 1);

            optionalDisplayGameObject.SetActive(true);
            settingsGameObject.GetComponent<Button>().enabled = false;
            helpGameObject.GetComponent<Button>().enabled = false;
            playGameObject.GetComponent<Button>().enabled = false;
            helpBlockGameObject.SetActive(true);
            settingsBlockGameObject.SetActive(false);
            leftArrowGameObject.SetActive(true);
            rightArrowGameObject.SetActive(true);

            (GameObject.Find("HowToPagesBlock")).SetActive(true);
            (GameObject.Find("RulesPagesBlock")).SetActive(true);


            foreach (GameObject obj in howToPagesGameObjects) 
            {
                obj.SetActive(false);
            }

            foreach(TextMeshProUGUI obj in rulesPagesGameObjects)
            {
                obj.gameObject.SetActive(false);
            }

            rulesPagesGameObjects[0].gameObject.SetActive(true);

        }

    }

     public void rulesButtonAction()
     {
        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        rulesPagesBlockObject.SetActive(true);
        howToPagesBlockObject.SetActive(false);
        whichPage = 0;
        PlayerPrefs.SetInt("currentPage", 1);

        GameObject ruleButtonObject = GameObject.Find("RulesText");
        GameObject howToButtonObject = GameObject.Find("HowToPlayText");

        TextMeshProUGUI rulesText = ruleButtonObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI howToText = howToButtonObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>();
        
        rulesText.color = new Color32(255, 255, 255, 255);
        howToText.color = new Color32(102, 94, 94, 255);

        rulesPagesGameObjects[0].gameObject.SetActive(true);
       
        leftArrowGameObject.gameObject.GetComponent<Button>().enabled = false;
        leftArrowText.color = new Color32(102, 94, 94, 255);
        rightArrowGameObject.gameObject.GetComponent<Button>().enabled = true;
        rightArrowText.color = new Color32(255, 255, 255, 255); 
    }

    public void howToPlayButtonAction()
    {
        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }
        rulesPagesBlockObject.SetActive(false);
        howToPagesBlockObject.SetActive(true);

        whichPage = 1;
        PlayerPrefs.SetInt("currentPage", 1);

        GameObject ruleButtonObject = GameObject.Find("RulesText");
        GameObject howToButtonObject = GameObject.Find("HowToPlayText");

        TextMeshProUGUI rulesText = ruleButtonObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI howToText = howToButtonObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>();

        leftArrowGameObject.gameObject.GetComponent<Button>().enabled = false;
        leftArrowText.color = new Color32(102, 94, 94, 255); ;
        rightArrowGameObject.gameObject.GetComponent<Button>().enabled = true;
        rightArrowText.color = new Color32(255, 255, 255, 255);

        rulesText.color = new Color32(102, 94, 94, 255);
        howToText.color = new Color32(255, 255, 255, 255);

        howToPagesGameObjects[0].gameObject.SetActive(true);
    } 

    public void resetButtonAction()
    {
        bettingNoEnabled();
        volumeNoLevel();
        playerOneButton();
        difficultyMedButton();

        PlayerPrefs.SetInt("soundEnabled", 0); //min 0, max 5
        PlayerPrefs.SetInt("bettingEnabled", 0); //0 == false, 1 == true
        PlayerPrefs.SetInt("playerCount", 1); //min 1, max 3
        PlayerPrefs.SetInt("difficultyLevel", 1); // 0 == beginner, 1 == normal, 2 == expert

                
    }

    public void saveButtonAction()
    {

        optionalDisplayGameObject.SetActive(false);
        settingsGameObject.GetComponent<Button>().enabled = true;
        helpGameObject.GetComponent<Button>().enabled = true;
        playGameObject.GetComponent<Button>().enabled = true;
        settingsBlockGameObject.SetActive(false);

    }

    public void doneButtonAction()
    {

        optionalDisplayGameObject.SetActive(false);
        settingsGameObject.GetComponent<Button>().enabled = true;
        helpGameObject.GetComponent<Button>().enabled = true;
        playGameObject.GetComponent<Button>().enabled = true;
        helpBlockGameObject.SetActive(false);
    }


    public void leftArrowButtonAction()
    {
        int maxPage;
        if (whichPage == 0)
        {
            maxPage = rulesPagesGameObjects.Length;
        }
        else
        {
            maxPage = howToPagesGameObjects.Length;
        }
            TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>();

            //determine which page is active


            if (PlayerPrefs.GetInt("currentPage") - 1 >= 1)
            {
                if (PlayerPrefs.GetInt("currentPage") == maxPage)
                {
                    rightArrowText.color = new Color32(255, 255, 255, 255);
                    rightArrowGameObject.GetComponent<Button>().enabled = true;

                }
                PlayerPrefs.SetInt("currentPage", PlayerPrefs.GetInt("currentPage") - 1);

                if (PlayerPrefs.GetInt("currentPage") == 1)
                {
                    leftArrowText.color = new Color32(102, 94, 94, 255);
                    leftArrowGameObject.GetComponent<Button>().enabled = false;

                }

                if (whichPage == 0)
                {
                    (rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage")]).gameObject.SetActive(false);

                    rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true);
                }
                else
                {
                    howToPagesGameObjects[PlayerPrefs.GetInt("currentPage")].gameObject.SetActive(false);
                    howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true);
                }
            }


    }



    

    public void rightArrowButtonAction()
    {
        int maxPage;
        if (whichPage == 0)
        {
            maxPage = rulesPagesGameObjects.Length;
        }
        else
        {
            maxPage = howToPagesGameObjects.Length;

        }

        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>();

        //determine which page is active


        if (PlayerPrefs.GetInt("currentPage", 1) + 1 <= maxPage)
        {
            if (PlayerPrefs.GetInt("currentPage", 1) == 1)
            {
                leftArrowText.color = new Color32(255, 255, 255, 255);
                Debug.Log(leftArrowGameObject == null);
                leftArrowGameObject.GetComponent<Button>().enabled = true;
            }
            PlayerPrefs.SetInt("currentPage", PlayerPrefs.GetInt("currentPage") + 1);

            if (PlayerPrefs.GetInt("currentPage", 1) == maxPage)
            {
                rightArrowText.color = new Color32(102, 94, 94, 255);
                rightArrowGameObject.GetComponent<Button>().enabled = false;

            }

            if (whichPage == 0)
            {

                rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 2].gameObject.SetActive(false);

                rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true);
            }
            else
            {

                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 2].gameObject.SetActive(false);
                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true);
            }

        }

        
    }
    
    //maybe disable currently used button?? 
    public void difficultyEasyButton()
    {
        PlayerPrefs.SetInt("difficultyLevel", 0);
        GameObject difficultyButtonObject = GameObject.Find("EasyButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("HardButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("MediumButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);
    }

    public void difficultyMedButton()
    {
        PlayerPrefs.SetInt("difficultyLevel", 1);
        GameObject difficultyButtonObject = GameObject.Find("MediumButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("EasyButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("HardButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

    }

    public void difficultyHardButton()
    {
        Debug.Log(" Inside dif hard");
        PlayerPrefs.SetInt("difficultyLevel", 2);
        GameObject difficultyButtonObject = GameObject.Find("HardButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("EasyButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("MediumButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);
    }

    public void playerOneButton()
    {
        PlayerPrefs.SetInt("playerCount", 1);

        GameObject difficultyButtonObject = GameObject.Find("OneButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("TwoButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("ThreeButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

    }

    public void playerTwoButton()
    {
        PlayerPrefs.SetInt("playerCount", 2);

        GameObject difficultyButtonObject = GameObject.Find("TwoButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("OneButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("ThreeButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

    }

    public void playerThreeButton()
    {
        PlayerPrefs.SetInt("playerCount", 3);

        GameObject difficultyButtonObject = GameObject.Find("ThreeButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("OneButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

        difficultyButtonObject = GameObject.Find("TwoButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

    }

    public void bettingNoEnabled()
    {
        PlayerPrefs.SetInt("bettingEnabled", 0);

        GameObject difficultyButtonObject = GameObject.Find("NoButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("YesButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);



    }

    public void bettingYesEnabled()
    {
        PlayerPrefs.SetInt("bettingEnabled", 1);

        GameObject difficultyButtonObject = GameObject.Find("YesButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("NoButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);

    }

    public void volumeNoLevel()
     {
            
        PlayerPrefs.SetInt("soundEnabled", 0);

        GameObject difficultyButtonObject = GameObject.Find("NoVButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("YesVButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);
    }

    public void volumeYesLevel()
    {
        PlayerPrefs.SetInt("soundEnabled", 1);

        GameObject difficultyButtonObject = GameObject.Find("YesVButton");
        Image difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(29, 255, 3, 255);

        difficultyButtonObject = GameObject.Find("NoVButton");
        difficultyText = difficultyButtonObject.GetComponent<Image>();
        difficultyText.color = new Color32(255, 255, 255, 255);
    }
}
