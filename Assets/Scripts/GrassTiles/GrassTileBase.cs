using System.Collections;
using UnityEngine;
using TMPro;

public class GrassTileBase : MonoBehaviour
{
    [SerializeField] Sprite[] grassTileLevelSprites;
    [SerializeField, Min(0.5f)] float growthSpeedMin;
    [SerializeField, Min(0.5f)] public float growthSpeedMax;

    [SerializeField] int maxHealth;
    [SerializeField] int health;

    public bool inSprinkleRange = false;

    SpriteRenderer spriteRenderer;
    GrassManager grassManager;
    PurchaseUpgrades purchaseUpgrades;
    SprinkleSpread sprinkleSpread;

    private void Awake()
    {
        health = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void Start()
    {
        grassManager = FindFirstObjectByType<GrassManager>();
        purchaseUpgrades = FindFirstObjectByType<PurchaseUpgrades>();
        sprinkleSpread = FindFirstObjectByType<SprinkleSpread>();
        StartCoroutine("GrowLoop");
    }

    private void Update()
    {
        if (!inSprinkleRange)
        {
            growthSpeedMax = sprinkleSpread.growthSpeedOut;
        }
    }

    public void Cut()
    {
        grassManager.moneyYouHave.text = grassManager.money.ToString();
        grassManager.defaultGrassMoney = 1f;

        if (health > 0)
        {
            grassManager.defaultGrassMoney = grassManager.defaultGrassMoney * purchaseUpgrades.cutterProviding;
            grassManager.moneyGain = grassManager.defaultGrassMoney + grassManager.rakeProviding;
            grassManager.money = grassManager.money + grassManager.moneyGain + purchaseUpgrades.plotGiving;

            grassManager.moneyYouHave.text = grassManager.money.ToString();

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