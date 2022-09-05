using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRender { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        this.spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animate), this.animationTime, this.animationTime);
    }

    private void Animate()
    {
        if (!this.spriteRender.enabled)
        {
            return;
        }

        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRender.sprite = this.sprites[this.animationFrame];
        }
    }

    public void RestartAnimation()
    {
        this.animationFrame = -1;
        Animate();
    }
}
