# COLB
Controle de OLs e Branches

Decisões de projeto:
1. Core - O Entity Framework Core possui o contexto InMemory da microsoft para testes reproduzíveis.
2. OData x Swagger - O Swagger não consegue ver Controllers OData, logo eu coloquei todas as consultas odata em um único controller.
3. Code First - O Reverse Poco Generator ainda não funciona com o EF Core.

# Instalação

1. Criar um novo eventlog no windows
```
New-EventLog -LogName COLB -Source API
```

2. Configurar o appsettings.json

# A fazer:
- [ ] Consertar o OData em DTOs
- [x] ~~Adicionar autenticação JWT~~ (Falta incluir validação de audience e issuer)
- [ ] Criar endpoints de alteração parcial (PATCH)
- [x] Model Binders customizados
- [ ] Separar as configurações do swagger do startup
- [ ] MAIS TESTES