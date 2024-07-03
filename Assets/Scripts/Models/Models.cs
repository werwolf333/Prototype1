using SQLite4Unity3d;


public class UnitDB
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Type { get; set; }
    public float Health { get; set; } 
    public float Stamina { get; set; }
    public float Equilibrium { get; set; }
    public float Mana { get; set; }
    public float Damage { get; set; }
    public float Protection { get; set; }
    public float AnimationSpeed { get; set; } //сомнительно, но окей
    public float AttackSpeed { get; set; } 
    public float RunningSpeed { get; set; }
    public float Level { get; set; }
}
