using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pickups
{
    Regenerate,
    ShipUpgrade
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
                case (Pickups.ShipUpgrade):
                    Debug.Log("Got upgrade: ShipUpgrade");
                    break;
                default:
                    break;
            }
        }
    }
}
