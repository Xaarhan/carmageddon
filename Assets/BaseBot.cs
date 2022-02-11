using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBot : BaseMob
{


    public const int STATE_STAY = 0;
    public const int STATE_ATTACK = 1;
    public const int STATE_BACK = 3;
    public const int STATE_HARVEST = 4;


    void Start()
    {
        _dynamyte_count = 0;
        _motor_enable = true;
        _target_point = new Vector3();
        setSpeed(wheelSpeed);
        setState(STATE_HARVEST);
        
    }


    protected override void onUpdate() {
        base.onUpdate();
        if ( _state_time <= 0 ) {
             getNextState();
        }

        _noHarvestTime -= Main.delta;
        switch (_state) {
            case STATE_ATTACK: {
                updateAttackTarget();
                break;
            }
            case STATE_BACK: {
                updateBack();
                break;
            }
            case STATE_HARVEST: {
                updateFindItem();
                break;
            }

        }

    }

  


    private void updateAttackTarget() {
        if ( _dynamyte_count == 0 && _noHarvestTime <= 0 ) {
             setState(STATE_HARVEST);
             return; 
        }

        if ( target == null || target.isDead ) {
             target = null;
             setState(STATE_HARVEST);
             return;
        }

        updateTargetMob();
        moveToTarget();
    }





    private void updateFindItem() {
        if ( _dynamyte_count > 1 ) {
             setState(STATE_ATTACK);
             return;
        }

        if (_target_item != null && _target_item.picked) {
            _target_item = null;
        }

        if ( _target_item == null ) {
             _target_item = Main.getItem(this);
             if ( _target_item == null ) {
                  _noHarvestTime = 1000;
                  setSpeed(0);
                  return;
             }  else {
                  setSpeed(wheelSpeed);
             }
        }

        if (_target_item == null) {
            setState(STATE_ATTACK);
            return;
        }

        if ( _dynamyte_count > 0 && target != null ) {
             Vector3 dist1 = transform.localPosition - target.transform.localPosition;
             Vector3 dist2 = transform.localPosition - _target_item.transform.localPosition;
             if ( dist1.magnitude < dist2.magnitude ) {
                  setState(STATE_ATTACK);
                  return;
             }
        }
        
        updateTargetItem();
        moveToTarget();
    }



    private void updateTargetItem() {
        _target_point = _target_item.transform.localPosition;
    }


  
    



    public void updateTargetMob() {
        _target_point = target.transform.localPosition;
    }


    public void updateTargetBack() {
        _target_point = target.transform.localPosition;
    }



    public void moveToTarget() {
        if ( !_motor_enable || _current_speed == 0 ) return;
        if ( _back_time >= 0) {
             _back_time -= Main.delta;
             return;
        } else {
             setSpeed( wheelSpeed );
        }

        Vector2 dir = new Vector2(transform.forward.x, transform.forward.z);
        Vector3 target_dir = new Vector2(_target_point.x - transform.localPosition.x, _target_point.z - transform.localPosition.z);
        float angle = Vector2.SignedAngle(dir, target_dir);
        if (Mathf.Abs(angle) > 1) {
            if (angle > 0) {
                car.setWeelRotation(-Mathf.Min(angle, 45));
            } else {
                car.setWeelRotation(Mathf.Min(-angle, 45));
            }
        } else {
            car.setWeelRotation(0);
        }
    }



    private void setState( int val ) {
        _state = val;
        switch (_state ) {
            case STATE_ATTACK: {
                _state_time = int.MaxValue;
                setSpeed(wheelSpeed);
                break;
            }
            case STATE_HARVEST: {
                _state_time = int.MaxValue;
                setSpeed(wheelSpeed);
                break;
            }
            case STATE_BACK: {
                _state_time = 1000;
                break;
            }

        }
    }



    protected override void onCollide(Collision collision) {
        base.onCollide(collision);
        if ( target != null && collision.gameObject == target.gameObject) {
           // onCollideTarget();
        } else {
            BaseBot bot = collision.gameObject.GetComponent<BaseBot>();
            if ( bot != null ) {
                 onCollideBot(bot);
            }
        }
    }

    protected override void onCollisionStay(Collision collision) {
        base.onCollisionStay(collision);
        if (target != null && collision.gameObject == target.gameObject) {
             _collision_stay_count++;
             if ( _collision_stay_count > 20 ) {
                  _collision_stay_count = 0;
                  onCollideTarget();
             }
        }
    }

    private void onCollideTarget() {
        _back_time = 1000;
        Vector2 dir = new Vector2(transform.forward.x, transform.forward.z);
        Vector3 target_dir = new Vector2(target.transform.localPosition.x  - transform.localPosition.x, target.transform.localPosition.z - transform.localPosition.z );
        float angle = Vector2.SignedAngle(dir, target_dir);
        if ( Mathf.Abs(angle) < 90f ) {
             setSpeed(-wheelSpeed);
        } else {
             setSpeed(-wheelSpeed);
        }

        if ( Mathf.Abs(angle) > 60 && Mathf.Abs(angle) < 120 ) {
             car.setWeelRotation(0);
        }

    }

    private void onCollideBot( BaseBot anotherBot ) {
        Vector2 dir = new Vector2(transform.forward.x, transform.forward.z);
        Vector3 bot_dir = new Vector2(anotherBot.transform.localPosition.x - transform.localPosition.x, anotherBot.transform.localPosition.z - transform.localPosition.z);
        float angle = Vector2.SignedAngle(dir, bot_dir);
        if (Mathf.Abs(angle) < 45) {
            _back_time = 2000;
            setSpeed(-wheelSpeed);
        }
    }



    private void setSpeed( float val ) {
        if (_current_speed == val) return;
        _current_speed = val;
        _move_back = val < 0;
        car.setWheelSpeed(val);
    }

    public override void useItemOn(ItemProps item, BaseMob target, Vector3 point) {
        base.useItemOn(item, target, point);
        if  ( item.name == "bomb" ) {
              _dynamyte_count = 0;
        }
       
    }


    public void setState() {

    }


    private void getNextState() {
        switch (_state) {
        case STATE_ATTACK: {
            setState(STATE_ATTACK);
            break;
        }
        case STATE_BACK: {
            setState(STATE_ATTACK);
            break;
        }

        }
    }


    private void updateBack() {

    }



    public override void PickItem(ItemProps picked) {
        base.PickItem(picked);
        if ( picked.name == "bomb" ) {
             _dynamyte_count += picked.count;
        }
    }


    // кого будем преследовать
    public void setTargetCar( BaseMob mob ) {
        target = mob;
    }






    private int _collision_stay_count;

    protected Vector3 _target_point;


    private int _dynamyte_count;
    private int _noHarvestTime;
    private int _state_time;
    private int _state;
    private int _back_time;
    public int wheelSpeed;
    protected BaseItem _target_item;
    public BaseMob target;
    private bool _motor_enable;
    private bool _move_back;
    private float _current_speed;


}
