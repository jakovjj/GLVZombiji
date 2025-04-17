using UnityEngine;
using System.Collections;

public class LaserTemporary: MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform gunTip;
    public float laserRange = 100f;
    public LayerMask obstacleLayer;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && lineRenderer.enabled == false)
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        Vector3 start = gunTip.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = start.z;

        Vector3 direction = (mousePosition - start).normalized;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, laserRange, obstacleLayer);

        lineRenderer.enabled = true;

        if (hit.collider != null)
        {
            Debug.Log(gunTip.position);
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, start + direction * laserRange);
        }

        StartCoroutine(DisableLaserAfterDelay(2f));
    }

    IEnumerator DisableLaserAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        lineRenderer.enabled = false;
    }
}