using UnityEngine;


public class TestSound : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    // AudioSource를 오브젝트가 들지 않고 매니저가 들고있으면
    // 문제가 생기는(오브젝트가 파괴되면 재생이 중지) 경우를 막을 수 있을거 같다..
    public AudioClip audioClip;
    public AudioClip audioClip2;

    int turn = 0;
    private void OnTriggerEnter(Collider other)
    {
        // 테스트용 하드코딩
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(audioClip);
        //audio.PlayOneShot(audioClip2);
        //// Audio를 플레이하는 도중 gameObject가 파괴되면 재생이 중지된다
        //// audioClip들의 길이를 알아내 그시간 이후에 파괴되게 할 수 있다
        //float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);
        //GameObject.Destroy(gameObject, lifeTime);
        turn++;

        if (turn % 2 == 0)
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm);
        else
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Bgm);
    }
}
