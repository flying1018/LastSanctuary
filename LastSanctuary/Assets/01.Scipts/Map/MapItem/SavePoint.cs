using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public int index;
    private bool isInteracted;
    public void Interact()
    {
        DebugHelper.Log($"{this.name}의 Interact() 작동됨");

        if (isInteracted) { return; }

        SaveManager.Instance.SetSavePoint(this.transform.position);
        ItemManager.Instance.playerCondition.PlayerRecovery(); // 회복

        StartCoroutine(SaveAnimation());
    }

    private IEnumerator SaveAnimation()
    {
        isInteracted = true;
        //SoundManager.Instance.PlaySFX(StringNameSpace.SoundAddress.SavePointSFX);

        yield return new WaitForSeconds(2f);

        isInteracted = false;
    }
}