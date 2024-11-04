using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmployerInfo : MonoBehaviour
{
    public Employer EmployerJob;

    [SerializeField] private TextMeshProUGUI NameSurname;
    [SerializeField] private TextMeshProUGUI Job;
    [SerializeField] private TextMeshProUGUI YearDeveloper;
    [SerializeField] private TextMeshProUGUI Skills;

    public void UpdateInfo()
    {
        NameSurname.text = $"{EmployerJob.JobNS.Name} {EmployerJob.JobNS.Surname}";
        Job.text = $"{EmployerJob.JobName}";
        YearDeveloper.text = $"{EmployerJob.YearDeveloping} года (лет)";
        Skills.text = $"{EmployerJob.JobSkill.Name} {EmployerJob.JobSkill.Effect}";
    }
}
