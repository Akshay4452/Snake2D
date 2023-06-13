using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // snake movement speed
    private Vector2 direction = Vector2.right; // Default movemement direction
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        moveSpeed = 0.51f;
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
            newPosition.x = -newPosition.x + 0.1f; // Adding 0.1f for smooth movement
        }
        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    private void FixedUpdate()
    {
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
            FoodSpawner.Instance.SpawnFood();
        }
    }
}
