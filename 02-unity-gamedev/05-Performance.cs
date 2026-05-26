// ============================================================================
// Unity Study Module 05: Performance Optimization
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleInterviewPrep.UnityStudy
{
    /// <summary>
    /// Unity Performance Optimization — Complete Reference
    /// 
    /// THE THREE BOTTLENECKS:
    /// 1. CPU — too much game logic, physics, AI per frame
    /// 2. GPU — too many draw calls, complex shaders, high-res textures
    /// 3. Memory/GC — frequent allocations cause GC pauses (frame drops)
    /// 
    /// GC OPTIMIZATION (Most Important for Interviews):
    /// ┌─────────────────────────────────────────────────────────────┐
    /// │ Problem: C# garbage collection causes "hitches" (frame drops)│
    /// │ Root cause: Allocating heap memory during gameplay           │
    /// │                                                              │
    /// │ SOLUTIONS:                                                   │
    /// │ 1. Object Pooling — reuse objects instead of Create/Destroy  │
    /// │ 2. Cache results — don't call GetComponent() in Update       │
    /// │ 3. Use structs — stack-allocated, no GC pressure             │
    /// │ 4. StringBuilder — avoid string concatenation in loops       │
    /// │ 5. Non-alloc APIs — Physics.RaycastNonAlloc, etc.            │
    /// │ 6. Avoid LINQ in Update — it allocates enumerators           │
    /// │ 7. Avoid boxing — don't cast value types to object           │
    /// └─────────────────────────────────────────────────────────────┘
    /// 
    /// DRAW CALL OPTIMIZATION:
    /// - Static Batching: mark static objects → Unity combines meshes
    /// - Dynamic Batching: automatic for small meshes (<300 vertices)
    /// - GPU Instancing: same mesh+material drawn in one call
    /// - SRP Batcher: batches by shader, not material (URP/HDRP)
    /// - Texture Atlasing: combine textures to reduce material changes
    /// - Goal: keep draw calls under 1000 for mobile, 2000-3000 for PC
    /// 
    /// LOD (Level of Detail):
    /// - LOD0: high-poly model (close to camera)
    /// - LOD1: medium-poly (mid distance)
    /// - LOD2: low-poly (far away)
    /// - Culled: invisible (beyond max distance)
    /// - Unity LODGroup component manages transitions automatically
    /// 
    /// OCCLUSION CULLING:
    /// - Don't render objects hidden behind other objects
    /// - Bake occlusion data (Window > Rendering > Occlusion Culling)
    /// - Critical for indoor environments with many rooms/corridors
    /// 
    /// PROFILER WORKFLOW:
    /// 1. Open Window > Analysis > Profiler
    /// 2. Record gameplay, identify spikes
    /// 3. Deep Profile for detailed call stacks (slower)
    /// 4. Check: CPU time, GC allocations, Draw calls, Triangles
    /// 5. Target: 16.67ms for 60fps, 11.11ms for 90fps (VR)
    /// </summary>

    // --- GC-Friendly Coding Examples ---
    public class PerformanceReference
    {
        // BAD: Allocates new string every frame
        // void Update() { _text.text = "Score: " + score; }

        // GOOD: Use StringBuilder, reuse buffer
        private readonly StringBuilder _sb = new(64);
        public string GetScoreText(int score)
        {
            _sb.Clear();
            _sb.Append("Score: ");
            _sb.Append(score);
            return _sb.ToString();
        }

        // BAD: GetComponent every frame
        // void Update() { GetComponent<Rigidbody>().AddForce(...); }

        // GOOD: Cache in Awake
        // private Rigidbody _rb;
        // void Awake() { _rb = GetComponent<Rigidbody>(); }
        // void FixedUpdate() { _rb.AddForce(...); }

        // BAD: Allocates array every call
        // var hits = Physics.RaycastAll(origin, direction);

        // GOOD: Pre-allocate array, use NonAlloc
        private readonly object[] _hitBuffer = new object[32]; // RaycastHit[] in Unity
        // int count = Physics.RaycastNonAlloc(origin, direction, _hitBuffer);

        // BAD: foreach on List<T> in older Unity versions allocates enumerator
        // GOOD: Use for loop
        public int SumList(List<int> items)
        {
            int sum = 0;
            for (int i = 0; i < items.Count; i++) sum += items[i];
            return sum;
        }

        // STRUCT vs CLASS for temp data
        public struct DamageInfo // stack-allocated, no GC
        {
            public int Amount;
            public bool IsCritical;
        }
    }

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: What causes GC spikes in Unity and how do you fix them?
    // A: Heap allocations in the game loop (Update). Fix with: object pooling,
    //    caching, structs for temp data, StringBuilder, NonAlloc physics APIs.
    //    GC.Collect() can be called during loading screens for predictable cleanup.
    //
    // Q: What's the difference between Static and Dynamic Batching?
    // A: Static batching combines meshes of non-moving objects at build time
    //    (increases memory but reduces draw calls dramatically). Dynamic batching
    //    combines small moving meshes at runtime (limited to <300 vertices).
    //    GPU Instancing handles many copies of the same mesh efficiently.
    //
    // Q: How would you profile a frame rate issue?
    // A: 1) Open Profiler, identify the frame spike 2) Check CPU module for
    //    expensive scripts 3) Check GC Alloc column for allocations 4) Check
    //    Rendering module for draw call count 5) Use Deep Profile if needed
    //    6) Profile on target device (PC profiling != mobile performance).
    //
    // Q: What's the target frame budget for 60fps?
    // A: 16.67ms total per frame. Split between CPU and GPU — if CPU takes 10ms,
    //    GPU has 6.67ms. For VR: 11.11ms at 90fps. For mobile: aim for <33ms.
    // ============================================================================
}
