using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInertia : MonoBehaviour
{
    private bool _isTween;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    private void Start(){
        _initialPosition = transform.position;
        _initialRotation = transform.rotation.eulerAngles;
    }

    private void MoveTween(Vector3 dir, float amount)
    {
        float time = (amount * Time.deltaTime) * 2;
        _isTween = true;

        if(dir == Vector3.zero)
        {
            LeanTween.moveLocal(gameObject, new Vector3(dir.x * amount / 2, _initialPosition.y, amount / 3), time / 2).setOnComplete(() => {
                LeanTween.moveLocal(gameObject, new Vector3(dir.x * amount / 4, _initialPosition.y, -amount / 4), time * 2).setOnComplete(() => {
                    LeanTween.moveLocal(gameObject, _initialPosition, time ).setOnComplete(() => { _isTween = false; });
                });
            });
        }
        else
        {
            LeanTween.moveLocal(gameObject, new Vector3(dir.x * (amount / 3f ), _initialPosition.y, (-amount / 2)), time).setOnComplete(() => { _isTween = false; });
        }

        LeanTween.rotateLocal(gameObject, new Vector3( dir.x < 0 ? dir.x * (amount * 10) : -dir.x * (amount * 10), _initialRotation.y, 0f), time);
    }

    public void Move(Vector3 dir, float amount)
    {
        if (dir == Vector3.zero){
            LeanTween.cancel(gameObject);
            MoveTween(dir, amount);
        }

        if (_isTween)
            return;

        LeanTween.cancel(gameObject);
        MoveTween(dir, amount);
    }
}
