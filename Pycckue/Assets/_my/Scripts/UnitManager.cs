using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    [SerializeField] internal BaseUnit[] _units;
    [SerializeField] private BaseUnit[] _unitsEnemy;

    private void Awake()
    {
        instance = this;       
        //GameManager.OnGameStateChanged += SpawnHeroes;
    }

    public void SpawnHeroes()
    {
        PlayerController.instance.m_heroes = new BaseUnit[3];
        for (int i = 0; i < _units.Length; i++)
        {
            var un = Instantiate(_units[i].gameObject);
            PlayerController.instance.m_heroes[i] = un.GetComponent<BaseUnit>();
            var spawnTile = GridManager.instance.GetTileAtPosition(new Vector2(i, 0));
            un.transform.position = spawnTile.transform.position;
            un.GetComponent<BaseUnit>().m_attachedTile = spawnTile;
            spawnTile.currentState = TileState.hero;
            spawnTile._attachedUnit = un.GetComponent<BaseUnit>();
        }

    }
    public void SpawnEnemy()
    {
        for (int i = 0; i < _unitsEnemy.Length; i++)
        {
            var un = Instantiate(_unitsEnemy[i].gameObject);
            var spawnTile = GridManager.instance.GetTileAtPosition(new Vector2(i, 4));
            un.transform.position = spawnTile.transform.position;
            un.GetComponent<BaseUnit>().m_attachedTile = spawnTile;
            spawnTile.currentState = TileState.enemy;
            spawnTile._attachedUnit = un.GetComponent<BaseUnit>();
        }

    }
}
