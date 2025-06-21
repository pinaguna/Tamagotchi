using UnityEngine;
using UnityEngine.UI;

public class PetStats : MonoBehaviour
{
    [Header("Stat Bars (Slider)")]
    public Slider hungerBar;
    public Slider happinessBar;
    public Slider hygieneBar;

    [Header("Fill Images (for color)")]
    public Image hungerFillImage;
    public Image happinessFillImage;
    public Image hygieneFillImage;

    [Header("Stat Values")]
    public float hunger = 100f;
    public float happiness = 100f;
    public float hygiene = 100f;
    public float maxValue = 100f;

    [Header("Decay Rates")]
    public float hungerDecay = 6f;
    public float happinessDecay = 5.5f;
    public float hygieneDecay = 5.75f;


    private Animator catAnimator;
    public GameObject catObject;
    public GameObject OutroPanel;

    private float randomAnimCooldown = 6f;
    private float lastRandomAnimTime = 0f;

    public AudioSource gameOverAudio;


    void Start()
    {
        catAnimator = catObject.GetComponent<Animator>();

        SetBarColor(hungerFillImage, true);
        SetBarColor(happinessFillImage, true);
        SetBarColor(hygieneFillImage, true);
    }

    void Update()
    {
        hunger -= hungerDecay * Time.deltaTime;
        happiness -= happinessDecay * Time.deltaTime;
        hygiene -= hygieneDecay * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0, maxValue);
        happiness = Mathf.Clamp(happiness, 0, maxValue);
        hygiene = Mathf.Clamp(hygiene, 0, maxValue);

        UpdateUI();
        CheckGameOver();
        HandleAnimations();
    }

    void UpdateUI()
    {
        hungerBar.value = hunger;
        happinessBar.value = happiness;
        hygieneBar.value = hygiene;

        SetBarColor(hungerFillImage, hunger >= maxValue * 0.5f);
        SetBarColor(happinessFillImage, happiness >= maxValue * 0.5f);
        SetBarColor(hygieneFillImage, hygiene >= maxValue * 0.5f);
    }

    void SetBarColor(Image fillImage, bool isAboveHalf)
    {
        fillImage.color = isAboveHalf ? Color.green : Color.red;
    }

    void CheckGameOver()
    {
        if (hunger <= 0 || happiness <= 0 || hygiene <= 0)
        {
            if (catObject != null)
                Destroy(catObject);

            OutroPanel.SetActive(true);

            if (gameOverAudio != null)
            {
                Debug.Log("GameOver sesi çalýyor!");
                gameOverAudio.PlayDelayed(0.01f);
            }
            Time.timeScale = 0f;
        }
    }

    public enum CatAnimState
    {
        Idle = 0,
        Cry = 1,
        Sad = 2,
        Laydown = 3,
        Dance = 4,
        Sleepy = 5
    }

    void HandleAnimations()
    {
        if (hunger < 20)
            catAnimator.SetInteger("State", (int)CatAnimState.Cry);
        else if (happiness < 20)
            catAnimator.SetInteger("State", (int)CatAnimState.Sad);
        else if (hygiene < 20)
            catAnimator.SetInteger("State", (int)CatAnimState.Laydown);
        else
        {
            if (Time.time - lastRandomAnimTime > randomAnimCooldown)
            {
                lastRandomAnimTime = Time.time;

                int randomAnim = Random.Range(0, 3);
                switch (randomAnim)
                {
                    case 0:
                        catAnimator.SetInteger("State", (int)CatAnimState.Idle);
                        break;
                    case 1:
                        catAnimator.SetInteger("State", (int)CatAnimState.Dance);
                        break;
                    case 2:
                        catAnimator.SetInteger("State", (int)CatAnimState.Sleepy);
                        break;
                }
            }
        }
    }



    //  HUNGER
    public void FeedFromFood()
    {
        hunger += 15f;
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void FeedFromFoodBag()
    {
        hunger += 20f;
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void FeedFromWater()
    {
        hunger += 10f;
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    // HAPPINESS (but happiness +=, hunger -=)
    public void PlayWithCatBed()
    {
        happiness += 20f;
        hunger -= 5f;
        happiness = Mathf.Clamp(happiness, 0, maxValue);
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void PlayWithBlueBall()
    {
        happiness += 15f;
        hunger -= 5f;
        happiness = Mathf.Clamp(happiness, 0, maxValue);
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void PlayWithGreenBall()
    {
        happiness += 10f;
        hunger -= 5f;
        happiness = Mathf.Clamp(happiness, 0, maxValue);
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void PlayWithCatHouse()
    {
        happiness += 25f;
        hunger -= 5f;
        happiness = Mathf.Clamp(happiness, 0, maxValue);
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    //  HYGIENE
    public void CleanWithSoap()
    {
        hygiene += 30f;
        happiness -= 10f;
        hygiene = Mathf.Clamp(hygiene, 0, maxValue);
        happiness = Mathf.Clamp(happiness, 0, maxValue);
    }

    public void UsePlant1()
    {
        hygiene -= 15f;
        hygiene = Mathf.Clamp(hygiene, 0, maxValue);
    }

    public void UsePlant2()
    {
        hygiene -= 10f;
        hygiene = Mathf.Clamp(hygiene, 0, maxValue);
    }
}
