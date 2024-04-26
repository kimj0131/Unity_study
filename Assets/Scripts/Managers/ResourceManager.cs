using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            // original 이미 들고 있으면 바로 사용
            // 프리팹일 확률이 높다
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject original = Load<GameObject>($"Prefebs/{path}");
        if (original == null)
        {
            // 프리팹을 찾을 수 없는 문제가 있을경우 로그메세지를 출력
            Debug.Log($"Failed to load prefeb : {path}");
            return null;
        }

        // 풀링된 오브젝트가 있으면 해당 오브젝트를 리턴
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        // 오브젝트 이름에 (Clone) 지우기
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        // Object를 붙이지 않으면 이 파일 내의 Instantiate를 재귀적으로 불러오므로 명시적으로 하기 위해 Object를 붙임
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 풀링 오브젝트는 비활성화 한다
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}