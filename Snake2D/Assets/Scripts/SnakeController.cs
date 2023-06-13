using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // snake movement speed
    private Vector2 direction = Vector2.right; // Default movemement direction
    private Camera mainCamera;
    private List<Transform> segments;  // List of Snake Segments

    public Transform segmentPrefab;  // Prefab for the snake segment

    private void Start()
    {
        mainCamera = Camera.main;
        moveSpeed = 0.51f;

        segments = new List<Transform>(); // Initialize the Segments list of transforms
        segments.Add(this.transform);  // Adding the snake head to the list of segments

        if (segmentPrefab == null)
        {
            Debug.LogError("Snake Segment Prefab is missing in Inspector");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction= Vector2.right;
        } else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) 
        {
            direction = Vector2.left;   
        }

        KeepPlayerOnScreen();
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 1.5f; // Adding 0.1f for smooth movement
        }
        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 1.5f;
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 2.0f;
        }
        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 2.0f;
        }

        transform.position = newPosition;
    }

    private void FixedUpdate()
    {
        // We need to make snake segments follow along the snake
        // For this purpose, we need to make tail follow its predecessor
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the Snake Sprite in fixed update
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x * moveSpeed,
            Mathf.Round(this.transform.position.y) + direction.y * moveSpeed,
            0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Snake head comes in contact with Food then food gets instantiated randomly elsewhere...
        if(collision.tag == "Food")
        {
            Grow(); // When snake head collides with food, grow the snake
            //FoodSpawner.Instance.SpawnFood();
        }
    }

    private void Grow()
    {
        Transform _segment = Instantiate(this.segmentPrefab);
        _segment.position = segments[segments.Count - 1].position;

        segments.Add( _segment ); // Add the new snake segment to the list
    }
}
