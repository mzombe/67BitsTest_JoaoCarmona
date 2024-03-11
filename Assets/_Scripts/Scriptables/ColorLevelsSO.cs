using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Colors Levels")]
public class ColorLevelsSO : ScriptableObject
{
    public List<Levels> Colors = new List<Levels>();
}
[Serializable]
public class Levels
{
    public string Name;
    public Material Material;
}
