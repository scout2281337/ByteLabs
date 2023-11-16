using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight; 
    [SerializeField] private GameObject _highlightForAttack;
    [SerializeField] public BaseUnit _attachedUnit;
    [SerializeField] private Vector2 _index;
    public TileState currentState;
    
    
    public void Init(float _tileSize, Vector2 ind)
    {
        _renderer.color = _baseColor;
        transform.localScale = new Vector3(_tileSize, _tileSize, _tileSize);
        _index = ind;
    }

    void OnMouseEnter()
    {
        if (currentState == TileState.emptyZone && PlayerController.instance.m_playerState == PlayerState.ChangeHero||
            currentState == TileState.hero && PlayerController.instance.m_playerState == PlayerState.Idle)
        {
            _highlight.SetActive(true);
        }
        else if (currentState == TileState.enemy && PlayerController.instance.m_playerState == PlayerState.ChangeHero)
        {
            _highlightForAttack.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        _highlightForAttack.SetActive(false);
    }

    private void OnMouseDown()
    {
        _highlight.SetActive(false);
        _highlightForAttack.SetActive(false);
    }

    public void State(TileState state)
    {
        currentState = state;
    }
}
public enum TileState
{
    hero,
    enemy,
    emptyZone,
    enemyZone
}
