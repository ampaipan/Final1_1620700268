using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
    public class Planet2 : Basespaceship, IDamagable
    {
        public event Action OnExploded;

        public int Hp { get; protected set; }

        public void TakeHit(int damage)
        {
            Hp -= damage;

            if (Hp > 0)
            {
                return;
            }

            Explode();
        }

        public void Explode()
        {
            SoundManager.Instance.Play(SoundManager.Sound.Planet);
            Debug.Assert(Hp <= 0, "HP is more than zero");
            gameObject.SetActive(false);
            OnExploded?.Invoke();
            GameManager.Instance.playerSpaceshipHp += 40;
            Destroy(gameObject);
        }
        public override void Fire()
        {
        }

    }
}