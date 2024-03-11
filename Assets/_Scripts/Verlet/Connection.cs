using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Dot DotA {  get; }
    public Dot DotB {  get; }
    public float Length { get; }

    public Connection(Dot dotA, Dot dotB, float length)
    {
        DotA = dotA;
        DotB = dotB;
        Length = length;
    }
    public Connection(Dot dotA, Dot dotB)
    {
        DotA = dotA;
        DotB = dotB;
        Length = (dotA.CurrentPosition - dotB.CurrentPosition).magnitude;
    }
    public Dot Other(Dot dot) => dot == DotA ? DotB : DotA;
}
