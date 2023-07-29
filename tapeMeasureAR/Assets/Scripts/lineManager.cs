using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TMPro;

public class lineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable aRPlacementInteractable;

    public TextMeshPro dist_text;
    void Start()
    {
        aRPlacementInteractable.objectPlaced.AddListener(drawLine);
    }

    void drawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, args.placementObject.transform.position);
        if(lineRenderer.positionCount >1)
        {
            Vector3 pointA = lineRenderer.GetPosition(lineRenderer.positionCount-1);
            Vector3 pointB = lineRenderer.GetPosition(lineRenderer.positionCount-2);
            float distanceBetweenPoints = Vector3.Distance(pointA,pointB);

            float distanceToCentimeters = distanceBetweenPoints *100f;

            TextMeshPro inst_dist_point = Instantiate(dist_text);
            
            dist_text.text = distanceToCentimeters.ToString("F2")+ "cm";

            Vector3 directionVector = (pointB  - pointA);
            Vector3 normal = args.placementObject.transform.up;

            Vector3 upd = Vector3.Cross(directionVector, normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(-normal , upd);

            dist_text.transform.rotation = rotation;
            dist_text.transform.position = (pointA+ directionVector* 0.5f)+ upd *0.05f;

        }
    }
}
