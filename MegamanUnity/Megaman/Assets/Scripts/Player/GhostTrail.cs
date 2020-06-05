using DG.Tweening;
using UnityEngine;



/// <summary>
/// El camino que dibuja el jugador al hacer el dash
/// </summary>
public class GhostTrail : MonoBehaviour
{
    private Movement move;
    private AnimationScript anim;
    private SpriteRenderer sr;
    public Transform ghostsParent;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        anim = FindObjectOfType<AnimationScript>();
        move = FindObjectOfType<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Pone un efecto tras el personaje para formar el dash, son 3 sombras que van trás de si
    /// </summary>
    public void ShowGhost()
    {
        Sequence s = DOTween.Sequence();

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(() => currentGhost.position = move.transform.position);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = anim.sr.flipX);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = anim.sr.sprite);
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
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