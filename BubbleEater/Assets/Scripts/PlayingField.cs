using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField]
    private GameObject backGround;

    public Camera mainCamera;

    private float CameraSize =5;
    private Vector2 backgroundScale = new Vector2(5.62f, 10f);

    public float scaleCoefficient = 1;

    public float rightLine = 2.81f;
    public float leftLine = -2.81f;
    public float upperLine = 5;
    public float bottomLine = -5;

    void Start()
    {
        //mainCamera.orthographicSize = CameraSize;
    }

    void Update()
    {

    }
}
