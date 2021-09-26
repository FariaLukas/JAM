using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public List<GameObject> melees;
    public List<GameObject> rangeds;

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

    private void Awake()
    {

        InvokeRepeating(nameof(Spawn), spawnRatio, spawnRatio);
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

    public void SetSpawn(int id, List<GameObject> setups)
    {
        GameObject g = (GameObject)Instantiate(setups[id], transform.position, Quaternion.identity);
        g.SetActive(false);
        int index = GetRandomInt(new Vector2(0, randoms.Count));

        float i = GetRandom(randoms[index].minMaxWdth);
        float a = GetRandom(randoms[index].minMaxHeight);

        Vector2 spawnPos = new Vector2(i, a);

        g.transform.position = spawnPos;
        g.SetActive(true);
    }

}

