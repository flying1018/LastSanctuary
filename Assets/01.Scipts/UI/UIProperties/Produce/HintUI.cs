using TMPro;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI hintText;

    private bool _isOpen;

    void Update()
    {
        if (_isOpen && Input.GetKeyDown(KeyCode.Escape))
            CloseHint();
    }

    public void OpenHint(string body)
    {
        hintText.text = body;
        panel.SetActive(true);
        _isOpen = true;
    }

    public void CloseHint()
    {
        this.gameObject.SetActive(false);
        _isOpen = false;
    }
}