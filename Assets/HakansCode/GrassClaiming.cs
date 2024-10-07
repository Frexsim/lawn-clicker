using TMPro;
using UnityEngine;

public class GrassClaiming : MonoBehaviour
{
    PurchaseARake purchaseARake;

    private void Start()
    {
        purchaseARake = FindFirstObjectByType<PurchaseARake>();
    }
    
}
