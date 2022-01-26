using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item : int
{
    None = 0,
    InfoKillingLicense = 1 << 0,
    Hamburger = 1 << 1,
    Fairly = 1 << 2,
    Rudolf = 1 << 3,
    Rayder = 1 << 4,
    Vaccine = 1 << 5,
} 

public static class CharacterStat
{
    public static int Hp = 100;
    public static Item Items = Item.None;
    public static int RemainingDays = 357;
    public static int Money = 0;

    public static void AddItem(Item item)
    {
        Items |= item;
    }

    public static void RemoveItem(Item item)
    {
        Items &= ~item;
    }

    public static bool HasItem(Item item)
    {
        return (Items & item) == item;
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("Hp", Hp);
        PlayerPrefs.SetInt("Items", (int)Items);
        PlayerPrefs.SetInt("RemainingDays", RemainingDays);
        PlayerPrefs.SetInt("Money", Money);
    }

    public static void LoadData()
    {
        Hp = PlayerPrefs.GetInt("Hp", Hp);
        Items = (Item)PlayerPrefs.GetInt("Items", (int)Items);
        RemainingDays = PlayerPrefs.GetInt("RemainingDays", RemainingDays);
        Money = PlayerPrefs.GetInt("Money", Money);
    }
}
