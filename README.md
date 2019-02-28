# Cadastro API

Api responsável pelos cadastros de várias entidade do sistema

## Cadastros Entidades
* Rotas
* Clones


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
    * GET: Busca todos os trechos

api/rotas/trechos/{id}
* Métodos
    * GET: Busca por id do trecho

## Rotas
Rota completa para ir de um ponto ao outro da linha, utilizando trechos e as direções desses trechos.

* Json 1
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

* Json 2
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

* Json 3
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
```sh
api/rotas
```
* Métodos
   * GET: Busca todas as rotas cadastradas sem (Json 1).
   * POST: Cria uma nova rota no cadastro
       * Obrigatório: (Json 2)
```sh
api/rotas/{id}
```
* Métodos
    * GET: Busca por id da rota retornando os detalhes da rota (Json 3).
    * PUT: Atualiza uma rota no cadastro
        * Obrigatório: Body (Json 2)

---------------------------------------------------------------------------------------
# Clone
Endpoints para telas de cadastro de clones

## Município
Municípios que cadastrados no sistema.

* Json Municípo
```json
[
    {
        "id": 1,
        "municipio": "São Paulo"
    },
    {
        "id": 2,
        "municipio": "São Caetano do Sul"
    },
    {
        "id": 3,
        "municipio": "Três Lagoas"
    }
]
```

### Endpoint
```sh
api/clone/municipio
```
* Métodos
   * GET: Busca todos os municípios cadastrados.

## Clone
Endpoint para cadastro das informações do cabeçalho de um clone

* Json 1 - Get
```json
[
    {
        "id": 1,
        "codigo": "123",
        "descricao": "teste",
        "cor": "#000000",
        "ativo": true,
        "percentualBifurcada": 10,
        "portaEnxerto": "1",
        "nomeCientifico": "1",
        "nomeComum": "1",
        "categoria": "1",
        "municipioColetaId": 1,
        "municipioColeta": null,
        "criterioSelecao": "1",
        "intensidadeSelecao": "1",
        "municipoTesteId": 2,
        "municipoTeste": null,
        "codigoCultivar": "1",
        "numeroRegistroCultivar": "1",
        "dataRegistroCultivar": "2019-02-15T00:00:00",
        "mantenedoraCultivar": "1",
        "rotaIdCasaVegetacao": 1,
        "rotaCasaVegetacao": null,
        "dataMigracaoSap": "2019-02-15T00:00:00"
    }
]
```

* Json 2 - Get por Id
```json
{
    "id": 1,
    "codigo": "123",
    "descricao": "teste",
    "cor": "#000000",
    "ativo": true,
    "percentualBifurcada": 10,
    "portaEnxerto": "1",
    "nomeCientifico": "1",
    "nomeComum": "1",
    "categoria": "1",
    "municipioColetaId": 1,
    "municipioColeta": {
        "id": 1,
        "municipio": "São Paulo"
    },
    "criterioSelecao": "1",
    "intensidadeSelecao": "1",
    "municipoTesteId": 2,
    "municipoTeste": {
        "id": 2,
        "municipio": "São Caetano do Sul"
    },
    "codigoCultivar": "1",
    "numeroRegistroCultivar": "1",
    "dataRegistroCultivar": "2019-02-15T00:00:00",
    "mantenedoraCultivar": "1",
    "rotaIdCasaVegetacao": 1,
    "rotaCasaVegetacao": {
        "id": 1,
        "nome": "Rota 1",
        "prioridade": 1,
        "ativo": true,
        "trechoId": null,
        "trechoRota": null
    },
    "dataMigracaoSap": "2019-02-15T00:00:00"
}
```

