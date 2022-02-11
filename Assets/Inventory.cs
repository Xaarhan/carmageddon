using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    
    void Start(){
        oldHash = mob.uiHash;
        _icons = new List<ItemIcon>();
    }

    void Update() {
        if ( oldHash != mob.uiHash ) {
             updateItems(mob.items);
             oldHash = mob.uiHash;
        }
    }

    public void updateItems( List<ItemProps> mob_items ) {
        ItemIcon icon = null;
        for ( int i = 0; i < mob_items.Count; i++ ) {
              if ( _icons.Count < mob_items.Count) {
                   icon = addIcon();
              } else {
                   icon = _icons[i];
              }
              icon.gameObject.SetActive(true);
              icon.setCount(mob.items[i].count);

              // тут в будущем должна меняться иконка но раз только один предмет - то оставил так
              icon.setIcon(mob.items[i].icon);
        }

        int n = mob_items.Count;
        while ( n < _icons.Count) {
                _icons[n].gameObject.SetActive(false);
                n++;
        }

    }

    public ItemIcon addIcon() {
        ItemIcon icon = Instantiate(def_icon, transform);
        _icons.Add(icon);
        return icon;
    }


    private int oldHash;
    private List<ItemIcon> _icons;

    // чей инвентарь отображается
    public BaseMob mob;
    public ItemIcon def_icon;
}
