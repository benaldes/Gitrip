using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordOfTheflies : MonoBehaviour
{
    public int MaxHP = 500;
    public int HP = 500;

    [SerializeField] private SpriteRenderer _sprite;

    private Vector3 _currentPoint;
    void Start()
    {
        _currentPoint = transform.position;
    }


    void Update()
    {
        DirectionSwitch();
    }
    private void DirectionSwitch()
    {
        if (transform.position.x > _currentPoint.x)
        {
            _sprite.flipX = true;
        }
        else if (transform.position.x < _currentPoint.x)
        {
            _sprite.flipX = false;
        }
        _currentPoint.x = transform.position.x;
    }
}