* Json 3 - Put
```json
{
    "id": 1,
    "codigo": "123",
    "descricao": "teste",
    "cor": "#000000",
    "ativo": true,
    "percentualBifurcada": 10,
    "portaEnxerto": "1",
    "nomeCientifico": "1",
    "nomeComum": "1",
    "categoria": "1",
    "municipioColetaId": 1,
    "municipioColeta": {
        "id": 1,
        "municipio": "São Paulo"
    },
    "criterioSelecao": "1",
    "intensidadeSelecao": "1",
    "municipoTesteId": 2,
    "municipoTeste": {
        "id": 2,
        "municipio": "São Caetano do Sul"
    },
    "codigoCultivar": "1",
    "numeroRegistroCultivar": "1",
    "dataRegistroCultivar": "2019-02-15T00:00:00",
    "mantenedoraCultivar": "1",
    "rotaIdCasaVegetacao": 1,
    "rotaCasaVegetacao": {
        "id": 1,
        "nome": "Rota 1",
        "prioridade": 1,
        "ativo": true,
        "trechoId": null,
        "trechoRota": null
    },
    "dataMigracaoSap": "2019-02-15T00:00:00"
}
```

### Endpoint
```sh
api/clone
```
* Métodos
   * GET: Busca de todos os clones cadastrados (JSon 1).

```sh
api/clone/{id}
```
* Métodos
   * GET: Busca pelo id de cadastro do clone (JSon 2).
   * PUT: Atualiza as informações do clone.
        * Obrigatório: Body (Json 3)


## Seleção
Endpoint para cadastro das informações das etapas de seleção e classificação.

* Json 1 - Get por Clone Id
```json
[
    {
        "id": 1,
        "selecao": "SOMBREAMENTO",
        "classificacao": [
            {
                "id": 1,
                "classificacao": "A",
                "classificacaoPorClone": {
                    "id": 1,
                    "cloneId": 1,
                    "classificacaoId": 1,
                    "altura": 10,
                    "diametroColo": 10,
                    "quantidadePares": 10,
                    "angulo": 10,
                    "quantidadeRamificacoes": 10,
                    "presencaBifurcacao": 10,
                    "coloracao": "10",
                    "tamanhoRamificacao": 10,
                    "posicaoRamificacao": 10,
                    "danosAnatomicos": "10",
                    "manchasFoliares": "10",
                    "tamanhoAreaFoliar": 10,
                    "espacamentoBandeja": "10",
                    "rotaId": 2,
                    "tempoGalpao": 60,
                    "tempoFase": 1
                }
            },
            {
                "id": 2,
                "classificacao": "B",
                "classificacaoPorClone": {
                    "id": 2,
                    "cloneId": 1,
                    "classificacaoId": 2,
                    "altura": 15,
                    "diametroColo": 15,
                    "quantidadePares": 15,
                    "angulo": 15,
                    "quantidadeRamificacoes": 15,
                    "presencaBifurcacao": 15,
                    "coloracao": "15",
                    "tamanhoRamificacao": 15,
                    "posicaoRamificacao": 15,
                    "danosAnatomicos": "15",
                    "manchasFoliares": "15",
                    "tamanhoAreaFoliar": 15,
                    "espacamentoBandeja": "15",
                    "rotaId": 2,
                    "tempoGalpao": 60,
                    "tempoFase": 1
                }
            }
        ]
    }
]
```

* Json 2 - POST/PUT 
```json
{
    "id": 2,
    "selecao": "CRESCIMENTO",
    "classificacao": [
        {
            "id": 4,
            "classificacao": "B",
            "classificacaoPorClone": {
                "id": 5,
                "cloneId": 1,
                "classificacaoId": 4,
                "altura": 40,
                "diametroColo": 40,
                "quantidadePares": 40,
                "angulo": 40,
                "quantidadeRamificacoes": 40,
                "presencaBifurcacao": 40,
                "coloracao": "40",
                "tamanhoRamificacao": 40,
                "posicaoRamificacao": 40,
                "danosAnatomicos": "40",
                "manchasFoliares": "04",
                "tamanhoAreaFoliar": 40,
                "espacamentoBandeja": "40",
                "rotaId": 1,
                "tempoGalpao": 60,
                "tempoFase": 1
            }
        }
    ]
}
```

