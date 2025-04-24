using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHoldCntrl : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform weaponPosition;

    private GameObject weapon;

    private float rotateSpeed = 125.0f;
    private float rotateAngle = 0.0f;
    private bool rotating = true;

    // Start is called before the first frame update
    void Start()
    {
        weapon = Instantiate(weaponSO.weaponPrefab, weaponPosition.position, Quaternion.identity);

        StartCoroutine(Turn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Turn()
    {
        while(rotating)
        {
            rotateAngle = rotateSpeed * Time.deltaTime;
            weapon.transform.Rotate(Vector3.up, rotateAngle);

            yield return null;
        }
    }
}
