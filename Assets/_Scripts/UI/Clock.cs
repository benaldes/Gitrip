using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public bool _bossFight = false;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AudioClip _minAudio;
    [SerializeField] private Transform _clockHandMin;
    [SerializeField] private Transform _clockHandHour;
    [SerializeField] private BossSpawner _bossSpawner;
    private float Timer = 0;
    [SerializeField] private int _bossWaveCount = 0;
    
    private void Update()
    {
        ClockTime();
    }
    private void ClockTime()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if(!_bossFight)
        {
            Timer += Time.deltaTime;
            MoveMinClockHand();
        }
        if (Timer > _enemySpawner.WaveInterval)
        {
            Timer = 0;
            _bossWaveCount++;
            MoveHourClockHand();
            if (_bossWaveCount == _enemySpawner.BossWaveInterval) 
            {
                _bossSpawner.SpawnBoss(); 
                _bossFight = true;  
            }
            else _enemySpawner.WaveStart();
        }    
        bool play = false;
        if ((_enemySpawner.WaveInterval - Timer) <= 2)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = _minAudio;
                audioSource.time = 0.6f;
                audioSource.Play();
            }

        }

    }
    private void MoveMinClockHand()
    {
        float angle = 360 / _enemySpawner.WaveInterval;
        float clockAngle = Timer * angle;
        _clockHandMin.eulerAngles = new Vector3(0, 0, -clockAngle);

    }
    private void MoveHourClockHand()
    {
        float angle = 360 / _enemySpawner.BossWaveInterval;
        float clockAngle = _bossWaveCount * angle;
        _clockHandHour.eulerAngles = new Vector3(0, 0, -clockAngle);
    }
}
