using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleButtonScript : MonoBehaviour
{
    GameObject settingsGameObject; //declares settings button object
    GameObject helpGameObject; //declares help button object
    GameObject playGameObject; //declares play button object
    GameObject settingsBlockGameObject; //declares settingsBlock object
    GameObject leftArrowGameObject; //declares left arrow object
    GameObject rightArrowGameObject; //declares right arrow object
    GameObject leftArrowTextGameObject; //declares left text arrow object
    GameObject rightArrowTextGameObject; //declares right text arrow object
    GameObject optionalDisplayGameObject; //declares optionalDisplay block object
    GameObject helpBlockGameObject; //declares help block object
    GameObject[] howToPagesGameObjects; //declares howToPages block (text) objects
    TextMeshProUGUI[] rulesPagesGameObjects; // declares rulesPages block text objects
    GameObject rulesPagesBlockObject; //declares rulesPages block object
    GameObject howToPagesBlockObject; //declare howToPages block object
 
    int whichPage; // 0 == rules, 1 == howTo

    // Start is called before the first frame update
    void Start()
    {
        //if rounds aren't set, set rounds amount to 0
        if (PlayerPrefs.GetInt("setRounds") == 0)
        {
            PlayerPrefs.SetInt("rounds", 0);
        }

        //if player amount is not set, set to default 1
        if (PlayerPrefs.GetInt("playerCount") == 0)
        {
            PlayerPrefs.SetInt("playerCount", 1);
        }

        //if player amount is set to 1, execute associated function
        else if(PlayerPrefs.GetInt("playerCount") == 1)
        {
            playerOneButton();
        }
        //if player amount is set to 2, execute associated function
        else if (PlayerPrefs.GetInt("playerCount") == 2)
        {
            playerTwoButton();
        }
        //if player amount is set to 3, execute associated function
        else if (PlayerPrefs.GetInt("playerCount") == 3)
        {
            playerThreeButton();
        }

        //if difficulty is set to 0, set to default 2 (medium)
        if (PlayerPrefs.GetInt("difficultyLevel") == 0)
        {
            PlayerPrefs.SetInt("difficultyLevel", 2);
        }
        //if difficulty is set to 1, execute associated function
        else if (PlayerPrefs.GetInt("difficultyLevel") == 1)
        {
            difficultyEasyButton();
        }
        //if difficulty is set to 2, execute associated function
        else if (PlayerPrefs.GetInt("difficultyLevel") == 2)
        {
            difficultyMedButton();
        }
        //if difficulty is set to 3, execute associated function
        else if (PlayerPrefs.GetInt("difficultyLevel") == 3)
        {
            difficultyHardButton();
        }

        //if gameSpeed is set to 0, set to default (medium)
        if(PlayerPrefs.GetFloat("gameSpeed") == 0)
        {
            mediumButton();
        }
        //if gameSpeed is set to 1, execute associated function
        else if (PlayerPrefs.GetFloat("gameSpeed") == 1)
        {
            slowButton();
        }
        //if gameSpeed is set to 2, execute associated function
        else if (PlayerPrefs.GetFloat("gameSpeed") == 2)
        {
            mediumButton();
        }
        //if gameSpeed is set to 3, execute associated function
        else if (PlayerPrefs.GetFloat("gameSpeed") == 3)
        {
            fastButton();
        }        
       
        //if betting isn't initialized, set to default 0 (false/off)
        if(PlayerPrefs.GetInt("bettingEnabled") == 0)
        {
            PlayerPrefs.SetInt("bettingEnabled", 1);
        }
        //if betting is set to 1, execute associated function
        else if (PlayerPrefs.GetInt("bettingEnabled") == 1)
        {
            bettingNoEnabled();
        }
        //if betting is set to 2, execute associated function
        else if (PlayerPrefs.GetInt("bettingEnabled") == 2)
        {
            bettingYesEnabled();
        }

        optionalDisplayGameObject.SetActive(false); //hide all optionalDisplays
        settingsBlockGameObject.SetActive(false); //hides settings block 
        helpBlockGameObject.SetActive(false); //hides help block
        PlayerPrefs.SetInt("currentPage", 1); //sets currentPage to 1 
        whichPage = 0; //sets the page section to 0 (rules pages)

        PlayerPrefs.Save(); //saves all PlayerPrefs variables
    }

    void Awake()
    {
        howToPagesGameObjects = GameObject.FindGameObjectsWithTag("Pages"); //finds all pages for howToPages
        rulesPagesGameObjects = (GameObject.Find("RulesPagesBlock")).GetComponentsInChildren<TextMeshProUGUI>(); // finds all pages for rulesPages
        rulesPagesBlockObject = GameObject.Find("RulesPagesBlock"); //finds rulesPages block
        howToPagesBlockObject= GameObject.Find("HowToPagesBlock"); //finds howToPages block
        settingsBlockGameObject = GameObject.Find("SettingsGroupBlock"); //finds settings block
        helpBlockGameObject = GameObject.Find("HelpGroupBlock"); //finds help block
        settingsGameObject = GameObject.Find("SettingsButton"); //finds settings button
        playGameObject = GameObject.Find("PlayButton"); //finds play button
        helpGameObject = GameObject.Find("HelpButton"); //finds help button
        optionalDisplayGameObject = GameObject.Find("OptionalDisplay"); //finds optionalDisplay block
        leftArrowGameObject = GameObject.Find("LeftArrowButton"); //finds left arrow button
        rightArrowGameObject = GameObject.Find("RightArrowButton"); //finds right arrow button
        leftArrowTextGameObject = GameObject.Find("BackText"); //finds left button text 
        rightArrowTextGameObject = GameObject.Find("NextText"); //finds right button text 
    }

    //starts game
    public void playButtonAction()
    {
        SceneManager.LoadScene("GameplayScene"); //loads game scene
    }

    //settings button
    public void settingsButtonAction()
    {
        if (optionalDisplayGameObject != null) //if parent block is accessible (not null)
        {
            PlayerPrefs.SetInt("currentPage", 1); //set current page to first page

            optionalDisplayGameObject.SetActive(true); //set parent block active
            settingsGameObject.GetComponent<Button>().enabled = false; //disables setting button
            helpGameObject.GetComponent<Button>().enabled = false; //disables help button
            playGameObject.GetComponent<Button>().enabled = false; //disables play button
            settingsBlockGameObject.SetActive(true); // shows/enables setting block screen
            helpBlockGameObject.SetActive(false); // hides help block screen
        }
    }

    //help button
    public void helpButtonAction()
    {
        //if parent block is accessible (not null)
        if (optionalDisplayGameObject != null)
        {
            PlayerPrefs.SetInt("currentPage", 1); //set currentPage to first page

            optionalDisplayGameObject.SetActive(true); // sets parent block active
            settingsGameObject.GetComponent<Button>().enabled = false; //disables setting button
            helpGameObject.GetComponent<Button>().enabled = false; //disables help button
            playGameObject.GetComponent<Button>().enabled = false; //disables play button
            helpBlockGameObject.SetActive(true); //shows/enables help block screen
            settingsBlockGameObject.SetActive(false); //hides setting block screen
            leftArrowGameObject.SetActive(true); //enables left button
            rightArrowGameObject.SetActive(true); //enable right button
             
            (GameObject.Find("HowToPagesBlock")).SetActive(true); //enables howToPages block
            (GameObject.Find("RulesPagesBlock")).SetActive(true); //enables rulePages block

            //hides each howToPages block page
            foreach (GameObject obj in howToPagesGameObjects) 
            {
                obj.SetActive(false);
            }

            //hides each rulesPages block page
            foreach(TextMeshProUGUI obj in rulesPagesGameObjects)
            {
                obj.gameObject.SetActive(false);
            }

            rulesPagesGameObjects[0].gameObject.SetActive(true); //show first page for rulesPages block
        }
    }

    //rules button
    public void rulesButtonAction()
    {
        //hides each howToPages block page
        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        //hides each rulePages block page
        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        rulesPagesBlockObject.SetActive(true); //shows rulesPages block
        howToPagesBlockObject.SetActive(false); //hides howToPages block
        whichPage = 0; //sets which page to 0 (rules page)
        PlayerPrefs.SetInt("currentPage", 1); //sets current page to first page

        GameObject ruleButtonObject = GameObject.Find("RulesText"); //finds rules object
        GameObject howToButtonObject = GameObject.Find("HowToPlayText"); //finds howToPlay object

        TextMeshProUGUI rulesText = ruleButtonObject.GetComponent<TextMeshProUGUI>(); //gets rules text object
        TextMeshProUGUI howToText = howToButtonObject.GetComponent<TextMeshProUGUI>(); //gets howToPlay text object
        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //gets left arrow text object
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //gets right arrow text object
        
        rulesText.color = new Color32(255, 255, 255, 255); //changes rules text to white (actively in use section)
        howToText.color = new Color32(102, 94, 94, 255); //chages howTo text to gray (inactive section)

        rulesPagesGameObjects[0].gameObject.SetActive(true); //shows first rulesPages page
       
        leftArrowGameObject.gameObject.GetComponent<Button>().enabled = false; //disables left arrow button
        leftArrowText.color = new Color32(102, 94, 94, 255); //changes arrow color to gray (disabled color)
        rightArrowGameObject.gameObject.GetComponent<Button>().enabled = true; //enables right arrow button
        rightArrowText.color = new Color32(255, 255, 255, 255);  //changes arrow color to white (enabled color)
    }

    //howToPlay button
    public void howToPlayButtonAction()
    {
        //hides each howToPages block page
        foreach (GameObject obj in howToPagesGameObjects)
        {
            obj.SetActive(false);
        }

        //hides each rulePages block page
        foreach (TextMeshProUGUI obj in rulesPagesGameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        rulesPagesBlockObject.SetActive(false); //hides rulesPages block
        howToPagesBlockObject.SetActive(true); //shows howToPages block

        whichPage = 1; //sets which page to 0 (howTo page)
        PlayerPrefs.SetInt("currentPage", 1); //sets current page to first page

        GameObject ruleButtonObject = GameObject.Find("RulesText"); //finds rules object
        GameObject howToButtonObject = GameObject.Find("HowToPlayText"); //finds howToPlay object

        TextMeshProUGUI rulesText = ruleButtonObject.GetComponent<TextMeshProUGUI>(); //gets rules text object
        TextMeshProUGUI howToText = howToButtonObject.GetComponent<TextMeshProUGUI>(); //gets howToPlay text object
        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //gets left arrow text object
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //gets right arrow text object

        leftArrowGameObject.gameObject.GetComponent<Button>().enabled = false; //disables left arrow button
        leftArrowText.color = new Color32(102, 94, 94, 255); ; //changes arrow color to gray (disabled color)
        rightArrowGameObject.gameObject.GetComponent<Button>().enabled = true; //enables right arrow button
        rightArrowText.color = new Color32(255, 255, 255, 255); //changes arrow color to white (enabled color)

        rulesText.color = new Color32(102, 94, 94, 255); //changes rules text to gray (inactive section)
        howToText.color = new Color32(255, 255, 255, 255); //chages howTo text to white (actively in use section)

        howToPagesGameObjects[0].gameObject.SetActive(true); //shows howToPlay first page
    } 

    //reset button
    public void resetButtonAction()
    {
        //resets settings values to default values 
        bettingNoEnabled(); //executes default betting function, no
        playerOneButton(); //executes default player amount function, 1 
        difficultyMedButton(); //executes default difficulty function, medium
        mediumButton(); //executes default game speed function, medium

        PlayerPrefs.SetInt("bettingEnabled", 1); //1 == false, 2 == true
        PlayerPrefs.SetInt("playerCount", 1); //min 1, max 3
        PlayerPrefs.SetInt("difficultyLevel", 2); // 1 == beginner, 2 == normal, 3 == expert   
        PlayerPrefs.SetFloat("gameSpeed", 1.5f); //sets gamespeed to default value, medium
    }

    //save button
    public void saveButtonAction()
    {
        optionalDisplayGameObject.SetActive(false); //hides optionalDisplay screen
        settingsGameObject.GetComponent<Button>().enabled = true; //enables setting button
        helpGameObject.GetComponent<Button>().enabled = true; //enables help button
        playGameObject.GetComponent<Button>().enabled = true; // enables play button
        settingsBlockGameObject.SetActive(false); //hides setting block screen
        PlayerPrefs.Save(); //saves PlayerPrefs variable values

    }

    //done button
    public void doneButtonAction()
    {
        optionalDisplayGameObject.SetActive(false); //hides optionalDisplay screen
        settingsGameObject.GetComponent<Button>().enabled = true; //enables setting button
        helpGameObject.GetComponent<Button>().enabled = true; //enables help button
        playGameObject.GetComponent<Button>().enabled = true; //enables play button
        helpBlockGameObject.SetActive(false); // hides help block screen
    }

    //left arrow button
    public void leftArrowButtonAction()
    {
        int maxPage; //declares max page value

        //if whichPage is 0 (rulesPage)
        if (whichPage == 0)
        {
            maxPage = rulesPagesGameObjects.Length; //get length of rulesPages
        }
        else
        {
            maxPage = howToPagesGameObjects.Length; //get length of howToPages 
        }
       
        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //finds left arrow text object
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //finds right arrow text object

        //determine which page is active
        if (PlayerPrefs.GetInt("currentPage") - 1 >= 1)
        {
            //if currentPage equals max possible page
            if (PlayerPrefs.GetInt("currentPage") == maxPage)
            {
                rightArrowText.color = new Color32(255, 255, 255, 255); //change right arrow text to white (enabled color)
                rightArrowGameObject.GetComponent<Button>().enabled = true; //enable right arrow
            }
            
            PlayerPrefs.SetInt("currentPage", PlayerPrefs.GetInt("currentPage") - 1); //decrement currentPage variable

            //if currentPage is min value
            if (PlayerPrefs.GetInt("currentPage") == 1)
            {
                leftArrowText.color = new Color32(102, 94, 94, 255); //change left arrow text to grey (disabled color)
                leftArrowGameObject.GetComponent<Button>().enabled = false; //disable left arrow
            }

            //if on rulesPage
            if (whichPage == 0)
            {
                (rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage")]).gameObject.SetActive(false); //hide previous rules page
                rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true); //show current rules page
            }
            else
            {
                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage")].gameObject.SetActive(false); //hide previous howTo page
                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true); //show current howTo page
            }
        }
    }

    //righ arrow button
    public void rightArrowButtonAction()
    {
        int maxPage; //declares max page value

        //if whichPage is 0 (rulesPage)
        if (whichPage == 0)
        {
            maxPage = rulesPagesGameObjects.Length; //get length of rulesPages
        }
        else
        {
            maxPage = howToPagesGameObjects.Length; //get length of howToPages 
        }

        TextMeshProUGUI leftArrowText = leftArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //finds left arrow text object
        TextMeshProUGUI rightArrowText = rightArrowTextGameObject.GetComponent<TextMeshProUGUI>(); //finds right arrow text object

        //determine which page is active
        if (PlayerPrefs.GetInt("currentPage") + 1 <= maxPage)
        {
            //if currentPage equals min possible page
            if (PlayerPrefs.GetInt("currentPage") == 1)
            {
                leftArrowText.color = new Color32(255, 255, 255, 255); //change left text color to white (enabled color)
                leftArrowGameObject.GetComponent<Button>().enabled = true; //enable left arrow button
            }

            PlayerPrefs.SetInt("currentPage", PlayerPrefs.GetInt("currentPage") + 1); //increments currentPage variable

            //if currentPage equals maxPage
            if (PlayerPrefs.GetInt("currentPage") == maxPage)
            {
                rightArrowText.color = new Color32(102, 94, 94, 255); //change right text color to gray (disabled color)
                rightArrowGameObject.GetComponent<Button>().enabled = false; //disable right arrow button
            }

            //if on rulesPage
            if (whichPage == 0)
            {
                rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 2].gameObject.SetActive(false); //hide previous rules page
                rulesPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true); //show current rules page
            }
            else
            {
                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 2].gameObject.SetActive(false); //hide previous howTo page
                howToPagesGameObjects[PlayerPrefs.GetInt("currentPage") - 1].gameObject.SetActive(true); //show current howTo page
            }
        }   
    }
    
    //easy difficulty button
    public void difficultyEasyButton()
    {
        PlayerPrefs.SetInt("difficultyLevel", 1); //sets difficultly to 1 (easy)
        GameObject difficultyButtonObject = GameObject.Find("EasyButton"); //finds easy button
        Image difficultyImage = difficultyButtonObject.GetComponent<Image>(); // finds easy button image
        difficultyImage.color = new Color32(29, 255, 3, 255); //sets easy button color to green

        difficultyButtonObject = GameObject.Find("HardButton"); // finds hard button 
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); //finds hard button image
        difficultyImage.color = new Color32(255, 255, 255, 255); //sets color to white

        difficultyButtonObject = GameObject.Find("MediumButton"); //finds medium button
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); //finds image
        difficultyImage.color = new Color32(255, 255, 255, 255); //sets color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //medium difficulty button
    public void difficultyMedButton()
    {
        PlayerPrefs.SetInt("difficultyLevel", 2); //sets difficulty to 2 (medium)
        GameObject difficultyButtonObject = GameObject.Find("MediumButton"); // find medium button
        Image difficultyImage = difficultyButtonObject.GetComponent<Image>(); //find image
        difficultyImage.color = new Color32(29, 255, 3, 255); //change color to green

        difficultyButtonObject = GameObject.Find("EasyButton"); //find easy button
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); //find image
        difficultyImage.color = new Color32(255, 255, 255, 255); //change color to white

        difficultyButtonObject = GameObject.Find("HardButton"); //find hard button
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); //find image
        difficultyImage.color = new Color32(255, 255, 255, 255); //change color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //hard difficulty button
    public void difficultyHardButton()
    {
        PlayerPrefs.SetInt("difficultyLevel", 3); //sets difficulty level to 3 (hard)
        GameObject difficultyButtonObject = GameObject.Find("HardButton"); //finds hard button
        Image difficultyImage = difficultyButtonObject.GetComponent<Image>(); //finds image
        difficultyImage.color = new Color32(29, 255, 3, 255); //changes color to green

        difficultyButtonObject = GameObject.Find("EasyButton"); //finds easy button
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); // finds image
        difficultyImage.color = new Color32(255, 255, 255, 255); //changes color to white

        difficultyButtonObject = GameObject.Find("MediumButton"); //finds medium button
        difficultyImage = difficultyButtonObject.GetComponent<Image>(); //finds image
        difficultyImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //one player button
    public void playerOneButton()
    {
        PlayerPrefs.SetInt("playerCount", 1); //sets player count to 1

        GameObject playerButtonObject = GameObject.Find("OneButton"); //finds "one" button
        Image playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(29, 255, 3, 255); //changes color to green

        playerButtonObject = GameObject.Find("TwoButton"); //finds "two" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        playerButtonObject = GameObject.Find("ThreeButton"); //finds "three" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //two player button
    public void playerTwoButton()
    {
        PlayerPrefs.SetInt("playerCount", 2); //sets player count to 2

        GameObject playerButtonObject = GameObject.Find("TwoButton"); //finds "two" button
        Image playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(29, 255, 3, 255); //changes color to green

        playerButtonObject = GameObject.Find("OneButton"); //finds "one" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        playerButtonObject = GameObject.Find("ThreeButton"); //finds "three" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //three player button
    public void playerThreeButton()
    {
        PlayerPrefs.SetInt("playerCount", 3); //sets player count to 3

        GameObject playerButtonObject = GameObject.Find("ThreeButton"); //finds "three" button
        Image playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(29, 255, 3, 255); //changes color to green

        playerButtonObject = GameObject.Find("OneButton"); //finds "one" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        playerButtonObject = GameObject.Find("TwoButton"); //finds "two" button
        playerImage = playerButtonObject.GetComponent<Image>(); //finds image
        playerImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //betting not enabled button
    public void bettingNoEnabled()
    {
        PlayerPrefs.SetInt("bettingEnabled", 1); //sets betting enabled to 1 (not enabled)

        GameObject bettingButtonObject = GameObject.Find("NoButton"); //finds "no" button
        Image bettingImage = bettingButtonObject.GetComponent<Image>(); //finds image
        bettingImage.color = new Color32(29, 255, 3, 255); //changes color to green

        bettingButtonObject = GameObject.Find("YesButton"); //finds "yes" button
        bettingImage= bettingButtonObject.GetComponent<Image>(); //finds image
        bettingImage.color = new Color32(255, 255, 255, 255); //changs color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    //betting enabled button
    public void bettingYesEnabled()
    {
        PlayerPrefs.SetInt("bettingEnabled", 2); //sets betting enabled to 2 (enabled)

        GameObject bettingButtonObject = GameObject.Find("YesButton"); //finds "yes" button
        Image bettingImage = bettingButtonObject.GetComponent<Image>(); //finds image
        bettingImage.color = new Color32(29, 255, 3, 255); //changes color to green

        bettingButtonObject = GameObject.Find("NoButton"); //finds "no" button
        bettingImage = bettingButtonObject.GetComponent<Image>(); //finds image
        bettingImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values
    }

    
    //slow button
    public void slowButton()
    {    
        PlayerPrefs.SetFloat("gameSpeed", 1.25f); //sets game speed to 

        GameObject speedButtonObject = GameObject.Find("SlowButton"); //finds "slow" button
        Image speedImage = speedButtonObject.GetComponent<Image>(); //find image
        speedImage.color = new Color32(29, 255, 3, 255); //changes color to green

        speedButtonObject = GameObject.Find("FastButton"); //finds "fast" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        speedButtonObject = GameObject.Find("MediumSpeedButton"); //finds "medium" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values

    }

    //medium button
    public void mediumButton()
    {
        PlayerPrefs.SetFloat("gameSpeed", 1.5f); //sets game speed to 1.5

        GameObject speedButtonObject = GameObject.Find("MediumSpeedButton"); //finds "medium" button
        Image speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(29, 255, 3, 255); //changes color to green

        speedButtonObject = GameObject.Find("FastButton"); //finds "fast" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        speedButtonObject = GameObject.Find("SlowButton"); //finds "slow" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values

    }

    //fast button
    public void fastButton()
    {
        PlayerPrefs.SetFloat("gameSpeed", .75f); //sets game speed to .75

        GameObject speedButtonObject = GameObject.Find("FastButton"); //finds "fast" button
        Image speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(29, 255, 3, 255); //changes color to green

        speedButtonObject = GameObject.Find("SlowButton"); //finds "slow" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        speedButtonObject = GameObject.Find("MediumSpeedButton"); //finds "medium" button
        speedImage = speedButtonObject.GetComponent<Image>(); //finds image
        speedImage.color = new Color32(255, 255, 255, 255); //changes color to white

        PlayerPrefs.Save(); //saves PlayerPrefs values

    }

    //exit game button
    public void exitGame()
    {
        optionalDisplayGameObject.SetActive(true); //shows (briefly) optionalDisplay
        settingsBlockGameObject.SetActive(true); //shows (briefly) settings screen
        
        //resets all player money to 500
        for(int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("playersMoney" + i, 500);
        }

        resetButtonAction(); //reset game values

        #if UNITY_EDITOR //preprocessor directive to test if Unity Editor is defined
        
        Application.Quit(); //quits application

        //if editor is stopped, also resets
        #endif
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
