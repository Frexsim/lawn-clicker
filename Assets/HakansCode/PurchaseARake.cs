using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseARake : MonoBehaviour
{
    GrassClaiming grassClaiming;

    public float money = 100f;
    [SerializeField] int defaultGrassMoney = 1;
    [SerializeField] int rakes = 0;
    [SerializeField] float rakeProvidingIncrease = 1.3f;
    public float rakeProviding = 1f;


    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] TMP_Text moneyYouHave;


    [SerializeField] float costAmount = 100f;
    [SerializeField] float costIncrease = 1.2f;
    [SerializeField] int numberOfDecimals = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grassClaiming = FindFirstObjectByType<GrassClaiming>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GrassClaimed()
    {
        money = money * rakeProviding;
        money = Mathf.Round(money * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);
        moneyYouHave.text = money.ToString();
    }

    public void PurchasingTime()
    {
        if (money >= costAmount)
        {
            money = money - costAmount;
            costAmount = costAmount * costIncrease;
            costAmount = Mathf.Round(costAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            rakes++;

            costText.text = costAmount.ToString();
            itemAmountText.text = rakes.ToString();
            moneyYouHave.text = money.ToString();
        }
        if (rakes == 1)
        {
            rakeProviding = rakeProvidingIncrease;
        }
    }
}
