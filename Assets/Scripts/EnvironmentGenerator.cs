using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;

    [SerializeField, Range(3f, 100f)] public int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    [SerializeField] public float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;

    private UnityEngine.Vector3 _lastPos;

    private void OnValidate()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLength; i++)
        {
            _lastPos = transform.position + new UnityEngine.Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, _curveSmoothness * _xMultiplier * UnityEngine.Vector3.left);
                _spriteShapeController.spline.SetRightTangent(i, _curveSmoothness * _xMultiplier * UnityEngine.Vector3.right);
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLength, new UnityEngine.Vector3(_lastPos.x, transform.position.y - _bottom));

        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new UnityEngine.Vector3(transform.position.x, transform.position.y - _bottom));
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLength; i++)
        {
            _lastPos = new UnityEngine.Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, _curveSmoothness * _xMultiplier * UnityEngine.Vector3.left);
                _spriteShapeController.spline.SetRightTangent(i, _curveSmoothness * _xMultiplier * UnityEngine.Vector3.right);
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLength, new UnityEngine.Vector3(_lastPos.x, 0 - _bottom));

        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new UnityEngine.Vector3(0, 0 - _bottom));
    }
}
