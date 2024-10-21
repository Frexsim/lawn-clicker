using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static UnityEditor.VersionControl.Asset;

public class PurchaseARake : MonoBehaviour
{

    
    

    [Header("Rakes")]
    [SerializeField] int rakes = 0;
    [SerializeField] float rakeProvidingIncrease = 1.3f;
    public float rakeProviding = 1;
    [SerializeField] TMP_Text rakeCostText;
    [SerializeField] TMP_Text rakeAmountText;

    [Header("Grass Gaining")]
    [SerializeField] float moneyGain = 1f;
    [SerializeField] float defaultGrassMoney = 1;
    public float money = 100f;
    [SerializeField] float costAmount = 100f;
    [SerializeField] float costIncrease = 1.2f;
    [SerializeField] int numberOfDecimals = 0;
    [SerializeField] TMP_Text moneyYouHave;

    [Header("Slaves")]
    [SerializeField] float slaveCostAmount;
    [SerializeField] float slaveCostIncrease = 1.5f;
    [SerializeField] int slaves;
    [SerializeField] float slaveMoneyMaking;
    [SerializeField] TMP_Text slaveCostText;
    [SerializeField] TMP_Text slaveAmountText;


    [Header("CutterProviding")]
    [SerializeField] int cutterLevel;
    [SerializeField] int cutterProviding;

    [Header("Cutter")]
    [SerializeField] float cutterCostAmount;
    [SerializeField] float cutterCostIncrease = 1.3f;
    [SerializeField] TMP_Text cutterCostText;
    [SerializeField] TMP_Text cutterAmountText;


    [Header("SellingYourPlot")]
    [SerializeField] int plotGiving;
    [SerializeField] float plotSellAmount;
    [SerializeField] float plotSellIncrease;
    [SerializeField] TMP_Text plotAmountText;
    [SerializeField] TMP_Text plotSellText;
    int plots;

    GrassManager grassManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grassManager = FindFirstObjectByType<GrassManager>();
        moneyYouHave.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GrassClaimed()
    {
        float moneyGainCheck;

        // The money you will get
        if (cutterProviding == 0)
        {
            cutterProviding = 1;
        }
        if (plots == 0)
        {
            plotGiving = 1;
        }
        defaultGrassMoney = 1;

        defaultGrassMoney = defaultGrassMoney * cutterProviding;
        moneyGain = defaultGrassMoney * rakeProviding + plotGiving;

        moneyGainCheck = moneyGain;

        // Make the money you get into a non decimal number.
        moneyGain = Mathf.Round(money * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

        //Failed way of removing the extreme increase of money after removing the decimals.
        if (moneyGain > moneyGainCheck + 1)
        {
            moneyGain = moneyGainCheck - 1;
        }


        //Give money
        money = money + moneyGain;

        //Mention amount
        moneyYouHave.text = money.ToString();
        moneyGain = 0f;
    }

    public void PurchasingRake()
    {
        if (money >= costAmount)
        {
            money = money - costAmount;
            costAmount = costAmount * costIncrease;
            costAmount = Mathf.Round(costAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            rakes++;

            rakeCostText.text = costAmount.ToString();
            rakeAmountText.text = rakes.ToString();
            moneyYouHave.text = money.ToString();
        }
        if (rakes == 1)
        {
            rakeProviding = rakeProvidingIncrease;
        }
    }

    public void PurchaseTheSlave()
    {
        if (money >= slaveCostAmount)
        {
            money = money - slaveCostAmount;
            slaveCostAmount = slaveCostAmount * slaveCostIncrease;
            slaveCostAmount = Mathf.Round(slaveCostAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            slaves++;

            slaveCostText.text = slaveCostAmount.ToString();
            slaveAmountText.text = slaves.ToString();
            moneyYouHave.text = money.ToString();
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
        money = money + (slaveMoneyMaking * slaves);
        moneyYouHave.text = money.ToString();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SlaveClaimer());
    }

    public void CutterPurchasing()
    {
        if (money >= cutterCostAmount)
        {
            money = money - cutterCostAmount;
            cutterCostAmount = cutterCostAmount * cutterCostIncrease;
            cutterCostAmount = Mathf.Round(cutterCostAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            cutterLevel++;
            cutterCostText.text = cutterCostAmount.ToString();
            cutterAmountText.text = cutterLevel.ToString();
            moneyYouHave.text = money.ToString();
        }
        cutterProviding = cutterLevel + 1;
    }

    public void PlotSelling()
    {
        if (money >= plotSellAmount)
        {
            money = money - plotSellAmount;
            plotSellAmount = plotSellAmount * plotSellIncrease;
            plotSellAmount = Mathf.Round(plotSellAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            plots++;
            plotSellText.text = plotSellAmount.ToString();
            plotAmountText.text = plots.ToString();
            moneyYouHave.text = money.ToString();

            plotGiving = 1000 * plots;
        }
    }
}