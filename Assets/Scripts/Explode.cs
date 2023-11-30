using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] float radius = 5f;
    [SerializeField] float force = 500f;
    [SerializeField] bool explodeOnCollision = false;
    [SerializeField] float delayTimer;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float effectDisplayTime;

    private void Awake()
    {
        delayTimer = 0;
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delay)
        {
            OnExplosion();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (explodeOnCollision && collision.gameObject.layer != 6 && delayTimer >= 0.1f)
        {
            OnExplosion();
            Destroy(gameObject);
        }
    }

    void OnExplosion()
    {
        HandleEffects();
        HandleExplode();
    }

    void HandleEffects()
    {
        GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, effectDisplayTime);
    }

    private void HandleExplode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
