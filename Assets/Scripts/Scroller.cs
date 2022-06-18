using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    public void StopScrolling()
    {
        _x = 0f;
        _y = 0f;
    }

    public void ContinueScrolling()
    {
        _x = 0.005f;
        _y = 0.005f;
    }

    public void SpeedUpScrolling()
    {
        _x = 0.02f;
        _y = 0.02f;
    }
    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
