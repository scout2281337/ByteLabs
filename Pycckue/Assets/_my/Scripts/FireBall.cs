using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Vector3 m_target;
    [SerializeField] float m_damage;
    [SerializeField] GameObject m_deathEffect;
    Rigidbody m_rig;
    private void Start()
    {
        m_rig = GetComponent<Rigidbody>();
    }
    public void Init(float speed, Vector3 target, float damage)
    {
        m_speed = speed;
        m_target = target;
        m_damage = damage;
    }

    private void Update()
    {
        m_rig.velocity = (m_target - transform.position).normalized * m_speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var oth = other.GetComponent<BaseUnit>();
        if (oth != null && oth.m_attachedTile.currentState == TileState.enemy)
        {
            oth.GetDamage(m_damage);
            var death = Instantiate(m_deathEffect, transform.position, Quaternion.identity);
            death.GetComponent<ParticleSystem>().Play();
            Destroy(death, 8);
            Destroy(gameObject);
        }
    }

}
