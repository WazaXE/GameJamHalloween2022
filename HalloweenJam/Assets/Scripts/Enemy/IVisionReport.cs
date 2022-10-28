using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisionReport
{
    public void ReportCanSeeTarget();
    public void ReportLostTarget();
    public void ReportIsInAttackRange();
}
