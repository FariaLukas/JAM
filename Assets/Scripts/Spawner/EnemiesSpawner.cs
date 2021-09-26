using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Setups")]
    [InfoBox("Needs to be in order")]
    public List<EnemySpawnSetup> melees;
    public List<EnemySpawnSetup> rangeds;

    [Header("Chances")]
    [Range(0, 9)]
    [InfoBox("Chance to Spawn Melee")]
    public int firstChance = 7;
    [Range(0, 9)]
    [InfoBox("Chance to Spawn Melee 1")]
    public int meleeChance = 7;
    [Range(0, 9)]
    [InfoBox("Chance to Spawn Ranged 1")]
    public int rangedChance = 8;

    [Header("SpawnArea")]
    public List<RandomDuble> randoms;

    public float spawnRatio = 2.3f;
    public bool startEnabled;
    public bool stopped;

    private void Start()
    {
        PoolSetup(melees);
        PoolSetup(rangeds);

        GameManager.Instance.OnPlayerDie += DisableRespawn;
        
        if (startEnabled)
            EnableSpawn();

    }

    [Button]
    public void EnableSpawn()
    {
        stopped = false;
        StartCoroutine(Repeat());
    }

    public void DisableRespawn()
    {
        stopped = true;
    }

    IEnumerator Repeat()
    {
        while (!stopped)
        {
            yield return new WaitForSeconds(spawnRatio);
            if (stopped) yield break;

            Spawn();
        }
    }

    private void PoolSetup(List<EnemySpawnSetup> setups)
    {
        foreach (var s in setups)
        {
            GameObject go = new GameObject();
            go.name = s.prefab.name + " - PoolManager";
            s.pool = go.AddComponent<Pool>();
            s.pool.poolSize = s.poolSize;
            s.pool.prefab = s.prefab;
            s.pool.WarmPool();

        }
    }

    [Button]
    public void Spawn()
    {
        int firstCheck = GetRandom();


        if (firstCheck < firstChance)//Melee
        {
            int secondCheck = GetRandom();
            int id = secondCheck < meleeChance ? 0 : 1;

            //            print(secondCheck + ": MELEE: " + id);
            SetSpawn(id, melees);

        }  //Ranged
        else
        {
            int secondCheck = GetRandom();
            int id = secondCheck < rangedChance ? 0 : 1;
            // print(secondCheck + ": RANGED: " + id);
            SetSpawn(id, rangeds);

        }

    }

    public int GetRandom()
    {
        return Random.Range(0, 10);
    }

    [Button]
    public float GetRandom(Vector2 value)
    {
        return Random.Range(value.x, value.y);
    }

    public int GetRandomInt(Vector2 value)
    {
        return Random.Range((int)value.x, (int)value.y);
    }

    public void SetSpawn(int id, List<EnemySpawnSetup> setups)
    {
        GameObject g = setups[id].pool.GetPooledGameObject();

        int index = GetRandomInt(new Vector2(0, randoms.Count));

        float i = GetRandom(randoms[index].minMaxWdth);
        float a = GetRandom(randoms[index].minMaxHeight);

        Vector2 spawnPos = new Vector2(i, a);

        g.transform.position = spawnPos;

        g.SetActive(true);
    }


}

[System.Serializable]
public class EnemySpawnSetup
{
    public GameObject prefab;
    public int poolSize;
    public Pool pool;
}

[System.Serializable]
public class RandomDuble
{
    public Vector2 minMaxWdth;
    public Vector2 minMaxHeight;

}