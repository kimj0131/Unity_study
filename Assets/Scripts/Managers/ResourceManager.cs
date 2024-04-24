using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefebs/{path}");
        if (prefab == null)
        {
            // 프리팹을 찾을 수 없는 문제가 있을경우 로그메세지를 출력
            Debug.Log($"Failed to load prefeb : {path}");
            return null;
        }

        // 오브젝트 이름에 (Clone) 지우기
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        // Object를 붙이지 않으면 이 파일 내의 Instantiate를 재귀적으로 불러오므로 명시적으로 하기 위해 Object를 붙임
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}