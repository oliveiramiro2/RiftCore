using UnityEngine;

public class PooledObject : MonoBehaviour
{
  private ObjectPool pool;

  public void SetPool(ObjectPool pool)
  {
    this.pool = pool;
  }

  public void ReturnToPool()
  {
    if (pool != null)
      pool.ReturnObject(gameObject);
    else
      Destroy(gameObject); // fallback se não houver pool
  }
}
