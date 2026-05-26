# System Design Cheatsheet

## Quick Decision Guides

### Database Selection
```
Need ACID + complex queries?          → PostgreSQL / Cloud Spanner
Need flexible schema + fast dev?      → MongoDB / Firestore
Need caching / sessions?              → Redis
Need time-series / analytics?         → Bigtable / InfluxDB
Need full-text search?                → Elasticsearch
Need graph relationships?             → Neo4j
```

### Caching Selection
```
Read-heavy, tolerance for stale?      → Cache-Aside (most common)
Need strong consistency?              → Write-Through
Need high write throughput?           → Write-Behind (async)
Global distribution needed?           → CDN at edge
```

### Communication Pattern
```
Request-Response (sync)?              → REST / gRPC
Event-driven (async)?                 → Message Queue (Pub/Sub, Kafka)
Real-time bidirectional?              → WebSocket
Streaming data?                       → gRPC streaming / Kafka
```

## Estimation Quick Reference

| What | Number | Notes |
|------|--------|-------|
| Seconds in a day | 86,400 ≈ 10⁵ | Round to 100K |
| Seconds in a year | ~3 × 10⁷ | ~30 million |
| 1 char (ASCII) | 1 byte | |
| 1 char (UTF-8) | 1-4 bytes | Most CJK = 3 bytes |
| UUID | 16 bytes | 128 bits |
| Integer | 4-8 bytes | int32 / int64 |
| Typical URL | ~100 bytes | |
| Typical tweet | ~300 bytes | Text + metadata |
| Typical image | ~300 KB | Compressed JPEG |
| SSD random read | ~100μs | |
| Network round-trip (same DC) | ~0.5ms | |
| Network round-trip (cross-region) | ~50-150ms | |

## Common Architectures

### Read-Heavy System (e.g., URL Shortener)
```
Client → LB → API → Cache (Redis) → DB (NoSQL)
                        ↓ miss
                       DB read → Cache write
```

### Write-Heavy System (e.g., Logging)
```
Client → LB → API → Message Queue → Workers → DB (Write-optimized)
                                        ↓
                                   Search Index
```

### Real-Time System (e.g., Chat/Game)
```
Client ↔ WebSocket Server ↔ Pub/Sub → Other WS Servers → Other Clients
                              ↓
                          Persistence
```

## Scalability Toolkit

| Technique | When | How |
|-----------|------|-----|
| **Horizontal scaling** | More traffic | Add more servers behind LB |
| **Database sharding** | DB bottleneck | Partition data by key (user_id % N) |
| **Read replicas** | Read-heavy | Primary for writes, replicas for reads |
| **Caching** | Repeated reads | Redis/Memcached, CDN for static |
| **Async processing** | Slow operations | Queue + workers for email, video, etc. |
| **Rate limiting** | Abuse prevention | Token bucket / sliding window |
| **Circuit breaker** | Cascading failures | Fail fast when dependency is down |
