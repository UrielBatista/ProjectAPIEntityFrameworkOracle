# API com CQRS e MediaTR

Projeto tem como princípio emular um mini sistema de cadastro e pedido de comida.

## Tecnologias Usadas

 - OracleDB/PL-SQL
 - .NET Core
 - Entity Framework
 - GraphQL
 - OData Request
 - RabbitMQ
 - MediaTR
 - AutoMapper
 - FluentValidation


## Documentação da API
Esta API foi documentada pelo Swagger que pode ser acessado da seguinte forma:

### Localmente:
```bash
localhost:port/swagger/index.html
```
### Publicamente:
```bash
base_url:port/swagger/index.html
```
### Rodar o RabbitMQ via Docker:
```bash
docker run --platform linux/arm64 -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```
## Testes
Esse projeto conta com uma cobertura atual de testes unitários de 55%, segue screenshot como exemplo:

![Imagem de teste 1](https://github.com/UrielBatista/ProjectAPIEntityFrameworkOracle/blob/main/CodeCoveragePercentualTest/screenshot.png)

![Imagem de teste 2](https://github.com/UrielBatista/ProjectAPIEntityFrameworkOracle/blob/main/CodeCoveragePercentualTest/screenshot1.png)

## Arquivo Insomnia
Esse projeto ainda conta com um payload onde auxiliara no uso dos endpoints, você pode baixa-lo logo a baixo:

[Insomnia-endpoint-PersonAPI](https://drive.google.com/file/d/15AUBvYMk0ABjBW8oDZbLH49wDg3tP3Lb/view?usp=sharing)

## Contribuidores
Projeto apenas para simulação.

Ass: Uriel Batista

## License
[MIT](https://choosealicense.com/licenses/mit/)
