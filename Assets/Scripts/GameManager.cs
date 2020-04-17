using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine.UI;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float deltaTime = 0.0f;

    private float surviveTime = 5.0f;
    float _msec;
    float _fps;
    private int sphereCount = 0;
    public Text timer;
    public Text msec;
    public Text fps;
    public Text spheresCount;
    public Text warningText;

    public int spheresPerFrame;
    public GameObject spherePrefab;
    public float sphereSpeed = 3f;

    private bool spawnSpheres;
    public Transform spawnPos;

    private Entity sphereEntityPrefab;
    private EntityManager manager;
    private BlobAssetStore blobAssetStore;
    

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;


        // Converting a GameObject (sphere) into an Entity trhough code
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        sphereEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(spherePrefab, settings);
       

    }

    public void OnDestroy()
    {
        blobAssetStore.Dispose();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnSpheres = false;
        warningText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        Timer();
        myGUI();
    }


    private void Timer()
    {
        if(surviveTime > 0)
        {
            surviveTime -= Time.deltaTime;
            timer.text = "Survive Time: " + surviveTime.ToString("N0");
        }
        
        if (surviveTime < 0)
        {
            warningText.gameObject.SetActive(true);
            GameOver();
        }
    }

    private void GameOver()
    {
        if (!spawnSpheres)
        {
            spawnSpheres = true;
            StartCoroutine(SpawnLotsOfSpheres());
        }
    }
    
    IEnumerator SpawnLotsOfSpheres()
    {
        while (spawnSpheres)
        {
            for (int i = 0; i < spheresPerFrame; i++)
            {
                sphereCount++;
                SpawnNewSphere();
            }
            yield return null;
        }
    }

    
    private void SpawnNewSphere()
    {
        Entity newSphereEntity = manager.Instantiate(sphereEntityPrefab);
        spheresCount.text = "Spheres : " + sphereCount.ToString("N0");
        Translation spawn = new Translation()
        {
            Value = spawnPos.transform.position
        };

        manager.AddComponentData(newSphereEntity, spawn);
    }

    public void myGUI()
    {
        _msec = deltaTime * 1000.0f;
        _fps = 1.0f / deltaTime;
        fps.text = "FPS: " + _fps.ToString("N1");
        msec.text = "MSEC: " + _msec.ToString("N1");
    }

    // create a visible text to display FPS and MS 
    /*
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 7 / 100;
        style.normal.textColor = new Color(255.0f, 255.0f, 255.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
    */
}
