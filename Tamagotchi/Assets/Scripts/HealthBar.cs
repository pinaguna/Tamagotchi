using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image currentHappy;
    public Image currentClean;
    public Image currentHunger;


    public Image foodBubble;
    public Image cleanBubble;
    public Image playBubble;

    public Text HappyText;
    public Text CleanText;
    public Text HungryText;

    public float happiness = 100;
    public float hygiene = 100;
    public float hunger = 100;
    public float max = 100;

    public Button Feed;
    public Button Clean;
    public Button Play;

    public Text GsmeOverText;


    public Image FrogHappy;
    public Image FrogSad;

    private void Start()
    {
        //Button Listener for Hunger
        Button btn1 = Feed.GetComponent<Button>();
        btn1.onClick.AddListener(FeedThePet);

        //Button Listener for Hygiene
        Button btn2 = Clean.GetComponent<Button>();
        btn2.onClick.AddListener(CleanThePet);

        //Button Listener for Happiness
        Button btn3 = Play.GetComponent<Button>();
        btn3.onClick.AddListener(PlayWithThePet);


        //make bubbles not visible upon game start
        cleanBubble.CrossFadeAlpha(0, 0.001f, true);
        foodBubble.CrossFadeAlpha(0, 0.001f, true);
        playBubble.CrossFadeAlpha(0, 0.001f, true);

        //make frog happy at the start
        FrogSad.gameObject.SetActive(false);



        UpdateHungerBar();
        UpdateHappyBar();
        UpdateHygieneBar();

        Update();


    }

    private void Update()
    {
        //added +5.0 to all decrease count for testing
        //this deplinishes happiness over time
        happiness -= 5.5f * Time.deltaTime;
        if (happiness < 0) //this cheks that it doesn't go to negative values
        {
            happiness = 0;
        }

        //this deplinishes hunger over time
        hunger -= 6f * Time.deltaTime;
        if (hunger < 0)
        {
            hunger = 0;
        }

        //this deplinishes hygiene over time
        hygiene -= 5.75f * Time.deltaTime;
        if (hygiene < 0)
        {
            hygiene = 0;
        }

        needsCheck();
        goodParentCheck();

        UpdateHungerBar();
        UpdateHappyBar();
        UpdateHygieneBar();

        GameOver();
    }


    //check needs of frog
    private void needsCheck()
    {
        if (hunger <= 50)
        {
            foodBubble.CrossFadeAlpha(1, 0.5f, true);

        } else
        {
            foodBubble.CrossFadeAlpha(0, 0.5f, true);
        }

        if (hygiene <= 30)
        {
            cleanBubble.CrossFadeAlpha(1, 0.5f, true);
        } else
        {
            cleanBubble.CrossFadeAlpha(0, 0.5f, true);
        }

        if (happiness <= 60)
        {
            playBubble.CrossFadeAlpha(1, 0.5f, true);

        }
        else
        {
            playBubble.CrossFadeAlpha(0, 0.5f, true);
        }
    }
    //determines if frog is happy or neglected
    private void goodParentCheck()
    {
        if (happiness <= 60 || hygiene <= 30 || hunger <= 50)
        {
            FrogHappy.gameObject.SetActive(false);
            FrogSad.gameObject.SetActive(true);
        }
        else
        {
            FrogSad.gameObject.SetActive(false);
            FrogHappy.gameObject.SetActive(true);
        }
    }


    private void UpdateHungerBar()
    {
        float ratio = hunger / max;
        currentHunger.rectTransform.localScale = new Vector3(ratio, 1, 1);
        HungryText.text = (ratio * 100).ToString("0") + '%';
    }

        private void UpdateHappyBar()
    {
        float ratio = happiness / max;
        currentHappy.rectTransform.localScale = new Vector3(ratio, 1, 1);
        HappyText.text = (ratio * 100).ToString("0") + '%';
    }

    private void UpdateHygieneBar()
    {
        float ratio = hygiene / max;
        currentClean.rectTransform.localScale = new Vector3(ratio, 1, 1);
        CleanText.text = (ratio * 100).ToString("0") + '%';
    }

    private void FeedThePet()
    {
        hunger += 20;
        if (hunger > max)
        {
            hunger = max;
        }
    }

    private void CleanThePet()
    {
        hygiene += 25;
        if (hygiene > max)
        {
            hygiene = max;
        }
    }

    private void PlayWithThePet()
    {
        happiness += 30;
        if (happiness > max)
        {
            happiness = max;
        }
    }

    private void GameOver()
    {
        if (hunger <= 0 || hygiene <= 0 || happiness <= 0)
        {
            GsmeOverText.text = "Game Over!";
            GsmeOverText.enabled = true;
            Time.timeScale = 0f;
        }
    }
}
