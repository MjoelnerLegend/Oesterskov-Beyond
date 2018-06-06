using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    #region SQL connection variables
    OdbcConnection conn;
    OdbcCommand cmd;
    public string dsn;
    #endregion

    #region test variables
    public static List<Student> students;
    public static List<Group> groups;
    public static List<Item> itemDict;

    public List<Student> testStudents;
    public List<Group> testGroup;
    public List<Item> testItem;

    public GameObject groupgo;
    public GameObject infogo;
    #endregion

    private void FixedUpdate()
    {
        testStudents = students;
        testGroup = groups;
        testItem = DatabaseHandler.itemDict;
    }

    private void Start()
    {
        DatabaseHandler.students = new List<Student>();
        DatabaseHandler.groups = new List<Group>();
        DatabaseHandler.itemDict = new List<Item>();
        getStudents();

        foreach (Group g in DatabaseHandler.groups)
        {
            groupgo.GetComponent<groupPanel>().addGroup(g);
        }

        updateItemDict();
    }

    #region SQL Funcitons
    public void getStudents()
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        List<Student> _students = new List<Student>();
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge
            cmd.CommandText = "SELECT * FROM `elever`"; //skriv command 
            data = cmd.ExecuteReader();//execute commnand

            //Handle Data
            while (data.Read())
            {
                //Save student
                Student tmp = new Student(data["Navn"].ToString());
                _students.Add(tmp);
                DatabaseHandler.students = _students;

                //Add students to group
                if (DatabaseHandler.groups.Count > 0)
                {
                    bool added = false;
                    foreach (Group g in DatabaseHandler.groups.ToArray())
                    {
                        if (g.groupNumber == (int)data["GrpNr"])
                        {
                            g.members.Add(tmp);
                            added = true;
                        }
                    }
                    if (!added)
                    {
                        Group newGroup = new Group();
                        newGroup.groupNumber = (int)data["GrpNr"];
                        newGroup.members.Add(tmp);
                        DatabaseHandler.groups.Add(newGroup);
                    }
                }
                else
                {
                    Group newGroup = new Group();
                    newGroup.groupNumber = (int)data["GrpNr"];
                    newGroup.members.Add(tmp);
                    DatabaseHandler.groups.Add(newGroup);
                }
            }
            conn.Close(); //Connection is kill
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
        }

        //Correct Grouptable
        foreach (Group g in DatabaseHandler.groups.ToArray())
        {
            g.invTable = "`grp" + g.groupNumber + "`";
        }

    }

    public void getItemList()
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge
            cmd.CommandText = "SELECT * FROM `items`"; //skriv command 
            data = cmd.ExecuteReader();//execute commnand          
            conn.Close(); //Connection is kill
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
        }
}

    public void addItem()
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge
            cmd.CommandText = "SELECT * FROM `elever`"; //skriv command 
            data = cmd.ExecuteReader();//execute commnand          
            conn.Close(); //Connection is kill
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
        }
    }

    public void updateitems(List<Item> inventory, string table)
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge

            //empty table
            cmd.CommandText = "DELETE FROM " + table + " WHERE 1"; //skriv command
            var a = cmd.ExecuteNonQuery();//execute commnand       

            foreach (Item t in inventory)
            {
                cmd.CommandText = "INSERT INTO " + table + "(`ItemID`, `Amount`) VALUES ("+"'"+ t.id +"'"+ " , " + t.amount + ")"; //skriv command
                Debug.Log(cmd.CommandText);
                var e = cmd.ExecuteNonQuery();//execute commnand       
            }

            conn.Close(); //Connection is kill
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
            Debug.Log(table);
        }
    }

    public List<Item> getitems(string table)
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        List<Item> inventory = new List<Item>();
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge
            cmd.CommandText = "SELECT * FROM " + table;//skriv command 
            data = cmd.ExecuteReader();//execute commnand

            //Handle Data
            while (data.Read())
            {
                Item newItem = new Item();
                newItem.id = data["ItemID"].ToString();
                newItem.amount = (int)data["Amount"];
                inventory.Add(newItem);
            }
            conn.Close(); //Connection is kill
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
        }
        updateItemDict();
        return inventory;
    }

    public void updateItemDict()
    {
        conn = new OdbcConnection("dsn=" + dsn); //Etabler hvor forbindelsen skal skabes til
        OdbcDataReader data;//Data Storage
        List<Item> dict = new List<Item>();
        try
        {
            conn.Open(); //forsøg at connect til databasen (dsn)
            cmd = new OdbcCommand(); //lav en ny sql commando
            cmd.Connection = conn; //Specificer hvilken forbindelse den skal bruge
            cmd.CommandText = "SELECT * FROM `items`";//skriv command 
            data = cmd.ExecuteReader();//execute commnand

            //Handle Data
            while (data.Read())
            {
                Item newItem = new Item();
                newItem.id = data["Navn"].ToString();
                newItem.amount = 0;
                dict.Add(newItem);
            }
            conn.Close(); //Connection is kill
            DatabaseHandler.itemDict = dict;
        }
        catch (OdbcException caught)
        {//Error handling
            Debug.Log(caught.Message); //print the odbc error
        }
    } 

    #endregion

}
