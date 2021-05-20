using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
    public class PlayerSpaceship : Basespaceship, IDamagable
    {
        public event Action OnExploded;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet);
        }

        public override void Fire()
        {
            //SoundManager.Instance.Play(audioSource,SoundManager.Sound.PlayerFire);
            SoundManager.Instance.Play(SoundManager.Sound.PlayerFire);
            //var bullet = Instantiate(defaultBullet, gunPosition.position, Quaternion.identity);
            var bullet = PoolManager.Instance.GetPooledObject(PoolManager.PoolObjectType.PlayerBullet);
            if (bullet)
            {
                bullet.transform.position = gunPosition.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Init(Vector2.up);                
            }            
            
        }

        public void TakeHit(int damage)
        {
            GameManager.Instance.playerSpaceshipHp -= damage;
            if (GameManager.Instance.playerSpaceshipHp > 0)
            {
                return;
            }
            Explode();
        }

        public void Explode()
        {
            //SoundManager.Instance.Play(audioSource,SoundManager.Sound.PlayerExplode);
            SoundManager.Instance.Play(SoundManager.Sound.PlayerExplode);
            Debug.Assert(GameManager.Instance.playerSpaceshipHp <= 0, "HP is more than zero");
            Destroy(gameObject);
            OnExploded?.Invoke();
        }
    }
}