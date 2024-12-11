using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private readonly Dictionary<ELevelNumber, string> levelNumDirectory = new Dictionary<ELevelNumber, string>();
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

    // Start is called before the first frame update
    void Start()
    {
        SetLevelNumDirectory();
        gameSettings = new Settings();
        ResetGameSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLevelNumDirectory()
    {
        levelNumDirectory.Add(ELevelNumber.level1, "Level 1");
        levelNumDirectory.Add(ELevelNumber.level2, "Level 2");
        levelNumDirectory.Add(ELevelNumber.level3, "Level 3");
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

    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }

    public string GetLevelNumTextureDirectoryName()
    {
        if (levelNumDirectory.ContainsKey(gameSettings.LevelsNumber))
        {
            return "Graphics/LevelNum/" + levelNumDirectory[gameSettings.LevelsNumber] + "/";
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY NAME");
            return "";
        }
    }
}
