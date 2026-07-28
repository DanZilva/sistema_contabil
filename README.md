# 📄 Sistema de Notas Fiscais

Sistema web para gerenciamento e distribuição de notas fiscais entre o setor Contábil e os demais setores da empresa.

O projeto foi desenvolvido em ASP.NET Core utilizando Razor Pages, Entity Framework Core e SQLite.

## Funcionalidades

- Login de usuários
- Controle de acesso por perfil
- Perfis Contábil e Setor
- Cadastro de usuários
- Cadastro de notas fiscais
- Associação da nota a uma empresa
- Associação da nota ao setor responsável
- Upload de PDF da nota fiscal
- Visualização das notas pelo setor responsável
- Alteração de status das notas
- Edição e exclusão de notas
- Dashboard

## Fluxo

O setor Contábil cadastra a nota fiscal, seleciona a empresa e o setor responsável e anexa o PDF.

O usuário do setor visualiza apenas as notas destinadas ao seu setor e pode concluir o processamento da nota.

##  Tecnologias

- C#
- ASP.NET Core
- Razor Pages
- Entity Framework Core
- SQLite
- HTML
- CSS
- Bootstrap

##  Executando o projeto

Clone o repositório:

git clone https://github.com/DanZilva/sistema_contabil.git

Entre na pasta do projeto e restaure as dependências:

dotnet restore

Crie/atualize o banco de dados:

dotnet ef database update

Execute:

dotnet run

##  Status

🚧 Projeto em desenvolvimento.

### Próximas melhorias

- Importação de XML de NF-e
- Integração com serviços fiscais
- Melhorias no controle de acesso
- Histórico de movimentações
- Auditoria de usuários
- Notificações

##  Observação

O banco SQLite local e os documentos enviados pelos usuários não são versionados no repositório.

##  Licença

Projeto desenvolvido para fins de estudo e desenvolvimento de uma solução de gerenciamento interno de notas fiscais.
