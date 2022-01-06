using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if(playerController)
        {
            GameManager.Instance.EatOrb();
            playerController.IncreasePlayerMass();
            Destroy(gameObject);
        }
    }
}
