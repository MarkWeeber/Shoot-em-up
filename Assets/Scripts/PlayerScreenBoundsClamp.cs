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
    private float boundaryTop, boundaryLeft, boundaryRight, boundaryBottom;
    private Vector3 viewPosition;
    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        playerSpriteWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        playerSpriteHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        boundaryLeft = screenBounds.x + playerSpriteWidth;
        boundaryRight = (screenBounds.x * -1f) - playerSpriteWidth;
        boundaryTop = (screenBounds.y * -1f) - playerSpriteHeight;
        boundaryBottom = screenBounds.y + playerSpriteHeight;
    }

    private void LateUpdate()
    {
        viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, boundaryLeft, boundaryRight);
        viewPosition.y = Mathf.Clamp(viewPosition.y, boundaryBottom, boundaryTop);
        transform.position = viewPosition;
    }
}
