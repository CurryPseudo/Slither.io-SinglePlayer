using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FindSomethingNear : Conditional {
    public string layerName;
    public SharedFloat findDistance;
    public SharedTransform target;
    public override TaskStatus OnUpdate()
    {
        if (layerName==null) return TaskStatus.Failure;
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, findDistance.Value,LayerMask.GetMask(layerName));
        if (objects.Length == 0) return TaskStatus.Failure;
        float minDistance=findDistance.Value;
        Transform target = null;
        for (int i = 0; i < objects.Length; i++)
        {
            Transform one = objects[i].gameObject.transform;
            float distance = (one.position - gameObject.transform.position).magnitude;
            if (distance <= minDistance && !(layerName=="Body" && objects[i].gameObject.GetComponent<Body>().head.gameObject == this.gameObject))
            {
                minDistance = distance;
                target = objects[i].transform;
            }
        }
        if (target == null) return TaskStatus.Failure;
        this.target.Value = target;
        return TaskStatus.Success;
    }

}
