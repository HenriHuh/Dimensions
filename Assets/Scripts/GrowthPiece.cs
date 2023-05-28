using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthPiece : MonoBehaviour
{

    public bool TryGetNearbyEmptySlot(out Vector3 pos)
    {
        pos = Vector3.zero;

        for (int i = 0; i < 8; i++)
        {
            Vector3 cur = Tools.GetOrdinalDirection(i) * 2;


            if (!Physics.CheckBox(transform.position + cur, Vector3.one / 2, Quaternion.identity, GameManager.Instance.growthLayer))
            {
                pos = transform.position + cur;
                return true;
            }
        }


        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController playerController))
        {
            GameManager.Instance.ToggleLimbo(Tools.BooleanType.True);
        }

    }
}
