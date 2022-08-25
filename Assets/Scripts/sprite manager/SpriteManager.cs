using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public static class SpriteManager
{
    public static SpriteStruct red; //fireball
    public static SpriteStruct blue; //lightning
    public static SpriteStruct yellow; //barrier
    public static SpriteStruct green; //aoedot
    public static SpriteStruct purple; //stun

    public static int spriteSum { get { return (red.active + blue.active + yellow.active + green.active + purple.active); } }

    public static bool isDead;
    public static void Damaged(int damage)
    {
        if (damage > spriteSum)
        {
            isDead = true;
            return;
        }
        else
        {

        }
    }
}
public struct SpriteStruct
{
    public int active;
    int toAdd;
    int toRemove;

    public int ToAdd()
    {
        int temp = toAdd;
        toAdd = 0;
        return temp;
    }
    public void Add(int quantity)
    {
        active += quantity;
        toAdd = quantity;
    }
    public bool Remove(int quantity)
    {
        if (active - quantity >= 0)
        {
            active -= quantity;
            toRemove = quantity;
            return true;
        }
        else
            return false;
    }
    public int ToRemove()
    {
        int temp = toRemove;
        toRemove = 0;
        return temp;
    }
}
