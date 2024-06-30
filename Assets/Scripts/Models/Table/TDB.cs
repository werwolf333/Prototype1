using SQLite4Unity3d;
using UnityEngine;
using System.IO;// добавил
using System.Linq;// добавил
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class TDB : DataService
{
    public TDB(string DatabaseName) : base(DatabaseName)
    {
    }
//------------------------------работа с таблицами---------------------------------------

	public bool CheckDatabaseExists(string DatabaseName)
	{
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		return File.Exists(dbPath);
	}

    public void CreateTable<T>() where T : class
    {
        var type = typeof(T);
        _connection.CreateTable(type);
    }

    public bool TableExists(string type)
    {
        return _connection.GetTableInfo(type) != null;
    }

//---------------------------------работа со трокамми--------------------------------------

    public int AddRow<T>(T row) where T : class
    {
        var newRow = row;
        _connection.Insert(newRow);
        int insertedId = _connection.ExecuteScalar<int>("SELECT last_insert_rowid()");
        return insertedId;
    }

    public List<T> GetAllRows<T>() where T : class, new()
    {
        var type = typeof(T);
        return _connection.Table<T>().ToList();
    }

    public T GetRowById<T>(int id) where T : class, new()
    {
        var foundRow = _connection.Table<T>().FirstOrDefault(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
        return foundRow;
    }

    public void UpdateRow<T>(T row) where T : class, new()
    {
        _connection.Update(row);
    }
    
    public void DeleteRow<T>(T row) where T : class, new()
    {
        _connection.Delete(row);
    }
}
