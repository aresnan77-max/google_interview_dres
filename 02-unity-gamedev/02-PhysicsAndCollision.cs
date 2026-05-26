// ============================================================================
// Unity Study Module 02: Physics & Collision
// ============================================================================

namespace GoogleInterviewPrep.UnityStudy
{
    /// <summary>
    /// Unity Physics System Reference
    /// 
    /// RIGIDBODY CONFIGURATION:
    /// - Mass: affects how forces are applied (heavier = harder to move)
    /// - Drag: air resistance (linear), Angular Drag: rotational resistance
    /// - Use Gravity: enable/disable gravity for this body
    /// - Is Kinematic: if true, not affected by physics engine (move via transform)
    /// - Interpolation: smooths rendering between physics steps
    ///   - None: default, may look jittery
    ///   - Interpolate: smooths based on previous frame
    ///   - Extrapolate: predicts next position (can overshoot)
    /// - Collision Detection:
    ///   - Discrete: default, fast but can miss fast objects (tunneling)
    ///   - Continuous: prevents tunneling for fast-moving objects (expensive)
    ///   - ContinuousDynamic: for very fast objects hitting other dynamic objects
    /// 
    /// COLLISION vs TRIGGER:
    /// ┌──────────────────────┬──────────────────────────────────────┐
    /// │ Collision             │ Trigger                              │
    /// ├──────────────────────┼──────────────────────────────────────┤
    /// │ Collider.isTrigger=F │ Collider.isTrigger=T                │
    /// │ Physical response    │ No physical response (pass through) │
    /// │ OnCollisionEnter()   │ OnTriggerEnter()                    │
    /// │ OnCollisionStay()    │ OnTriggerStay()                     │
    /// │ OnCollisionExit()    │ OnTriggerExit()                     │
    /// │ At least 1 Rigidbody │ At least 1 Rigidbody                │
    /// │ Use for: walls,floor │ Use for: pickups, zones, detection  │
    /// └──────────────────────┴──────────────────────────────────────┘
    /// 
    /// RAYCASTING:
    /// - Physics.Raycast(origin, direction, out hit, maxDistance, layerMask)
    /// - Physics.SphereCast — wider ray (like a thick laser)
    /// - Physics.BoxCast — box-shaped ray
    /// - Physics.OverlapSphere — find all colliders in a sphere
    /// Use cases: shooting, ground detection, line-of-sight, mouse picking
    /// 
    /// LAYER MASKS:
    /// - Each GameObject has a Layer (0-31)
    /// - Physics.Raycast can filter by LayerMask to only hit specific layers
    /// - Collision Matrix (Edit > Project Settings > Physics) defines which
    ///   layers can collide with each other
    /// - Optimization: disable collisions between layers that never interact
    /// 
    /// FORCES:
    /// - AddForce(force, ForceMode.Force) — continuous force (affected by mass)
    /// - AddForce(force, ForceMode.Impulse) — instant impulse (affected by mass)
    /// - AddForce(force, ForceMode.Acceleration) — ignores mass
    /// - MovePosition() — for kinematic rigidbodies, smooth teleport
    /// - velocity — directly set velocity (use sparingly, bypasses physics)
    /// </summary>
    public class PhysicsReference
    {
        // Pseudo-code examples (not compilable without Unity)
        // void OnCollisionEnter(Collision collision) {
        //     if (collision.gameObject.CompareTag("Enemy"))
        //         TakeDamage(collision.relativeVelocity.magnitude);
        // }
        // void OnTriggerEnter(Collider other) {
        //     if (other.CompareTag("Coin")) { score++; Destroy(other.gameObject); }
        // }
        // void GroundCheck() {
        //     isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        // }
    }

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: What's the difference between a Collider and a Trigger?
    // A: A Collider (isTrigger=false) creates physical interactions — objects bounce
    //    off each other. A Trigger (isTrigger=true) detects overlap without physical
    //    response. Both require at least one object to have a Rigidbody.
    //
    // Q: Why should physics be done in FixedUpdate?
    // A: Physics.Simulate runs at fixed time intervals. Applying forces in Update()
    //    would cause inconsistent results because Update is frame-rate dependent.
    //
    // Q: How do Layer Masks improve performance?
    // A: By filtering which layers participate in collision/raycast checks. Without
    //    masks, every raycast checks against ALL colliders. With masks, only relevant
    //    layers are tested, significantly reducing physics computation.
    //
    // Q: What is "tunneling" and how do you prevent it?
    // A: Tunneling is when a fast object passes through a thin collider between
    //    physics steps. Solution: set Collision Detection to Continuous or
    //    ContinuousDynamic for fast-moving objects.
    // ============================================================================
}
