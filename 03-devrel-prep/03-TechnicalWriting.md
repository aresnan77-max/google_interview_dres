# Technical Writing — Google DRE Standards

## Google's Technical Writing Principles

1. **Use active voice**: "The function returns a list" ✅ not "A list is returned" ❌
2. **Write for your audience**: Know their skill level. Don't over-explain OR under-explain.
3. **One idea per sentence**: Short sentences are easier to parse.
4. **Define new terms**: Bold the term, define it immediately.
5. **Use lists and tables**: Scannable > readable for technical content.
6. **Provide working code**: Every example must be copy-pasteable and runnable.

---

## Blog Post Structure

```
Title:      "How to [Achieve X] with [Technology Y]"
Hook:       1-2 sentences on the problem being solved
Overview:   What you'll learn, prerequisites, time estimate
Section 1:  Setup / Background
Section 2:  Core implementation (with code)
Section 3:  Advanced features or optimization
Section 4:  Testing / Verification
Conclusion: Summary, next steps, links to resources
```

### Blog Post Checklist
- [ ] Title is specific and action-oriented
- [ ] First paragraph hooks the reader
- [ ] Code samples are complete and tested
- [ ] Screenshots/diagrams for visual steps
- [ ] Links to API docs and source code
- [ ] Conclusion with clear next steps

---

## Codelab Structure (Google Standard)

A codelab is a step-by-step, hands-on tutorial (typically 30-60 minutes).

### Format
```
1. Overview         — What you'll build, prerequisites, difficulty
2. Setup            — Environment setup, project creation
3-N. Steps          — Incremental building blocks (each ~5 min)
    Each step:
    - Goal statement
    - Instructions (numbered)
    - Code snippets
    - "What you've done" summary
N+1. Congratulations — Summary, what you learned, next steps
```

### Codelab Writing Tips
- Each step should be completable independently
- Include a "checkpoint" — expected output after each step
- Provide a complete "solution" branch/tag for reference
- Use info boxes for tips: ℹ️ Note, ⚠️ Warning, 💡 Tip

---

## Sample Codelab Outline: Unity + Google Play Games Services

### 1. Overview
- **What you'll build**: A Unity game with Google Play sign-in and cloud save
- **Prerequisites**: Unity 2022+, Android SDK, Google Play Console account
- **Duration**: 45 minutes
- **Difficulty**: Intermediate

### 2. Create a New Unity Project
- Create 3D URP project
- Configure Android build settings
- Import Google Play Games Plugin

### 3. Configure Play Console
- Create app in Google Play Console
- Enable Play Games Services
- Create OAuth credentials
- Add test accounts

### 4. Implement Sign-In
```csharp
// PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//     .EnableSavedGames()
//     .Build();
// PlayGamesPlatform.InitializeInstance(config);
// PlayGamesPlatform.Activate();
// Social.localUser.Authenticate((success) => { ... });
```

### 5. Implement Cloud Save
- Serialize game state to byte array
- Open saved game snapshot
- Write data to cloud
- Read and restore on sign-in

### 6. Add Achievements & Leaderboards
- Define achievements in Play Console
- Unlock achievement on game event
- Submit score to leaderboard
- Display leaderboard UI

### 7. Congratulations!
- Summary of what was built
- Links: API reference, sample project, community forum

---

## API Documentation Standards

### Method Documentation Format
```
## Method: createPlayer

Creates a new player profile.

**HTTP Request**
POST https://api.example.com/v1/players

**Request Body**
| Field | Type | Required | Description |
|-------|------|----------|-------------|
| displayName | string | Yes | Player's display name (3-30 chars) |
| region | string | No | ISO 3166-1 region code |

**Response**
Returns a Player resource.

**Errors**
| Code | Description |
|------|-------------|
| 400 | Invalid display name format |
| 409 | Display name already taken |

**Example**
[Include curl command and response JSON]
```

### Documentation Checklist
- [ ] Every parameter documented with type and constraints
- [ ] Error codes listed with human-readable descriptions
- [ ] Working code examples in at least 2 languages
- [ ] Authentication requirements stated
- [ ] Rate limits documented
- [ ] Versioning scheme explained
