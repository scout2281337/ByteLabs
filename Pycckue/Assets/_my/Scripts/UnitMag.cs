using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMag : BaseUnit
{
    [SerializeField] GameObject m_baseFireBall;
    public override void StartAttack(Vector3 target)
    {
        var a = Instantiate(m_baseFireBall, transform.position + Vector3.up, Quaternion.identity);
        a.GetComponent<FireBall>().Init(5, target, 5);
    }
}
