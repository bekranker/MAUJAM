using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private Transform CheckPoint;
    [SerializeField] private Vector2 CheckSize;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask Layer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(CheckPoint.position, CheckSize, 0, Layer);
    }
}
