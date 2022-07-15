using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FireObject
{
    public Transform Transform;
    public float Lifetime;
    public Rigidbody2D Rigidbody2D;
    public FireObject(Transform Transform, float LifeTime)
    {
        this.Transform = Transform;
        this.Lifetime = LifeTime;
        Rigidbody2D = this.Transform.GetComponent<Rigidbody2D>();
        this.Transform.gameObject.SetActive(false);
    }
}

public class FirePod : MonoBehaviour
{

    [SerializeField] private Transform ProjectilePrefab;
    [SerializeField] private float StartSpeed = 30f;
    [SerializeField] private float ProjectileLifetime = 5f;
    [SerializeField] private int PrefabCacheLimit = 30;
    private GameObject prefabObject;
    private FireObject[] prefabCache;
    private int currentIndex = 0;
    private void Start()
    {
        List<FireObject> _prefabCahe = new List<FireObject>();
        for (int i = 0; i < PrefabCacheLimit; i++)
        {
            _prefabCahe.Add(new FireObject(Instantiate(ProjectilePrefab, this.transform.position, Quaternion.identity, this.transform), 0f));
        }
        prefabCache = _prefabCahe.ToArray();
    }
    public void Fire()
    {
        prefabCache[currentIndex].Transform.gameObject.SetActive(true);
        prefabCache[currentIndex].Transform.position = this.transform.position;
        prefabCache[currentIndex].Rigidbody2D.velocity = Vector2.right * StartSpeed;
        prefabCache[currentIndex].Lifetime = ProjectileLifetime;
        if(currentIndex == PrefabCacheLimit - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }

    public void Update()
    {
        for (int i = 0; i < PrefabCacheLimit; i++)
        {
            float _lifeTime = prefabCache[i].Lifetime;
            if (_lifeTime > 0)
            {
                prefabCache[i].Lifetime -= Time.deltaTime;
            }
            if(_lifeTime < 0)
            {
                prefabCache[i].Rigidbody2D.velocity = Vector2.zero;
                prefabCache[i].Lifetime = 0;
                prefabCache[i].Transform.gameObject.SetActive(false);
            }
        }
    }

}
