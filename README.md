> just a draft

- Response Wrapper for global responses
- Request and response models, so I don't expose my entities
- Mapster for better object mapping
- Entity Framework as ORM
- MediatR and CQRS
- FluentValidation for request and domain level validations via pipeline
- Redis for caching

To do:
- Implement fluent validation for all commands and queries
- Create documentation for all endpoints (agents and properties)
- Write automated tests (unit and integration)
- Create issue: when updating an agent or a property, rewrite information in cache (when you get and agent by id, update this agent and then get by id again, it shows the old information)
