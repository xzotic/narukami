using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public PlayerMovement pm;

    public LayerMask layerMask;
    public Animator anim;
    public GameObject[] attacks;
    public float chargeTimer;
    public float chargeCooldown;
    public int currentCharge;

    [Header("Charge Time")]
    public float charge2time;
    public float charge3time;
    public float chargeBreakTime;

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }

        if (chargeCooldown != 0)
        {
            chargeCooldown -= Time.deltaTime;
            chargeCooldown = Mathf.Clamp(chargeCooldown, 0, 5f);
        }

        if (Input.GetMouseButton(0) && chargeCooldown == 0)
        {
            chargeTimer += Time.deltaTime;
            pm.isCharging = true;
            pm.rb.useGravity = false;
            pm.chargeLight.SetActive(true);
            anim.SetInteger("charge", 1);
            currentCharge = 1;
            if (chargeTimer >= charge2time && chargeTimer < charge3time)
            {
                anim.SetInteger("charge", 2);
                //pm.chargeAura.SetActive(true);
                currentCharge = 2;
            }
            else if (chargeTimer >= charge3time && chargeTimer <chargeBreakTime)
            {
                anim.SetInteger("charge", 3);
                //pm.chargeAura.SetActive(true);
                currentCharge = 3;
            }
            else if (chargeTimer >= chargeBreakTime)
            {
                currentCharge = 3;
                SpawnFist();
                ResetCharge();
            }
        }
        
        if (Input.GetMouseButtonUp(0) && chargeCooldown ==0)
        {
            SpawnFist();
            ResetCharge();
        }
    }

    private void ResetCharge()
    {
        chargeCooldown = 1;
        chargeTimer = 0;
        anim.SetInteger("charge", 0);
        currentCharge = 0;
        pm.rb.useGravity = true;
    }

    private void SpawnFist()
    {
        GameObject fist = Instantiate(attacks[currentCharge - 1], transform.parent.position, Quaternion.identity);
        fist.transform.LookAt(transform.position);
        fist.GetComponent<Rigidbody>().AddForce(fist.transform.forward * 1000 * currentCharge, ForceMode.Impulse);
        
        //StartCoroutine(FadeOutMaterial(2f, fist));
        //if (currentCharge == 1) Destroy(fist, 0.5f);
    }

    IEnumerator FadeOutMaterial(float fadeSpeed, GameObject objectToFade)
    {
        Renderer rend = objectToFade.transform.GetComponent<Renderer>();
        Color matColor = rend.material.color;
        float alpha = rend.material.color.a;
        while(rend.material.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alpha);
            yield return null;
        }
    }
}
