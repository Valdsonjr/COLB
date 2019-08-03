# COLB
Controle de OLs e Branches

Decisões de projeto:
1. Core - O Entity Framework Core possui o contexto InMemory da microsoft para testes reproduzíveis.
2. OData x Swagger - O Swagger não consegue ver Controllers OData, logo eu coloquei todas as consultas odata em um único controller.
3. Code First - O Reverse Poco Generator ainda não funciona com o EF Core.

# A fazer:
- [ ] Consertar o OData em DTOs
- [ ] Adicionar autenticação JWT
- [ ] Criar endpoints de alteração parcial (PATCH)
- [ ] Model Binders customizados
- [ ] MAIS TESTES