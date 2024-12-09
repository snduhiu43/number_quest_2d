using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameButton : MonoBehaviour
{
    public enum EButtonType
    {
        NotSet = 0,
        LevelNumberBtn
    }
    
    [SerializeField] public EButtonType ButtonType;
    [HideInInspector] public GameSettings.ELevelNumber LevelNumber = GameSettings.ELevelNumber.NotSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameOption(string GameSceneName)
    {
        var comp = gameObject.GetComponent<SetGameButton>();

        switch (comp.ButtonType)
        {
            case SetGameButton.EButtonType.LevelNumberBtn:
                GameSettings.Instance.SetLevelNumber(comp.LevelNumber);
                break;
        }

        if (GameSettings.Instance.AllSettingsReady())
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
