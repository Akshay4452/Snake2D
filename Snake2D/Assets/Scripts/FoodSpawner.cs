using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    //private static FoodSpawner instance;
    //public static FoodSpawner Instance { get { return instance; } }

    //[SerializeField] private Transform food;
    [SerializeField] private GameObject gridArea; // Grid Area for Food Spawning
    private BoxCollider2D grid;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Start()
    {
        if(gridArea == null)
        {
            Debug.LogError("Grid Area game object is missing");
        }

        grid = gridArea.GetComponent<BoxCollider2D>();

        SpawnFood();
    }

    public void SpawnFood()
    {
        Bounds bounds = grid.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 spawnPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);

        //Instantiate(this.food, spawnPosition, Quaternion.identity);
        this.transform.position = spawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SpawnFood();
        }
    }
}
