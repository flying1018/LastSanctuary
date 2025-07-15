using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


//<summary>
//어드레서블 이용한 리소스 로더
//</summary>
public class ResourceLoader : MonoBehaviour
{
    /// <summary>
    /// 어드레서블 주소 기반 로더
    /// </summary>
    /// <typeparam name="T">제네릭 타입 파라미터</typeparam>
    /// <param name="key">주소값 넣을 파라미터, 하드코딩 지양하고 StringNameSpace 사용권장</param>
    /// <returns>T타입 에셋을 반환</returns>
    public static async Task<T> LoadAssetAddress<T>(string key) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result; // 로딩 성공시 반환
        }
        else
        {
            DebugHelper.LogError($"주소 로딩 실패 : \"{key}\"");
            return null;
        }
    }

    /// <summary>
    /// 어드레서블 라벨 기반 로더
    /// </summary>
    /// <typeparam name="T">제네릭 타입 파라미터</typeparam>
    /// <param name="label">해당 라벨이 붙은걸 호출 </param>
    /// <returns>T타입 라벨들을 일괄 반환</returns>
    public static async Task<List<T>> LoadAssetsLabel<T>(string label) where T : Object
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T> // 라벨 붙은 에셋을 로딩
        (
            label,
            null
        );

        await handle.Task; // 끝나길 기다리는 곳

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return new List<T>(handle.Result); // 로딩 성공시 반환
        }
        else
        {
            DebugHelper.LogError($"라벨 로딩 실패 : \'{label}\'"); // 로딩 실패시 오류메시지
            return null;
        }
    }

    /// <summary>
    /// 어드레서블은 수동으로 로드 해제해야 메모리 누수가 없음
    /// </summary>
    /// <param name="obj"> 다쓴건 obj로 파라미터 넣어서 호출</param>
    public static void Release(object obj)
    {
        Addressables.Release(obj);  // 사용한 어드레서블 메모리 해제
    }

    /// <summary>
    /// 다른곳에서 어드레서블 사용할때
    /// 참조할 테스트 코드
    /// </summary>
    public async void TestCode()
    {
        // 테스트 코드용 주소입력, 실제로는 주소를 기록한 StringNameSpace의 변수가 들어가야함
        string testKey = StringNameSpace.test;

        // 주소기반 로딩으로 testKey가 불러올 에셋의 주소 파라미터
        GameObject testObj = await LoadAssetAddress<GameObject>(testKey); // await를 통해 로딩이 다 되어야 다음코드로 넘어감

        Instantiate(testObj, Vector3.zero, Quaternion.identity); // 불러온 오브젝트를 용도에 맞게 활용
        DebugHelper.Log("테스트 프리팹 생성 완료");

    }
}