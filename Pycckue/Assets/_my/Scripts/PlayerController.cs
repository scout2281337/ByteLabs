using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  
    [SerializeField] public Vector3 target;
    [SerializeField] public BaseUnit[] m_heroes;
    [SerializeField] public BaseUnit m_currentHero;
    [SerializeField] internal UnitManager m_unitMan;

    private GameState m_gameState;
    public PlayerState m_playerState;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        m_playerState = PlayerState.Idle;
        GameManager.OnGameStateChanged += StateChanged;
    }


    public void StateChanged(GameState state)
    {
        m_gameState = state;
        if(m_gameState == GameState.HeroesTurn)
        {
            for(int i = 0; i < m_heroes.Length; i++)
            {
                m_heroes[i].haveAttackPoint = true;
                m_heroes[i].haveWalkPoint = true;
            }
        }
    }

    private void Update()
    {
        if (m_gameState == GameState.HeroesTurn)
        {
            CheckHeroes();
        }
        
        if (Input.GetMouseButtonUp(0) && m_gameState == GameState.HeroesTurn && m_playerState != PlayerState.HeroMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                if (placeInfo.collider.CompareTag("Grid"))
                {
                    var tile = placeInfo.collider.GetComponent<Tile>();
                    if (tile == null) Debug.Log("undefined tile");
                    if (m_playerState == PlayerState.ChangeHero)
                    {
                        if (tile.currentState == TileState.emptyZone && m_currentHero != null && m_currentHero.haveWalkPoint)
                        {
                            target = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
                            m_playerState = PlayerState.HeroMove;
                            m_currentHero.ChangeTile(tile);
                            m_currentHero.ChangeColor(false);
                            m_currentHero.StartMove(target);
                        }
                        else if(tile.currentState == TileState.hero)
                        {
                            if (tile._attachedUnit.haveWalkPoint || tile._attachedUnit.haveAttackPoint)
                            {
                                if (m_currentHero != null) m_currentHero.ChangeColor(false);
                                m_currentHero = tile._attachedUnit;
                                m_currentHero.ChangeColor(true);
                                m_playerState = PlayerState.ChangeHero;
                            }
                        }
                    }
                    else if (m_playerState == PlayerState.Idle)
                    {
                        if (tile.currentState == TileState.hero)
                        {
                            if (tile._attachedUnit.haveWalkPoint || tile._attachedUnit.haveAttackPoint)
                            {
                                if (m_currentHero != null) m_currentHero.ChangeColor(false);
                                m_currentHero = tile._attachedUnit;
                                m_currentHero.ChangeColor(true);
                                m_playerState = PlayerState.ChangeHero;
                            }
                        }
                    }
                }
            }
            else
            {
                m_currentHero.ChangeColor(false);
                m_playerState = PlayerState.Idle;
            }
        }

        if (Input.GetMouseButtonDown(1) && m_gameState == GameState.HeroesTurn && m_playerState == PlayerState.ChangeHero && m_currentHero.haveAttackPoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                if (placeInfo.collider.CompareTag("Grid"))
                {
                    var tile = placeInfo.collider.GetComponent<Tile>();
                    if (tile == null) Debug.Log("undefined tile");

                    if (tile.currentState == TileState.enemy)
                    {                        
                        m_currentHero.StartAttack(tile._attachedUnit.transform.position + Vector3.up);                       
                    }
                    else if (tile.currentState == TileState.enemyZone)
                    {

                    }                   
                }
            }
            else
            {
                m_currentHero.ChangeColor(false);
                m_playerState = PlayerState.Idle;
            }
        }
    }
    public void CheckHeroes()
    {
        bool flag = true;
        for (int i = 0; i < m_heroes.Length; i++)
        {
            if(m_heroes[i].haveAttackPoint || m_heroes[i].haveWalkPoint)
            {
                flag = false;
            }
        }
        if (flag)
        {
            GameManager.instance.ChangeState(GameState.EnemiesTurn);
            Debug.Log("aaaaaaaaaa");
        }
    }
    public void HeroMovedToTarget()
    {
        m_playerState = PlayerState.ChangeHero;
        m_currentHero.ChangeColor(true);
    }
}

public enum PlayerState
{
    Idle = 0,
    ChangeHero = 1,
    HeroMove = 2
}
