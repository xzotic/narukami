using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    public float fallInterval;
    public float fallingTime;
    public ParticleSystem ParticleSystem;
    [SerializeField] private bool isFalling;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFalling)
        {
            fallingTime += Time.deltaTime;
            if (fallingTime >= fallInterval)
            {
                //ParticleSystem.Clear();
                ParticleSystem.Play();
                fallingTime = 0;
                isFalling = true;
            }
        } else
        {
            fallingTime += Time.deltaTime;
            if (fallingTime >= 3)
            {
                ParticleSystem.Stop();
                //ParticleSystem.Clear();
                fallingTime = 0;
                isFalling = false;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("a");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Checkpoint();
        }
    }
}
