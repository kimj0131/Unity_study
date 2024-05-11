using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // 리소스 매니저를 보조하는 역할을 할것이다

    #region Pool
    // 풀링할 오브젝트별로 그룹화해야 구분이 쉬울것
    // 풀링할 오브젝트 자체가 풀이 되어 하위에 오브젝트들이 쌓이게 한다
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        // count는 풀링할 개수
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());

        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            //DontDestroyOnLoad 에서 제대로 하위에 들어있지 않는 문제가 발생

            // DontDestroyOnLoad 해제 용도로 사용
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }

    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            // 하이어라키에 해당 이름으로 된 오브젝트를 생성 및 파괴불가 속성 지정
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        // 최상위 연결
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        // 유니티 내에서 직접연결했거나 한 경우 문제가 있을 수 있으므로 파괴시킨다
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        // 일단 체크 먼저 해야한다
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        // Key는 original오브젝트의 이름으로 Pop한다
        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        // 있는지 체크먼저하고
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
