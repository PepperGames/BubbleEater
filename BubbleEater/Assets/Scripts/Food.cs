using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Material material;

    private bool isDissolving = false;
    private float fade = 1;
    public float speed = 1;

    Vector3 newScale;
    float goscale = 1;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        newScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1)
        {
            isDissolving = changeBool(isDissolving);
        }
        if (isDissolving)
        {
            fade -= speed * Time.deltaTime;
            goscale += (1 / fade) / 200;
            Debug.Log(goscale);
            if (fade <= 0)
            {
                fade = 0;
                isDissolving = false;
            }
            SetFade();
            if (fade >= 0.1f)
            {
                SetMaterialScale();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) || Input.touchCount == 2)
        {
            fade = 1;
            goscale = 1;
            SetFade();
            SetMaterialScale();
        }
    }
    bool changeBool(bool b)
    {
        return !b;
    }
    void SetFade()
    {
        material.SetFloat("_Fade", fade);
    }
    void SetMaterialScale()
    {
        material.SetFloat("_Scale", (30) * (1 + 5 * fade));
        SetGameobjectScale();
    }
    void SetGameobjectScale()
    {
        Vector3 newScale1 = newScale * goscale;
        transform.localScale = newScale1;
    }
}
