using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobInfoLogic : MonoBehaviour
{
    public Employer EmployerJob;

    [SerializeField] private TextMeshProUGUI NameSurname;
    [SerializeField] private TextMeshProUGUI Job;
    [SerializeField] private EmployerInfo Еmployer;

    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(SubscribeOnClick);
    }

    public void UpdateInfo()
    {
        NameSurname.text = $"{EmployerJob.JobNS.Name} {EmployerJob.JobNS.Surname}";
        Job.text = $"{EmployerJob.JobName}";
    }

    void SubscribeOnClick()
    {
        Еmployer.EmployerJob = EmployerJob;
        Еmployer.UpdateInfo();
    }
}
