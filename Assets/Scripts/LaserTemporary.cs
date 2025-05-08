using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using static UnityEngine.ParticleSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class LaserTemporary : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform gunTip;
    public float laserRange = 100f;
    public LayerMask obstacleLayer;
    public bool isShooting;
    public bool isCharging;
    public GameObject startVFX;
    public GameObject endVFX;
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    public AudioSource charge;
    public AudioSource beam;
    public LayerMask enemyLayermask;
    public GameObject beamStruck;
    public LayerMask obstacleLayerMask;
    public static bool isLaserOn=false;
    private void Start()
    {
        beam.Stop();
        charge.Stop();
        FillList();
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }
    void Update()
    {

        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
        startVFX.transform.position = gunTip.position;
        //Debug.Log(gunTip.position);
        if (Input.GetButtonDown("Fire1") && lineRenderer.enabled == false && isShooting == false && isCharging==false && GameObject.Find("gameManager").GetComponent<manager>().equippedWeapon==3 && EventSystem.current.IsPointerOverGameObject()==false)
        {
            isCharging = true;
            isLaserOn = true;
            charge.Play();
            StartCoroutine(ChargeLaser(2));

        }

        if (isShooting)
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, direction, laserRange, enemyLayermask | obstacleLayerMask);

        List<RaycastHit2D> validHits = new List<RaycastHit2D>();

        foreach (RaycastHit2D hit2 in hits)
        {
            if ((obstacleLayerMask & (1 << hit2.collider.gameObject.layer)) != 0)
            {
                // Stop the laser when hitting an obstacle
                break;
            }

            if ((enemyLayermask & (1 << hit2.collider.gameObject.layer)) != 0)
            {
                // Collect enemies that are hit before encountering an obstacle
                validHits.Add(hit2);
            }
        }
        //Vector3 knockbackDir =  hits[0].transform.position-gunTip.transform.position;
        // knockbackDir=knockbackDir.normalized;
        for (int i = 0; i < validHits.Count; i++)
        {
            if (validHits[i].collider == null) continue;
            if (!validHits[i].collider.gameObject.GetComponentInChildren<BeamStruck>())
            {
                Instantiate(beamStruck, validHits[i].collider.gameObject.transform);
                validHits[i].collider.gameObject.GetComponent<enemyHealth>().Damage(MasterControllerScript.hyperbeamDamage, false, Vector3.zero);
            }
        }

       startVFX.transform.position=gunTip.position;
        //startVFX.transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.forward, -direction);
        startVFX.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);

        if (hit.collider != null)
        {
            //Debug.Log(hit.point);
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, start + direction * laserRange);
        }
        endVFX.transform.position = lineRenderer.GetPosition(1);
        endVFX.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);

    }

    IEnumerator DisableLaserAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        beam.Stop();
        lineRenderer.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
        isShooting = false;
        isLaserOn = false;
        isCharging = false;
    }
    IEnumerator ChargeLaser(float delay)
    {
        startVFX.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(delay);
        charge.Stop();
        beam.Play();
        isShooting = true;
        isLaserOn=true;
        lineRenderer.enabled = true;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
        StartCoroutine(DisableLaserAfterDelay(2f));
    }


    void FillList()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) { particles.Add(ps); }

        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) { particles.Add(ps); }

        }
    }
}