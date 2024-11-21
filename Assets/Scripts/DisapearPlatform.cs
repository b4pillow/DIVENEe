using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float delayBeforeFade = 2f;
    public float fadeTime = 1f;
    public float respawnDelay = 5f;

    private SpriteRenderer renderer;
    private Collider2D collider;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(StartFade), delayBeforeFade - fadeTime);
        }
    }

    void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color initialColor = renderer.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            renderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        collider.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color initialColor = renderer.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            renderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        collider.enabled = true;
    }
}