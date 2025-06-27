using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


//<summary>
// 게임 시작 시 각 매니저들을 의도하는 순서대로 초기화
//</summary>
public class GameInitializer : MonoBehaviour
{
    
    private static GameInitializer instance;
    private async void Awake()
    {
        // 중복 초기화 방지
        if (instance != null)
        {
            Destroy(gameObject); // 중복이면 즉시 파괴
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지

        // 각 매니저 Init()으로 호출하여 인스턴스 생성 및 초기세팅
        GameManager.Instance.Init();
        //await SoundManager.Instance.Init();
        await GameDB.Init();

        InitEventSystem();
    }
    
    private void InitEventSystem()
    {
        // 이미 존재하는 EventSystem 있는지 확인
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.Log("EventSystem이 자동 생성.");

            GameObject go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
            DontDestroyOnLoad(go);
        }
        else
        {
            Debug.Log("EventSystem이 이미 존재.");
        }
    }
}
