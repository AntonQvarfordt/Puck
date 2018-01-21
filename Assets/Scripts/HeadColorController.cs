using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PuckState
{
    Empty,
    Boosting,
    Idle,
}

public class HeadColorController : MonoBehaviour
{

    public Color BoostColor;
    public Color EmptyColor;
    public Color IdleColor;
    public Color FadeOut;

    private SpriteRenderer _headSprite;

    private void Awake()
    {
        _headSprite = GetComponent<SpriteRenderer>();
    }

    public void EmptyColors()
    {
        _headSprite.transform.DOShakePosition(0.1f *0.5f, 0.2f, 50);
        _headSprite.transform.DOShakeScale(0.1f *0.5f, 0.2f, 50);

        _headSprite.DOColor(EmptyColor, 0.1f *0.1f);
    }

    public void IdleColors()
    {
        _headSprite.DOColor(IdleColor, 0.2f).OnComplete(ColorPop);
    }

    public void ColorPop ()
    {
        var dupe = Instantiate(gameObject);
        dupe.transform.SetParent(transform);
        dupe.transform.localPosition = Vector3.zero;

        var headColorScript = dupe.GetComponent<HeadColorController>();
        var spriteRend = dupe.GetComponent<SpriteRenderer>();

        spriteRend.sortingOrder = 2;

        headColorScript.FadeDestroy();
    }

    public void FadeDestroy (float time = 0.5f)
    {
        var newScale = transform.localScale * 2f;
        transform.DOScale(newScale, time);
        _headSprite.DOColor(Color.clear, time);
    }

}
