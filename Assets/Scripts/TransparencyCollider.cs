using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransparencyCollider : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public static Color transparency;

    public static float speed = 0.032f;

#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        transparency = new Color(1, 1, 1, 0.25f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Enemy"))
        {
            StopAllCoroutines();
            StartCoroutine(IChangeColor(transparency));
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Enemy"))
        {
            StopAllCoroutines();
            StartCoroutine(IChangeColor(Color.white));
        }
    }

    private IEnumerator IChangeColor(Color color)
    {
        float t = 0;
        while (t < 1)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, color, t);
            t += Time.deltaTime * speed;
            yield return null;
        }
        spriteRenderer.color = color;
    }
#pragma warning restore IDE0051 // Remove unused private members

    private static IEnumerator SChangeColor(System.Action<float> changeColor)
    {
        float t = 0;
        while (t < 1)
        {
            changeColor(t);
            t += Time.deltaTime * speed;
            yield return null;
        }
        changeColor(1);
    }

    public static Coroutine ChangeColor(SpriteRenderer spriteRenderer, Color color, MonoBehaviour mb)
    {
        return mb.StartCoroutine(SChangeColor((t) =>
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, color, t);
        }));
    }

}
