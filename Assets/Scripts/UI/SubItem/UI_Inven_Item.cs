using UnityEngine;
using UnityEngine.UI;

// 종속적이거나 독립적이진 않기 때문에 UI_Base를 상속받아 작성한다?
public class UI_Inven_Item : UI_Base
{
    // 생성할 오브젝트(프리팹?)의 하위 오브젝트가 적을경우 GameObject로 뭉뚱그려 한번에 생성해도 괜찮다.
    // 본래라면 원래 오브젝트인 Text, Image를 각각 생성해서 넣어줘야 한다
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        // Text를 수정하기위해 컴포넌트를 가져오는 과정
        // UI_Base의 Get<T>(index); 메서드를 통해 index에 해당하는 게임오브젝트에서 GetComponent<T(Text)>통해 컴포넌트를 불러오고 .text를 통해 보여줄 텍스트를 작성
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // ItemIcon을 클릭했을때 로그를 출력하도록 설정
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}