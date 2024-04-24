using UnityEngine;


public class TestSound : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }


    public AudioClip audioClip;
    public AudioClip audioClip2;
    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
        audio.PlayOneShot(audioClip2);

        // Audio를 플레이하는 도중 gameObject가 파괴되면 재생이 중지된다
        // audioClip들의 길이를 알아내 그시간 이후에 파괴되게 할 수 있다
        float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);
        GameObject.Destroy(gameObject, lifeTime);
    }
}
