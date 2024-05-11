using UnityEngine;
using UnityEngine.SceneManagement;

// 별도의 씬매니저를 생성해서 관리하기하고 Unity에서 제공하는 SceneManager와 이름충돌을 피하기위해 Ex를 붙인다
public class SceneManagerEx
{
    // Scene의 기본이 되는 BaseScene을 관리하기위한 래핑

    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        // 매니저들의 Clear() 메서드를 Managers의 Clear() 메서드에 모아두었으므로 불러와서 불필요한 메모리를 정리한 다음 로드할 준비를 한다
        Managers.Clear();
        // Clear후에 다음 Scene을 로드한다
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        // 현재 사용하던 Scene을 날리기위해 호출
        CurrentScene.Clear();
    }
}
