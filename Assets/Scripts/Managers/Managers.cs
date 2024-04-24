using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;   // static 으로 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } }    // 유일한 매니저를 갖고온다

    // managers에 기능별매니저를 연결
    // 유니티 함수를 따로 래핑하는 이유 :
    // 나중에 다르게 처리하거나 혹은 코드의 변경이 필요할 때 해당 manager의 코드만 변경하면 일괄로 처리되어 작업의 효율이 올라가기 때문

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            // 만약 @Managers 라는 이름의 게임오브젝트가 없을경우 생성
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            // 오브젝트가 삭제되지않게 변경
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}