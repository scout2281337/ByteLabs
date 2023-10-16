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
    GameState m_state;

    private void Awake()
    {
        GameManager.OnGameStateChanged += StateChanged;
    }
    private void Start()
    {      
        m_rig = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }
    public void StateChanged(GameState state)
    {
        m_state = state;
        Debug.Log("HeroState");
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && m_state == GameState.HeroesTurn)
        {
            m_rig.velocity = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                if (placeInfo.collider.CompareTag("Grid"))
                {
                    var tile = GridManager.instance.GetTileAtPosition(new Vector2(placeInfo.collider.transform.position.x, placeInfo.collider.transform.position.z));
                    if(tile!= null && tile.currentState == TileState.empty)
                    {
                        target = new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z);
                        isMoving = true;
                        GameManager.instance.ChangeState(GameState.HeroMove);
                    }
                    else
                    {
                        if (tile == null) Debug.Log("undefined tile");
                        else Debug.Log("tile init error");
                    }
                }
            }
        }

        if (m_state == GameState.HeroMove)
        {
            transform.LookAt(target);
            m_rig.velocity = (target - transform.position).normalized * speed;
            if (Vector3.Distance(target, transform.position) < 0.3)
            {
                isMoving = false;
                GameManager.instance.ChangeState(GameState.HeroesTurn);
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
