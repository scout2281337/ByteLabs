using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] public float m_walkRange;
    [SerializeField] private bool m_isMoving = false;
    [SerializeField] private Vector3 m_target;
    [SerializeField] Renderer m_rend;
    [HideInInspector] private Helth m_health;
    [HideInInspector] private UnitHealthView m_healthView;
    private Rigidbody m_rig;
    private Animator m_anim;
    [HideInInspector] public Tile m_attachedTile;

    bool m_attack = false;
    private void Start()
    {
        m_rig = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_health = GetComponent<Helth>();
        m_healthView = GetComponent<UnitHealthView>();
    }

    private void Update()
    {
        if (m_isMoving)
        {
            transform.LookAt(m_target);
            m_rig.velocity = (m_target - transform.position).normalized * m_speed;
            if (Vector3.Distance(m_target, transform.position) < 0.3)
            {
                EndMove();
            }
        }

        m_healthView.ReCalculateHealthView(m_health);
        UpdateAnimation();
    }
    public void StartMove(Vector3 target)
    {
        m_isMoving = true;
        m_target = target;
    }
    public void EndMove()
    {
        transform.LookAt(transform.position + Vector3.forward);
        m_rig.velocity = Vector3.zero;
        m_isMoving = false;
        PlayerController.instance.HeroMovedToTarget();
    }
    public void ChangeTile(Tile tile)
    {
        m_attachedTile.State(TileState.emptyZone);
        m_attachedTile._attachedUnit = null;
        m_attachedTile = tile;
        m_attachedTile.State(TileState.hero);
        m_attachedTile._attachedUnit = this;
    }
    public void ChangeColor(bool ch)
    {
        if(ch) m_rend.material.SetFloat("_OtlWidth", 4.0f);
        else m_rend.material.SetFloat("_OtlWidth", 0f);
    }
    public void Attack()
    {
        m_attack = true;
    }
    public void GetDamage(float d)
    {
        m_health.GetDamage(d);
        if (!m_health.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
    void UpdateAnimation()
    {
        if (m_isMoving)
        {
            m_anim.SetBool("move", true);
        }
        else
        {
            m_anim.SetBool("move", false);
        }

        if (m_attack)
        {
            m_anim.SetTrigger("attack");
        }
        m_attack = false;

    }


}
