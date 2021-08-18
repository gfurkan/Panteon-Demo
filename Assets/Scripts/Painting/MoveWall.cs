using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;

public class MoveWall : MonoBehaviour
{
    [SerializeField] private Transform wallPosition;
    [SerializeField] private GameObject painter;
    [SerializeField] private Text percentageText;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wallPosition.position, 0.1f);
        if (transform.position.y == wallPosition.position.y)
        {
            percentageText.GetComponent<CanvasGroup>().alpha = 1;
            painter.SetActive(true);
        }
    }
}
