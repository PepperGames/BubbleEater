using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    private float currentScale = 1;

    public Transform foodParent;
    public GameObject[] foodPrefabs;

    public GameObject[] needles;

    [SerializeField]
    private float repeatRateToSpawnFood = 0.7f;
    private float timeToSpawnFood = 0.7f;


    private float repeatRateToSpawnNeedle = 2.5f;
    private float timeToSpawnNeedle = 2.5f;

    public GameObject player;
    public Vector2 targetToMove;

    private void Start()
    {
        SpawnFood();
        SpawnFood();
        SpawnFood();
    }

    private void Update()
    {
        timeToSpawnFood -= Time.deltaTime;
        if (timeToSpawnFood <= 0)
        {
            SpawnFood();
            timeToSpawnFood = repeatRateToSpawnFood;
            SetRepeatRateToSpawnFood();
        }

        timeToSpawnNeedle -= Time.deltaTime;
        if (timeToSpawnNeedle <= 0)
        {
            MoveNeedle(needles[Random.Range(0, needles.Length)]);
            timeToSpawnNeedle = repeatRateToSpawnNeedle;
        }
    }

    public void ResizeAll(float weight)
    {
        scaleCoefficient += weight / 2;

        rightLine = (startBackgroundX / 2) * scaleCoefficient;
        leftLine = -(startBackgroundX / 2) * scaleCoefficient;
        upperLine = (startBackgroundY / 2) * scaleCoefficient;
        bottomLine = -(startBackgroundY / 2) * scaleCoefficient;
        StartCoroutine(CameraSizeCoroutine());
    }

    IEnumerator CameraSizeCoroutine()
    {
        for (; currentScale <= scaleCoefficient; currentScale += 0.002f)
        {
            mainCamera.orthographicSize = startCameraSize * currentScale;
            background.transform.localScale = new Vector3(startBackgroundX * currentScale, startBackgroundY * currentScale, 1);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnFood()
    {
        Vector3 v = new Vector3(Random.Range(leftLine, rightLine), Random.Range(upperLine, bottomLine));
        Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], v, Quaternion.Euler(0, 0, Random.Range(0, 360)), foodParent);
    }

    private void SetRepeatRateToSpawnFood()
    {
        repeatRateToSpawnFood = 0.7f / Mathf.Sqrt(scaleCoefficient) - Time.time / 1200;
        if (repeatRateToSpawnFood < 0.05f)
        {
            repeatRateToSpawnFood = 0.05f;
        }
    }

    private void MoveNeedle(GameObject needle)
    {
        Vector3 position = GetPointForNeedleMoving(1);
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        needle.transform.position = position;
        needle.transform.rotation = rotation;
    }

    private Vector2 GetPointForNeedleMoving(int i)
    {
        Vector3 position = new Vector3(Random.Range(leftLine, rightLine), Random.Range(upperLine, bottomLine), 0);
        float radius = player.GetComponent<Player>().GetRadius();

        if (CanBePlaced(position, GetProjection(new PointF(position.x, position.y),
            new PointF(player.transform.position.x, player.transform.position.y), new PointF(targetToMove.x, targetToMove.y))))
        {
            return position;
        }
        else
        {
            if (i > 3)
            {
                if (CanBePlaced(new Vector2(0, 0), GetProjection(new PointF(position.x, position.y),
                    new PointF(player.transform.position.x, player.transform.position.y), new PointF(targetToMove.x, targetToMove.y))))
                {
                    return new Vector2(0, 0);
                }
            }
            i++;
            print(i);
            return GetPointForNeedleMoving(i);
        }
    }

    private bool CanBePlaced(Vector3 position, PointF point)
    {
        Circle playerCircle = new Circle(point.X, point.Y, player.GetComponent<Player>().GetRadius());
        Circle needleCircle = new Circle(position.x, position.y, 1.4f * Mathf.PI);
        return !playerCircle.IsIntersect(needleCircle);
    }

    private PointF GetProjection(PointF p, PointF p1, PointF p2)
    {
        float fDenominator = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
        if (fDenominator == 0) // p1 and p2 are the same
            return p1;

        float t = (p.X * (p2.X - p1.X) - (p2.X - p1.X) * p1.X + p.Y * (p2.Y - p1.Y) - (p2.Y - p1.Y) * p1.Y) / fDenominator;

        return new PointF(p1.X + (p2.X - p1.X) * t, p1.Y + (p2.Y - p1.Y) * t);
    }
}
