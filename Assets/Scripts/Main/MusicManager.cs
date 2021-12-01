using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using System;

[Serializable]
public class MusicManager
{
    [SerializeField] private FMOD.Studio.EventInstance musicInstance;
    [SerializeField] private FMOD.Studio.EventInstance tempInstance;



    public MusicManager()
    {

    }

    public void Initialize()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Menu");
        musicInstance.setVolume(.7f);
        musicInstance.start();
    }

    //0 Main menu
    //1 main song
    public void ChangeMusic(int song)
    {

        tempInstance = musicInstance;
        musicInstance.stop(STOP_MODE.IMMEDIATE);
        musicInstance.release();
        switch (song)
        {
            case 0:
                musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Menu");
                break;
            case 1:
                musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
                break;
            default:
                break;

        }

        musicInstance.setVolume(.7f);
        GC.Collect();
        musicInstance.start();
    }

    public void NextLevel()
    {
        ChangeMusic(1);
        musicInstance.setParameterByName("Proximity", 0);
        musicInstance.setParameterByName("Combat Trigger", 0f);
    }

    public void StartLevel()
    {
        musicInstance.setParameterByName("Combat Trigger", 0);
        musicInstance.setParameterByName("Proximity", 10f);
    }

    public void StartFight()
    {
        musicInstance.setParameterByName("Combat Trigger", 1f);
    }

    public void HealthChange(float healthPercent)
    {
        musicInstance.setParameterByName("Health", healthPercent);
    }
    public void FadeOut()
    {
        musicInstance.stop(STOP_MODE.IMMEDIATE);
        musicInstance.release();

    }
}
