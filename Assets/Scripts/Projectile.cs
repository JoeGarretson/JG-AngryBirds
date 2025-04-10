using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody) )]
public class Projectile : MonoBehaviour
{
    const int LOOKBACK_COUNT = 10;
    static List<Projectile> PROJECTILES = new List<Projectile>(); //

    [SerializeField]
    private bool _awake = true;
    public bool awake {
        get { return _awake; }
        private set { _awake = value; }
    }

    private Vector3 prevPos;
    private List<float> deltas = new List<float>(); // Private list stores the history of Projectile's move distance
    private Rigidbody rigid;

    void Start(){
        rigid = GetComponent<Rigidbody>();
        awake = true;
        prevPos = new Vector3(1000,1000,0);
        deltas.Add( 1000 );
        PROJECTILES.Add( this );
    }

    void FixedUpdate() {
        if ( rigid.isKinematic || !awake ) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add( deltaV3.magnitude );
        prevPos = transform.position;

        // Limit lookback
        while ( deltas.Count > LOOKBACK_COUNT ) {
            deltas.RemoveAt( 0 );
        }

        // Iterate over deltas and find the greatest one

        float maxDelta = 0;

        foreach ( float f in deltas ) {
            if ( f > maxDelta ) maxDelta = f;
        }

        // If the Projectile hasnt moved more than the sleepThreshold
        if ( maxDelta <= Physics.sleepThreshold ) {
            // Set awake to false and put the Rigidbody to sleep
            awake = false;
            rigid.Sleep();
        }
    }

    private void OnDestroy() {
        PROJECTILES.Remove( this );
    }

    static public void DESTROY_PROJECTILES() {
        foreach ( Projectile p in PROJECTILES ) {
            Destroy( p.gameObject );
        }
    }
}
