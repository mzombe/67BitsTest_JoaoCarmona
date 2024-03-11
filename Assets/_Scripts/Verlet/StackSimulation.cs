using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class StackSimulation : MonoBehaviour
{
    [SerializeField] private GameEvent _onCollectNpc;
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private int _iteractions = 5;
    [SerializeField] private float _length = 0.5f;

    private VerletSimulator _simulator;
    private CarryController _carryController;

    private void Start()
    {
        Initialize();
        UpdateDots();
    }

    private void OnEnable()
    {
        _onCollectNpc.Subcribe(UpdateDots);
    }
    private void OnDisable()
    {
        _onCollectNpc.Unsubcribe(UpdateDots);
    }

    private void Initialize()
    {
        _carryController = GetComponent<CarryController>();
        _simulator = new VerletSimulator(_mass, _iteractions);
        for (int i = 0; i < CarryManager.Npcs.Count; i++)
        {
            CarryManager.Npcs[i].transform.parent = null;
        }
    }
    private void FixedUpdate()
    {
        if (_simulator.Dots.Count <= 0)
            return;

        _simulator.AddForce(_gravity * Vector3.down);
        _simulator.Simulate(Time.fixedDeltaTime);
        _simulator.Dots[0].CurrentPosition = _rootTransform.position;

        for (int i = 0; i < _simulator.Dots.Count - 1; i++)
        {
            LeanTween.move(CarryManager.Npcs[i].gameObject, _simulator.Dots[i].CurrentPosition, 0.05f);
        }
    }

    private void UpdateDots()
    {
        float dotsQuantity = _carryController.GetAvaliableQuantity();

        if (_simulator.Dots.Count > 0 && dotsQuantity > 0)
            _simulator.Dots.Clear();

        Dot startDot = new Dot(CarryManager.Npcs[0].position, true);
        Dot lastDot = startDot;
        _simulator.Dots.Add(lastDot);

        for (int i = 0; i < dotsQuantity; i++)
        {
            Vector3 dotPosition = CarryManager.Npcs[0].position + Vector3.up * _length * (i + 1);
            Dot newDot = new Dot(dotPosition, false);
            Dot.Connect(lastDot, newDot);
            _simulator.Dots.Add(newDot);
            lastDot = newDot;
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
            return;

        if (_simulator.Dots.Count > 0) {
            Dot lastDot = _simulator.Dots[0];
            Gizmos.color = !lastDot.IsLocked ? Color.green : Color.red;
            Gizmos.DrawSphere(lastDot.CurrentPosition, 0.1f);

            for (int i = 1; i < _simulator.Dots.Count; i++)
            {
                Dot currentDot = _simulator.Dots[i];
                Gizmos.color = !currentDot.IsLocked ? Color.green : Color.red;
                Gizmos.DrawSphere(currentDot.CurrentPosition, 0.1f);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(currentDot.CurrentPosition, lastDot.CurrentPosition);
                lastDot = currentDot;
            }
        }
#endif
    }
}
