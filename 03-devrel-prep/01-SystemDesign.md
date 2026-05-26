# System Design — Interview Preparation

## The 4-Step Framework (45 Minutes)

| Step | Time | What To Do |
|------|------|-----------|
| 1. Requirements | 5 min | Clarify functional & non-functional requirements |
| 2. High-Level Design | 10 min | Draw core components, data flow |
| 3. Deep Dive | 20 min | Detail 1-2 critical components |
| 4. Wrap Up | 10 min | Trade-offs, bottlenecks, scaling |

---

## Core Building Blocks

### Load Balancers
- **L4 (Transport)**: Routes TCP/UDP packets. Fast, no content inspection.
- **L7 (Application)**: Routes HTTP requests. Can route by URL, headers.
- Algorithms: Round Robin, Least Connections, IP Hash, Weighted

### Databases
| Type | Examples | Use When |
|------|----------|----------|
| **Relational (SQL)** | PostgreSQL, MySQL, Cloud Spanner | ACID required, complex queries, joins |
| **Document (NoSQL)** | MongoDB, Firestore | Flexible schema, rapid iteration |
| **Key-Value** | Redis, Memcached | Caching, sessions, rate limiting |
| **Wide-Column** | Bigtable, Cassandra | Time-series, IoT, analytics |
| **Graph** | Neo4j | Social networks, recommendations |

### Caching Strategy
- **Cache-Aside**: App checks cache first → miss → read DB → populate cache
- **Write-Through**: Write to cache + DB simultaneously (consistent but slow)
- **Write-Behind**: Write to cache → async write to DB (fast but risky)
- **Eviction**: LRU (most common), LFU, TTL-based

### Message Queues
- Decouple producers from consumers
- Buffer spikes in traffic
- Enable async processing
- Examples: Pub/Sub, Kafka, RabbitMQ, SQS

### CDN (Content Delivery Network)
- Cache static content at edge locations worldwide
- Reduce latency for global users
- Examples: Cloud CDN, CloudFlare, Akamai

---

## CAP Theorem
- **Consistency**: Every read gets the latest write
- **Availability**: Every request gets a response
- **Partition Tolerance**: System works despite network failures

**You can only guarantee 2 of 3:**
- CP: Banking systems (consistency critical) — sacrifice availability during partitions
- AP: Social media feeds (availability critical) — sacrifice immediate consistency
- In practice: tune consistency per feature (strong for payments, eventual for likes)

---

## Estimation Cheatsheet

| Metric | Formula |
|--------|---------|
| QPS | DAU × avg_requests_per_user / 86,400 |
| Peak QPS | QPS × 2-5 |
| Storage/year | records/year × avg_record_size |
| Bandwidth | QPS × avg_response_size |

Quick numbers: 1 day = 86,400s ≈ 100K seconds | 1 million req/day ≈ 12 QPS

---

## Design Exercise 1: URL Shortener (Google Scale)

### Requirements
- Functional: shorten URL, redirect, custom aliases, analytics
- Non-functional: 100M new URLs/day, 10:1 read/write, <100ms latency, 99.99% uptime

### High-Level Design
```
Client → Load Balancer → API Servers → Cache (Redis) → Database
                                          ↓
                                    Analytics Pipeline
```

### Key Decisions
1. **URL Encoding**: Base62 (a-z, A-Z, 0-9) → 62^7 = 3.5 trillion unique codes
2. **ID Generation**: Pre-generated IDs (counter service) > hash-based (collision risk)
3. **Database**: NoSQL (DynamoDB/Bigtable) — simple key-value, massive scale
4. **Caching**: Redis with LRU eviction — cache hot URLs (80/20 rule)
5. **Analytics**: Async pipeline — log clicks to Pub/Sub → BigQuery

---

## Design Exercise 2: Real-Time Game Leaderboard

### Requirements
- Functional: Submit score, Get top-K, Get player rank, Nearby rankings
- Non-functional: 10M DAU, <50ms for rank queries, real-time updates

### High-Level Design
```
Game Client → API Gateway → Score Service → Redis Sorted Set
                                ↓
                          Persistence (Bigtable)
```

### Key Decisions
1. **Ranking**: Redis SORTED SET → `ZADD` O(log N), `ZRANK` O(log N), `ZRANGE` O(K+log N)
2. **Sharding**: Shard by game mode, not by user (need global ranking per mode)
3. **Persistence**: Async write to Bigtable for historical data
4. **Nearby ranks**: `ZRANGE` with player's rank ± 10

---

## Design Exercise 3: Push Notification System

### Requirements
- Functional: Send notifications (scheduled, triggered, broadcast), user preferences
- Non-functional: 500M users, 1B notifications/day, <5s delivery

### High-Level Design
```
Trigger Source → Notification Service → User Preference Filter
                                            ↓
                                   Platform Dispatchers
                                   ├── APNs (iOS)
                                   ├── FCM (Android/Web)
                                   └── Email Service
```

### Key Decisions
1. **Queue**: Pub/Sub for decoupled, at-least-once delivery
2. **Rate Limiting**: Per-user throttle (max 5/hour) to prevent spam
3. **Template Engine**: Pre-compile templates, inject user data at send time
4. **Preferences**: User settings stored in low-latency cache (Redis)
5. **Tracking**: Delivery receipts, open tracking, analytics pipeline
