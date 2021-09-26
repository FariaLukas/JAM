using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmFeedback : MonoBehaviour
{
    public Image fill;


    public void UpdateFill(float amout)
    {
        fill.fillAmount = amout;
    }
}
