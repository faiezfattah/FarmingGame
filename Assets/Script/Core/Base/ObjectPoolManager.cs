using UnityEngine;
using UnityEngine.Pool;

namespace Script.Core.Base {
public class ObjectPoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxCapacity = 100;
    [SerializeField] private bool collectionChecks = true;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        InitializePool();
    }

    /// <summary>
    /// Initialize the object pool
    /// </summary>
    private void InitializePool()
    {
        // Create the object pool
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: collectionChecks,
            defaultCapacity: defaultCapacity,
            maxSize: maxCapacity
        );
    }

    /// <summary>
    /// Create a new instance of the object
    /// </summary>
    protected virtual T CreateObject()
    {
        var newObject = Instantiate(prefab, transform);
        newObject.gameObject.name = $"{prefab.name}_pooled";
        
        return newObject;
    }

    /// <summary>
    /// Called when an object is taken from the pool
    /// </summary>
    protected virtual void OnGetFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when an object is returned to the pool
    /// </summary>
    protected virtual void OnReleaseToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when an object is destroyed (when the pool is full)
    /// </summary>
    protected virtual void OnDestroyPoolObject(T obj)
    {
        Destroy(obj.gameObject);
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    public T Get()
    {
        return _pool.Get();
    }

    /// <summary>
    /// Get an object from the pool and set its position and rotation
    /// </summary>
    public T Get(Vector3 position, Quaternion rotation)
    {
        var obj = _pool.Get();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    /// <summary>
    /// Release an object back to the pool
    /// </summary>
    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    /// <summary>
    /// Clear the pool
    /// </summary>
    public void ClearPool()
    {
        _pool.Clear();
    }

    /// <summary>
    /// Get the count of active objects
    /// </summary>
    public int CountActive => _pool.CountActive;

    /// <summary>
    /// Get the count of inactive objects
    /// </summary>
    public int CountInactive => _pool.CountInactive;

    /// <summary>
    /// Get the total count of objects (active + inactive)
    /// </summary>
    public int CountAll => _pool.CountAll;
}
}