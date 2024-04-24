using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1. 나 혹은 상대한테 RigidBody 있어야 한다 (IsKinematic : off)
    // 2. 나한테 Collider가 있어야 한다 (IsTrigger : off)
    // 3. 상대한테 Collider가 있어야 한다 (IsTrigger : off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision. @ {collision.gameObject.name}");
    }

    // 1. 둘 다 Collider가 있어야 한다
    // 2. 둘 중 하나는 IsTrigger : On
    // 3. 둘 중 하나는 RigidBody가 있어야 한다
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger. @{other.gameObject.name}");
    }

    void Start()
    {
    }

    void Update()
    {
        // local <-> world <-> Viewport <-> Screen (화면)

        // Screen 좌표계 -> 픽셀로 표시한다
        //Debug.Log(Input.mousePosition);

        // Viewport 좌표 -> 비율로 표시한다
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        // 마우스 클릭시
        if (Input.GetMouseButtonDown(0))
        {
            // 화면에서 World 좌표
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 디버그화면에 레이를 그려줌
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            // 레이어 이름으로 레이어를 찾아옴
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            //int mask = (1 << 8) | (1 << 9);

            // 레이캐스트에 적중하면 해당 오브젝트의 이름을 출력
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @{hit.collider.name}");
                Debug.Log($"Raycast Camera @{hit.collider.gameObject.tag}");
            }
        }

        // 상세히 풀어서 구현한 방법
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // 화면에서 World 좌표 구하기
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    // 카메라 위치에서 클리핑 평면까지의 방향벡터
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    // 방향은 유지하고 크기를 1로 조정 (단위 벡터화)
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    // 레이캐스트에 적중하면 해당 오브젝트의 이름을 출력
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @{hit.collider.name}");
        //    }
        //}
    }
}