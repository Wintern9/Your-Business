using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HireEmployer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image imageComponent;
    
    float procentBar = 0f;
    
    private bool isMouseButtonDown;

    [SerializeField] private GameObject employerInfoObject;
    [SerializeField] private GameObject employerHireObject;
    [SerializeField] private GameObject employerNotHireObject;

    private void Awake()
    {
        if (imageComponent == null)
            imageComponent = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isMouseButtonDown = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isMouseButtonDown = false;
        }
    }

    private void Update()
    {
        if (isMouseButtonDown)
        {
            if (procentBar < 1f)
                procentBar += Time.deltaTime;
        } else
        {
            if (procentBar > 0f)
                procentBar -= Time.deltaTime;
            else
                procentBar = 0f;
        }

        imageComponent.fillAmount = procentBar;

        if(procentBar >= 1f)
        {
            if (true)
                EmployerHire();
            else
                EmployerNotHire();
        }
    }

    void EmployerHire()
    {
        if (DBValues.CompanyJobPlaces.Recruter >= 0)
        {
            employerInfoObject.SetActive(false);
            employerHireObject.SetActive(true);
            procentBar = 0f;
            isMouseButtonDown = false;
        }
    }

    void EmployerNotHire()
    {
        if (DBValues.CompanyJobPlaces.Recruter >= 0)
        {
            employerNotHireObject.SetActive(true);
            procentBar = 0f;
            isMouseButtonDown = false;
        }
    }
}
