using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Button closeButton;
    
    private Image _image;
    private UIManager _uiManager;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButton);
        _uiManager = UIManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCloseButton();
        }
    }

    public void Init(Sprite sprite, string title, string explanation)
    {
        _uiManager.PopUpQueue.Enqueue(this);
        
        Transform imageTransform = transform.Find("Image");
        if (_image == null)
        {
            _image = imageTransform.GetComponentInChildren<Image>();
        }
        _image.sprite = sprite;
        texts[0].text = title.ToString();
        texts[1].text = explanation.ToString();
    }

    private void OnCloseButton()
    {
        gameObject.SetActive(false);
        _uiManager.PopUpQueue.Dequeue();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
