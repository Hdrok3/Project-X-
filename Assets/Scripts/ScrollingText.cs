using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    private static int order = 0;
    public float duration = 1f;
    public float speed;

    TextMeshPro textMesh;
    Color textColor;
    float startTime;
    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        order = order++ % 100;
        textMesh.sortingOrder = order; 
        startTime = Time.time;
        transform.rotation = Camera.main.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime < duration)
        {
            // kameraya bakmasi gerekebilir
            // transform.Translate(Vector3.up * speed * Time.deltaTime);
            float percent = (duration - (Time.time - startTime));
            textMesh.fontSize = Mathf.Lerp(1, 6, ((Time.time - startTime)*speed));
            //textColor.a =  percent + 0.5f;
            //textMesh.color = textColor;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        textColor = color;
        textMesh.color = color;
    }
}
