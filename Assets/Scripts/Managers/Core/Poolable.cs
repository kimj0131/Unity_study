using UnityEngine;

public class Poolable : MonoBehaviour
{
    // 메모리 풀링을 할 오브젝트 구분할 컴포넌트 스크립트

    // 사용중인지 확인할 수 있는 변수만 추가해준다
    public bool IsUsing;
}
