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

    [Header("Game Over")]
    public Text gameOverText;

    void Start()
    {
        gameOverText.enabled = false;

        // Başlangıç renkleri yeşil
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
            gameOverText.text = "Game Over!";
            gameOverText.enabled = true;
            Time.timeScale = 0f;
        }
    }

    // 🔹 Manuel bağlamak için public olmalı:
    public void FeedPet()
    {
        hunger += 20f;
        hunger = Mathf.Clamp(hunger, 0, maxValue);
    }

    public void PlayWithPet()
    {
        happiness += 25f;
        happiness = Mathf.Clamp(happiness, 0, maxValue);
    }

    public void CleanPet()
    {
        hygiene += 30f;
        hygiene = Mathf.Clamp(hygiene, 0, maxValue);
    }
}
