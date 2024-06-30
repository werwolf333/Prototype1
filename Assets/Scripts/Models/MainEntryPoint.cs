using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Reflection;
using SQLite4Unity3d;


public class MainEntryPoint : MonoBehaviour {

    private TDB db;

	public void JustStart() {
        db = new TDB("DB.db");
        db.CreateTable<SaveDB>();
	}

    //-------------------------------------------------------------------
    public int AddRow<T>(T newData) where T: class
    {
        return db.AddRow(newData);
    }

    /*public List<T> LoadRows<T>() where T : class, new()
    {
        Debug.Log(db.TableExists(typeof(T).Name));
        return db.GetAllRows<T>();
    }*/
    public List<T> LoadRows<T>() where T : class, new()
    {
        try
        {
            if (db.TableExists(typeof(T).Name))
            {
                return db.GetAllRows<T>();
            }
            else
            {
                Debug.LogError($"Table for type {typeof(T).Name} does not exist.");
                return new List<T>(); // Возвращаем пустой список, чтобы избежать null reference исключений
            }
        }
        catch (SQLiteException ex)
        {
            Debug.LogError($"SQLiteException caught: {ex.Message}");
            return new List<T>(); // Возвращаем пустой список в случае исключения
        }
    }

    public T GetRow<T>(int id) where T : class, new()
    {
        return db.GetRowById<T>(id);
    }

    public void UpdateRow<T>(T row) where T : class, new()
    {
        db.UpdateRow(row);
    }

    public void DeleteRow<T>(T row) where T : class, new()
    {
        db.DeleteRow(row);
    }

    //===============================================================//
    public string ConvertIntArrayToString(int[] numbersArray)
    {
        string[] numberStrings = new string[numbersArray.Length];
        for (int i = 0; i < numbersArray.Length; i++)
        {
            numberStrings[i] = numbersArray[i].ToString();
        }
        string numbersString = string.Join(",", numberStrings);

        return numbersString;
    }

    public int[] ConvertStringToIntArray(string numbersString)
    {
        string[] numberStrings = numbersString.Split(',');
        int[] numbersArray = new int[numberStrings.Length];
        for (int i = 0; i < numberStrings.Length; i++)
        {
            int.TryParse(numberStrings[i], out numbersArray[i]);
        }
        return numbersArray;
    }

public string[] ConvertStringToStringArray(string inputString)
{
    string[] stringArray = inputString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    
    for (int i = 0; i < stringArray.Length; i++)
    {
        stringArray[i] = stringArray[i].Trim();
    }

    return stringArray;
}
}