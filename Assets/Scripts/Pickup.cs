using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pickups
{
    Regenerate,
    WeaponUpgrade,
    ShipUpgrade,
    Shield
}

public class Pickup : MonoBehaviour
{
    public Pickups pickupType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerPlane>() != null)
        {
            switch (pickupType)
            {
                case (Pickups.Regenerate):
                    other.GetComponent<PlayerPlane>().Regenerate();
                    Debug.Log("Got upgrade: Regenerate");
                    Destroy(gameObject);
                    break;
                case (Pickups.WeaponUpgrade):
                    Debug.Log("Got upgrade: WeaponUpgrade");
                    break;
                case (Pickups.ShipUpgrade):
                    Debug.Log("Got upgrade: ShipUpgrade");
                    break;
                case (Pickups.Shield):
                    Debug.Log("Got upgrade: Shield");
                    break;


                default:
                    break;
            }
        }
    }
}
