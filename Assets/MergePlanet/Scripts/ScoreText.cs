using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public float MoveSpeed;
    public float AlphaSpeed;
    public float DestroySpeed;
    public int score;
    TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = score.ToString();
        alpha = text.color;
        Invoke("DestroyObject", DestroySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, MoveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * AlphaSpeed);
        text.color = alpha;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
