using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletSimulator 
{
    public List<Dot> Dots { get; } = new List<Dot>();
    private int _interactions;
    private float _mass;
    private Vector3 _currentForce = Vector3.zero;
    public void Simulate(float deltaTime)
    {
        ApplyPhysicsToDots(deltaTime);
        ConstraintLength();
    }

    public VerletSimulator (float mass, int interactions)
    {
        this._mass = mass;
        this._interactions = interactions;
    }

    public void AddForce(Vector3 force)
    {
        _currentForce += force;
    }

    private void ApplyPhysicsToDots(float deltaTime)
    {
        float squaredDeltaTime = deltaTime * deltaTime;
        Vector3 acceleration = _currentForce / _mass;
        Vector3 positionVariation = acceleration * squaredDeltaTime;

        foreach (Dot dot in Dots)
        {
            if (dot.IsLocked)
                continue;

            Vector3 oldPostion = dot.CurrentPosition;

            dot.CurrentPosition += dot.CurrentPosition - dot.LastPosition;
            dot.CurrentPosition += positionVariation;
            dot.LastPosition = oldPostion;
        }

        _currentForce = Vector3.zero;
    }

    private void ConstraintLength()
    {
        for (int i = 0; i < _interactions; i++)
        {
            foreach (Dot dotA in Dots)
            {
                foreach (Connection connection in dotA.Connections)
                {
                    Dot dotB = connection.Other(dotA);
                    Vector3 center = (dotA.CurrentPosition + dotB.CurrentPosition) / 2f;
                    Vector3 dir = (dotA.CurrentPosition - dotB.CurrentPosition).normalized;
                    Vector3 connectionSize = dir * connection.Length / 2f;

                    if (!dotA.IsLocked)
                        dotA.CurrentPosition = center + connectionSize;

                    if (!dotB.IsLocked)
                        dotB.CurrentPosition = center - connectionSize;
                }
            }
        }
    }
}
