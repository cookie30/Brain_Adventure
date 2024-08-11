using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject frontsight;
    public GameObject weaponCarma;
    public bool isScope=false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isScope = !isScope;
            animator.SetBool("Scope", isScope);

            if (isScope)
            {
                StartCoroutine(OnScoped());
            }
            else
            {
                OnUnScoped();
            }
        }
    }

    void OnUnScoped()
    {
        frontsight.SetActive(true);
        scopeOverlay.SetActive(false);
        weaponCarma.SetActive(true);
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        frontsight.SetActive(false);
        weaponCarma.SetActive(false);
    }
}
