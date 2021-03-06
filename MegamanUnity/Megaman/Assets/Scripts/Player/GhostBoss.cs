using DG.Tweening;
using UnityEngine;
/// <summary>
/// Pone al boss en rojo para que parezca una nueva fase
/// </summary>
public class GhostBoss : MonoBehaviour
{
    private readonly Transform move;
    public SpriteRenderer sr;
    public Transform ghostsParent;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
    }

    /// <summary>
    /// Activa el efecto de boss cuando llega a menos de la mitad de la vida
    /// </summary>
    public void ShowGhost()
    {
        Sequence s = DOTween.Sequence();

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(() => currentGhost.position = move.transform.position);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = false);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = sr.sprite);
            s.Append(currentGhost.GetComponentInParent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval);
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }
}