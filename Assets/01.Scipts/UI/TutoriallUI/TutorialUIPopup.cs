using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Button closeButton;
    
    private Image _image;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButton);
    }

    public void Init(Sprite sprite, string title, string explanation)
    {
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
    }
}
