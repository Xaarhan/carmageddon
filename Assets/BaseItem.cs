using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    void Start()
    {
        _collider = this.GetComponent<Collider>();
    }

    void Update() {
        if ( _elapsed > 0) {
             _elapsed -= Main.delta;
             if ( _elapsed <= 0 ) {
                  respawn();
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if ( other.gameObject.CompareTag("caracter")) {
             BaseMob caracter = other.gameObject.GetComponent<BaseMob>();
             caracter.PickItem( props.clone() );
             onPick();
        }
    }

    private void onPick() {
        container.SetActive(false);
        if (_collider != null) _collider.enabled = false;
        _elapsed = reload;
        _picked = true;
    }


    private void respawn() {
        container.SetActive(true);
        if (_collider != null) _collider.enabled = true;
        _picked = false;
    }


    public bool picked {
        get {
            return _picked;
        }
    }


    public int reload;
    [SerializeField]
    public ItemProps props;
    private int _elapsed;
    private bool _picked;


    private Collider _collider;
    public GameObject container;

}
