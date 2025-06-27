using UnityEditor;
using UnityEngine;

public static class DebugHelper
{
    public static void Log(string text)
    {
#if UNITY_EDITOR
        Debug.Log(text);
#endif
    }

    public static void LogWarning(string text)
    {
#if UNITY_EDITOR
        Debug.LogWarning(text);
#endif
    }

    public static void LogError(string text)
    {
#if UNITY_EDITOR
        Debug.LogError(text);
#endif
    }

    public static void ShowRay(Vector3 start, Vector3 direction, Color color, float duration = 1f)
    {
#if UNITY_EDITOR
        Debug.DrawRay(start, direction, color, duration);
#endif
    }
}

#if UNITY_EDITOR

// 디버그 툴을 위한 클래스
public class DebugWindow : EditorWindow
{
    // 디버그할 기능의 필드 변수 선언공간
    private string test; // 예시용로 사용할 테스트 변수 선언
    private AudioClip bgm;
    private GameObject go;
    private int testDamage;


    [MenuItem("Window/My Debug Tool")]
    public static void ShowWindow()
    {
        GetWindow<DebugWindow>("Debug Tool");
    }

    private void OnGUI()
    {
        // 예시용 테스트코드
        EditorGUILayout.LabelField("디버그툴 예시"); // 디버그툴에 뜰 헤더 이름
        test = EditorGUILayout.TextField("테스트용 문자열 출력", test); // 디버그 툴에서 수정 가능한 필드값
        if (GUILayout.Button("출력")) // 디버그 툴에 뜰 버튼 이름
        {
            // 버튼 누를시 실제 작동할 기능
            // 코루틴, 메서드나 여러 기능을 넣을시 해당 기능이 작동됨
            DebugHelper.Log(test);
        }

        EditorGUILayout.LabelField("바꿀 BGM");
        bgm = (AudioClip)EditorGUILayout.ObjectField("BGM을 넣어주세요", bgm, typeof(AudioClip), true);
        if (GUILayout.Button("Bgm바꾸기")) // 디버그 툴에 뜰 버튼 이름
        {
            
        }
        
        EditorGUILayout.LabelField("가드 테스트");
        go = (GameObject)EditorGUILayout.ObjectField("playerObject", go, typeof(GameObject), true); 
        testDamage = EditorGUILayout.IntField("데미지", testDamage);
        if (GUILayout.Button("데미지 테스트"))
        {
           
        }
    }
}
#endif