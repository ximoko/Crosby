﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstancer : MonoBehaviour {

    public GameObject[] particleSystems;
    public List<ParticleAndQuantity> particleSystemsToPool;

    Dictionary<string, GameObject> particlePrefabs = new Dictionary<string, GameObject>();
    Dictionary<string, Queue<GameObject>> particlePrefabsToPool = new Dictionary<string, Queue<GameObject>>();

    //public Dictionary<string, Queue<GameObject>> particlePoolDictionary;

    private void Start()
    {
        foreach (GameObject particle in particleSystems)
        {
            particlePrefabs.Add(particle.name, particle);
        }

        foreach (ParticleAndQuantity particle in particleSystemsToPool)
        {
            Queue<GameObject> particlePool = new Queue<GameObject>();

            for (int i = 0; i < particle.quantity; i++)
            {
                GameObject particleInstance = Instantiate(particle.prefab);
                particleInstance.GetComponent<ParticlePrefabBehaviour>().particleTag = particle.prefab.name;
                particleInstance.SetActive(false);
                particlePool.Enqueue(particleInstance);
            }

            particlePrefabsToPool.Add(particle.prefab.name, particlePool);
        }
    }

    public void InstanciateParticleSystem (string particleName, Vector3 particlePosition, Quaternion particleRotation)
    {
        //int particlePrefabIndex = particlePrefabs.FindIndex(particle => particle.particleName == particleName);
        GameObject particleInstance = Instantiate(particlePrefabs[particleName], particlePosition, particleRotation, this.transform); //Se le asigna un ParentTranfsorm para que se instancie en la escena correcta
        particleInstance.transform.parent = null;
    }

    //Overload to instanciate as a child of a certain transform
    public void InstanciateParticleSystem(string particleName, Transform parentTransform, Vector3 particleLocalPosition, Quaternion particleLocalRotation)
    {
        //int particlePrefabIndex = particleSystems.FindIndex(particle => particle.particleName == particleName);
        GameObject particleInstance = Instantiate(particlePrefabs[particleName].gameObject, parentTransform.position, parentTransform.rotation, parentTransform);
        particleInstance.transform.localPosition = particleLocalPosition;
        particleInstance.transform.localRotation = particleLocalRotation;
    }

    public void PoolParticleSystem (string particleName, Vector3 particlePosition, Quaternion particleRotation)
    {
        GameObject particleToPool = particlePrefabsToPool[particleName].Dequeue();
        particleToPool.SetActive(true);
        particleToPool.transform.position = particlePosition;
        particleToPool.transform.rotation = particleRotation;
    }

    public void UnpoolParticleSystem (string particleTag, GameObject particleObject)
    {
        particlePrefabsToPool[particleTag].Enqueue(particleObject);
        particleObject.SetActive(false);
    }
}

[System.Serializable]
public class ParticleAndQuantity
{
    public GameObject prefab;
    public int quantity;

    public ParticleAndQuantity (GameObject particlePrefab, int particleQuantity)
    {
        prefab = particlePrefab;
        quantity = particleQuantity;
    }
}