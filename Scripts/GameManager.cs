using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private Bounds worldBounds;

    private GameObject heroPrefab;

    private GameObject potatoPrefab;

    private GameObject spikeBallPrefab;

    private GameObject verticalWallPrefab;

    private GameObject horizontalWallPrefab;

    public int TotalPotatoes { get; set; } = 5;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        heroPrefab = Resources.Load<GameObject>("Prefab/Player");

        potatoPrefab = Resources.Load<GameObject>("Prefab/Potato");

        spikeBallPrefab = Resources.Load<GameObject>("Prefab/SpikeBall");

        verticalWallPrefab = Resources.Load<GameObject>("Prefab/VerticalWall");
        horizontalWallPrefab = Resources.Load<GameObject>("Prefab/HorizontalWall");

        worldBounds = new Bounds(Vector3.zero, Vector3.one);

        UpdateWorldBounds();

        SpawnSpikeBalls();

        //AddWalls();

        /*
        for (int i = 0; i < TotalPotatoes; i++)
        {
            SpawnPotato(i);
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateWorldBounds()
    {
        var camera = Camera.main;

        if (camera == null)
        {
            return;
        }

        float maxY = camera.orthographicSize;
        float maxX = camera.orthographicSize * camera.aspect;
        float sizeX = 2 * maxX;
        float sizeY = 2 * maxY;
        float sizeZ = Mathf.Abs(camera.farClipPlane - camera.nearClipPlane);

        Vector3 cameraPosition = camera.transform.position;
        cameraPosition.z = 0.0f;
        worldBounds.center = cameraPosition;
        worldBounds.size = new Vector3(sizeX, sizeY, sizeZ);


        Debug.Log($"Center={worldBounds.center} Size={worldBounds.size}");
    }

    public bool IsObjectWithinWorldBounds(Bounds objectBounds)
    {
        return worldBounds.Intersects(objectBounds);
    }

    public void SpawnPotato(int sector)
    {
        GameObject potato = GameObject.Instantiate(potatoPrefab);

        var size = worldBounds.size.x / 2;

        var value = Random.Range(-size * 0.8f, -size * 0.8f + size * 0.8f / TotalPotatoes);

        var randX = sector * 2 * size * 0.9f / TotalPotatoes + value;

        var randY = worldBounds.size.y * Random.Range(-0.8f, 0.8f) / 2.0f;

        Debug.Log($"i={sector} Val={value} X={randX} Y={randY}");

        if (System.Math.Abs(randY) < 3 && System.Math.Abs(randY) < 3)
        {
            randY = 4;
        }

        var position = new Vector2(randX, randY);

        potato.transform.position = position;
        potato.transform.up = Vector2.up;
    }

    private void SpawnSpikeBalls()
    {
        var x = worldBounds.size.x / 5;

        GameObject spikeBallL = GameObject.Instantiate(spikeBallPrefab);

        var position = new Vector2(-x, 0);

        spikeBallL.transform.position = position;
        spikeBallL.transform.up = Vector2.up;
 
        GameObject spikeBallR = GameObject.Instantiate(spikeBallPrefab);

        position = new Vector2(x, 0);

        spikeBallR.transform.position = position;
        spikeBallR.transform.up = Vector2.up;

        var y = worldBounds.size.y / 4;

        float myScale = 0.25f;

        /*
           GameObject spikeBallT = GameObject.Instantiate(spikeBallPrefab);

           position = new Vector2(0, -y);

           spikeBallT.transform.position = position;
           spikeBallT.transform.up = Vector2.up;
           //spikeBallT.transform.localScale = 0.5f;

           spikeBallT.transform.localScale = new Vector3(myScale, myScale, 1);
           */
        GameObject spikeBallB = GameObject.Instantiate(spikeBallPrefab);

        position = new Vector2(0, y);

        spikeBallB.transform.position = position;
        spikeBallB.transform.up = Vector2.up;
        spikeBallB.transform.localScale = new Vector3(myScale, myScale, 1);
        
    }

    public void AddWalls()
    {
        GameObject leftWall = GameObject.Instantiate(verticalWallPrefab);
        GameObject rightWall = GameObject.Instantiate(verticalWallPrefab);

        var leftPos = new Vector2(-worldBounds.size.x / 2, 0);

        leftWall.transform.position = leftPos;
        leftWall.transform.up = Vector2.up;

        var rightPos = new Vector2(worldBounds.size.x / 2, 0);

        rightWall.transform.position = rightPos;
        rightWall.transform.up = Vector2.up;

        GameObject topWall = GameObject.Instantiate(horizontalWallPrefab);
        GameObject bottomWall = GameObject.Instantiate(horizontalWallPrefab);

        var topPos = new Vector2(0, -worldBounds.size.y / 2);

        topWall.transform.position = topPos;
        topWall.transform.up = Vector2.up;

        var bottomPos = new Vector2(0, worldBounds.size.y / 2);

        bottomWall.transform.position = bottomPos;
        bottomWall.transform.up = Vector2.up;
    }
}
