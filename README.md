﻿<h1 align="center">
  Apresentação de projeto para auditoria de tabelas utilizando EntityFramework
  </h1>

<h4 align="center">
	🚧  Github DotNet_Db_EF_Audit ♻️ Concluído 🚀 🚧
</h4>

<p align="center">
 <a href="#-sobre-o-projeto">Sobre</a> •
 <a href="#-funcionalidades">Funcionalidades</a> •
 <a href="#-como-executar-o-projeto">Como executar</a> •
 <a href="#-tecnologias">Tecnologias</a> •
 <a href="#-problemas-encontrados">Problemas encontrados</a> •
 <a href="#user-content--licença">Licença</a>
</p>

---

## 💻 Sobre o projeto

O projeto consiste em apresentar como realizar uma auditoria para CRUD em cada tabela utilizada no sistema.

O usuário poderá realizar a criação, edição busca e exclusão de informação, mas somente ações como criar, editar e excluir serão registrados na tabela AuditTrail.

Além disso, está disponível para três variações de banco de dados: Sql Server, MySql e Postegres.

Dentro do código coloquei alguns comentários necessário para compreender quando utilizar SQL Server, MySql ou Postegres.

✅ Arquitetura Limpa <br/>
✅ .Net 8 <br/>
✅ EntityFramework <br/>
✅ Pomelo <br/>
✅ Npgsql <br/>
✅ NWebse <br/>
✅ Jwt <br/>

---

## 🚀 Como executar o projeto

Para rodar o projeto, será necessário realizar realizar as seguintes etapas de acordo com o banco de dados desejado.

### 1-Docker

Será necessário rodar o arquivo docker-compose.yaml dentro do projeto.
Via terminal, acessar o repositório onde está o arquivo e executar o comando abaixo:

```
	docker-compose up -d
```

### 1-Migrations

Para criação das tabelas, será necessário, primeiramente, escolher o banco de dados utilizado.

#### 1-SqlServer || Postegres || MySql

Dentro de 'AuditTrailConfiguration' existe o mesmo trecho de código para cada banco de dados, entretanto, existem variações, conforme abaixo:

```
  // ------ TO POSTEGRES ----
  builder.Property(e => e.ChangedColumns).HasColumnType("jsonb");
  builder.Property(e => e.OldValues).HasColumnType("jsonb");
  builder.Property(e => e.NewValues).HasColumnType("jsonb");
```

Dentro de 'InfraModule' também existe o mesmo conceito de código para cada banco de dados.

```
 // ------ TO POSTEGRES ----
 services.AddPostgres(configuration);

 // ------ TO SQL SERVER ----
 services.AddSqlServer(configuration);

 // ------ TO SQL MYSQL ----
 services.AddMySql(configuration);
```

Estando dentro da pasta 'DotNet_Db_EF_Audit.Infra' você deverá executar o seguinte comendo via Visual Studio, para criar o script das tabelas.

```
	 Add-Migration -OutputDir Db\Migrations -Context ApplicationDbContext -Verbose
```

Após descomentar os código e executado o comando com sucesso, basta realizar o start do projeto.

---

## Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[.Net core](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/), [Visuall Studio](https://visualstudio.microsoft.com/pt-br/downloads/).

Utilizar o docker no laptop para criar os bancos de dados [Docker Desktop](https://docs.docker.com/desktop/setup/install/windows-install/),

---

## ❌Problemas encontrados

Foram encontrados alguns problemas iniciais ao rodar o projeto para diversos bancos de dados, entre eles:

---

## 🛠 Tecnologias

- **[.NET](https://dotnet.microsoft.com/en-us/)**
