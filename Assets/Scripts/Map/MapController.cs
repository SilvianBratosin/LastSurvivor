using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector3 playerLastPosition;


    //Optimization
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDistance;
    float opDistance;
    float opCD;
    public float opCDTime;


    void Start()
    {
        playerLastPosition= player.transform.position;
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        if(directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }
        if(directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if(directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }
        if(directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
    }

    void CheckAndSpawnChunk(string direction)
    {
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //Horizontal
            if (direction.y > 0.5f)
            {
                //Also up
                return direction.x > 0 ? "RightUp" : "LeftUp";
            }
            else if(direction.y < -0.5f)
            {
                //Also down
                return direction.x > 0 ? "RightDown" : "LeftDown";
            }
            else
            {
                //Only horizontal
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            //Vertical
            if(direction.x > 0.5f)
            {
                //Also up
                return direction.y > 0 ? "RightUp" : "RightDown";
            }
            else if(direction.x < -0.5f)
            {   
                //Also down
                return direction.y > 0 ? "LeftUp" : "LeftDown";
            }
            else
            {
                //Only vertical
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        opCD -= Time.deltaTime;

        if (opCD <= 0)
        {
            opCD = opCDTime;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDistance > maxOpDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
