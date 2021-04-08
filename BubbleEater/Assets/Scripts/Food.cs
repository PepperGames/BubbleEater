using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private float dissolvingSpeed;

    private Material material;

    private float fade = 1;

    private Vector3 currentScale;
    private float scaleCoefficient = 1;

    [SerializeField]
    private float timeToExplosion;

    public CircleCollider2D circleCollider;

    private float radius;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        currentScale = transform.localScale;
        radius = transform.localScale.x;
        dissolvingSpeed = Random.Range(1f, 4f);
        timeToExplosion = Random.Range(2f, 4f);
    }

    void Update()
    {
        timeToExplosion -= Time.deltaTime;

        if(timeToExplosion<=0)
        {
            circleCollider.enabled = false;

            fade -= dissolvingSpeed * Time.deltaTime;
            scaleCoefficient += (1 / fade) / 80;

            if (fade <= 0)
            {
                Destroy(gameObject);
            }

            SetFade();

            if (fade >= 0.1f)
            {
                SetMaterialScale();
            }
        }
    }
    private void SetFade()
    {
        material.SetFloat("_Fade", fade);
    }
    private void SetMaterialScale()
    {
        material.SetFloat("_SpraySize", (30) * (1 + 5 * fade));
        SetGameobjectScale();
    }
    private void SetGameobjectScale()
    {
        Vector3 newScale1 = currentScale * scaleCoefficient;
        transform.localScale = newScale1;
    }
    public float GetWeight()
    {
        return Mathf.PI * radius * radius;
    }
}
