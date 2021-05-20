using System;
using Spaceship;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceship playerSpaceship;
        [SerializeField] private EnemySpaceship enemySpaceship;
        [SerializeField] public int playerSpaceshipHp;
        [SerializeField] private int playerSpaceshipMoveSpeed;
        [SerializeField] private int enemySpaceshipHp;
        [SerializeField] private int enemySpaceshipMoveSpeed;
        [SerializeField] private Planet planet;
        [SerializeField] private int planetHp;
        [SerializeField] private Planet2 planet2;
        [SerializeField] private int planet2Hp;

        [HideInInspector]
        public PlayerSpaceship spawnedPlayerShip;
        
        public event Action OnRestarted;
        
        public static GameManager Instance { get; private set; } // this is singleton
        
        private void Awake()
        {
            Debug.Assert(playerSpaceship != null, "playerSpaceship cannot be null");
            Debug.Assert(enemySpaceship != null, "enemySpaceship cannot be null");
            Debug.Assert(playerSpaceshipHp > 0, "playerSpaceship hp has to be more than zero");
            Debug.Assert(playerSpaceshipMoveSpeed > 0, "playerSpaceshipMoveSpeed has to be more than zero");
            Debug.Assert(enemySpaceshipHp > 0, "enemySpaceshipHp has to be more than zero");
            Debug.Assert(enemySpaceshipMoveSpeed > 0, "enemySpaceshipMoveSpeed has to be more than zero");
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        
        public void StartGame()
        {
            SpawnPlayerSpaceship();
            SpawnEnemySpaceship();
        }
        
        private void SpawnPlayerSpaceship()
        {
            spawnedPlayerShip = Instantiate(playerSpaceship);
            spawnedPlayerShip.Init(playerSpaceshipHp, playerSpaceshipMoveSpeed);
            spawnedPlayerShip.OnExploded += OnPlayerSpaceshipExploded;
        }

        private void OnPlayerSpaceshipExploded()
        {
            Restart();
        }

        private void SpawnEnemySpaceship()
        {
            var spawnedEnemyShip = Instantiate(enemySpaceship);
            spawnedEnemyShip.Init(enemySpaceshipHp, enemySpaceshipMoveSpeed);
            spawnedEnemyShip.OnExploded += OnEnemySpaceshipExploded;
        }


        private void OnEnemySpaceshipExploded()
        {
            ScoreManager.Instance.UpdateScore(1);
            //Restart();
            SceneManager.LoadScene("Game2");
        }

        private void Restart()
        {
            OnRestarted?.Invoke();
            DestroyRemainingShips();
        }

        private void DestroyRemainingShips()
        {
            var remainingEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in remainingEnemy)
            {
                Destroy(enemy);
            }
            var remainingPlayer = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in remainingPlayer)
            {
                Destroy(player);
            }
            var remainingPlanet = GameObject.FindGameObjectsWithTag("Planet");
            foreach (var planet in remainingPlanet)
            {
                Destroy(planet);
            }
            var remainingPlanet2 = GameObject.FindGameObjectsWithTag("Planet2");
            foreach (var planet2 in remainingPlanet2)
            {
                Destroy(planet2);
            }
        }
    }
}
