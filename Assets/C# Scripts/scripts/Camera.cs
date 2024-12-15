using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _targetposition;
    [SerializeField] private float _smoothing ;
    [SerializeField] private Vector3 _offset;
    [SerializeField] float LeftLim ;
    [SerializeField] float RightLim  ;
    [SerializeField] float BottomLim  ;
    [SerializeField] float UpperLim  ;
    internal static Camera main;

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        
            CameraMove();
       

    }
    private void CameraMove()

    {
        var newPosition = Vector3.Lerp(transform.position, _targetposition.position + _offset, Time.deltaTime * _smoothing);
        transform.position = newPosition; 
        transform.position = new Vector3 
            (
            Mathf.Clamp(transform.position.x, LeftLim, RightLim),
            Mathf.Clamp(transform.position.y, BottomLim, UpperLim),
            transform.position.z
            );


    }

}