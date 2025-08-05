using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private Sprite[] saveSprites; 
    [SerializeField] private Image saveImage;

    [SerializeField] private float frameDelay = 0.1f;

    private Coroutine animCoroutine;

    public void SaveAnimation()
    {
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);
        animCoroutine = StartCoroutine(SaveAnimation_Coroutine());
    }
    public void StopSaveAnimation()
    {
        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine);
            animCoroutine = null;
        }
            
        if (saveSprites.Length > 0)
            saveImage.sprite = saveSprites[0];
    }

    private IEnumerator SaveAnimation_Coroutine()
    {
        int idx = 0;
        while (true)
        {
            saveImage.sprite = saveSprites[idx];
            idx = (idx + 1) % saveSprites.Length;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}