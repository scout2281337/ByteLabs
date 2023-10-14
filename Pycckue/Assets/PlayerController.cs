using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public bool isMoving = false;
    Rigidbody m_rig;
    Animator m_anim;

    private void Start()
    {
        m_rig = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                if (placeInfo.collider.CompareTag("Ground"))
                {
                    target = new Vector3(placeInfo.point.x, transform.position.y, placeInfo.point.z);
                    isMoving = true;
                }
            }
        }

        if (isMoving == true)
        {
            transform.LookAt(target);
            m_rig.velocity = (target - transform.position).normalized * speed;
            if (Vector3.Distance(target, transform.position) < 0.1)
            {
                isMoving = false;
            }
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (isMoving)
        {
            m_anim.SetBool("move", true);
        }
        else
        {
            m_anim.SetBool("move", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_anim.SetTrigger("attack");
        }
    }
}
