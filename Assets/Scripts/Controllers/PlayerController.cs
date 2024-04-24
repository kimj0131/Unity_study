using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    // state를 idle로 초기화
    PlayerState _state = PlayerState.Idle;

    void Start()
    {
        // 마우스 버튼 입력 이벤트 추가
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        // TEMP
        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    void UpdateDie()
    {
        // 아무것도 못함
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        // vector간의 빼기는 0이 나오지 않는 경우가 많다
        // vector.magnitude(벡터의 길이(크기)) 가 아주 작을경우 지정한(클릭한) 위치에 도달했다는 뜻으로 해석하게 한다
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            // 이동하는 값이 남은거리보다 작아야 한다는 것을 보장해줘야함
            // Mathf.Clamp : 최소 / 최대값을 설정하여 float 값이 범위 이외의 값을 넘지 않도록 해준다
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            // 바라보는 방향을 부드럽게 회전하도록
            //transform.LookAt(_destPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();

        // 현재 게임 상태에대한 정보를 넘겨준다
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        // 애니메이션
        Animator anim = GetComponent<Animator>();

        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;

            case PlayerState.Moving:
                UpdateMoving();
                break;

            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;

            // y좌표의 값을 고정
            //_destPos.y = 0;
            _destPos.y = GameObject.Find("Plane").transform.position.y;

            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @{hit.collider.gameObject.tag}");
        }
    }
}