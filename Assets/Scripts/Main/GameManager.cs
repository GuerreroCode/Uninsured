using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  [SerializeField] private static GameManager _instance;
  [SerializeField] private int level = 0;
  [SerializeField] private MusicManager mm = new MusicManager();

  //player stats
  [SerializeField] private Player player;
  [SerializeField] protected float baseHealth = 10f;
  [SerializeField] protected float baseStrength = 10f;
  [SerializeField] protected float baseSpeed = 2f;
  [SerializeField] protected Equip equip;
  [SerializeField] protected List<Effect> statusEffects;


  //finds itself
  public static GameManager Instance
  {
      get
      {
        if(_instance == null)
          {
              _instance = FindObjectOfType<GameManager>();
          }
          return _instance;
      }
  }

  //if instance != itself, destroy self (guaruntees theres one)
  public void Awake()
  {

      if (_instance != null && _instance != this)
      {
          Destroy(this.gameObject);
      }

      DontDestroyOnLoad(this);
  }

  void Start()
  {
      mm.Initialize();
      player = FindObjectOfType<Player>();

  }

  public void Play()
  {
      mm.ChangeMusic(1);
      LoadNextLevel();
  }

  public void LoadNextLevel()
  {
    if (level > 1)
    {
        SetPlayerStats();
        mm.NextLevel();
        mm.StartLevel();
    }
    level++;
    SceneManager.LoadScene(level);
  }

  public void MainMenu()
  {
      level = 0;
      SceneManager.LoadScene(level);
      mm.ChangeMusic(0);
  }

  public void SetPlayerStats()
  {
      statusEffects = player.GetStatusEffects();
      equip = player.GetEquip().GetPickup().GetEquipPrefab();
  }

  public void ResetLevel()
  {
      mm.StartLevel();
      mm.HealthChange(100f);
      SceneManager.LoadScene(level);
  }
  public bool IsNextLevel()
  {
      if (level > 1)
        return true;
      return false;
  }

  public float GetBaseHealth()
  {
    return baseHealth;
  }


  public float GetBaseSpeed()
  {
    return baseSpeed;
  }

  public float GetBaseStrength()
  {
    return baseStrength;
  }

  public Equip GetEquip()
  {
      return equip;
  }

  public List<Effect> GetStatusEffects()
  {
      return statusEffects;
  }

  public void EndGame()
  {
      Application.Quit();
  }

  public void StartFight()
  {
      mm.StartFight();
  }


  public void SetPlayer(Player _player)
  {
      player = _player;
  }

  public void SetPlayerHealth(float health, float maxHealth)
  {
      float healthPercent = (health/maxHealth) * 100f;
      mm.HealthChange(healthPercent);
  }

}
