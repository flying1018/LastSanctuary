using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    private bool _isInteracted;
    public void Interact()
    {
        DebugHelper.Log($"{this.name}의 Interact() 작동됨");
        
        if (_isInteracted) { return; }
        
        SaveManager.Instance.SetSavePoint(this.transform.position);
        // ItemManager.Instance.PlayerRecovery();
        
        StartCoroutine(SaveAnimation());
    }

    private IEnumerator SaveAnimation()
    {
        _isInteracted = true;
        //SoundManager.Instance.PlaySFX(StringNameSpace.SoundAddress.SavePointSFX);

        yield return new WaitForSeconds(2f);

        _isInteracted = false;
    }
}