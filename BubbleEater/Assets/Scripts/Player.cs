using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;

    private Vector2 target;

    public Camera mainCamera;

    public PlayingField playingField;

    private float currentSatiety = Mathf.PI * 1 * 1;
    private float weight = Mathf.PI * 1 * 1; //он же максимальная сытость

    public Slider satietySlider;
    public Text weightText;
    void Start()
    {
        target = transform.position;
        DrowWeight();
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

        ConsumeFood(step);
    }

    private void ConsumeFood(float step)
    {
        currentSatiety -= step * weight * 1f / 100f;
        DrowSatiety();
    }

    private void Eat(float weight)
    {
        currentSatiety += weight;
        if (currentSatiety > this.weight)
        {
            float currentRadius = Mathf.Sqrt(this.weight / Mathf.PI);

            weight = (currentSatiety - this.weight) / 2;
            currentSatiety = this.weight += weight;

            float deltaRadius = Mathf.Sqrt(this.weight / Mathf.PI) - currentRadius;
            print(deltaRadius);
            Scale(deltaRadius);
        }
        DrowSatiety();
        DrowWeight();
    }

    private void Scale(float deltaRadius)
    {
        Vector3 newScale = transform.localScale + new Vector3(deltaRadius, deltaRadius, 0);
        transform.localScale = newScale;
        playingField.ResizeAll(deltaRadius);
    }

    private void DrowSatiety()
    {
        satietySlider.value = currentSatiety / weight;
    }

    private void DrowWeight()
    {
        weightText.text = $"WEIGTH: \n{Math.Round(weight, 2)}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("тригер");
        if (collision.CompareTag("Food"))
        {
            Eat(collision.GetComponent<Food>().GetWeight());
            Destroy(collision.gameObject);
        }
    }

}
