using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    [SerializeField] private BaseUnit[] _units;

    private void Awake()
    {
        instance = this;       
        //GameManager.OnGameStateChanged += SpawnHeroes;
    }

    public void SpawnHeroes()
    {
        for (int i = 0; i < _units.Length; i++)
        {
            var un = Instantiate(_units[i].gameObject);
            var spawnTile = GridManager.instance.GetTileAtPosition(new Vector2(i, 0));
            un.transform.position = spawnTile.transform.position;
            un.GetComponent<BaseUnit>().m_attachedTile = spawnTile;
            spawnTile.currentState = TileState.hero;
            spawnTile._attachedUnit = un.GetComponent<BaseUnit>();
        }

    }
}
