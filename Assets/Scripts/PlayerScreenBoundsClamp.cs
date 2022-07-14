using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerScreenBoundsClamp : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float playerSpriteWidth;
    private float playerSpriteHeight;
    private Vector3 viewPosition;
    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        playerSpriteWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        playerSpriteHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void LateUpdate()
    {
        viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x + playerSpriteWidth, (screenBounds.x * -1f) - playerSpriteWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y + playerSpriteHeight, (screenBounds.y * -1f) - playerSpriteHeight);
        transform.position = viewPosition;
    }
}
