using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject mirrorBullet;
    public GameObject bouncyBullet;
    public GameObject bullet;

    public float bulletSpeed = 30f;
    public int numberOfBullets = 5;
    public float startAngle = 0f;
    public float endAngle = 180f;
    public float firingRate = 1f;

    public float initialDuration = 15f;
    public float moveSpeedX = 5f;

    public float moveSpeedZ = 15f;

    private bool MovingBossX = true;

    private bool MovingBossZ = true;

    void Start()
    {

        StartCoroutine(PatternTwo());
    }

    IEnumerator MoveBossX()
    {   
        while (MovingBossX)
        {
            // Move the boss between x positions of 53 and -53
            float newX = Mathf.PingPong(Time.time * moveSpeedX, 106) - 53;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            yield return null;
        }
    }


    IEnumerator MoveBossZ()
    {
        while (MovingBossZ)
        {
            // Move the boss between x positions of 53 and -53
            float newZ = Mathf.PingPong(Time.time * moveSpeedZ, 140) - 70;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            yield return null;
        }
    }
    IEnumerator PatternOne() {

        StartCoroutine(MoveBossX());

        Debug.Log("Coroutine Started");

        initialDuration = 5f;

        float startTime = Time.time;
        float endTime = startTime + initialDuration;
        startAngle = 0;
        endAngle = 360;
        bulletSpeed = 30;
        startAngle = 90;
        endAngle = -90;
        firingRate = 1f;

        for (numberOfBullets = 5; numberOfBullets < 10; numberOfBullets += 1)
        {
            while (Time.time < endTime)
            {
                MirrorParameters(numberOfBullets, startAngle, endAngle, bulletSpeed);
                yield return new WaitForSeconds(firingRate);
            }

            endTime += 5;
        }

        MovingBossX = false;

        Debug.Log("Coroutine Ended after " + (Time.time - startTime) + " seconds");
    }
    IEnumerator PatternTwo() {
        int numberOfBullets = 3;
        bulletSpeed = 60;
        firingRate = 0.5f;

        int angleChangeTime = 5; // Changed to an integer value
        float patternDuration = 30f;
        float patternStartTime = Time.time;

        while (Time.time - patternStartTime < patternDuration)
        {
            float startAngle = Random.Range(0f, 360f);
            float endAngle = startAngle + Random.Range(45f, 180f);

            float startTime = Time.time;
            float endTime = startTime + 5f;

            while (Time.time < endTime)
            {
                if (Time.time - startTime >= angleChangeTime)
                {
                    startAngle = Random.Range(0f, 360f);
                    endAngle = startAngle + Random.Range(22.5f, 180f);
                    startTime = Time.time;
                    endTime = startTime + 5f;
                }

                BouncyParameters(numberOfBullets, startAngle, endAngle, bulletSpeed);
                yield return new WaitForSeconds(firingRate);
            }
        }
    }

    /* IEnumerator PatternThree(){

    }*/

    void BouncyParameters(int bullets = 5, float angleStart = 0f, float angleEnd = 180f, float speedBullet = 30f) {
        float angleStep = (angleEnd - angleStart) / (bullets);

        for (int i = 0; i < bullets; i++)
        {
            float angle = angleStart + i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            GameObject bBullet = Instantiate(bouncyBullet, transform.position, Quaternion.identity);
            bBullet.GetComponent<Rigidbody>().velocity = direction * speedBullet;
        }
    }

    void NormalParameters(int bullets = 5, float angleStart = 0f, float angleEnd = 180f, float speedBullet = 30f) {
        float angleStep = (angleEnd - angleStart) / (bullets);

        for (int i = 0; i < bullets; i++)
        {
            float angle = angleStart + i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().velocity = direction * speedBullet;
        }
    }
    
    void MirrorParameters(int bullets = 5, float angleStart = 0f, float angleEnd = 180f, float speedBullet = 30f) {
        float angleStep = (angleEnd - angleStart) / (bullets);

        for (int i = 0; i < bullets; i++)
        {
            float angle = angleStart + i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            GameObject Bullet = Instantiate(mirrorBullet, transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().velocity = direction * speedBullet;
        }
    }

}