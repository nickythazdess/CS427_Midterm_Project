using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    TMP_Text textMesh;
    public static int coinAmount;
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        coinAmount = 0;
    }

    void Update() {
        textMesh.SetText(coinAmount.ToString());
    }
}
