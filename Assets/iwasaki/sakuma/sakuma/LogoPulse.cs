using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoPulse : MonoBehaviour
{
    // Start is called before the first frame update
    public float scaleAmount = 1.2f;
    public float speed = 2f;
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        transform.localScale = Vector3.Lerp(baseScale, baseScale * scaleAmount, t);
    }

}
