using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    public Camera mainCamera;

    private const float startCameraSize = 5f;
    private const float startBackgroundX = 5.62f;
    private const float startBackgroundY = 10f;

    public float scaleCoefficient = 1;

    private float rightLine = 2.81f;
    private float leftLine = -2.81f;
    private float upperLine = 5;
    private float bottomLine = -5;

    float currentScale = 1;

    public Transform foodParent;
    public GameObject[] foodPrefabs;
    void Start()
    {
        //Invoke("ResizeAll",0f);

        InvokeRepeating("SpawnFood", 0, 0.5f);
    }

    public void ResizeAll(float weight)
    {
        scaleCoefficient += weight/2;

        rightLine = (startBackgroundX / 2) * scaleCoefficient;
        leftLine = -(startBackgroundX / 2) * scaleCoefficient;
        upperLine = (startBackgroundY / 2) * scaleCoefficient;
        bottomLine = -(startBackgroundY / 2) * scaleCoefficient;
        StartCoroutine(CameraSizeCoroutine());
    }
   
    IEnumerator CameraSizeCoroutine()
    {
        for (; currentScale <= scaleCoefficient; currentScale+=0.002f)
        {
            mainCamera.orthographicSize = startCameraSize * currentScale;
            background.transform.localScale = new Vector3(startBackgroundX * currentScale, startBackgroundY * currentScale, 1);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnFood()
    {
        Vector3 v = new Vector3(Random.Range(leftLine, rightLine), Random.Range(upperLine, bottomLine));
        Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], v, Quaternion.identity, foodParent);
    }
}
