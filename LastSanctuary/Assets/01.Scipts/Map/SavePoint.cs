using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private bool _inRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = true;
            DebugHelper.Log("세이브 포인트에 접근함");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = false;
            DebugHelper.Log("세이브 포인트에서 벗어남");
        }
    }

    public bool IsNearSave()
    {
        return _inRange;
    }
}