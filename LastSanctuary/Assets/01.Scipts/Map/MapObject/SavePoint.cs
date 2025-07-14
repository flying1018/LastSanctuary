using System.Collections;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public int index;
    private bool _isInteracted;
    public void Interact()
    {
        DebugHelper.Log($"{this.name}의 Interact() 작동됨");

        if (_isInteracted) { return; }

        SaveManager.Instance.SetSavePoint(this.transform.position);
        ItemManager.Instance.playerCondition.PlayerRecovery(); // 회복
        MapManager.Instance.RespawnEnemies();
        //MapManager.Instance.RespawnItems();

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