using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private bool alive = true;

    private Material material;
    private float fade = 1;
    private float scaleCoefficient = 1;
    [SerializeField]
    private float dissolvingSpeed;
    private Vector3 currentScale;

    public AudioSource ateAudioSource;
    public AudioSource deadAudioSource;

    void Start()
    {
        target = transform.position;
        DrowWeight();

        material = GetComponent<SpriteRenderer>().material;
        dissolvingSpeed = 1f;
    }

    void Update()
    {
        if (alive)
        {
            Move();
        }
        else
        {
            DeadAnimation();
        }
    }

    private void DeadAnimation()
    {
        
        fade -= dissolvingSpeed * Time.deltaTime;
        scaleCoefficient += (1 / fade) ;

        if (fade <= 0)
        {
            if (fade < -2)
            {
                SetScore();
                ToDeadMenu();
            }
        }

        SetFade();

        if (fade >= 0.1f)
        {
            if (!deadAudioSource.isPlaying)
            {
                deadAudioSource.Play();
            }
            SetMaterialScale();
        }
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playingField.targetToMove = target;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            target = Camera.main.ScreenToWorldPoint(touch.position);
            playingField.targetToMove = target;
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);


        ConsumeFood(step);
    }

    private void ConsumeFood(float step)
    {
        currentSatiety -= step * weight * 1f / 100f;
        if (currentSatiety <= 0)
        {
            SetDead();
        }
        DrowSatiety();
    }

    private void Eat(float weight)
    {
        ateAudioSource.Play();
        currentSatiety += weight;
        if (currentSatiety > this.weight)
        {
            float currentRadius = GetRadius(this.weight);

            weight = (currentSatiety - this.weight) / 2;
            currentSatiety = this.weight += weight;

            float deltaRadius = GetRadius(this.weight) - currentRadius;
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
        if (alive)
        {
            if (collision.CompareTag("Food"))
            {
                Eat(collision.GetComponent<Food>().GetWeight());
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("Needle"))
            {
                SetDead();
            }
        }
    }

    public float GetWeight()
    {
        return weight;
    }

    public float GetRadius(float weight)
    {
        return Mathf.Sqrt(weight / Mathf.PI);
    }

    public float GetRadius()
    {
        return Mathf.Sqrt(this.weight / Mathf.PI);
    }

    private void SetFade()
    {
        material.SetFloat("_Fade", fade);
    }

    private void SetMaterialScale()
    {
        material.SetFloat("_SpraySize", (10) * (1 + 5 * fade));
        SetGameobjectScale();
    }

    private void SetGameobjectScale()
    {
        Vector3 newScale1 = currentScale * scaleCoefficient;
        transform.localScale = newScale1;
    }

    private void SetDead()
    {
        print("смерть");
        alive = false;
        currentScale = transform.localScale;
    }

    private void ToDeadMenu()
    {
        SceneManager.LoadScene("DeadScene");
    }
    public void SetScore()
    {
        PlayerPrefs.SetFloat("SaveScore", weight);

        float bestScore = 0;
        if (PlayerPrefs.HasKey("SaveBestScore")) 
        {
            print("счет" + weight);
            bestScore = PlayerPrefs.GetFloat("SaveBestScore");
        }

        if (weight > bestScore)
        {
            print("новый лучший" + weight);
            PlayerPrefs.SetFloat("SaveBestScore", weight);
        }

    }
}
