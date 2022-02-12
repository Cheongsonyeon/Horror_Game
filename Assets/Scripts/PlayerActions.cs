using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;

    public void OnUse()
    {
        Debug.DrawRay(Camera.position, Camera.forward * MaxUseDistance, Color.blue, 0.1f);
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance))
        {
            if (hit.collider.gameObject.layer==LayerMask.NameToLayer("Door"))
            {
                hit.collider.TryGetComponent<Door>(out Door door);

                if (door.isOpen)
                {
                    door.Close();
                }
                else
                {

                    ///
                    door.Open(Camera.position);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("F"))
        {
            OnUse();
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
            && hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.isOpen)
            {
                UseText.SetText("Close \"F\"");
            }
            else
            {
                UseText.SetText("Open \"F\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);


        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
