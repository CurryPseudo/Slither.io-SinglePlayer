using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class EscapeBorder : Action {
    public SharedBool speedUp;
    public SharedFloat borderDistance;
    public SharedFloat angle;
    public override TaskStatus OnUpdate()
    {
        if (transform.position.x > 40 - borderDistance.Value || transform.position.x < -40 + borderDistance.Value)
        {
            angle.Value = transform.position.x > 0 ? 180 : 0;
            speedUp = false;
            return TaskStatus.Success;
        }
        if (transform.position.y > 22.5 - borderDistance.Value || transform.position.y < -22.5 + borderDistance.Value)
        {
            angle.Value = transform.position.y > 0 ? -90 : 90;
            speedUp = false;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
