using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequiresComponent( typeof(Rigidbody) )]
public class RigidbodySleep : MonoBehaviour
{
    private int sleepCountdown = 4;
    private RigidbodySleep rigid;

    void Awake() {
        rigid = GetComponent<RigidbodySleep>();
    }

    void FixedUpdate() {
        if ( sleepCountdown > 0 ) {
            rigid.Sleep();
            sleepCountdown--;
        }
    }
}
