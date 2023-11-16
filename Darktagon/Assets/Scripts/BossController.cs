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
    public float moveSpeedX = 15f;

    public float moveSpeedZ = 300f;

    private bool MovingBossX = true;

    private bool MovingBossZ = true;

    void Start()
    {

        StartCoroutine(PatternOne());
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

        MovingBossZ = true;
        MovingBossX = true;

        int patternChoice = Random.Range(0, 2); // 0 or 1

        if (patternChoice == 0)
        {
            StartCoroutine(MoveBossZ());
        }
        else
        {
            StartCoroutine(MoveBossX());
        }

        initialDuration = 5f;

        float startTime = Time.time;
        float endTime = startTime + initialDuration;
        startAngle = 0;
        endAngle = 360;
        bulletSpeed = 29;
        firingRate = 1f;
        
        for (numberOfBullets = 5; numberOfBullets < 8; numberOfBullets += 1)
        {
            while (Time.time < endTime)
            {
                MirrorParameters(numberOfBullets, startAngle, endAngle, bulletSpeed);
                yield return new WaitForSeconds(firingRate);
            }

            endTime += 5;
        }

        MovingBossZ = false;
        MovingBossX = false;

        yield return new WaitForSeconds(4f);


        if (patternChoice == 0)
        {
            // Call PatternOne
            yield return StartCoroutine(PatternThree());
        }
        else
        {
            // Call PatternTwo
            yield return StartCoroutine(PatternTwo());
        }


    }
    IEnumerator PatternTwo() {

        MovingBossZ = true;
        MovingBossX = true;

        int patternChoice = Random.Range(0, 2); // 0 or 1

        if (patternChoice == 0)
        {
            StartCoroutine(MoveBossZ());
        }
        else
        {
            StartCoroutine(MoveBossX());
        }

        int numberOfBullets = 3;
        bulletSpeed = 40;
        firingRate = 0.7f;

        int angleChangeTime = 3;
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
                }

                BouncyParameters(numberOfBullets, startAngle, endAngle, bulletSpeed);
                yield return new WaitForSeconds(firingRate);
            }
        }

        MovingBossZ = false;
        MovingBossX = false;

        yield return new WaitForSeconds(4f);
        

        patternChoice = Random.Range(0, 2); // 0 or 1

        if (patternChoice == 0)
        {
            // Call PatternOne
            yield return StartCoroutine(PatternOne());
        }
        else
        {
            // Call PatternTwo
            yield return StartCoroutine(PatternThree());
        }
    }

    IEnumerator PatternThree() {
    
    MovingBossZ = true;
    MovingBossX = true;

    int patternChoice = Random.Range(0, 2); // 0 or 1

    if (patternChoice == 0)
    {
        StartCoroutine(MoveBossZ());
    }
    else
    {
        StartCoroutine(MoveBossX());
    }

    firingRate = 0.5f;
    initialDuration = 2f;
    float patternDuration = 30f;
    startAngle = 0;
    endAngle = 360;
    float addAngle = 0f;
    int numberOfBullets = 4;
    float startTime = Time.time;
    bulletSpeed = 30f;
    float patternStartTime = Time.time;
    float startAngleN = 45f;    
    float endAngleN = 405f;
    float bulletSpeedN = 20f;
    int numberOfBulletsN = 4;
    float addAngleN = 0f;
    float angleChangeTime = 0.05f;
    float firingRateN = 0.05f;

    while (Time.time - patternStartTime < patternDuration) {

        MirrorParameters(numberOfBullets, startAngle + addAngle, endAngle + addAngle, bulletSpeed);
        NormalParameters(numberOfBulletsN, startAngleN, endAngleN, bulletSpeedN);

        addAngle = 0f;
        
        yield return new WaitForSeconds(firingRate);

        addAngleN = 0f;
        yield return new WaitForSeconds(firingRateN);
    }

    MovingBossZ = false;
    MovingBossX = false;

    patternChoice = Random.Range(0, 2); // 0 or 1

        if (patternChoice == 0)
        {
            // Call PatternOne
            yield return StartCoroutine(PatternOne());
        }
        else
        {
            // Call PatternTwo
            yield return StartCoroutine(PatternTwo());
        }

}


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