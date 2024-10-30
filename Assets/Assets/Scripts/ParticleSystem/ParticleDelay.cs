using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelay : MonoBehaviour
{
    [SerializeField]private ParticleSystem particleSystems;
    public float startTime = -10f;

    void Start()
    {
        if (particleSystems == null)
            particleSystems = GetComponent<ParticleSystem>();


        particleSystems.Simulate(-startTime, true, true);

        particleSystems.Play();
    }
}
