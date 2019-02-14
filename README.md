# Cadastro API

Api responsável pelos cadastros de várias entidade do sistema

## Cadastro
* Rotas

# Rotas

## Trechos
Pequenos trechos da linha utilizada para criar uma rota
```json
[
    {
        "id": 1,
        "trecho": "Trecho 1",
        "ativo": true,
        "direcao": [
            {
                "id": 1,
                "sentido": "Cima",
                "ativo": true,
                "proximoTrecho": [
                    {
                        "id": 1,
                        "trechoId": 2
                    }
                ]
            }
        ]
    }
]
```

### Endpoint

api/rotas/trechos
* Métodos
    GET: Busca todos os trechos

api/rotas/trechos/{id}
* Métodos
    GET: Busca por id do trecho

## Rotas
Rota completa para ir de um ponto ao outro da linha, utilizando trechos e as direções desses trechos.

Json 1
```json
[
    {
        "id": 1,
        "nome": "Rota 1",
        "prioridade": 1,
        "ativo": true,
        "trechoId": null,
        "trechoRota": null
    },
    {
        "id": 2,
        "nome": "Rota 2",
        "prioridade": 1,
        "ativo": true,
        "trechoId": null,
        "trechoRota": null
    }
]
```

Json 2
```json
{
    "nome": "Rota 15",
    "prioridade": 1,
    "ativo": true,
    "trechoId": [
        {
            "sentidoId": 4,
            "ordem": 2
        },
        {
            "sentidoId": 1,
            "ordem": 1
        }
    ]
}
```

Json 3
```json
{
    "id": 1,
    "nome": "Rota 1",
    "prioridade": 1,
    "ativo": true,
    "trechoId": [
        {
            "id": 2,
            "sentidoId": 4,
            "ordem": 1,
            "rotaId": 1
        },
        {
            "id": 1,
            "sentidoId": 1,
            "ordem": 2,
            "rotaId": 1
        }
    ],
    "trechoRota": [
        {
            "id": 2,
            "trecho": "Trecho 2",
            "ativo": true,
            "direcao": [
                {
                    "id": 4,
                    "sentido": "Direita",
                    "ativo": true,
                    "proximoTrecho": null
                }
            ]
        },
        {
            "id": 1,
            "trecho": "Trecho 1",
            "ativo": true,
            "direcao": [
                {
                    "id": 1,
                    "sentido": "Cima",
                    "ativo": true,
                    "proximoTrecho": null
                }
            ]
        }
    ]
}
```



### Endpoint

api/rotas
* Métodos
    GET: Busca todas as rotas cadastradas sem (Json 1).
    POST: Cria uma nova rota no cadastro
        Obrigatório: (Json 2)

api/rotas/{id}
* Métodos
    GET: Busca por id da rota retornando os detalhes da rota (Json 3).
    PUT: Atualiza uma rota no cadastro
        Obrigatório: (Json 2)
