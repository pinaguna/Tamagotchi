using UnityEngine;

using TMPro;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject devPet; 
    public GameObject puffEffect;
    public TMP_InputField nameInput;
    public TextMeshProUGUI petNameText;
    public GameObject gameUI;

    public GameObject introMusicObject;

    public GameObject catPrefab;
    public Transform spawnPoint;


    public void OnStartButtonClicked()
    {
        string name = nameInput.text;

        if (string.IsNullOrEmpty(name)) return;

        petNameText.text = name;

        if (introMusicObject != null)
        {
            AudioSource source = introMusicObject.GetComponent<AudioSource>();
            if (source != null) source.Stop();
        }
        StartCoroutine(StartGameRoutine());
    }

    System.Collections.IEnumerator StartGameRoutine()
    {
            puffEffect.transform.position = devPet.transform.position;
            puffEffect.SetActive(true);

            AudioSource puffSound = puffEffect.GetComponent<AudioSource>();
            if (puffSound != null)
            {
                puffSound.Play();
            }

            devPet.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            introPanel.SetActive(false);
            gameUI.SetActive(true);

            GameObject cat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
            FindObjectOfType<PetStats>().catObject = cat;
            FindObjectOfType<PetStats>().catAnimator = cat.GetComponent<Animator>();


    }

    public void ReturnToIntro()
    {
        // Game UI'yi kapat
        gameUI.SetActive(false);

        // Intro ekranýný ve devPet'i aç
        introPanel.SetActive(true);
        devPet.SetActive(true);
        nameInput.text = "";

        // GameOver panelini kapat
        FindObjectOfType<PetStats>().OutroPanel.SetActive(false);

        // Statlarý sýfýrla
        FindObjectOfType<PetStats>().ResetStats();

        // Müziði tekrar baþlat
        if (introMusicObject != null)
        {
            AudioSource source = introMusicObject.GetComponent<AudioSource>();
            if (source != null && !source.isPlaying)
            {
                source.Play();
            }
        }

        // Zamaný tekrar baþlat
        Time.timeScale = 1f;
    }



}