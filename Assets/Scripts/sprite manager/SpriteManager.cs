using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public static class SpriteManager
{
    public static SpriteStruct red = new(0, 0); //fireball
    public static SpriteStruct blue = new(1, 0); //lightning
    public static SpriteStruct yellow = new(2, 0); //barrier
    public static SpriteStruct green = new(3, 0); //aoedot
    public static SpriteStruct purple = new(4, 0); //stun

    public static int spriteSum { get { return (red.active + blue.active + yellow.active + green.active + purple.active); } }
    public static int spriteMin { get { return Mathf.Min(red.active, blue.active, yellow.active, green.active, purple.active); } }

    static bool isDead;
    public static bool diedThisFrame = false;
    public static bool wasDamaged;
    public static bool IsDead { get { return isDead; } set { isDead = diedThisFrame = value; } }
    public static void Damage(int damage)
    {
        List<SpriteStruct> list = new List<SpriteStruct>();
        list.Add(red);
        list.Add(blue);
        list.Add(yellow);
        list.Add(green);
        list.Add(purple);
        AudioManager.Play("DamagedSound");

        if (damage > spriteSum)
        {
            IsDead = true;
            return;
        }
        else
        {
            for(int i = 0; i < 5; i++)
            {
                int index = Random.Range(0,list.Count-1);
                int subtract = Mathf.Min(list[index].active, damage);

                damage -= subtract;

                RemoveIndex(list[index].index, subtract);

                if (damage <= 0)
                {
                    break;
                }
                list.RemoveAt(index);
            }

            if (spriteSum <= 0) IsDead = true;
            wasDamaged = true;
        }
    }
    static void RemoveIndex(int index, int quantity)
    {
        if (index == 0) red.Remove(quantity);
        if (index == 1) blue.Remove(quantity);
        if (index == 2) yellow.Remove(quantity);
        if (index == 3) green.Remove(quantity);
        if (index == 4) purple.Remove(quantity);
    }
}
public struct SpriteStruct
{
    public int index;
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

    public SpriteStruct(int _index,int starting)
    {
        index = _index;
        toAdd = starting;
        toRemove = 0;
        active = starting;
    }
}
