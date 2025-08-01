using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button newButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        newButton.onClick.AddListener(OnClickGameStart);
        exitButton.onClick.AddListener(OnClickExit);
    }

    public void OnClickGameStart()
    {
        //테스트용
        SceneManager.LoadScene(StringNameSpace.Scenes.RenewalTutorials);
    }
    
    public void OnClickExit()
    {
        Application.Quit();
    }
    
}
