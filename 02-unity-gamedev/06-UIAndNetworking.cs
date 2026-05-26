// ============================================================================
// Unity Study Module 06: UI Systems & Networking
// ============================================================================

namespace GoogleInterviewPrep.UnityStudy
{
    /// <summary>
    /// Unity UI Systems
    /// 
    /// CANVAS RENDER MODES:
    /// ┌──────────────────┬──────────────────────────────────────────┐
    /// │ Screen Space -   │ UI overlays screen. Always in front.     │
    /// │ Overlay           │ Simplest. Use for HUD, menus.           │
    /// ├──────────────────┼──────────────────────────────────────────┤
    /// │ Screen Space -   │ UI rendered by a specific camera.        │
    /// │ Camera            │ Can be affected by post-processing.     │
    /// │                  │ Use for: UI with 3D effects/particles.   │
    /// ├──────────────────┼──────────────────────────────────────────┤
    /// │ World Space      │ UI exists in 3D space like a GameObject. │
    /// │                  │ Use for: health bars above heads,         │
    /// │                  │ in-world menus, VR/AR interfaces.        │
    /// └──────────────────┴──────────────────────────────────────────┘
    /// 
    /// UGUI vs UI TOOLKIT:
    /// - UGUI (Unity UI): Mature, GameObject-based, artist-friendly
    ///   - Canvas, RectTransform, Image, Text, Button, Layout Groups
    ///   - Good for: game UI, HUD, in-world UI
    /// - UI Toolkit: CSS/HTML-inspired, UXML + USS
    ///   - Declarative, efficient rendering, theming support
    ///   - Good for: editor tools, complex menus, data-heavy UI
    ///   - Becoming the recommended approach for runtime UI
    /// 
    /// UI OPTIMIZATION:
    /// - Split canvases: separate static UI from dynamic UI
    /// - Disable Raycast Target on non-interactive elements
    /// - Use TextMeshPro instead of legacy Text component
    /// - Avoid layout rebuilds: minimize SetActive toggles on UI
    /// - Object pool UI list items (inventory, leaderboards)
    /// 
    /// ─────────────────────────────────────────────────────────────
    /// 
    /// NETWORKING ARCHITECTURES:
    /// 
    /// 1. CLIENT-SERVER (Authoritative Server)
    ///    Client → sends input → Server (validates, simulates) → sends state → Client
    ///    Pros: Cheat-resistant, consistent state
    ///    Cons: Input delay (lag), server cost
    ///    Use: Competitive games, MMOs
    /// 
    /// 2. PEER-TO-PEER (P2P)
    ///    Each client simulates locally, syncs with peers
    ///    Pros: No server cost, low latency
    ///    Cons: Cheat-vulnerable, complex sync
    ///    Use: Fighting games, casual co-op
    /// 
    /// 3. CLIENT-SIDE PREDICTION + SERVER RECONCILIATION
    ///    Client predicts result locally → Server validates → Client corrects
    ///    Pros: Responsive feel + server authority
    ///    Cons: Complex to implement, visual corrections
    ///    Use: FPS games (Overwatch, Valorant)
    /// 
    /// UNITY NETWORKING SOLUTIONS:
    /// - Netcode for GameObjects (official Unity solution)
    ///   - NetworkObject, NetworkBehaviour, ClientRpc, ServerRpc
    ///   - NetworkVariable<T> for auto-synced state
    /// - Mirror (community, mature, based on UNET)
    /// - Photon (PUN/Fusion — popular for indie, cloud-hosted)
    /// 
    /// LAG COMPENSATION:
    /// - Client-side prediction: apply input immediately, reconcile with server
    /// - Server-side lag compensation: rewind state to validate hits
    /// - Interpolation: smooth between received states (adds small delay)
    /// - Extrapolation: predict future position (can cause rubber-banding)
    /// 
    /// ─────────────────────────────────────────────────────────────
    /// 
    /// RENDER PIPELINES:
    /// ┌──────────┬──────────────────────────────────────────────┐
    /// │ Built-in │ Legacy pipeline. Max flexibility, complex.   │
    /// │          │ Being phased out. Use for older projects.    │
    /// ├──────────┼──────────────────────────────────────────────┤
    /// │ URP      │ Universal Render Pipeline.                    │
    /// │          │ Optimized for mobile/VR/consoles.            │
    /// │          │ Single-pass rendering, Shader Graph support. │
    /// │          │ Use for: most games, especially mobile.      │
    /// ├──────────┼──────────────────────────────────────────────┤
    /// │ HDRP     │ High Definition Render Pipeline.              │
    /// │          │ Photorealistic rendering, ray tracing.       │
    /// │          │ Requires powerful GPU (PC/Console only).     │
    /// │          │ Use for: AAA games, architectural viz.       │
    /// └──────────┴──────────────────────────────────────────────┘
    /// </summary>
    public class UIAndNetworkingReference { }

    // ============================================================================
    // INTERVIEW Q&A
    // ============================================================================
    // Q: When would you use World Space canvas vs Screen Space?
    // A: World Space for UI that exists in the game world (health bars above
    //    enemies, VR menus, interactive screens). Screen Space Overlay for
    //    HUD elements that should always be visible regardless of camera.
    //
    // Q: How does client-side prediction work in multiplayer games?
    // A: The client applies input immediately (predicting the result) without
    //    waiting for the server. The server processes the same input and sends
    //    back the authoritative result. If they match, no correction needed.
    //    If they differ, the client "snaps" or smoothly corrects to server state.
    //
    // Q: URP vs HDRP — how do you choose?
    // A: URP for most projects (mobile, VR, consoles, indie PC). HDRP only for
    //    AAA quality on powerful hardware. URP is more performant, has broader
    //    platform support, and is Unity's recommended default. HDRP adds ray
    //    tracing, volumetric fog, and physically-based rendering.
    //
    // Q: What's the difference between NetworkVariable and RPC in Netcode?
    // A: NetworkVariable auto-syncs state (e.g., health). The server changes it,
    //    clients automatically receive updates. RPCs are one-time method calls:
    //    ServerRpc (client→server), ClientRpc (server→clients). Use variables
    //    for continuous state, RPCs for discrete events (fire weapon, chat message).
    // ============================================================================
}
