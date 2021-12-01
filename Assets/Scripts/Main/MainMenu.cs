using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{


    [SerializeField] private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    public void Play()
    {
        gm.Play();
    }

    public void Quit()
    {
        gm.EndGame();
    }

    public void Next()
    {
        gm.LoadNextLevel();
    }
    public void GoToMainMenu()
    {
      gm.MainMenu();
    }
}
