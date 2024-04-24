using UnityEngine;

public class SoundManager
{
    // 필요한 기능을 예상
    // ex
    // MP3 Player   -> AudioSource
    // MP3 음원?    -> AudioClip 
    // 사용자(귀)   -> AudioListener

    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundName.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundName[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            // 배경음악은 루프를 활성화 한다
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Play(Define.Sound type, string path, float pitch = 1.0f)
    {
        // 경로에 Sounds/ 를 적지않는 실수를 체크
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! {path}");
                return;
            }

            // TODO

        }
        else // Define.Sound.Effect : 단발성
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

}
