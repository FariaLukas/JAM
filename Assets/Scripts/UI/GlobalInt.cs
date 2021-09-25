using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalInt", menuName = "SO/Global")]
public class GlobalInt : ScriptableObject
{
    [TextArea]
    public string description;

    public int value;
}
