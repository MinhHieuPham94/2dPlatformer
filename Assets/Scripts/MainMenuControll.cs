using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControll : MonoBehaviour
{
    public GameObject oppitionScene, mainMenu, volumeSlider;
    public float volume;
    public AudioClip buttonClick;
    public AudioSource mainMenuSource;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void Start()
    {
        volumeSlider.GetComponent<Slider>().value = 0.6f;
        mainMenu.SetActive(true);
        oppitionScene.SetActive(false);
    }

    void Update()
    {
        volume = volumeSlider.GetComponent<Slider>().value;
        GetComponent<AudioSource>().volume =  volumeSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volume",volume);

    }

    public void PlayGameButton()
    {
        SceneManager.LoadScene("Scene_1", LoadSceneMode.Single);
        mainMenuSource.PlayOneShot(buttonClick);
    }

    public void QuitGame()
    {
        Application.Quit();
        mainMenuSource.PlayOneShot(buttonClick);
    }

    public void ContinuousGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        mainMenuSource.PlayOneShot(buttonClick);
    }

    public void OppitionButton()
    {
        oppitionScene.SetActive(true);
        mainMenu.SetActive(false);
        mainMenuSource.PlayOneShot(buttonClick);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        oppitionScene.SetActive(false);
        mainMenuSource.PlayOneShot(buttonClick);
    }


}
