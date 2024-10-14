using System.Collections;
using UnityEngine;

public class GrassTileBase : MonoBehaviour
{
    [SerializeField] Sprite[] grassTileLevelSprites;
    [SerializeField, Min(0.5f)] float growthSpeedMin;
    [SerializeField, Min(0.5f)] float growthSpeedMax;

    [SerializeField] int maxHealth;
    [SerializeField] int health;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void Start()
    {
        StartCoroutine("GrowLoop");
    }

    public void Cut()
    {
        if (health > 0)
        {
            health -= 1;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        int spriteIndex = Mathf.FloorToInt((float) health / (maxHealth / (grassTileLevelSprites.Length - 1)));

        Sprite sprite = grassTileLevelSprites[spriteIndex];
        spriteRenderer.sprite = sprite;
    }

    private IEnumerator GrowLoop()
    {
        yield return new WaitForSeconds(Random.Range(growthSpeedMin, growthSpeedMax));

        if (health + 1 <= maxHealth)
        {
            health += 1;
            UpdateSprite();
        }

        StartCoroutine("GrowLoop");
    }
}