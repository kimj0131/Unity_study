using System.Collections;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        // 코루틴을 실행(유니티가 지원함)
        co = StartCoroutine("CoExplodeAfterSeconds", 4.0f);
        StartCoroutine(CoStopExplode(2f));
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }

    }

    // 코루틴을 활용해 특정 시간후에 다음 로직을실행 (4초후에 폭팔한다)
    IEnumerator CoExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!");
        co = null;
    }

    public override void Clear()
    {

    }

}
