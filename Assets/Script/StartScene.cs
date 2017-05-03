using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScene : MonoBehaviour {

    public Canvas setCanvas;
    public Button startButton;
    public Button previousButton;
    public Button setButton;
    public Button exitButton;
    public Button setExit;
    //public GameObject setPanel;  //추가
    public Slider slider;  //
    public AudioClip clip;  //
    public AudioSource audio;
    AsyncOperation async_operation;
    public void Start()
    {
        setCanvas = setCanvas.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        previousButton = previousButton.GetComponent<Button>();
        setButton = setButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        setExit = setExit.GetComponent<Button>();
        setCanvas.enabled = false;
        //setPanel.SetActive(false); //
        //audio.Loop = true;

        audio.clip = clip;

        audio.Play();
        AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2),slider.value); // 
        audio.volume = 0.5f;
        slider.value = audio.volume;
    }
    void Awake()
    {
        
    }
    
    public void Start_Button()
    {
        async_operation = SceneManager.LoadSceneAsync("loading");
    }

    public void Exit_Button()
    {
        // async_operation = Application.CancelQuit();
        Application.Quit();
    }

    public void Setting_Button()
    {
        //setPanel.SetActive(true); //
        setCanvas.enabled = true;
        startButton.enabled = false;
        previousButton.enabled = false;
        setButton.enabled = false;
        exitButton.enabled = false;
    }
    public void setExit_Button()
    {
        //setPanel.SetActive(false); //
        setCanvas.enabled = false;
        startButton.enabled = true;
        previousButton.enabled = true;
        setButton.enabled = true;
        exitButton.enabled = true;
    }

    public void SoundSlider()
    {
        //AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2), slider.value); //\
        audio.volume = slider.value;
    }
    
}
