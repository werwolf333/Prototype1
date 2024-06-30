using SQLite4Unity3d;


public class TaskDB
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Message { get; set; }
    public string CountGood { get; set; }
    public string NameGood { get; set; }
}


public class PlayerDB
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    //---------------------дата и время---------------
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    //--------------------параметры героя-------------
    public int Power { get; set; }
    public int LimitPower { get; set; }
    public float Money { get; set; }
    public string Health { get; set; }
    public string Satiety { get; set; }
    //---положение игрок в мире и доступность путей-----
    public string PositionHero { get; set; }
    public bool lockAlimazon { get; set; }
    //public bool lockSlum { get; set; }
    public string Room { get; set; }
    public string Scene { get; set; }
    //------------------прохождение обучения-----------
    public bool Guide1Completed { get; set; }
    public bool Guide2Completed { get; set; }

}

public class SaveDB
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int CurSavePlayer { get; set; }
    //public string CurDoor { get; set; }
}
