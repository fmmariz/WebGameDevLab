using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAgent : AbstractAgent
{

    private void FixedUpdate()
    {
        if (!_isJobComplete || !(Vector3.Distance(transform.position, _destination) <= 1.0f))
        {
            return; 
        }
            _isJobComplete = true;
            CompleteJob();
        
    }

    public override void Navigate(Transform location)
    {
        var destination = location.position;
        _myself.destination = destination;
    }

    public override void CompleteJob()
    {
        _isJobComplete = true;
        Debug.Log("Successfully reached the position");
        Destroy(gameObject, 1.5f);
    }


}
