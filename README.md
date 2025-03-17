# INVEST_CHALLENGE

Este projeto é uma plataforma de investimentos desenvolvida para auxiliar usuários a obter ativos de renda fixa de forma eficiente e intuitiva.

## Funcionalidades

- **Login**: Usuário: `Adm` | Senha: `Adm123`
- **Exibir saldo**: O saldo do cliente estará sempre atualizado.
- **Listar produtos**: Lista todos os produtos cadastrados no sistema.
- **Selecionar produto**: O cliente pode selecionar um produto e informar a quantidade desejada.
- **Comprar produto**: Ao clicar em "Comprar", será realizado o processamento da compra, atualizando o saldo do cliente e o estoque do produto.

## Tecnologias Utilizadas

Este projeto é construído com uma combinação de tecnologias para oferecer uma plataforma de investimentos completa.

- **Backend**:
  - C#
  - .NET Core 8.0
  - API RESTful
 
- **Frontend**:
  - HTML5
  - CSS3
  - JavaScript
  - Bootstrap

- **Banco de Dados**:
  - SQL Server
  
- **Testes**:
  - xUnit
  - Moq
  
## Instalação

Para rodar este projeto localmente, siga as instruções abaixo.

### Pré-requisitos

Antes de começar, certifique-se de que você tem as seguintes ferramentas instaladas:

- [**.NET Core SDK**](https://dotnet.microsoft.com/download) (Versão recomendada: 8.0 ou superior)
- [**SQL Server**](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


### Execução do Projeto

1. **Clone o repositório:**
```bash
git clone https://github.com/lucianapestana/INVEST_CHALLENGE.git
cd INVEST_CHALLENGE
```
   
2. **Configure o Banco de Dados:**
- Execute os scripts localizados em /INVEST_CHALLENGE/SQL_SCRIPTS.
 
3. **Configure as Strings de Conexão:**
- No arquivo appsettings.json do projeto **INVEST.API**, atualize a string de conexão com as credenciais do seu banco de dados.
- No arquivo appsettings.json do projeto **INVEST.SITE**, atualize a URL com as informações do seu localhost.

4. **Inicie a API:**
```bash
cd INVEST.API
dotnet run
```

5. **Inicie o Frontend:**
```bash
cd INVEST.SITE
npm install
npm start
```

## Testes

Para executar os testes unitários:

```bash
cd INVEST.API.TEST
dotnet test
```
