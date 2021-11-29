using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnManager : MonoBehaviour
{
    
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;
    public ObjectManager objectManager;

    void Awake()
    {
        enemyObjs = new string[] { "EnemyL", "EnemyM", "EnemyS" };
        spawnList = new List<Spawn>();
        ReadSpawnFile();
    }

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void ReadSpawnFile()
    {
        //변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 리스폰 파일 읽기
        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
          //  Debug.Log(line);

            if (line == null)
                break;

            // 리스폰 데이터 생성
            Spawn spawnData = new Spawn
            {
                delay = float.Parse(line.Split(',')[0]),
                type = line.Split(',')[1],
                point = int.Parse(line.Split(',')[2])
            };
            spawnList.Add(spawnData);
        }

        //텍스트 파일 닫기
        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }


    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        
        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();        
            curSpawnDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;       

        //리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note_A"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
