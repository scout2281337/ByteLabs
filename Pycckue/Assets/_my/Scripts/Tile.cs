using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight; 
    [SerializeField] private GameObject _highlightEnter;
    private Collider _col;
    public TileState currentState;
    
    public void Init(bool isOffset, float _tileSize)
    {
        _col = GetComponent<Collider>();
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        transform.localScale = new Vector3(_tileSize, _tileSize, _tileSize);
        currentState = TileState.empty;
        foreach (var hitCollider in Physics.OverlapBox(transform.position, new Vector3(_tileSize / 2, _tileSize / 10, _tileSize / 2)))
        {
            if(hitCollider.transform.CompareTag("Ground")){
                currentState = TileState.obstacle;
                break;
            }
        }
        
    }

    void OnMouseEnter()
    {
        if (currentState == TileState.empty)
        {
            _highlight.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        _highlight.SetActive(false);
    }
}
public enum TileState
{
    empty,
    close,
    obstacle
}
