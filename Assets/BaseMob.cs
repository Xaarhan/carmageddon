using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseMob : MonoBehaviour
{

    void Start() {

    }

    void Update() {
        //смерть можно проверять по коллайдеру игровой зоны, но пока что так

        if (_isDead) return;

        if ( transform.position.y < -3f ) { // считаем что свалились с арены
             _isDead = true;
             return;
        }

        onUpdate();
    }

    protected virtual void onUpdate() {
        updateBuffs();
    }


    protected virtual void updateBuffs() {
        int i = 0;
        while ( i < _buffs.Count ) {
            BaseBuff buff = _buffs[i];
            buff.update();
            if ( buff.elapsed <= 0 ) {
                 dissapllyBuff(buff);
            } else {
                 i++;
            }
        }
       
    }

    protected virtual void dissapllyBuff(BaseBuff buff) {
        buff.onDissapply();
        removeBuff(buff);
    }


    protected virtual void removeBuff(BaseBuff buff) {
        _buffs.Remove(buff);
    }

    public virtual void PickItem(ItemProps picked) {
        for (int i = 0; i < items.Count; i++) {
            ItemProps item = items[i];
            if (picked.type == item.type) {
                item.count++;
                uiHash++;
                return;
            }
        }

        items.Add(picked);
        uiHash++;
    }


    public virtual void removeItem( ItemProps item ) {
        items.Remove(item);
        uiHash++;
    }



    public virtual void UpdateCollideItems(BaseMob target, Vector3 point) {

        if (  !target.car.GetComponent<Collider>().bounds.Contains( car.attackPoint.transform.position )  ) {
              return;
        }

        for ( int i = 0; i < items.Count; i++) {
              ItemProps item = items[i];
              if ( item.type == ItemTypes.ON_COLLIDE_FRONT ) {
                   if ( item.targetType == TargetTypes.ALL 
                        ||  item.targetType == TargetTypes.ENEMY &&  target.owner != owner 
                        || item.targetType == TargetTypes.ALLY && target.owner == owner) {
                        useItemOn(item, target, car.attackPoint.transform.position);
                   }
              }
        }
    }

    public virtual void useItemOn(ItemProps item, BaseMob target, Vector3 point) {
        BaseBuff buff = BaseBuff.getBuff((int)item.buffType);

        // тут создаю параметры баффа вручную, но при разработке здесь будет что то вроде BuffProps buffprops = Config.getBuffProps( buff_id );
        BuffProps buffprops = new BuffProps();
        buffprops.value = item.count;
        buffprops.duration = 5000;
        buff.init(buffprops);
        target.addBuff(buff, point);
        removeItem(item);
    }


    protected virtual void onCollide(Collision collision) {
        BaseMob mob = collision.gameObject.GetComponent<BaseMob>();
        if (mob != null) {
            UpdateCollideItems(mob, collision.contacts[0].point);
        }
    }


    void OnCollisionEnter(Collision collision) {
        onCollide(collision);
    }


    void OnCollisionStay(Collision collision) {
        onCollisionStay(collision);
    }

    protected virtual void onCollisionStay(Collision collision) {
        onCollide(collision);
    }


    protected virtual void addBuff( BaseBuff buff, Vector3 pos ) {
        // в баффах немного другой подход сделан, не через monobehaviors, в реальном проекте я бы выбрал один подход ко всему
        _buffs.Add(buff);
        buff.onApply(this, pos);
    }


    public bool isDead {
        get {
            return _isDead;
        }
    }


    public int uiHash;
    public BaseCar car;
    protected bool _isDead;
    public int owner;

    public List<ItemProps> items = new List<ItemProps>();
    protected List<BaseBuff> _buffs = new List<BaseBuff>();
   

}
