using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [SerializeField]
    protected Transform trackingTarget;

    public float CamMoveSpeed = 1f;
    // ...

    void Update()
    {
        if (trackingTarget == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(trackingTarget.position.x,
             trackingTarget.position.y, transform.position.z), CamMoveSpeed * Time.deltaTime);
    }
}
