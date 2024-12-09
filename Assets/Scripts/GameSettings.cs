using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private int settings;
    private const int settingsNumber = 1;

    private Settings gameSettings;

    public static GameSettings Instance;

    public enum ELevelNumber
    {
        NotSet = 0,
        level1 = 9,
        level2 = 16,
        level3 = 25
    }

    public struct Settings
    {
        public ELevelNumber LevelsNumber;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = new Settings();
        ResetGameSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Avoid the scene from being destroyed
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetLevelNumber(ELevelNumber Number)
    {
        if (gameSettings.LevelsNumber == ELevelNumber.NotSet)
            settings++;
        gameSettings.LevelsNumber = Number;
    }

    public ELevelNumber GetLevelNumber()
    {
        return gameSettings.LevelsNumber;
    }

    public void ResetGameSettings()
    {
        settings = 0;
        gameSettings.LevelsNumber = ELevelNumber.NotSet;
    }

    public bool AllSettingsReady()
    {
        return settings == settingsNumber;
    }
}
