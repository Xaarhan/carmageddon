using UnityEngine;
using System.Collections;

public class BaseBuff
{

    public const int BUFF_EXPLODE = 1;


    // в баффах немного другой подход сделан, не через monobehaviors, в реальном проекте я бы выбрал один подход ко всему
  
    public BaseBuff() {

    }

    public virtual void init( BuffProps buffprops) {
        props = buffprops;
        elapsed = props.duration;
    }


    public virtual void onApply( BaseMob mob, Vector3 pos) {
        targetMob = mob;
        position = pos;

    }


    public virtual void update() {
        elapsed -= Main.delta;
    }


    public virtual void onDissapply() {

    }



    public static BaseBuff getBuff( int id ) {
        switch ( id ) {
            case (int)BuffTypes.EXPLODE: {
                return new BuffExplode();
                break;
            }
        }
        return null;
    }

    public BuffProps props;
    public int elapsed;
    public BaseMob targetMob;
    public Vector3 position;


}
