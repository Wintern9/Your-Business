using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class JobListUpdate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private JobInfoLogic[] jobInfoLogic;

    bool ButtonEnable = true;

    private void Start()
    {
        DBValues.EmployerJob = new List<Employer>();
        for (int i = 0; i < jobInfoLogic.Length; i++)
        {
            AddEmployer();
            jobInfoLogic[i].EmployerJob = DBValues.EmployerJob[i];
            jobInfoLogic[i].UpdateInfo();
        }
    }

    public void UpdateViewStartAnimation()
    {
        if (ButtonEnable)
        {
            animator.SetBool("ButtonClick", true);
            StartCoroutine(DelayedAction());
            StartCoroutine(DelayedButton());
        }

        ButtonEnable = false;
        text.color = Color.gray;
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(3f);

        UpdateInfo();
    }

    IEnumerator DelayedButton()
    {
        yield return new WaitForSeconds(8f);
        Button();
    }

    void UpdateInfo()
    {
        Debug.Log("Coroutine");
        animator.SetBool("ButtonClick", false);

        DBValues.EmployerJob = new List<Employer>();
        for (int i = 0; i < jobInfoLogic.Length; i++)
        {
            AddEmployer();
            jobInfoLogic[i].EmployerJob = DBValues.EmployerJob[i];
            jobInfoLogic[i].UpdateInfo();
        }
    }

    void Button()
    {
        ButtonEnable = true;
        text.color = Color.white;
    }

    void AddEmployer()
    {
        int g = Random.Range(0, 2);
        string name = (g == 0) ? DBValues.JobsNS.Names[Random.Range(0, DBValues.JobsNS.Names.Length)].Male_name
                               : DBValues.JobsNS.Names[Random.Range(0, DBValues.JobsNS.Names.Length)].Female_name;
        string surname = (g == 0) ? DBValues.JobsNS.Surnames[Random.Range(0, DBValues.JobsNS.Surnames.Length)].Male_surname
                                  : DBValues.JobsNS.Surnames[Random.Range(0, DBValues.JobsNS.Surnames.Length)].Female_surname;

        JobNS jobNS = new JobNS(name, surname);

        JobName randomJob = (JobName)System.Enum.GetValues(typeof(JobName)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(JobName)).Length));
        JobSkill randomSkill = JobSkillGenerator.GetRandomSkillForJob(randomJob);

        Employer employer = new Employer(jobNS, randomJob, randomSkill, Random.Range(0,7));
        DBValues.EmployerJob.Add(employer);
    }

    /*
     1. Рандом имени
     2. Рандом навыков
     3. 
    */
}
