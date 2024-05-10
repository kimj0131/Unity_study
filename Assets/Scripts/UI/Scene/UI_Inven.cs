using UnityEngine;

public class UI_Inven : UI_Scene

{
    enum GameObjects
    {
        GridPanel,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        // inven에서 자손들(여기서는 아이템)을 우선 제거한다
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // 원래는 실제 인벤토리 정보를 참고해서 만들어줘야한다
        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집판검{i}번");
        }
    }
}