// ============================================================================
// Unity Study Module 01: MonoBehaviour Lifecycle
// ============================================================================
// This file documents Unity's execution order without Unity dependencies.
// Use as a study reference for interviews.
// ============================================================================

namespace GoogleInterviewPrep.UnityStudy
{
    /// <summary>
    /// Unity MonoBehaviour Lifecycle — Complete Execution Order
    /// 
    /// INITIALIZATION PHASE (called once per object):
    /// ┌─────────────────────────────────────────────────┐
    /// │ 1. Awake()          — Called when script instance is loaded           │
    /// │                       Called even if the script is disabled           │
    /// │                       Use for: self-initialization, caching refs      │
    /// │                                                                       │
    /// │ 2. OnEnable()       — Called when the object becomes active           │
    /// │                       Called every time the object is re-enabled      │
    /// │                       Use for: subscribing to events                  │
    /// │                                                                       │
    /// │ 3. Start()          — Called before the first frame Update            │
    /// │                       Only called if the script is enabled            │
    /// │                       Use for: initialization that depends on others  │
    /// └─────────────────────────────────────────────────┘
    /// 
    /// GAME LOOP (called every frame/physics step):
    /// ┌─────────────────────────────────────────────────┐
    /// │ 4. FixedUpdate()    — Called at fixed intervals (default 0.02s)       │
    /// │                       Use for: physics calculations, Rigidbody forces │
    /// │                       NOT tied to frame rate                          │
    /// │                                                                       │
    /// │ 5. Update()         — Called once per frame                           │
    /// │                       Use for: input, non-physics game logic          │
    /// │                       Frame-rate dependent                            │
    /// │                                                                       │
    /// │ 6. LateUpdate()     — Called after all Update() calls complete        │
    /// │                       Use for: camera follow, post-processing logic   │
    /// │                       Ensures all objects have moved before camera    │
    /// └─────────────────────────────────────────────────┘
    /// 
    /// DECOMMISSIONING PHASE:
    /// ┌─────────────────────────────────────────────────┐
    /// │ 7. OnDisable()      — Called when the object is deactivated           │
    /// │                       Use for: unsubscribing from events              │
    /// │                                                                       │
    /// │ 8. OnDestroy()      — Called when the object is destroyed             │
    /// │                       Use for: final cleanup, releasing resources     │
    /// └─────────────────────────────────────────────────┘
    /// 
    /// COROUTINES:
    /// - StartCoroutine() — begins execution, yields control back to Unity
    /// - yield return null — resumes after Update() next frame
    /// - yield return new WaitForFixedUpdate() — resumes after FixedUpdate
    /// - yield return new WaitForSeconds(n) — resumes after n seconds
    /// - yield return new WaitForEndOfFrame() — resumes after rendering
    /// - yield return StartCoroutine(Other()) — waits for nested coroutine
    /// 
    /// COMMON PITFALLS:
    /// 1. Don't call GetComponent in Update() — cache in Awake/Start
    /// 2. Awake() order between scripts is NOT guaranteed — use Start() for cross-refs
    /// 3. Don't use FixedUpdate for input — inputs can be missed between fixed steps
    /// 4. OnDestroy may not be called if the app is force-quit
    /// 5. Coroutines stop when the GameObject is deactivated (not just the script)
    /// </summary>
    public class LifecycleReference
    {
        // This class exists purely for documentation. In Unity, you would
        // inherit from MonoBehaviour and override these methods.

        public void Awake() { /* Self-init: _rb = GetComponent<Rigidbody>(); */ }
        public void OnEnable() { /* EventBus.OnPlayerDied += HandleDeath; */ }
        public void Start() { /* Cross-ref init: _target = FindObjectOfType<Player>(); */ }
        public void FixedUpdate() { /* _rb.AddForce(Vector3.up * jumpForce); */ }
        public void Update() { /* if (Input.GetKeyDown(KeyCode.Space)) Jump(); */ }
        public void LateUpdate() { /* _camera.position = _player.position + offset; */ }
        public void OnDisable() { /* EventBus.OnPlayerDied -= HandleDeath; */ }
        public void OnDestroy() { /* Release unmanaged resources */ }
    }

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: What's the difference between Awake and Start?
    // A: Awake is called when the script is loaded (even if disabled), Start is
    //    called before the first Update only if the script is enabled. Use Awake
    //    for self-initialization, Start for cross-object dependencies.
    //
    // Q: Why use FixedUpdate instead of Update for physics?
    // A: FixedUpdate runs at consistent intervals (default 50Hz) regardless of
    //    frame rate. Physics simulations need consistent time steps for stability.
    //    Update is frame-rate dependent and would cause jittery physics.
    //
    // Q: When does a Coroutine resume relative to Update?
    // A: yield return null resumes AFTER Update() of the next frame, before
    //    LateUpdate(). WaitForEndOfFrame resumes after all rendering is done.
    //
    // Q: What happens to Coroutines when a GameObject is disabled?
    // A: They STOP. If you re-enable the object, the coroutine does NOT resume.
    //    You must restart it manually. This is a common source of bugs.
    // ============================================================================
}
