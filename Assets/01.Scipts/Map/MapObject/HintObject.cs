using UnityEngine;

public class HintObject : MonoBehaviour, IInteractable
{
    [Header("퍼즐 힌트")]
    [TextArea(4, 10)] public string body;

    public void Interact()
    {
        UIManager.Instance.ShowHint(body);
    }
}