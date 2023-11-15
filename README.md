<h1 align="left">.NET 7 e Identity: Implementando Controle de Usuário</h1>

| :placard: Vitrine.Dev |                                     |
| --------------------- | ----------------------------------- |
| :sparkles: Nome       | **CSharp_UsuarioAPI**               |      
| :label: Tecnologias   | ASP.NET C# EntityFramework Identity |

![image](https://github.com/FabioIngenito/CSharp_UsuarioAPI/assets/24603753/2067283b-681e-442e-af4d-3b7f3ad4c3b9#vitrinedev)

<h2 align="left">Detalhes do projeto</h2>

Fiz algumas alterações em cima do curso original da Alura:

Curso de
.NET 6 e Identity: implementando controle de usuário

https://cursos.alura.com.br/course/dot-net-6-identity-controle-usuario

- Atualizei o framework para versão 7 e todos os drives. 
- Coloquei alguns comentários do curso.
- Alterei algum código para novas versões como, por exemplo, o namespace como ponto-e-vírgula no final

------------------------
1 * PARA FAZER FUNCIONAR

  É preciso configurar o Banco de Dados (Usuário e Senha) dentro do projeto com a SUA "Connection String" (string de conexão) do seu Banco de Dados.
  As "ConnectionsStrings" estão no arquivo "appsettings.json":

  O curso usa somente o MySQL, mas tenho certeza que é possível configurar para outros BD.

  O proprio programa se encaregará de criar a base de dados e a tabela usando Entity Framework.

  Guardei um arquivo "UsuariosAPI.sql" na pasta "SQL" para o MySQL.

  No "Gerenciador de Soluções" clique com o botão direito do mouse em cima do nome do projeto: "UsuariosAPI", na lista aparecerá a opção: "Gerenciar Segredos do Usuário". O arquivo "secrets.json" será criado.

  Na pasta "JSON" tem o arquivo "secrets.json", mas ele NÃO fica nesta pasta, este arquivo fica em:
  C:\Users\SEU_USUARIO\AppData\Roaming\Microsoft\UserSecrets\CHAVE_UUID\secrets.json

------------------------

2 * Emuladores

Eu testei três softwares para emular, a saber:

  - SoupUI; 
  - Postman; 
  - Swagger; 

Mas se você conhecer algum outro programa legal, tenho certeza que vai funcionar. Abaixo descrevo cada um.

---------------------------------
3 * SoupUI (https://www.soapui.org/)

  Salvei as configurações do SoapUI na PASTA "SoapUI", basta carregar.
  Este foi o primeiro programa que usei quando estudei sobre SOAP. Se NÃO engano ele NÃO tinha REST e foi incorporado depois.

---------------------------------
4 * Postman (https://www.postman.com/postman/)

  Na MINHA opinião o programa mais fácil de trabalhar, mas tem menos recursos que o SoapUI. Não achei um lugar para salvar as configurações.

  - Crie o POST e o GET usando os caminhos: 

http://localhost:5119/
https://localhost:7267/

  - Exemplo de JSON do "Body" para cadastro:
  - Use método "GET" e dentro do Body escolha a opção "raw" e o tipo do arquivo "JSON"

https://localhost:7267/usuario/cadastro
{
    "UserName": "paulo",
    "DataNascimento": "2018-01-01",
    "Password": "Senha123@",
    "RePassword": "Senha123@"
}

  - Exemplo de JSON para login:
  - Use método "POST" e dentro do Body escolha a opção "raw" e o tipo do arquivo "JSON"

http://localhost:5119/usuario/login
{
    "Username": "david",
    "Password": "Senha123@"
}

  - Depois do Login de autorização vem a autenticação. Copie a chave de autenticação JWT (JSON Web Tokens) e coloque em:
  - AUTHORIZATION - TYPE: Beared Token
  - O JSON do "Body" é o mesmo do cadastro:
  - Use método "GET" e dentro do Body escolha a opção "raw" e o tipo do arquivo "JSON"

https://localhost:7267/Acesso
{
    "UserName": "paulo",
    "DataNascimento": "2018-01-01",
    "Password": "Senha123@",
    "RePassword": "Senha123@"
}

  - Se você quiser ver o conteúdo da chave JWT, use o link: https://jwt.io/

---------------------------------
5 * Swagger (https://editor.swagger.io/)

   Se você criar um projeto MÍNIMO ele NÃO traz o pacote Swagger. De outras formas, o pacote Swagger já vem junto como auxiliar.

   Caso você NÃO queira trabalhar com o Swagger, recomendo dentro da pasta "Properties" no arquivo "launchSettings.json", colocar como "FALSE" (menos a linha "launchUrl") ou comentar (menos os cabeçalhos) as seguintes linhas:

"profiles": {
   ...
   "dotnetRunMessages": false,
   "launchBrowser": false,
   "launchUrl": "swagger",
   ...
   }
},
"IIS Express": {
   "launchBrowser": false,
   "launchUrl": "swagger",
}

---------------------------------
6 * MAPPER

Ajuda a mappear os objetos. Veja mais em:

AutoMapper - A convention-based object-object mapper.
Um mapeador objeto-objeto baseado em convenção.

https://docs.automapper.org/en/stable/

---------------------------------
O AspNet Identity é um framework para o gerenciamento de identidades de usuários e tarefas como por exemplo, verificar: 

  - Senhas;
  - Nomes de usuários;
  - Data de Nascimento;

Gerencia contas de usuários construindo uma autenticação.

O Identity é um framework ASP.NET para o gerenciamento de identidades. Ele NÃO é um framework de segurança!

DETALHE, o "secrets":

  - NÃO embaralha os caracteres do código para evitar engenharia reversa.
  - NÃO Criptografa senhas da aplicação.
  - SOMENTE Armazenar informações confidenciais durante o desenvolvimento de um projeto.

Os usuários serão guardados no Identity.

.Core -> Pacotes completos.
.EntityFramework -> Framework com Interface.

  POWERSHELL:
..\UsuariosAPI\UsuariosAPI
dotnet user-secrets init

Incializou e colocou comandos dentro do: UsuariosAPI: "UserSecretsId"
dotnet user-secrets set "SymmetricSecurityKey" "this is my custom Secret key for authentication"

E colocou na pasta:
C:\Users\SEU_USUARIO\AppData\Roaming\Microsoft\UserSecrets

  Para guardar o caminho da conexão ao banco de dados:
  dotnet user-secrets set "ConnectionStrings:UsuarioConnection" "server=localhost;port=3306;database=usuariodb;user=root;password=teste"
  Por meio deste comando, criaremos um secret com determinada chave e valor informados:
  Padrão: dotnet user-secrets set <chave> <valor>
  O nome do arquivo é: "secrets.json"

---------------------------------

LINKS:

---------------------------------

Armazenamento seguro de segredos do aplicativo em desenvolvimento no ASP.NET Core
Safe storage of app secrets in development in ASP.NET Core
Article - 04/10/2023

https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows
https://learn.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows

---------------------------------

Os JSON Web Tokens são um método RFC 7519 padrão do setor aberto para representar declarações com segurança entre duas partes.
JWT. IO permite decodificar, verificar e gerar JWT.

https://jwt.io/

---------------------------------
