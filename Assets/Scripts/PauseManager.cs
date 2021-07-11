using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool pause = false;
    public GameObject pauseUI; 
    public GameObject oppitionUI;
    public GameObject volumeSlider;
    public Text score;
    public PlayerHealth playerHealthScr;
    public GameObject endScene;
    // Start is called before the first frame update
    void Start()
    {
        if( !PlayerPrefs.HasKey("HightScore"))
        {
            PlayerPrefs.SetInt("HightScore", 0);
        }

        pauseUI.SetActive(false);
        oppitionUI.SetActive (false);
        endScene.SetActive(false);
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        
    }

    // Update is called once per frame
    void Update()
    {
        Pause(); 

        GetComponent<AudioSource>().volume =  volumeSlider.GetComponent<Slider>().value; 
        
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.P) && !pause)
        {
            pauseUI.SetActive(true);
            pause = !pause;
            Time.timeScale = 0;
        }else if(Input.GetKeyDown(KeyCode.P) && pause)
        {
            pauseUI.SetActive(false);
            pause = !pause;
            Time.timeScale = 1;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        endScene.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        pause = !pause;
        Time.timeScale = 1;
    }

    public void OppitionGame()
    {
        pauseUI.SetActive(false);
        oppitionUI.SetActive (true);

    }

    public void Quit()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Back()
    {
        pauseUI.SetActive(true);
        oppitionUI.SetActive (false);
    }

    public void Dead()
    {
        endScene.SetActive(true);
        if(playerHealthScr.score > PlayerPrefs.GetInt("HightScore"))
        {
            PlayerPrefs.SetInt("HightScore", playerHealthScr
            .score);
            score.text = "HightScore: "+ playerHealthScr.score;
        } else if(playerHealthScr.score < PlayerPrefs.GetInt("HightScore"))
        {
            score.text = "Score: "+ playerHealthScr.score;
            
        }
         PlayerPrefs.DeleteKey("Score");
         PlayerPrefs.DeleteKey("CurHealth");
         PlayerPrefs.DeleteKey("CurKunai");
    }

}
