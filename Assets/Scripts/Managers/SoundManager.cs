using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // 필요한 기능을 예상
    // ex
    // MP3 Player   -> AudioSource
    // MP3 음원?    -> AudioClip 
    // 사용자(귀)   -> AudioListener

    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    // AudioClip을 매번 리소스매니저를 이용해 긁어오는 것이 부하가 걸릴 수 있을것
    // AudioClip들을 저장하기 위한 Dictionary 생성해 매니저를 이용하지 않고 불러오면 개선할 수 있을 것이다
    // ** Managers에서 DontDestroyOnLoad 로 지정이 되있어 이 Dictionary가 데이터가 쌓이기만해 나중에 메모리에 문제가 생길 수 있다
    // ** Clear를 통해 데이터를 한번씩 정리해야 메모리 문제가 안생길 것
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

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

    // 캐싱한 데이터(_audioSources, _audioClips)를 날려 메모리 문제를 해결한다
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        // 경로에 Sounds/ 를 적지않는 실수를 체크
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Define.Sound.Effect : 단발성
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
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

    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false)
        {
            // 없을경우 생성
            audioClip = Managers.Resource.Load<AudioClip>(path);
            // Dictionary에 추가
            _audioClips.Add(path, audioClip);
        }

        return audioClip;
    }

}
