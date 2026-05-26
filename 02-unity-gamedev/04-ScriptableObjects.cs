// ============================================================================
// Unity Study Module 04: ScriptableObjects
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.UnityStudy
{
    /// <summary>
    /// ScriptableObjects — Data-Driven Architecture in Unity
    /// 
    /// WHAT: A data container that exists as an asset (saved to disk), not 
    /// attached to a GameObject in a scene. Lives in the Project folder.
    /// 
    /// WHY:
    /// 1. Reduce memory — shared data isn't duplicated per instance
    /// 2. Decouple systems — components reference data, not each other
    /// 3. Designer-friendly — editable in Inspector without code changes
    /// 4. Version control — clean diffs (no scene file changes)
    /// 
    /// WHEN TO USE:
    /// ✅ Game configuration (weapon stats, enemy data, level settings)
    /// ✅ Event channels (decouple sender/receiver without Singletons)
    /// ✅ Runtime sets (track active objects without FindObjectsOfType)
    /// ✅ Enumerations with data (replace enums with SO instances)
    /// ❌ Per-instance mutable state at runtime (use MonoBehaviour instead)
    /// </summary>

    // --- PATTERN 1: Data Container ---
    // [CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon Data")]
    public class WeaponData  // : ScriptableObject in Unity
    {
        public string WeaponName = "Sword";
        public int Damage = 25;
        public float AttackSpeed = 1.2f;
        public float Range = 2.0f;
        // In Unity Inspector: create multiple weapon assets, drag into weapon scripts
    }

    // --- PATTERN 2: Event Channel (Decouple Systems) ---
    // Instead of: HealthBar directly references Player → tight coupling
    // Use: Player raises event on SO → HealthBar listens to same SO → decoupled
    public class GameEvent  // : ScriptableObject
    {
        private readonly List<Action> _listeners = new();
        public void Register(Action listener) => _listeners.Add(listener);
        public void Unregister(Action listener) => _listeners.Remove(listener);
        public void Raise() { foreach (var l in _listeners) l.Invoke(); }
    }

    // Typed event channel with data
    public class GameEvent<T>
    {
        private readonly List<Action<T>> _listeners = new();
        public void Register(Action<T> listener) => _listeners.Add(listener);
        public void Unregister(Action<T> listener) => _listeners.Remove(listener);
        public void Raise(T value) { foreach (var l in _listeners) l.Invoke(value); }
    }

    // --- PATTERN 3: Runtime Set (Track Active Objects) ---
    // Problem: FindObjectsOfType<Enemy>() is O(n) over ALL objects — very slow.
    // Solution: Enemies add/remove themselves from a RuntimeSet SO.
    public class RuntimeSet<T>  // : ScriptableObject
    {
        private readonly List<T> _items = new();
        public IReadOnlyList<T> Items => _items;
        public void Add(T item) { if (!_items.Contains(item)) _items.Add(item); }
        public void Remove(T item) => _items.Remove(item);
        // Each enemy calls Add(this) in OnEnable, Remove(this) in OnDisable
    }

    // --- PATTERN 4: Variable Reference ---
    // A ScriptableObject that holds a single value, observable by multiple systems.
    public class FloatVariable  // : ScriptableObject
    {
        public float Value;
        public event Action<float>? OnValueChanged;
        public void SetValue(float newValue)
        {
            if (Math.Abs(Value - newValue) > 0.001f)
            {
                Value = newValue;
                OnValueChanged?.Invoke(Value);
            }
        }
    }
    // Usage: PlayerHealth (FloatVariable SO) referenced by HealthBar, AI, Sound

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: What's the advantage of ScriptableObjects over MonoBehaviours for data?
    // A: SOs are assets (project-level), not scene objects. Multiple objects can
    //    reference the same SO without duplicating data. They survive scene loads,
    //    produce cleaner version control diffs, and are editable by designers.
    //
    // Q: How do ScriptableObject Event Channels prevent Singleton abuse?
    // A: Instead of a Singleton EventManager that everything references, you
    //    create SO assets per event type. Components reference specific event
    //    assets through Inspector drag-and-drop. No hard-coded dependencies,
    //    easy to test, swap, and extend.
    //
    // Q: Can ScriptableObjects hold runtime state?
    // A: Yes, but with caveats. In the Editor, SO changes persist to disk.
    //    In builds, changes reset on restart. For true runtime state, pair SOs
    //    with a MonoBehaviour that initializes from SO defaults at Start().
    // ============================================================================
}
