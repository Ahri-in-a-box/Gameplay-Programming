using System.Collections.Generic;
using UnityEngine;

internal class Recepe
{
    public int id;
    public readonly string name;
    private readonly string[] elements = new string[3];

    public Recepe(int id, string name, string o1, string o2, string o3)
    {
        this.id = id;
        this.name = name;
        List<string> objects = new() { o1.ToLower(), o2.ToLower(), o3.ToLower() };
        objects.Sort((o1, o2) => o1.CompareTo(o2));
        elements = objects.ToArray();
    }

    public bool CheckRecepe(GameObject o1, GameObject o2, GameObject o3)
    {
        List<string> objects = new() { o1.name.ToLower(), o2.name.ToLower(), o3.name.ToLower() };
        objects.Sort((o1, o2) => o1.CompareTo(o2));
        return objects.ToArray() == elements;
    }
}

public class RecepeManager : MonoBehaviour
{
    //Checks valids recepes
    private List<Recepe> m_Recepes = new() 
    { 
        new Recepe(0, "Raclette", "Patate", "Salamit", "Fromage"),
        new Recepe(1, "Boisson du dragon", "Ketchup", "Poivron", "Cafe"),
        new Recepe(2, "Repas Noel", "Saumon", "Canne a sucre", "Gateau"),
        new Recepe(3, "Repas etudiant", "Coca", "Saucisson", "Pizza"),
        new Recepe(4, "Barbecue", "Brochette", "Steak", "Saucisse"),
        new Recepe(5, "Salade de fruits", "Pomme", "Banane", "Fraise"),
        new Recepe(6, "Hot Dog", "Saucisse", "Pain", "Ketchup"),
        new Recepe(7, "Pain a l'ail", "Pain", "Oignon", "Wasabi"),
        new Recepe(8, "Sandwish", "Pain", "Fromage", "Jambon"),
        new Recepe(9, "Soupe de legumes", "Patate", "Oignon", "Tomate"),
        new Recepe(10, "Pizza", "Tomate", "Fromage", "Pain"),
        new Recepe(11, "petit dej equilibre", "Cafe", "Cookie", "Pomme"),
        new Recepe(12, "Ratatouille", "Carotte", "Oignon", "Poivron"),
    };

    public int IsValid(GameObject o1, GameObject o2, GameObject o3)
    {
        foreach (Recepe recepe in m_Recepes)
            if (recepe.CheckRecepe(o1, o2, o3))
                return recepe.id;
        return -1;
    }
}
