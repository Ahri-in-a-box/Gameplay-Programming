using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public int id;
    public readonly string name;
    private readonly string[] elements = new string[3];

    public Recipe(int id, string name, string o1, string o2, string o3)
    {
        this.id = id;
        this.name = name;
        List<string> objects = new() { o1.ToLower(), o2.ToLower(), o3.ToLower() };
        objects.Sort((o1, o2) => o1.CompareTo(o2));
        elements = objects.ToArray();
    }

    public bool CheckRecepe(GameObject o1, GameObject o2, GameObject o3)
    {
        List<string> objects = new() { o1.tag.ToLower(), o2.tag.ToLower(), o3.tag.ToLower() };
        objects.Sort((o1, o2) => o1.CompareTo(o2));
        for (int i = 0; i < 3; i++)
            if (objects[i].CompareTo(elements[i]) != 0)
                return false;
        return true;
    }
}
