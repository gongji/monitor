using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

    public float _RotationSpeed = 10.0f;

	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * _RotationSpeed, Space.World); 
    }
}
