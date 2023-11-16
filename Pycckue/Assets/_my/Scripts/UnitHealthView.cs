using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthView : MonoBehaviour
{
    [SerializeField] private Canvas _can;
    [SerializeField] private Image _bar;
    public void ReCalculateHealthView(Helth h)
    {
        _bar.fillAmount = h._maxHealth / h._curHealth;
        _can.transform.rotation = Quaternion.Euler(90, 0, 0);
            //.LookAt(Camera.main.transform.position);
    }
}
