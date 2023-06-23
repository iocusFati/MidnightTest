using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.BulletFolder
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;
        
        public BulletStaticData BulletData;
        public Rigidbody Rigidbody { get; set; }

        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
            
            BulletData.SetupTrail(_trailRenderer);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collide");
            // if (collision.gameObject.CompareTag("Enemy"))
            // {
            //     
            // }

            _hitEffect.transform.position = collision.GetContact(0).point;
            _hitEffect.transform.forward = -transform.forward;
            _hitEffect.Play();
            
            Rigidbody.velocity = Vector3.zero;

            StartCoroutine(ReleaseBullet());

            // gameObject.SetActive(false);
        }

        private IEnumerator ReleaseBullet()
        {
            yield return new WaitForSecondsRealtime(BulletData.Time * 10);
            
            gameObject.SetActive(false);
        }
    }
}