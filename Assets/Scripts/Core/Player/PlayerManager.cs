using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PrimaryGun _primaryGun;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _primaryGun = GetComponent<PrimaryGun>();
    }

    public void IncreaseFireSpeed(float amt)
    {
        _primaryGun.FireRateModifier += amt;
    }
    public void IncreaseRange(float amt)
    {
        _primaryGun.BulletRange += amt;
    }

}
