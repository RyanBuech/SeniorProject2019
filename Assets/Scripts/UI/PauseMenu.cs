﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public string mainMenuScene;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    GameObject player;
    GameObject sanityWhispers;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sanityWhispers = GameObject.Find("Player/Sanity Whispers");
    }

    void Start()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<AudioSource>().Pause();
        sanityWhispers.GetComponent<AudioSource>().Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sanityWhispers.GetComponent<AudioSource>().Play();
        //pauseMenuUI.SetActive(false);
    }

    public void LoadOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene(mainMenuScene);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
    }

    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        if (InputManager.instance.Pause())
        {
            if (pauseCanvas.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void GameOver()
    {

    }

}
