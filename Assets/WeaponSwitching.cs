using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    
    List<Weapon> weapons;
    Weapon activeWeapon;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start ()
    {
        SetWeapons(new List<Weapon>(GetComponent<Entity>().obj_sprite.GetComponentsInChildren<Weapon>(true)));
        SetActiveWeapon(Weapon.E_WEAPON_TYPE.MINIGUN);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetWeapons(List<Weapon> weapons)
    {
        this.weapons = weapons;
    }

    public bool SetActiveWeapon(Weapon.E_WEAPON_TYPE weaponType)
    {
        foreach(Weapon weapon in weapons) {
            if (weaponType == weapon.weaponType) {
                if (activeWeapon != null)
                    activeWeapon.gameObject.SetActive(false);
                activeWeapon = weapon;
                activeWeapon.gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
