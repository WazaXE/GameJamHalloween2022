using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisionReport
{
    public void ReportCanSeeTarget(ITarget target);
    public void ReportLostTarget();
    public void ReportIsInAttackRange(ITarget target);
}
