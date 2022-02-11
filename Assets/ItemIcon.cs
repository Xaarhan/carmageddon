using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{


    public void setCount(int val) {
        text.text = val.ToString();
    }

    public void setIcon( string icon_name ) {
        if (_icon_name == icon_name) return;
        _icon_name = icon_name;
        // тут в будущем должна меняться иконка но раз только один предмет - то оставил так
    }

    private string _icon_name;

    public Text text;

}
