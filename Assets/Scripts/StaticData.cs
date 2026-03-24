
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class StaticData : MonoBehaviour
{
    //public class Resistances
    //{
    //    public List<ResistanceEntry> entries = new List<ResistanceEntry>();

    //    // Auto-fill list on changes in Inspector
    //    //Ensure all damage types are present exactly once.
    //    public void EnsureAllTypesPresent()
    //    {
    //        //Remove duplicates, keep first entry.
    //        List<DamageType> seenTypes = new List<DamageType>();
    //        entries.RemoveAll(entry =>
    //        {
    //            if (seenTypes.Contains(entry.type))
    //            { return true; } //remove duplicates
    //            seenTypes.Add(entry.type);
    //            return false;
    //        });

    //        foreach (DamageType type in System.Enum.GetValues(typeof(DamageType)))
    //        {
    //            if (!seenTypes.Contains(type))
    //            {
    //                entries.Add(new ResistanceEntry { type = type, value = 100 });
    //            }
    //        }
    //    }

        //// Returns the resistance value for a given damage type
        //public int Get(DamageType type)
        //{
        //    ResistanceEntry entry = entries.Find(e => e.type == type);
        //    return entry != null ? entry.value : 100; // default to 100 if not found
        //}

        //// Sets the resistance value for a given damage type
        //public void Set(DamageType type, int newValue)
        //{
        //    ResistanceEntry entry = entries.Find(e => e.type == type);
        //    if (entry != null)
        //    { entry.value = newValue; }
        //    else
        //    { entries.Add(new ResistanceEntry { type = type, value = newValue }); }
        //}

    //}
    

    public enum DamageType
    {
        Physical,
        Magical,
        Fire,
        Ice,
        Electric,
        Poison,
        Psychic,
        Dark,
        Holy,
        Fierce,
        Plasma,
        Healing,
    }

    
    public static Color GetElementColor(string element)
    {
        Color color = new Color();
        switch (element)
        {
            case "physical":
                //Orange
                color = ParseHexColor("#FFA500") * 1.1f; ;
                break;
            case "magical":
                //CrIMsOn
                color = ParseHexColor("#DC143C") * 1.3f; ;
                break;
            case "fire":
                //oRANGErED
                color = ParseHexColor("#FF4500") * 1.4f; ;
                break;
            case "ice":
                //MediumTurquoise
                color = ParseHexColor("#48D1CC") * 1.1f; ;
                break;
            case "electric":
                //cuSTOMblue
                color = ParseHexColor("#004CFF") * 1.4f; ;
                break;
            case "poison":
                //LIME
                color = ParseHexColor("#00FF00") * 1.4f;
                break;
            case "psychic":
                //deePpInK
                color = ParseHexColor("#FF1493") * 1.4f;
                break;
            case "dark":
                //BluevIOLET
                color = ParseHexColor("#8A2BE2") * 1f;
                break;
            case "holy":
                //KhAKI
                color = ParseHexColor("#F0E68C") * 1.4f;
                break;
            case "fierce":
                //AliceBlue
                color = ParseHexColor("#F0F8FF") * 1f;
                break;
            case "plasma":
                //MediumSpringGreen
                color = ParseHexColor("#00FA9A") * 1.4f;
                break;
            default:
                color = GetElementColor("physical");
                break;
        }
        return color;
    }
    public static int damageNumberOrder = 0; //max is 32767
    private static Color ParseHexColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            return newColor;
        }

        return Color.white;
    }
}
