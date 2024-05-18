using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _separationChance = 100;
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForce ;


    public event Action<Cube> Divided;
    public event Action<Cube> NotDivided;

    private Material _material;
    private Rigidbody _rigidbody;
    private int _decreaseNumber = 2;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        TryDivide();
        Destroy(gameObject);
    }

    public float GetExplodeRadius()
    {
        return _explodeRadius;
    }

    public float GetExplodeForce()
    {
        return _explodeForce;
    }

    public void TryDivide()
    {
        int _minSeparationChance = 0;
        int _maxSeparationChance = 101;

        float probability = UnityEngine.Random.Range(_minSeparationChance, _maxSeparationChance);

        if (_separationChance >= probability)
        {
            Divided?.Invoke(this);
        }
        else
        {
            NotDivided?.Invoke(this);
        }
    }

    public void Init()
    {
        ReduceSeparateChance();
        SetScale();
        SetColor();
        IncreaseExplodeForce();
        IncreaseExplodeRadius();
    }

    public void ReduceSeparateChance()
    {
        int reduceNumber = 2;

        _separationChance /= reduceNumber;
    }

    private void SetScale()
    {
        transform.localScale /= _decreaseNumber;
    }

    private void IncreaseExplodeForce()
    {
        int increaseNumber = 3;
        _explodeForce *= increaseNumber;
    }

    private void IncreaseExplodeRadius()
    {
        int increaseNumber = 2;
        _explodeRadius *= increaseNumber;
    }

    private void SetColor()
    {
        _material.color = UnityEngine.Random.ColorHSV();
    }
}
