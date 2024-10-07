using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlavePurchase : MonoBehaviour
{
    PurchaseARake purchaseARake;

    [SerializeField] float costAmount = 200f;
    [SerializeField] float costIncrease = 1.5f;
    [SerializeField] int numberOfDecimals = 0;

    [SerializeField] int slaves;
    [SerializeField] float slaveMoneyMaking;

    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] TMP_Text moneyYouHave;
    private void Start()
    {
        purchaseARake = FindFirstObjectByType<PurchaseARake>();
    }
    public void PurchaseTheSlave()
    {
        if (purchaseARake.money >= costAmount)
        {
            purchaseARake.money = purchaseARake.money - costAmount;
            costAmount = costAmount * costIncrease;
            costAmount = Mathf.Round(costAmount * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            slaves++;

            costText.text = costAmount.ToString();
            itemAmountText.text = slaves.ToString();
            moneyYouHave.text = purchaseARake.money.ToString();
        }

        if (slaves == 1)
        {
            StartCoroutine(SlaveClaimer());
        }
    }

    IEnumerator SlaveClaimer()
    {
        yield return new WaitForSeconds(0.9f);
        purchaseARake.money = purchaseARake.money + (slaveMoneyMaking * slaves);
        moneyYouHave.text = purchaseARake.money.ToString();
        yield return new WaitForSeconds (0.1f);
        StartCoroutine(SlaveClaimer());
    }
}
