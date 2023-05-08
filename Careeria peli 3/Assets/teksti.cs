using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class teksti : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    int Points = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
            Points++;
            pointsText.text = "Koodauksia tehty: " + Points;
        }
    }
}
