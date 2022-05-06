using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame() {
        SceneManager.LoadScene("SugarRush");
    }

    public void HowToPlay() {
        SceneManager.LoadScene("HowtoPlay");
    }

    public void Menu() {
        SceneManager.LoadScene("StartScreen");
    }
}
