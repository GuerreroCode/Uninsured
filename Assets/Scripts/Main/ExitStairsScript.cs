using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStairsScript : MonoBehaviour
{

    [SerializeField] private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    public void SetGameManager(GameManager _gm)
    {
        gm = _gm;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) //player layer
        {
            gm.LoadNextLevel();
        }
    }
}
