using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private Transform[] spaceRenders;
    private int currentIndex = 0;
    private int spaceRendersCount;
    private float spaceRendersWidth;
    private float distanceToShift;
    private Vector2 screenCenterViewPosition;
    private void Start()
    {
        spaceRenders = GetComponentsInChildren<Transform>().Where(item => item.gameObject != this.gameObject).ToArray();
        spaceRendersCount = spaceRenders.Count();
        spaceRendersWidth = GetComponentInChildren<SpriteRenderer>().size.x;
        for (int i = 1; i < spaceRendersCount; i++)
        {
            spaceRenders[i].transform.position = new Vector3(
                spaceRenders[i].transform.position.x + spaceRendersWidth * i,
                spaceRenders[i].transform.position.y,
                spaceRenders[i].transform.position.z);
        }
        screenCenterViewPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z));
        currentIndex = 1;
    }

    private void Update()
    {
        for (int i = 0; i < spaceRendersCount; i++)
        {
            spaceRenders[i].transform.position = new Vector3(
                spaceRenders[i].transform.position.x - scrollSpeed * Time.deltaTime,
                spaceRenders[i].transform.position.y,
                spaceRenders[i].transform.position.z);
        }
        distanceToShift = spaceRenders[currentIndex].transform.position.x - screenCenterViewPosition.x;
        if (distanceToShift <= 0)
        {
            if (currentIndex + 1 >= spaceRendersCount) // current index is the last one
            {
                ShiftSpace(currentIndex - 1);
                currentIndex = 0;
            }
            else
            {
                if(currentIndex == 0)
                {
                    ShiftSpace(spaceRendersCount - 1);
                }
                else
                {
                    ShiftSpace(currentIndex - 1);
                }
                currentIndex++;
            }
        }
    }

    private void ShiftSpace(int index)
    {
        spaceRenders[index].transform.position = new Vector3(
            spaceRenders[index].transform.position.x + spaceRendersWidth * 2 + distanceToShift,
            spaceRenders[index].transform.position.y,
            spaceRenders[index].transform.position.z);
    }
}
