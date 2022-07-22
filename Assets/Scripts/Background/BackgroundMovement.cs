using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Burst;
// public struct BackgroundMovementStruct : IJobParallelFor
// {
//     public void Execute(int index)
//     {

//     }
// }


public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    private Transform[] spaceRendersTransform;
    private int currentIndex = 0;
    private int spaceRendersCount;
    private float spaceRendersWidth;
    private float distanceToShift;
    private Vector2 screenCenterViewPosition;
    private void Start()
    {
        spaceRendersTransform = GetComponentsInChildren<Transform>().Where(item =>
            (item.gameObject != this.gameObject) &&
            (item.gameObject.GetComponent<SpriteRenderer>() != null)
            ).ToArray();
        spaceRendersCount = spaceRendersTransform.Count();
        spaceRendersWidth = GetComponentInChildren<SpriteRenderer>().size.x;
        for (int i = 1; i < spaceRendersCount; i++)
        {
            spaceRendersTransform[i].transform.position = new Vector3(
                spaceRendersTransform[i].transform.position.x + spaceRendersWidth * i,
                spaceRendersTransform[i].transform.position.y,
                spaceRendersTransform[i].transform.position.z);
        }
        screenCenterViewPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z));
        currentIndex = 1;
    }

    private void Update()
    {
        for (int i = 0; i < spaceRendersCount; i++)
        {
            spaceRendersTransform[i].transform.position = new Vector3(
                spaceRendersTransform[i].transform.position.x - scrollSpeed * Time.deltaTime,
                spaceRendersTransform[i].transform.position.y,
                spaceRendersTransform[i].transform.position.z);
        }
        distanceToShift = spaceRendersTransform[currentIndex].transform.position.x - screenCenterViewPosition.x;
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
        spaceRendersTransform[index].transform.position = new Vector3(
            this.transform.position.x + spaceRendersWidth * 2 + distanceToShift,
            spaceRendersTransform[index].transform.position.y,
            spaceRendersTransform[index].transform.position.z);
    }
}
