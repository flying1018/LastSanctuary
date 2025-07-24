using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    
    public void OnClickGameStart()
    {
        //테스트용
        SceneManager.LoadScene(StringNameSpace.Scenes.RenewalTutorials);
    }
    
}
