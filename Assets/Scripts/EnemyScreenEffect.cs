using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScreenEffect : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Image dangerImage;
    [SerializeField] private Image hitImage;

    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float maxDangerAlpha = 0.7f;

    private Color dangerColor;
    private Color hitColor;

    private void Start()
    {
        dangerColor = dangerImage.color;
        hitColor = hitImage.color;

        dangerColor.a = 0f;
        hitColor.a = 0f;

        dangerImage.color = dangerColor;
        hitImage.color = hitColor;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        float alpha = 1f - (distance / maxDistance);
        alpha = Mathf.Clamp01(alpha);

        dangerColor.a = alpha * maxDangerAlpha;
        dangerImage.color = dangerColor;
    }

    public void ShowHitEffect()
    {
        StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect()
    {
        hitColor.a = 1f;
        hitImage.color = hitColor;

        yield return new WaitForSeconds(0.25f);

        hitColor.a = 0f;
        hitImage.color = hitColor;
    }
}