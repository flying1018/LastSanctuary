using UnityEngine;

[CreateAssetMenu(fileName = "Hint", menuName = "New Hint")]
public class HintSO : ScriptableObject
{
    [Header("Info")]
    public int index;
    [TextArea(2, 10)] public string bodyText;
}

public class HintObject : MonoBehaviour
{
    [Header("퍼즐 힌트")]
    public HintSO hintData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            UIManager.Instance.ShowHint(hintData.bodyText);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            UIManager.Instance.CloseHint();
        }
    }
}