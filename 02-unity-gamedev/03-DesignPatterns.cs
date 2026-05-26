// ============================================================================
// Unity Study Module 03: Design Patterns for Game Development
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.UnityStudy
{
    // --- SINGLETON PATTERN (Thread-safe, DontDestroyOnLoad) ---
    // Use: AudioManager, GameManager — one instance that persists across scenes
    // Anti-pattern warning: Overuse creates tight coupling. Prefer dependency injection.
    public class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> _instance = new(() => new T());
        public static T Instance => _instance.Value;
        protected Singleton() { }
        // In Unity: override with DontDestroyOnLoad(gameObject) in Awake()
    }

    // --- OBSERVER PATTERN (Event-Driven Architecture) ---
    // Use: UI updates, achievement system, damage events — decouple sender & receiver
    public static class EventBus
    {
        public static event Action<int>? OnScoreChanged;
        public static event Action<string>? OnPlayerDied;
        public static event Action? OnGameOver;

        public static void RaiseScoreChanged(int newScore) => OnScoreChanged?.Invoke(newScore);
        public static void RaisePlayerDied(string cause) => OnPlayerDied?.Invoke(cause);
        public static void RaiseGameOver() => OnGameOver?.Invoke();
        // IMPORTANT: Always unsubscribe in OnDisable/OnDestroy to prevent memory leaks
    }

    // --- OBJECT POOL PATTERN (Minimize GC Pressure) ---
    // Use: Bullets, particles, enemies — frequently created/destroyed objects
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Queue<T> _pool = new();
        private readonly int _maxSize;

        public ObjectPool(int initialSize, int maxSize = 100)
        {
            _maxSize = maxSize;
            for (int i = 0; i < initialSize; i++) _pool.Enqueue(new T());
        }

        public T Get() => _pool.Count > 0 ? _pool.Dequeue() : new T();

        public void Return(T obj)
        {
            if (_pool.Count < _maxSize) _pool.Enqueue(obj);
            // In Unity: SetActive(false) instead of Destroy, SetActive(true) on Get
        }
    }

    // --- STATE MACHINE PATTERN (Game AI / Player States) ---
    // Use: Enemy AI (Idle→Patrol→Chase→Attack), Player (Idle→Run→Jump→Fall)
    public interface IState
    {
        void Enter();
        void Execute(); // Called every frame (in Update)
        void Exit();
    }

    public class StateMachine
    {
        private IState? _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Update() => _currentState?.Execute();
    }

    // Example: Enemy AI states
    public class IdleState : IState
    {
        public void Enter() { /* Play idle animation */ }
        public void Execute() { /* Check if player in range → transition to Chase */ }
        public void Exit() { /* Stop idle animation */ }
    }

    // --- COMMAND PATTERN (Undo/Redo System) ---
    // Use: Level editor, turn-based games, input replay
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class CommandHistory
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public void ExecuteCommand(ICommand cmd)
        {
            cmd.Execute();
            _undoStack.Push(cmd);
            _redoStack.Clear(); // New command invalidates redo history
        }

        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
        }

        public void Redo()
        {
            if (_redoStack.Count == 0) return;
            var cmd = _redoStack.Pop();
            cmd.Execute();
            _undoStack.Push(cmd);
        }
    }

    // --- FACTORY PATTERN (Enemy/Item Spawning) ---
    // Use: Spawning different enemy types, creating weapons, generating levels
    public enum EnemyType { Goblin, Orc, Dragon }

    public class EnemyData
    {
        public string Name { get; set; } = "";
        public int Health { get; set; }
        public int Damage { get; set; }
    }

    public static class EnemyFactory
    {
        public static EnemyData Create(EnemyType type) => type switch
        {
            EnemyType.Goblin => new EnemyData { Name = "Goblin", Health = 50, Damage = 10 },
            EnemyType.Orc => new EnemyData { Name = "Orc", Health = 150, Damage = 30 },
            EnemyType.Dragon => new EnemyData { Name = "Dragon", Health = 500, Damage = 100 },
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
        // In Unity: Instantiate from prefab based on type, configure components
    }

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: When would you use Observer vs direct references?
    // A: Use Observer (events) when multiple systems need to react to the same
    //    event and you want to avoid tight coupling. Use direct refs when there's
    //    a clear 1:1 relationship and performance matters (events have overhead).
    //
    // Q: Why is Object Pooling important in Unity?
    // A: Unity's Instantiate/Destroy causes GC allocations. GC pauses cause frame
    //    drops (stuttering). Pooling reuses objects, eliminating allocations during
    //    gameplay. Critical for mobile games and VR (where 90fps is required).
    //
    // Q: What's wrong with Singletons?
    // A: They create hidden dependencies, make testing difficult, and violate
    //    single responsibility. Alternatives: dependency injection, ScriptableObject
    //    events, service locator pattern. Use sparingly for true global services.
    //
    // Q: When should you use a State Machine vs behavior trees?
    // A: State machines are simpler and work well for linear flows (player movement).
    //    Behavior trees are better for complex AI with multiple conditions and
    //    priorities (enemy AI in open-world games). FSM for simplicity, BT for scale.
    // ============================================================================
}
