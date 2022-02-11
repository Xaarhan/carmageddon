using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _shared = this;
        delta = 0;
        sceneItems = FindObjectsOfType<BaseItem>();
    }

    // Update is called once per frame
    void Update() {
        delta = (int)(Time.deltaTime * 1000);
    }


    public static BaseItem getItem( BaseMob mob ) {
        float dist = int.MaxValue;
        BaseItem item = null ;
        BaseItem finded = null ;

        for ( int i = 0; i < sceneItems.Length; i++ ) {
              item = sceneItems[i];
             
              // это оптимизация, sqr чтобы лишний раз корень не считался, но вообще такое не обязательно
              float sqr_dist = (item.transform.localPosition - mob.transform.localPosition).sqrMagnitude;

              if ( !item.picked && sqr_dist < dist ) {
                    dist = sqr_dist;
                   finded = item; 
              }
        }

        return finded;
    }


    public static GameObject createObject( string name ) {
        Object prefab =  Resources.Load("models/" + name);
        GameObject model = null;
        if ( prefab != null ) {
             model = Instantiate(prefab) as GameObject;
        }
        return model;
    }

    public static void removeObject(GameObject obj) {
        Destroy(obj);
    }



    public static int delta;
    private static BaseItem[] sceneItems;
    private static Main _shared;
}
