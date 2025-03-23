using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isgameover = false;
    public bool isCoopMode = false;
    private bool _isPaused = false;

    [SerializeField]
    private GameObject pauseMenu;


    private SpawnManager _spawnManager;

    private void Start()
    {

        pauseMenu.SetActive(false);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isgameover == true)
        {
            ResetScene();
        }
  

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

    }
    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void isgameover()
    {
        _isgameover = true; 
    }


    private void TogglePauseMenu()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; 
      
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        _isPaused = false;
    }


     public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
