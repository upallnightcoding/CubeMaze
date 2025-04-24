using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Ip/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;

    public GameObject weaponPrefab;
}
