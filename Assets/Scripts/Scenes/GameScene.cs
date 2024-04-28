using System.Collections.Generic;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Stat> dict = Managers.Data.StatDict;
    }

    public override void Clear()
    {

    }

}
