using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponSwitch : MonoBehaviour {
    public Image itemFrame1;//minigun
    public Image weapon1;
    public Image itemFrame2;//faster minigun
    public Image weapon2;

    // Use this for initialization
    void Start () {
        setLighting();
    }
    private void setLighting()
    {
        int equippedWeapon = PlayerPrefs.GetInt("equip", 1);
        setDark();
        switch (equippedWeapon)
        {
            case 1:
                colorToLight(itemFrame1);
                colorToLight(weapon1);
                break;
            case 2:
                colorToLight(itemFrame2);
                colorToLight(weapon2);
                break;     
        }
    }
    private void setDark()
    {
        colorToDark(itemFrame1);
        colorToDark(weapon1);
        colorToDark(itemFrame2);
        colorToDark(weapon2);
    }
    private void colorToDark(Image item)
    {
        item.color = new Color(item.color.r, item.color.g,item.color.b, 0.5f);
    }
    private void colorToLight(Image item)
    {
        item.color = new Color(item.color.r, item.color.g, item.color.b, 0.8f);
    }

    public void onSwitch(int weaponNumber)
    {
        PlayerPrefs.SetInt("equip", weaponNumber);
        setLighting();
    }
}
