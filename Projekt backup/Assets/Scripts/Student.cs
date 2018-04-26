using System.Collections.Generic;

[System.Serializable]
public class Student {
    public string name;
    public List<Item> inventory;

    public Student(string _name)
    {
        name = _name;
        inventory = new List<Item>();
    }
}

