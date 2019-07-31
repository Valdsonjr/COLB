# COLB
Controle de OLs e Branches

Decisões de projeto:
1. Core - O Entity Framework Core possui o contexto InMemory da microsoft para testes reproduzíveis.
2. OData - Como o projeto é simples e não possui informações sigilosas nem necessidade de autenticação, o OData pode ser conectado direto nas entidades do banco para maior flexibilidade.
3. OData x Swagger - O Swagger não consegue ver Controllers OData, logo eu coloquei todas as consultas odata em um único controller.
4. Code First - O Reverse Poco Generator ainda não funciona com o EF Core.
