using UnityEngine;
using TMPro;
using static UnityEditor.VersionControl.Asset;
using System.Collections;

public class PurchaseUpgrades : MonoBehaviour
{

    GrassManager grassManager;

    [Header("Rake")]
    [SerializeField] float rakeCostAmount;
    [SerializeField] float rakeCostIncrease;
    [SerializeField] TMP_Text rakeCostText;
    [SerializeField] TMP_Text rakeAmountText;
    int rakes = 0;

    [Header("Slave")]
    [SerializeField] float slaveCostAmount;
    [SerializeField] float slaveCostIncrease;
    [SerializeField] float slaveMoneyMaking;
    [SerializeField] TMP_Text slaveCostText;
    [SerializeField] TMP_Text slaveAmountText;
    int slaves = 0;

    [Header("Cutter")]
    [SerializeField] float cutterCostAmount;
    [SerializeField] float cutterCostIncrease;
    [SerializeField] TMP_Text cutterCostText;
    [SerializeField] TMP_Text cutterLevelText;
    public int cutterProviding;
    int cutterLevel = 0;

    [Header("Plots")]
    [SerializeField] float plotSellAmount;
    [SerializeField] float plotSellIncrease;
    [SerializeField] TMP_Text plotSellText;
    [SerializeField] TMP_Text plotSellAmountText;
    public int plotGiving;
    [SerializeField] int plotsSold = 0;

    int numberOfDecimals = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grassManager = FindFirstObjectByType<GrassManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RakePurchased()
    {
        if (grassManager.money >= rakeCostAmount)
        {
            grassManager.money = grassManager.money - rakeCostAmount;
            rakeCostAmount = rakeCostAmount * rakeCostIncrease;
            rakeCostAmount = Mathf.Round(rakeCostAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            rakes++;

            rakeCostText.text = rakeCostAmount.ToString();
            rakeAmountText.text = rakes.ToString();
            grassManager.moneyYouHave.text = grassManager.money.ToString();
        }
        if (rakes == 1)
        {
            grassManager.rakeProviding = grassManager.rakeProvidingIncrease + 1f;
        }
        else
        {
            grassManager.rakeProviding = (grassManager.rakeProvidingIncrease * rakes) + 1f;
        }
    }

    public void SlavePurchased()
    {
        if (grassManager.money >= slaveCostAmount)
        {
            grassManager.money = grassManager.money - slaveCostAmount;
            slaveCostAmount = slaveCostAmount * slaveCostIncrease;
            slaveCostAmount = Mathf.Round(slaveCostAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            slaves++;

            slaveCostText.text = slaveCostAmount.ToString();
            slaveAmountText.text = slaves.ToString();
            grassManager.moneyYouHave.text = grassManager.money.ToString();
        }

        if (slaves == 1)
        {
            slaveMoneyMaking = 10f;
            StartCoroutine(SlaveClaimer());
        }
    }

    IEnumerator SlaveClaimer()
    {
        yield return new WaitForSeconds(4.9f);
        grassManager.money = grassManager.money + (slaveMoneyMaking * slaves);
        grassManager.moneyYouHave.text = grassManager.money.ToString();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SlaveClaimer());
    }

    public void CutterPurchasing()
    {
        if (grassManager.money >= cutterCostAmount)
        {
            grassManager.money = grassManager.money - cutterCostAmount;
            cutterCostAmount = cutterCostAmount * cutterCostIncrease;
            cutterCostAmount = Mathf.Round(cutterCostAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            cutterLevel++;
            cutterCostText.text = cutterCostAmount.ToString();
            cutterLevelText.text = cutterLevel.ToString();
            grassManager.moneyYouHave.text = grassManager.money.ToString();
        }
        cutterProviding = cutterLevel + 1;
    }

    public void PlotSelling()
    {
        if (grassManager.money >= plotSellAmount)
        {
            grassManager.money = grassManager.money - plotSellAmount;
            plotSellAmount = plotSellAmount * plotSellIncrease;
            plotSellAmount = Mathf.Round(plotSellAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            plotsSold++;
            plotSellText.text = plotSellAmount.ToString();
            plotSellAmountText.text = plotsSold.ToString();
            grassManager.moneyYouHave.text = grassManager.money.ToString();

            plotGiving = 1000 * plotsSold;
        }
    }
}
