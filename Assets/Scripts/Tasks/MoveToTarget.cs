using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class MoveToTarget : Action {
    public SharedFloat angle;
    public int moveDirection;// 1 forward -1 backward
    public SharedTransform target;
    public SharedBool shareSpeedUp;
    public bool speedUp;
    public override TaskStatus OnUpdate()
    {

        if (angle == null || target == null||target.Value==null) return TaskStatus.Failure;
        Vector3 dt= target.Value.position - transform.position;
        if (moveDirection < 0) dt = -dt;
        angle.Value = Vector3.Angle(dt, Vector3.right) * (dt.y > 0 ? 1 : -1);
        shareSpeedUp.Value = speedUp;
        return TaskStatus.Success;
    }

}
