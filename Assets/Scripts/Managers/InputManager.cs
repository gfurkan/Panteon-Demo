using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager _Instance = null;
    public static InputManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("InputManager").AddComponent<InputManager>();
            }
            return _Instance;
        }
    }
    #endregion

    private bool _touched = false;
    private float _touchPos, _touchPosNext;
    private float _direction;

    #region Get Set

    public float direction
    {
        get
        {
            return _direction;
        }
    }
    public bool touched 
    {
        get
        {
            return _touched;
        }
    }

    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchPos = Input.mousePosition.x;
            _touched = true;
        }
        if (Input.GetMouseButton(0))
        {
            _touchPosNext = Input.mousePosition.x;
            _direction = _touchPosNext - _touchPos;
            _touchPos = _touchPosNext;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _touched = false;
            _direction = 0;
        }
    }
}