using System.Collections;
using TMPro;
using UnityEngine;

public class ItemTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float moveUpDistance = 60f;
    [SerializeField] private float fadeDuration = 1.0f;

    private Vector3 startPos;

    public void Show(string message, Vector3 worldPos)
    {
        text.text = message;
        startPos = Camera.main.WorldToScreenPoint(worldPos);

        transform.position = startPos;
        gameObject.SetActive(true);

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float timer = 0;
        Color color = text.color;
        Vector3 endPos = startPos + Vector3.up * moveUpDistance;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            color.a = Mathf.Lerp(1, 0, t);
            text.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject); // 자동 삭제
    }
}
