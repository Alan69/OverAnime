using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class ThirdPersonController : MonoBehaviour
    {
        public float speed = 10f;
        public float run_speed = 14f;
        public float cameraRotateSmothnes = 0.1f;
        float tuenSmothVelocity;
        public Rigidbody rb;
        public Transform camera;
        public Animator anim;

        public float bullet_range = 50f;
        public float fire_rate = 15f;
        public float impact_forse = 30f;
        public int damage;
        public GameObject shootAngle;
        //public Text text;
        //public Slider slider;

        [SerializeField] private GameObject ImpactEffect;
        [SerializeField] private ParticleSystem muzzleFlash;
        //[SerializeField] private AudioSource gun_sound;
        public Health health;
        private float nextTimetofire = 0f;

        private void Start()
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            PlayerMovement();

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetofire) 
            {
                anim.SetBool("isShoot", true);
                nextTimetofire = Time.time + 1f / fire_rate;
                Shoot();
            }
            else
            {
                anim.SetBool("isShoot", false);
            }

            //text.text = _health.health.ToString();
            //slider.value = _health.health;
        }

        private void PlayerMovement()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 tempVect = new Vector3(h, 0, v).normalized;

            if(tempVect.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(tempVect.x, tempVect.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tuenSmothVelocity, cameraRotateSmothnes);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
            }

            if (h != 0 || v != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            if(v != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                speed = run_speed;
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
            }
            else
            {
                speed = 10f;
                anim.SetBool("isRunning", false);
            }
        }

    private void Shoot()
    {
        muzzleFlash.Play();
    //    gun_sound.Play();

        RaycastHit hit;
        if(Physics.Raycast(shootAngle.transform.position, shootAngle.transform.forward, out hit, bullet_range))
        {
            Health target = hit.transform.GetComponent<Health>();
            if(target != null && target.tag == "Enemy")
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impact_forse);
            }

            GameObject impact = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }

}
