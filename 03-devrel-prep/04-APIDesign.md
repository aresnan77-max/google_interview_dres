# API Design — Google Standards

## RESTful Design Principles

### Resource-Oriented Design
- **Resources** are nouns, not verbs: `/players/{id}` ✅ not `/getPlayer` ❌
- **Collections**: `/players` (plural)
- **Document**: `/players/{id}` (single resource)
- **Sub-collections**: `/players/{id}/achievements`
- **Standard methods**: GET (read), POST (create), PUT (replace), PATCH (update), DELETE

### URL Structure
```
https://api.example.com/v1/games/{gameId}/players/{playerId}/scores
         ─── host ───── ── v── ─── collection ── ─ resource ─ ── sub ──
```

---

## Google API Design Guide — Key Points

### Naming Conventions
| Element | Convention | Example |
|---------|-----------|---------|
| Collection | camelCase, plural | `gameServers`, `playerProfiles` |
| Resource | camelCase, singular | `gameServer`, `playerProfile` |
| Fields | camelCase | `displayName`, `createTime` |
| Enum values | UPPER_SNAKE | `GAME_MODE_RANKED`, `STATUS_ACTIVE` |

### Standard Fields (Google Standard)
| Field | Type | Description |
|-------|------|-------------|
| `name` | string | Resource name (e.g., `players/12345`) |
| `createTime` | Timestamp | When the resource was created |
| `updateTime` | Timestamp | Last modification time |
| `deleteTime` | Timestamp | Soft-delete timestamp |
| `etag` | string | Concurrency control |

### Error Handling (Google Standard)
```json
{
  "error": {
    "code": 400,
    "message": "Display name must be 3-30 characters.",
    "status": "INVALID_ARGUMENT",
    "details": [
      {
        "@type": "type.googleapis.com/google.rpc.BadRequest",
        "fieldViolations": [
          { "field": "displayName", "description": "Too short" }
        ]
      }
    ]
  }
}
```

### Standard Error Codes
| HTTP | gRPC | Meaning |
|------|------|---------|
| 400 | INVALID_ARGUMENT | Client sent bad data |
| 401 | UNAUTHENTICATED | Missing/invalid credentials |
| 403 | PERMISSION_DENIED | Authenticated but not authorized |
| 404 | NOT_FOUND | Resource doesn't exist |
| 409 | ALREADY_EXISTS | Conflict with existing resource |
| 429 | RESOURCE_EXHAUSTED | Rate limited |
| 500 | INTERNAL | Server bug |
| 503 | UNAVAILABLE | Service temporarily down |

---

## Versioning
- **URL versioning**: `/v1/players` → `/v2/players` (Google's approach)
- **Header versioning**: `Accept: application/vnd.api+json;version=2`
- Rule: Never break existing clients. Add fields, don't remove them.

---

## Pagination (Google Style)
```
GET /v1/players?pageSize=20&pageToken=abc123

Response:
{
  "players": [...],
  "nextPageToken": "def456"   // empty string = no more pages
}
```
- Use **opaque page tokens** (not offset-based — those break with inserts/deletes)
- Default page size: 20-100. Max: 1000.
- Total count is optional (can be expensive to compute)

---

## Field Masks (Partial Responses)
```
GET /v1/players/123?fields=displayName,score
PATCH /v1/players/123?updateMask=displayName
```
- Reduce bandwidth by requesting only needed fields
- Required for PATCH — specify which fields are being updated
- Google APIs use `FieldMask` extensively

---

## gRPC vs REST

| Aspect | REST | gRPC |
|--------|------|------|
| Protocol | HTTP/1.1 (JSON) | HTTP/2 (Protobuf) |
| Payload | Human-readable JSON | Binary (smaller, faster) |
| Streaming | Limited (SSE/WebSocket) | Built-in bidirectional |
| Code Gen | Manual or OpenAPI | Automatic from .proto |
| Browser | Native support | Requires grpc-web proxy |
| Use Case | Public APIs, web apps | Internal services, mobile |

### When to Choose
- **REST**: Public-facing APIs, web clients, simplicity
- **gRPC**: Microservice communication, mobile apps, streaming, performance

---

## Design Review Checklist

- [ ] Resources named correctly (nouns, plural collections)
- [ ] Standard HTTP methods used appropriately
- [ ] Consistent error format with actionable messages
- [ ] Pagination implemented for list endpoints
- [ ] Authentication documented (OAuth2, API key)
- [ ] Rate limits defined and communicated in headers
- [ ] Versioning strategy established
- [ ] Idempotency for PUT/DELETE
- [ ] Field masks for partial updates
- [ ] Request validation with clear error responses
