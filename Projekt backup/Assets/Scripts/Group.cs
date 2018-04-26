using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Group{
    public int groupNumber;
    public string invTable;
    public List<Student> members;
    public List<Item> inventory;

    public Group()
    {
        members = new List<Student>();
        inventory = new List<Item>();
    }
}
