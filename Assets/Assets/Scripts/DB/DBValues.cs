using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player
{
    public int ID { get; set; }
    private float money;

    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            BalanceView.BalanceChange();
        }
    }

    public string Settings { get; set; }

    public Player(int id, float initialMoney, string settings)
    {
        ID = id;
        money = initialMoney;
        Settings = settings;
    }

    public void Save()
    {
        MySQLConnection.SetPlayer(MySQLConnection.connectionString, this);
    }
}



public struct Credit
{
    public int ID { get; set; }
    public float Money { get; set; }
    private int repaid { get; set; }

    public int Repaid {
        get { return repaid; } 
        set
        { 
            repaid = value;
        } 
    }

    public Credit(int id, float money, int repaids)
    {
        ID = id;
        Money = money;
        repaid = repaids;
    }

    public Credit(float money, int repaids)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        repaid = repaids;
    }

    public Credit(float money)
    {
        ID = DBValues.Credit.Count+1;
        Money = money;
        repaid = 0;
    }

    public void UpdateRepaid()
    {
        MySQLConnection.UpdateCredits(MySQLConnection.connectionString, this);
    }
}

public struct HistoryCredit
{
    public int ID { get; set; }
    public float Money { get; set; }
}

public enum Room
{
    Room,
    RoomNT,
}
public enum TypeRoom
{
    Empty,
    Accountant,
    Marketing,
    Programmer
}


public struct Rooms
{
    public int ID { get; set; }
    public Room room { get; set; }
    public TypeRoom type { get; set; }
    public int level { get; set; }
    public Vector2 posRoom { get; set; }

    public Rooms(int id, Room room, TypeRoom type, int level, Vector2 posRoom)
    {
        ID = id;
        this.room = room;
        this.type = type;
        this.level = level;
        this.posRoom = posRoom;
    }
}

public struct JobsNames
{
    public string Male_name { get; set; }
    public string Female_name { get; set; }
}
public struct JobsSurnames
{
    public string Male_surname { get; set; }
    public string Female_surname { get; set; }
}

/// <summary>
/// Имя, Фамилия
/// </summary>
public struct JobNS
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public JobNS(string Name, string Surname)
    {
        this.Name = Name;
        this.Surname = Surname;
    }
}

public struct JobsNS
{
    public JobsNames[] Names { get; set; }
    public JobsSurnames[] Surnames { get; set; }

    public JobsNS(JobsNames[] Names, JobsSurnames[] Surnames)
    {
        this.Names = Names;
        this.Surnames = Surnames;
    }
}

/// <summary>
/// Тип работы
/// </summary>
public enum JobName
{
    Recruiter,
    Accountant,
    Marketing,
    Programmer
}

public struct JobSkill
{
    public string Name { get; set; }
    public int IndexSkill { get; set; }
    public JobName JobName { get; set; }
    public float Effect { get; set; }

    public JobSkill(string Name, int IndexSkill, JobName JobName , float Effect)
    {
        this.Name = Name;
        this.IndexSkill = IndexSkill;
        this.JobName = JobName;
        this.Effect = Effect;
    }
}

public struct Employer
{
    public JobNS JobNS { get; set; }
    public JobName JobName { get; set; }
    public JobSkill JobSkill { get; set; }
    public int YearDeveloping {get; set; }

    public Employer(JobNS JobNS, JobName JobName, JobSkill JobSkill, int YearDeveloping)
    {
        this.JobNS = JobNS;
        this.JobName = JobName;
        this.JobSkill = JobSkill;
        this.YearDeveloping = YearDeveloping;
    }
}

public struct Finder
{
    public bool Filter { get; set; }
    public bool Skills { get; set; }
    public int Employer { get; set; }

    public Finder(bool Filter, bool Skills, int Employer)
    {
        this.Filter = Filter;
        this.Skills = Skills;
        this.Employer = Employer;
    }
}

public struct CompanyJobPlaces
{
    public int Marketing { get; set; }
    public int Recruter { get; set; }
    public int Programmer { get; set; }

    public CompanyJobPlaces(int Marketing, int Recruter, int Programmer)
    {
        this.Marketing = Marketing;
        this.Recruter = Recruter;
        this.Programmer = Programmer;
    }
}

public class DBValues : MonoBehaviour
{
    public static int idCounter = -1;

    static public float MoneyValue;
    static public List<Credit> Credit = new List<Credit>();
    static public List<Rooms> Rooms = new List<Rooms>();
    static public Player Player = new Player();
    static public List<HistoryCredit> HistoryCredit = new List<HistoryCredit>();
    static public JobsNS JobsNS = new JobsNS();

    static public List<Employer> EmployerJob = new List<Employer>();
    static public List<Employer> EmployerCompany = new List<Employer>();

    static public CompanyJobPlaces CompanyJobPlaces = new CompanyJobPlaces();
}

public class JobSkillGenerator
{
    public static Dictionary<JobName, List<JobSkill>> skillPool = new Dictionary<JobName, List<JobSkill>>()
    {
        {
            JobName.Recruiter, new List<JobSkill>
            {
                new JobSkill("Открывает собеседование", 1, JobName.Recruiter, 1f),
                new JobSkill("Открывает фильтр", 2, JobName.Recruiter, 1f),
                new JobSkill("Добавляет слот в поисковике", 3, JobName.Recruiter, 1f)
            }
        },
        {
            JobName.Accountant, new List<JobSkill>
            {
                new JobSkill("Financial Reporting", 1, JobName.Accountant, 0.8f),
                new JobSkill("Budget Management", 2, JobName.Accountant, 0.87f)
            }
        },
        {
            JobName.Marketing, new List<JobSkill>
            {
                new JobSkill("Social Media", 1, JobName.Marketing, 0.75f),
                new JobSkill("Content Creation", 2, JobName.Marketing, 0.82f)
            }
        },
        {
            JobName.Programmer, new List<JobSkill>
            {
                new JobSkill("Coding", 1, JobName.Programmer, 0.9f),
                new JobSkill("Debugging", 2, JobName.Programmer, 0.88f)
            }
        }
    };

    public static JobSkill GetRandomSkillForJob(JobName jobName)
    {
        if (skillPool.ContainsKey(jobName))
        {
            var skills = skillPool[jobName];
            int randomIndex = Random.Range(0, skills.Count);
            return skills[randomIndex];
        }
        else
        {
            throw new System.Exception("No skills available for the selected job.");
        }
    }
}

