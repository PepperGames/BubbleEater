using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;

    private Vector2 target;

    public Camera mainCamera;

    public PlayingField playingField;
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            target = Camera.main.ScreenToWorldPoint(touch.position);
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("тригер");
        if (collision.CompareTag("Food"))
        {
            Vector3 newScale1 = transform.localScale + new Vector3(0.1f,0.1f,0.1f);
            transform.localScale = newScale1;
            playingField.ResizeAll(0.1f);
            Destroy(collision.gameObject);
        }
    }
}
