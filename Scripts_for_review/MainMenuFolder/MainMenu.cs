using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text _instructions;
    private bool _instructcheck = false;


    public void Start()
    {
        _instructions.gameObject.SetActive(false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCoOpGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TurnOnInstructions()
    {
        if (_instructcheck == true)
        {
            _instructions.gameObject.SetActive(false);
            _instructcheck = false;
            return;
        }
        if (_instructcheck == false)
        {
            _instructions.gameObject.SetActive(true);
            _instructcheck = true;
            return;
        }
    }

}