### Endpoint
```sh
api/clone/selecao/{cloneId}
```
* Métodos
   * GET: Retorna o json com as informações de seleção e classificação do clone (JSon 1).

```sh
api/clone/selecao/{cloneId}
```
* Métodos
   * POST: Cadastra as informações da seleção e classificação.
        * Obrigatório: Body (JSon 2)

   * PUT: Atualiza as informações da seleção e classificação.
        * Obrigatório: Body (Json 2)

# Pessoas

 

API com a função de editar e vizualizar pessoas.

 

* Json 1 - GET

```json

{

    "id": 1,

    "matricula": "12345",

    "nome": "Fulano",

    "admissão": "0001-01-01T00:00:00",

    "cargo": "ajudante",

    "dtNasc": "0001-01-01T00:00:00",

    "estadoCivil": "Solteiro",

    "sexo": "Masculino",

    "nvEscolar": null,

    "escalaTrabalho": "Integral",

    "municipioOrigem": "São Caetano do Sul",

    "situação": null,

    "tempoEmpresa": "366",

    "crachaRFID": null,

    "login": "fulanin",

    "password": null,

    "ativo": true

}

```

 

* Json 2 - PUT

```json

{

    "id": 1,

    "matricula": "12345",

    "nome": "Fulano",

    "admissão": "0001-01-01T00:00:00",

    "cargo": "ajudante",

    "dtNasc": "0001-01-01T00:00:00",

    "estadoCivil": "Solteiro",

    "sexo": "Masculino",

    "nvEscolar": null,

    "escalaTrabalho": "Integral",

    "municipioOrigem": "São Caetano do Sul",

    "situação": null,

    "crachaRFID": null,

    "login": "fulanin",

    "password": null,

    "ativo": true

}

```

 

### Endpoint
 
```sh
api/Pessoas
```
* Métodos

    * GET: Busca todas as pessoas (json 1)

 
```sh
api/Pessoas/{id}
```
* Métodos

    * GET: Busca pessoa por id (json 1)

 
```sh
api/Pessoas/id
```
* Métodos

    * PUT: Busca pessoa por id para que seja feita a alteração de qualquer campo onde essa ação seja permitida (json 2)


--------------------------------------------------------------

# Lote
Endpoint para cadastro e retorno dos lotes cadastrados

* Json 1  - Get
```json
[
    {
        "id": 4,
        "ordemProducaoId": 2,
        "qntMuda": 1,
        "qntPerdida": 1,
        "status": true,
        "semana": 1,
        "lote": "1"
    },
    {
        "id": 5,
        "ordemProducaoId": 2,
        "qntMuda": 1,
        "qntPerdida": 1,
        "status": true,
        "semana": 1,
        "lote": "1"
    },
    {
        "id": 6,
        "ordemProducaoId": 2,
        "qntMuda": 1,
        "qntPerdida": 1,
        "status": true,
        "semana": 1,
        "lote": "1"
    }
]

* Json 2 - Get por Id
```json

 {
    "id": 4,
    "ordemProducaoId": 2,
    "qntMuda": 1,
    "qntPerdida": 1,
    "status": true,
    "semana": 1,
    "lote": "1"
 }


* Json 3 - POST
```json

 {
    "id": 6,
    "ordemProducaoId": 2,
    "qntMuda": 1,
    "qntPerdida": 1,
    "status": true,
    "semana": 1,
    "lote": "1"
 }
```

### Endpoint
```sh
/api/lote
```
* Métodos
    * GET: Busca todos os lotes
```sh
/api/lote/{id}
```
* Métodos
    * GET: Busca por id do lote
    * POST: Cadastra as informações do lote
        * Obrigatório: Body (JSon 3) 
```sh
/api/lote/semanaVigente
```
    * GET: Retorna apenas os lote da semana vigente
