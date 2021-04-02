
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject Enemy;
    public GameObject player;
    private float moveSpeed;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    private float minX2;
    private float minY2;
    private float maxX2;
    private float maxY2;

    private float height;
    private float width;

    private List<Color> colorList = new List<Color>();
    private List<float> moveSpeedList = new List<float>();

    private EnemyMovement em;
    private EnemyTag et;
    public Material uncracked;
    public MapDimensions md;

    private void Awake()
    {
        height = md.height;
        width = md.width;
        minX = width / -2-5;
        maxX = width / 2+5;
        minY = height / -2-5;
        maxY = height / 2+5;
        minX2 = width / -2 - 4;
        maxX2 = width / 2 + 4;
        minY2 = height / -2 - 4;
        maxY2 = height / 2 + 4;
    }
    public void Start()
    {
        colorList.Clear();
        moveSpeedList.Clear();
        colorList.Add(Color.yellow);//0
        moveSpeedList.Add(8+(float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0))*0.11f);
        colorList.Add(Color.yellow);//1
        moveSpeedList.Add(11 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.11f);
        colorList.Add(Color.grey);//2
        moveSpeedList.Add(9 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.green);//3
        moveSpeedList.Add(9 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.cyan);//4
        moveSpeedList.Add(8 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.blue);//5
        moveSpeedList.Add(7 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.magenta);//6
        moveSpeedList.Add(6 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.red);//7
        moveSpeedList.Add(4 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(new Color(1,0.5f,0));//8
        moveSpeedList.Add(9 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(new Color(0.54f, 0, 0.6f));//9
        moveSpeedList.Add(8 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.red);//10
        moveSpeedList.Add(10 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);
        colorList.Add(Color.black);//11
        moveSpeedList.Add(12 + (float)System.Math.Sqrt(PlayerPrefs.GetInt("waveNumber", 0)) * 0.1f);

    }

    public void ExecuteSpawn(int EnemyType, int SpawnLocation, int SpawnSpeed, int SpawnDuration)
    {
        StartCoroutine(SpawnCycle(EnemyType, SpawnLocation, SpawnSpeed, SpawnDuration));
    }
    IEnumerator SpawnCycle(int EnemyType, int SpawnLocation, int SpawnAmount, int SpawnDuration)
    {
        float SpawnSpeed = (float)SpawnDuration/SpawnAmount;
        for (int i = 0; i < SpawnAmount; i++)
        {
            SpawnEnemy(EnemyType, SpawnLocation);
            yield return new WaitForSeconds(SpawnSpeed);
        }
        
    }
    private void SpawnEnemy(int EnemyType, int SpawnLocation)
    {
        PlayerPrefs.SetInt("enemies", PlayerPrefs.GetInt("enemies", 0) + 1);
        Vector3 EnemySpawnLocation = new Vector3(maxX, maxY, 0);
        Vector3 EnemyMoveLocation = new Vector3(maxX, maxY, 0);


        int spawnSide;
        switch (SpawnLocation)
        {
            case 0:
                //standard
                spawnSide = Random.Range(1, 5);
                if (spawnSide == 1) EnemySpawnLocation = new Vector3(maxX, Random.Range(minY, maxY), 0);
                if (spawnSide == 2) EnemySpawnLocation = new Vector3(Random.Range(minX, maxX), maxY, 0);
                if (spawnSide == 3) EnemySpawnLocation = new Vector3(minX, Random.Range(minY, maxY), 0);
                if (spawnSide == 4) EnemySpawnLocation = new Vector3(Random.Range(minX, maxX), minY, 0);
                break;
            case 1:
                //corners only
                spawnSide = Random.Range(1, 5);
                if (spawnSide == 1) EnemySpawnLocation = new Vector3(Random.Range(maxX2, maxX), Random.Range(maxY2, maxY), 0);
                if (spawnSide == 2) EnemySpawnLocation = new Vector3(Random.Range(minX, minX2), Random.Range(maxY2, maxY), 0);
                if (spawnSide == 3) EnemySpawnLocation = new Vector3(Random.Range(minX, minX2), Random.Range(minY, minY2), 0);
                if (spawnSide == 4) EnemySpawnLocation = new Vector3(Random.Range(maxX2, maxX), Random.Range(minY, minY2), 0);
                break;
            case 2:
                //top only
                EnemySpawnLocation = new Vector3(Random.Range(minX, maxX), maxY, 0);
                break;
            case 3:
                //bottom only
                EnemySpawnLocation = new Vector3(Random.Range(minX, maxX), minY, 0);
                break;
            case 4:
                //right only
                EnemySpawnLocation = new Vector3(maxX, Random.Range(minY, maxY), 0);
                break;
            case 5:
                //left only
                EnemySpawnLocation = new Vector3(minX, Random.Range(minY, maxY), 0);
                break;
            case 6:
                //top right only
                EnemySpawnLocation = new Vector3(Random.Range(maxX2, maxX), Random.Range(maxY2, maxY), 0);
                break;
            case 7:
                //bottom right only
                EnemySpawnLocation = new Vector3(Random.Range(maxX2, maxX), Random.Range(minY, minY2), 0);
                break;
            case 8:
                //top left only
                EnemySpawnLocation = new Vector3(Random.Range(minX, minX2), Random.Range(maxY2, maxY), 0);
                break;
            case 9:
                //bottom left only
                EnemySpawnLocation = new Vector3(Random.Range(minX, minX2), Random.Range(minY, minY2), 0);
                break;

            default:
                print("INVALID SPAWNLOCATION");
                break;
        }

        StartCoroutine(InstatiateEnemy(Enemy, EnemySpawnLocation, moveSpeedList[EnemyType], colorList[EnemyType], EnemyType));  
        
    }

    public void SpawnMore(int EnemyType, Vector3 SpawnLocation)
    {
        PlayerPrefs.SetInt("enemies", PlayerPrefs.GetInt("enemies", 0) + 1);
        Vector3 EnemySpawnLocation = new Vector3(maxX, maxY, 0);
        

        StartCoroutine(InstatiateEnemy(Enemy, SpawnLocation, moveSpeedList[EnemyType], colorList[EnemyType], EnemyType));
    }

    IEnumerator InstatiateEnemy(GameObject EnemyType, Vector3 SpawnLocation, float moveSpeed, Color color, int tag)
    {
        var Enemy = Instantiate(EnemyType, SpawnLocation, transform.rotation);
        em = Enemy.GetComponent<EnemyMovement>();
        em.moveSpeed = moveSpeed;
        //em.moveLocation = new Vector3()

        et = Enemy.GetComponent<EnemyTag>();
        et.tag = tag;

        MeshRenderer enemyRender = Enemy.GetComponent<MeshRenderer>();
        enemyRender.material = uncracked;
        enemyRender.material.color = color;
        TrailRenderer enemyTrail = Enemy.GetComponent<TrailRenderer>();
        enemyTrail.material.color = color;
        if (tag > 10)
        {
            Enemy.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            enemyTrail.widthMultiplier = 2.0f;
            enemyTrail.time = 0.5f;
        }
        else if(tag > 7)
        {
            Enemy.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            enemyTrail.widthMultiplier = 1.2f;
            enemyTrail.time = 0.5f;
        }
        else if (tag > 6)
       {
            Enemy.transform.localScale = new Vector3(6.8f, 6.8f, 6.8f);
            enemyTrail.widthMultiplier = 6.8f;
            enemyTrail.time = 2f;
        }
        else if (tag > 5)
        {
            Enemy.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
            enemyTrail.widthMultiplier = 2.7f;
            enemyTrail.time = 0.7f;
        }
        else if (tag > 3)
        {
            Enemy.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            enemyTrail.widthMultiplier = 1.2f;
            enemyTrail.time = 0.4f;
        }
        else
        {
            Enemy.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            enemyTrail.widthMultiplier = 0.8f;
            enemyTrail.time = 0.25f;
        }

        while (Enemy.transform.position.x > width / -2 - 10 && Enemy.transform.position.x < width / 2 + 10 && Enemy.transform.position.y > height / -2 - 10 && Enemy.transform.position.y < height / 2 + 10)
        {
            //Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            if (!Enemy.activeSelf)
            {                
                break;
            }
                yield return new WaitForSeconds(Time.deltaTime);
        }

        PlayerPrefs.SetInt("enemies", PlayerPrefs.GetInt("enemies", 0) - 1);
        Destroy(Enemy);

        if (PlayerPrefs.GetInt("point", 0) == 1)
        {
            PlayerPrefs.SetInt("point", 0);
            SpawnCoins(Enemy.transform.position);
        }           

    }

    public void SpawnCoins(Vector3 enemyPosition)
    {

        for (int i = 0; i < Random.Range(1+PlayerPrefs.GetInt("cashDrop", 0), 3+ PlayerPrefs.GetInt("cashDrop", 0)); i++)
        {
            StartCoroutine(spawnCoin(enemyPosition));
        }
    }
    public GameObject Coin;
    float coinSpeed;
    IEnumerator spawnCoin(Vector3 position)
    {
        Vector3 randomPosition = new Vector3(position.x + Random.Range(-1.0f, 1.0f), position.y + Random.Range(-1.0f, 1.0f), 0);
        var coinClone = Instantiate(Coin, randomPosition, Quaternion.identity);

        Vector3 targetPos = new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0);
         float x = 0;
        int onMove = 0;
        float randomSpeed = Random.Range(40.0f, 60.0f);
        while (coinClone.activeSelf)
        {
            x = x + 0.1f;
            coinSpeed = -0.5f * x * x + x + 5;
            if (coinSpeed < 0) coinSpeed = 0;
            if(System.Math.Abs(coinClone.transform.position.x-player.transform.position.x)<50 && System.Math.Abs(coinClone.transform.position.y - player.transform.position.y) < 50||onMove == 1)
            {
                onMove = 1;
                coinClone.transform.position = Vector3.MoveTowards(coinClone.transform.position, player.transform.position, randomSpeed * Time.deltaTime+ coinSpeed * Time.deltaTime);
            }
            else
            {
                coinClone.transform.position = Vector3.MoveTowards(coinClone.transform.position, targetPos, coinSpeed * Time.deltaTime);
            }
            if (!coinClone.activeSelf)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }


        Destroy(coinClone);
    }

}
