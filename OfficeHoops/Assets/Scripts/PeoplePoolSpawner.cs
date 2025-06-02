using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeoplePoolSpawner : MonoBehaviour
{
    public float TimeBeforeDestroy = 5f;
    public float TimeBetweenSpawns = 2f;

    [System.Serializable]
    public class PrefabData
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnProbability = 1f;

        [HideInInspector]
        public Queue<GameObject> pool = new Queue<GameObject>();
    }

    [Header("Lista de Prefabs con Probabilidades")]
    public List<PrefabData> prefabDataList;

    [Header("Punto de Spawn")]
    public Transform spawnPoint;

    [Header("Tamaño del Pool por Prefab")]
    public int poolSize = 10;

    void Start()
    {
        InicializarPools();
        StartCoroutine(SpawnLoop()); 
    }

    void InicializarPools()
    {
        foreach (var data in prefabDataList)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(data.prefab);
                obj.SetActive(false);
                data.pool.Enqueue(obj);
            }
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnConProbabilidad();
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }

    void SpawnConProbabilidad()
    {
        PrefabData seleccionado = ObtenerPrefabPorProbabilidad();
        if (seleccionado != null && seleccionado.pool.Count > 0)
        {
            GameObject obj = seleccionado.pool.Dequeue();
            obj.transform.position = spawnPoint.position;
            obj.transform.rotation = spawnPoint.rotation;
            obj.SetActive(true);

            StartCoroutine(DesactivarYReiniciar(obj, seleccionado, TimeBeforeDestroy));
        }
    }

    PrefabData ObtenerPrefabPorProbabilidad()
    {
        float total = 0f;
        foreach (var data in prefabDataList)
            total += data.spawnProbability;

        float rand = Random.Range(0, total);
        float acumulado = 0f;

        foreach (var data in prefabDataList)
        {
            acumulado += data.spawnProbability;
            if (rand <= acumulado)
                return data;
        }

        return null;
    }

    IEnumerator DesactivarYReiniciar(GameObject obj, PrefabData data, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        data.pool.Enqueue(obj);
    }
}