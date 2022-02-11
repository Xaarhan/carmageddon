using UnityEngine;
using System.Collections;

public class BuffExplode : BaseBuff
{

    public  BuffExplode() {

    }


    public override void onApply(BaseMob mob, Vector3 pos ) {
        base.onApply(mob, pos);
        anim = Main.createObject("buff_explode");
        anim.transform.parent = mob.transform;
        anim.transform.position = pos;
    }



    public override void onDissapply() {
        Rigidbody body = targetMob.GetComponent<Rigidbody>();
        if ( body != null ) {
             Vector3 fpos = -anim.transform.position;
             fpos.Normalize();
             Debug.DrawLine(anim.transform.position, targetMob.transform.position, Color.red, 20);
             body.AddRelativeForce(fpos * (300 * props.value), ForceMode.Impulse);
        }
        Main.DestroyObject(anim);
    }


    private GameObject anim;

}
