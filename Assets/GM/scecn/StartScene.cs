using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

    public Canvas setCanvas;
    public Canvas SelectStage;
    public Button startButton;
    public Button setButton;
    public Button exitButton;
    public Button setExit;
    public Button Stage1;
    public Button Stage2;
    public Button Stage3;
    public Button Stage4;
    public Button Stage5;
    public Button selectExit;
    public Slider slider;  //
    public AudioClip clip;  //
    public AudioSource audio;
    AsyncOperation async_operation;
    public void Start()
    {
        setCanvas = setCanvas.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        setButton = setButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        setExit = setExit.GetComponent<Button>();
        Stage1 = Stage1.GetComponent<Button>();
        Stage2 = Stage2.GetComponent<Button>();
        Stage3 = Stage3.GetComponent<Button>();
        Stage4 = Stage4.GetComponent<Button>();
        Stage5 = Stage5.GetComponent<Button>();
        selectExit = selectExit.GetComponent<Button>();
        SelectStage.enabled = false;
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
        //async_operation = Application.LoadLevelAsync("loading");
        setCanvas.enabled = false;
        SelectStage.enabled = true;
        startButton.enabled = false;
        setButton.enabled = false;
        exitButton.enabled = false;

    }
    public void Stage1Start()
    {
        async_operation = Application.LoadLevelAsync("loading");
    }
    public void Stage2Start()
    {
        async_operation = Application.LoadLevelAsync("stage2");
    }
    public void SelectExit_Button()
    {
        setCanvas.enabled = false;
        SelectStage.enabled = false;
        startButton.enabled = true;
        setButton.enabled = true;
        exitButton.enabled = true;
    }
    public void Exit_Button()
    {
        // async_operation = Application.CancelQuit();
        Application.Quit();
    }

    public void Setting_Button()
    {
        setCanvas.enabled = true;
        SelectStage.enabled = false;
        startButton.enabled = false;
        setButton.enabled = false;
        exitButton.enabled = false;
    }
    public void setExit_Button()
    {
        setCanvas.enabled = false;
        SelectStage.enabled = false;
        startButton.enabled = true;
        setButton.enabled = true;
        exitButton.enabled = true;
    }

    public void SoundSlider()
    {
        //AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2), slider.value); //\
        audio.volume = slider.value;
    }
    
}
