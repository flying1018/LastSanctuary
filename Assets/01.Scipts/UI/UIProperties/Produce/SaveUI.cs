using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private Sprite[] saveSprites;
    [SerializeField] private Image saveImage;

    [SerializeField] private float frameDelay = 0.4f;

    private Coroutine animCoroutine;

    public void SaveAnima()
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
        Color color = saveImage.color;
        color.a = 1f;

        int repeatCount = 2;
        for (int loop = 0; loop < repeatCount; loop++)
        {
            for (int idx = 0; idx < saveSprites.Length; idx++)
            {
                saveImage.sprite = saveSprites[idx];
                yield return new WaitForSeconds(frameDelay);
            }
        }

        yield return new WaitForSeconds(0.5f);
        color.a = 0f;
        gameObject.SetActive(false);
    }
}