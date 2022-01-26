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
    public static int hp = 100;
    public static Item Items = Item.None;
    public static int RemainingDays = 0;
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
}
