using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    //Checks valids recepes
    private readonly List<Recipe> m_Recipes = new() 
    { 
        new Recipe(0, "Raclette", "Patate", "Salamit", "Fromage"),
        new Recipe(1, "Boisson du dragon", "Ketchup", "Poivron", "Cafe"),
        new Recipe(2, "Repas Noel", "Saumon", "Canne a sucre", "Gateau"),
        new Recipe(3, "Repas etudiant", "Coca", "Saucisson", "Pizza"),
        new Recipe(4, "Barbecue", "Brochette", "Steak", "Saucisse"),
        new Recipe(5, "Salade de fruits", "Pomme", "Banane", "Fraise"),
        new Recipe(6, "Hot Dog", "Saucisse", "Pain", "Ketchup"),
        new Recipe(7, "Pain a l'ail", "Pain", "Oignon", "Wasabi"),
        new Recipe(8, "Sandwish", "Pain", "Fromage", "Jambon"),
        new Recipe(9, "Soupe de legumes", "Patate", "Oignon", "Tomate"),
        new Recipe(10, "Pizza", "Tomate", "Fromage", "Pain"),
        new Recipe(11, "petit dej equilibre", "Cafe", "Cookie", "Pomme"),
        new Recipe(12, "Ratatouille", "Carotte", "Oignon", "Poivron"),
    };

    private static RecipeManager m_Instance = null;
    public static RecipeManager Instance => m_Instance;

    protected void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        if (m_Instance != this)
            Destroy(this);
    }

    public int IsValid(GameObject o1, GameObject o2, GameObject o3)
    {
        foreach (Recipe recepe in m_Recipes)
            if (recepe.CheckRecepe(o1, o2, o3))
                return recepe.id;
        return -1;
    }

    public string GetRecipeName(int recipeId)
    {
        foreach (Recipe recipe in m_Recipes)
            if (recipeId == recipe.id)
                return recipe.name;
        return null;
    }
}
