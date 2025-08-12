using UnityEngine;

[CreateAssetMenu(fileName = "Hint", menuName = "New Hint")]
public class HintSO : ScriptableObject
{
    [Header("Info")]
    public int index;
    [TextArea(2, 10)] public string bodyText;
}