# Self-Guided System Design Mock Interview — 45 Minutes

## Problem Selection
Choose one. Design it at Google scale.
1. URL Shortener (Reference: 03-devrel-prep/01-SystemDesign.md)
2. Game Leaderboard
3. Push Notification System
4. Chat System (WhatsApp-like)
5. Video Streaming Platform

---

## Timer Schedule

| Time | Phase | What To Do |
|------|-------|-----------|
| 0:00-0:05 | Requirements | List functional & non-functional requirements. Estimate QPS, storage. |
| 0:05-0:15 | High-Level Design | Draw architecture. Name every component. Show data flow arrows. |
| 0:15-0:35 | Deep Dive | Pick 2 critical components. Discuss: data model, API design, scaling. |
| 0:35-0:45 | Trade-offs | Discuss bottlenecks, failure modes, how to scale further. |

---

## Structured Approach Template

### 1. Requirements (5 min)
**Functional:**
- _What can the user do?_
- _List 3-5 core features_

**Non-Functional:**
- _How many users?_ DAU: ___
- _Read vs Write ratio?_ ___:___
- _Latency requirement?_ <___ ms
- _Availability target?_ ___% uptime

**Estimation:**
- QPS: ___ (peak: ___)
- Storage/year: ___
- Bandwidth: ___

### 2. High-Level Design (10 min)
Draw on paper/whiteboard:
- [ ] Client
- [ ] Load Balancer
- [ ] API Servers
- [ ] Cache layer
- [ ] Database(s)
- [ ] Message Queue (if async needed)
- [ ] CDN (if static content)
- [ ] Arrows showing data flow

### 3. Deep Dive (20 min)
Pick 2 components and detail:
- **Data Model**: Tables/collections, key schema, indexes
- **API Design**: Endpoints, request/response format
- **Scaling**: Sharding strategy, replication, caching

### 4. Trade-offs & Extensions (10 min)
- What's the single point of failure?
- How would you handle 10x traffic?
- What's the consistency model?
- How would you monitor this system?

---

## Self-Evaluation Checklist

| Criterion | Yes | Partial | No |
|-----------|-----|---------|-----|
| Clarified requirements before designing | | | |
| Drew clear architecture diagram | | | |
| Justified technology choices | | | |
| Discussed data model with schema | | | |
| Defined API endpoints | | | |
| Addressed scalability (sharding, caching) | | | |
| Discussed trade-offs (CAP, consistency) | | | |
| Considered failure modes | | | |
| Stayed within time limits | | | |
| Communicated clearly throughout | | | |

**Score**: ___ / 10 "Yes" answers

- 8-10: Ready for the interview
- 5-7: Good foundation, practice more
- Below 5: Review 01-SystemDesign.md and retry
