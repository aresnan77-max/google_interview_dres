# Unity Quick Reference Card

## MonoBehaviour Lifecycle (Execution Order)
```
Awake → OnEnable → Start → FixedUpdate → Update → LateUpdate → OnDisable → OnDestroy
 ↑ once    ↑ on/off   ↑ once  ↑ fixed rate  ↑ per frame ↑ after Update  ↑ on/off  ↑ once
```

## Common API Methods

| Method | Purpose | When to Use |
|--------|---------|-------------|
| `GetComponent<T>()` | Get component on same GO | Awake/Start (cache result!) |
| `FindObjectOfType<T>()` | Find first instance in scene | Start only (expensive) |
| `Instantiate(prefab, pos, rot)` | Create object | Spawning (pool if frequent) |
| `Destroy(go, delay)` | Destroy object | Cleanup (pool if frequent) |
| `DontDestroyOnLoad(go)` | Persist across scenes | Singletons, managers |
| `Invoke("Method", delay)` | Call method after delay | Simple timers |
| `InvokeRepeating("M", t, r)` | Repeat at interval | Spawners, checks |
| `StartCoroutine(IEnum)` | Async-like behavior | Sequences, waits |

## Physics Quick Reference

| Method | Purpose |
|--------|---------|
| `Rigidbody.AddForce(F, mode)` | Apply force (FixedUpdate only) |
| `Physics.Raycast(origin, dir, out hit, dist, mask)` | Cast ray |
| `Physics.OverlapSphere(center, radius, mask)` | Find colliders in sphere |
| `OnCollisionEnter(Collision)` | Physical collision happened |
| `OnTriggerEnter(Collider)` | Trigger zone entered |
| `CompareTag("Enemy")` | Check tag (faster than `== "Enemy"`) |

## Component Configuration Tips

| Component | Key Settings | Notes |
|-----------|-------------|-------|
| Rigidbody | Mass, Drag, Is Kinematic, Interpolation | Use Continuous for fast objects |
| Collider | Is Trigger, Material (friction/bounce) | Match shape to visual roughly |
| Camera | Clear Flags, Culling Mask, Clipping Planes | Set Near clip to max reasonable |
| Canvas | Render Mode, Scaler (Scale with Screen Size) | Split dynamic from static UI |
| AudioSource | Spatial Blend (2D↔3D), Priority | Lower priority = may be culled |

## Performance Optimization Checklist

### CPU
- [ ] Cache GetComponent results in Awake
- [ ] Use object pooling for frequent spawn/destroy
- [ ] Avoid LINQ / foreach allocations in Update
- [ ] Use StringBuilder for string concatenation
- [ ] Reduce Physics.Raycast calls (use layer masks)

### GPU
- [ ] Keep draw calls < 1000 (mobile) / 3000 (PC)
- [ ] Use LOD groups for distant objects
- [ ] Enable occlusion culling for indoor scenes
- [ ] Atlas textures to reduce material switches
- [ ] Use GPU Instancing for repeated objects

### Memory
- [ ] Use structs for small temporary data
- [ ] Pre-allocate arrays (Physics.RaycastNonAlloc)
- [ ] Avoid closures/delegates that capture variables
- [ ] Profile with Memory Profiler package

## Render Pipeline Comparison

| Feature | Built-in | URP | HDRP |
|---------|----------|-----|------|
| Target | Legacy | Mobile/VR/Console/PC | High-end PC/Console |
| Performance | Varies | Optimized | Heavy |
| Shader Graph | No | Yes | Yes |
| Ray Tracing | No | Limited | Yes |
| Post Processing | Stack | Volume | Volume |
| SRP Batcher | No | Yes | Yes |
| Recommendation | Don't use for new | **Default choice** | AAA only |
