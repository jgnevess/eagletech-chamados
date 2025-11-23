# File Tree: pim

**Generated:** 29/10/2025, 10:13:06
**Root Path:** `/home/joao/Documentos/pim`

```
├── 📁 .github
│   └── 📁 workflows
│       └── ⚙️ main.yml
├── 📁 backup
│   ├── 📄 getapplogs.sh
│   └── 📄 testfront.sh
├── 📁 eagletechapi
│   ├── 📁 Contexts
│   │   └── 📄 AppDbContext.cs
│   ├── 📁 Controllers
│   │   ├── 📄 AuthController.cs
│   │   ├── 📄 ChamadosController.cs
│   │   ├── 📄 RelatorioController.cs
│   │   └── 📄 UsuarioController.cs
│   ├── 📁 Migrations
│   │   ├── 📄 20250810030347_chatbotAndMessage.Designer.cs
│   │   ├── 📄 20250810030347_chatbotAndMessage.cs
│   │   ├── 📄 20250810135133_AddChatbotId.Designer.cs
│   │   ├── 📄 20250810135133_AddChatbotId.cs
│   │   ├── 📄 20250810153439_AddChatbotId2.Designer.cs
│   │   ├── 📄 20250810153439_AddChatbotId2.cs
│   │   ├── 📄 20250810154056_FixChatbotRelation.Designer.cs
│   │   ├── 📄 20250810154056_FixChatbotRelation.cs
│   │   ├── 📄 20250815144506_CricaoEntidades.Designer.cs
│   │   ├── 📄 20250815144506_CricaoEntidades.cs
│   │   ├── 📄 20250817143224_UptadedMigration.Designer.cs
│   │   ├── 📄 20250817143224_UptadedMigration.cs
│   │   ├── 📄 20250817195042_UpdateUserTable.Designer.cs
│   │   ├── 📄 20250817195042_UpdateUserTable.cs
│   │   ├── 📄 20250821123535_updateUsuario.Designer.cs
│   │   ├── 📄 20250821123535_updateUsuario.cs
│   │   ├── 📄 20250824135853_updateChamados.Designer.cs
│   │   ├── 📄 20250824135853_updateChamados.cs
│   │   ├── 📄 20250901193155_IncreaseDescricaoLength.Designer.cs
│   │   ├── 📄 20250901193155_IncreaseDescricaoLength.cs
│   │   ├── 📄 20250908180756_atualizacaoUser.Designer.cs
│   │   ├── 📄 20250908180756_atualizacaoUser.cs
│   │   ├── 📄 20251011102941_setTables.Designer.cs
│   │   ├── 📄 20251011102941_setTables.cs
│   │   ├── 📄 20251017124057_updateFechamentoChamado.Designer.cs
│   │   ├── 📄 20251017124057_updateFechamentoChamado.cs
│   │   └── 📄 AppDbContextModelSnapshot.cs
│   ├── 📁 Properties
│   │   └── ⚙️ launchSettings.json
│   ├── 📁 Relatorios
│   ├── 📁 dto
│   │   ├── 📁 chamado
│   │   │   ├── 📄 ChamadoIn.cs
│   │   │   ├── 📄 ChamadoOut.cs
│   │   │   └── 📄 Fechamento.cs
│   │   ├── 📁 usuario
│   │   │   ├── 📄 CredentialsLogin.cs
│   │   │   ├── 📄 LoginResponse.cs
│   │   │   ├── 📄 PasswordUpdate.cs
│   │   │   ├── 📄 SimplePasswordUpdate.cs
│   │   │   ├── 📄 UserIn.cs
│   │   │   ├── 📄 UserOut.cs
│   │   │   └── 📄 UserUpdateIn.cs
│   │   └── 📄 ResponseList.cs
│   ├── 📁 models
│   │   ├── 📁 chamado
│   │   │   ├── 📁 enums
│   │   │   │   ├── 📄 Categoria.cs
│   │   │   │   ├── 📄 Prioridade.cs
│   │   │   │   └── 📄 Status.cs
│   │   │   └── 📄 Chamado.cs
│   │   └── 📁 usuario
│   │       ├── 📄 Funcao.cs
│   │       └── 📄 Usuario.cs
│   ├── 📁 service
│   │   ├── 📁 implements
│   │   │   ├── 📄 AuthService.cs
│   │   │   ├── 📄 ChamadoService.cs
│   │   │   ├── 📄 RelatorioService.cs
│   │   │   ├── 📄 RelatoriosPdf.cs
│   │   │   └── 📄 UserService.cs
│   │   └── 📁 interfaces
│   │       ├── 📄 IAuthService.cs
│   │       ├── 📄 IChamadoService.cs
│   │       ├── 📄 IRelatorioService.cs
│   │       └── 📄 IUserService.cs
│   ├── 📁 test
│   │   └── 📁 EagleTechApi.Tests
│   │       ├── 📄 AuthTest.cs
│   │       ├── 📄 ChamadoTest.cs
│   │       ├── 📄 EagleTechApi.Tests.csproj
│   │       ├── 📄 EagleTechApi.Tests.sln
│   │       ├── 📄 UsuarioServiceTest.cs
│   │       └── 📄 test.sh
│   ├── 📁 utils
│   │   ├── 📄 ArquivoRelatorio.cs
│   │   ├── 📄 FiltrosRelatorio.cs
│   │   ├── 📄 RelatorioResponse.cs
│   │   └── 📄 TipoRelatorio.cs
│   ├── ⚙️ .dockerignore
│   ├── ⚙️ .editorconfig
│   ├── 📄 Program.cs
│   ├── ⚙️ appsettings.Development.json
│   ├── 🐳 dockerfile
│   ├── 📄 eagletechapi.csproj
│   ├── 📄 eagletechapi.http
│   └── 📄 eagletechapi.sln
├── 📁 eagletechclient
│   ├── 📁 public
│   │   ├── 📁 images
│   │   │   └── 🖼️ Login.png
│   │   ├── 📄 favicon.ico
│   │   ├── 🌐 index.html
│   │   ├── ⚙️ manifest.json
│   │   └── 📄 robots.txt
│   ├── 📁 src
│   │   ├── 📁 components
│   │   │   ├── 📁 alert
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 btn-chamados
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 cadastrarUsuario
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chatbot
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 container
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 inputForm
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 loading
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 navbar
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   └── 📁 tabelaChamados
│   │   │       └── 📄 index.tsx
│   │   ├── 📁 hooks
│   │   │   ├── 📄 useBootstrapTheme.ts
│   │   │   └── 📄 useFirstLogin.ts
│   │   ├── 📁 images
│   │   │   ├── 🖼️ 35932-1-desktop-computer.png
│   │   │   ├── 🖼️ 5157509.jpg
│   │   │   ├── 🖼️ Login.png
│   │   │   ├── 🖼️ Profile-PNG-Image-HD.png
│   │   │   └── 🖼️ logo.png
│   │   ├── 📁 pages
│   │   │   ├── 📁 Home
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 admindashboard
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 cadastrar-usuario
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chamado
│   │   │   │   ├── 🎨 index.css
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chamados
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chamadosAbertos
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chamadosAtendidos
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 chamadosForAdmin
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 editar-chamado
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 login
│   │   │   │   ├── 📄 index.tsx
│   │   │   │   └── 🎨 login.css
│   │   │   ├── 📁 logout
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 naoautorizado
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 notfound
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 nova-senha
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 nova-senha-editar
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 novo-chamado
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 relatorio
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 solicitantedashboard
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 tecnicodashboard
│   │   │   │   └── 📄 index.tsx
│   │   │   ├── 📁 usuario
│   │   │   │   └── 📄 index.tsx
│   │   │   └── 📁 usuarios
│   │   │       └── 📄 index.tsx
│   │   ├── 📁 routes
│   │   │   ├── 📄 index.tsx
│   │   │   ├── 📄 privateRoute.tsx
│   │   │   └── 📄 redirect.tsx
│   │   ├── 📁 service
│   │   │   ├── 📁 login
│   │   │   │   ├── 📄 login.models.ts
│   │   │   │   └── 📄 login.ts
│   │   │   ├── 📁 relatorio
│   │   │   │   ├── 📄 Relatorio.model.ts
│   │   │   │   └── 📄 relatorioService.ts
│   │   │   ├── 📁 user
│   │   │   │   ├── 📄 user.models.ts
│   │   │   │   └── 📄 userService.ts
│   │   │   ├── 📄 chamado.ts
│   │   │   ├── 📄 chatbot.ts
│   │   │   └── 📄 firstlogin.ts
│   │   ├── 🎨 App.css
│   │   ├── 📄 App.test.tsx
│   │   ├── 📄 App.tsx
│   │   ├── 🎨 index.css
│   │   ├── 📄 index.tsx
│   │   ├── 🖼️ logo.svg
│   │   ├── 📄 react-app-env.d.ts
│   │   ├── 📄 reportWebVitals.ts
│   │   └── 📄 setupTests.ts
│   ├── ⚙️ .dockerignore
│   ├── ⚙️ .gitignore
│   ├── 📝 README.md
│   ├── 🐳 dockerfile
│   ├── ⚙️ nginx.conf
│   ├── ⚙️ package.json
│   └── ⚙️ tsconfig.json
├── 📁 terraform
│   ├── ⚙️ .terraform.lock.hcl
│   ├── 📄 main.tf
│   ├── 📄 output.tf
│   ├── 📄 provider.tf
│   ├── 📄 terraform.tfstate
│   ├── 📄 terraform.tfstate.backup
│   └── 📄 variables.tf
├── ⚙️ .gitignore
├── 📝 FileTree:eagletechsolution.md
├── 📄 LICENSE
├── 📝 README.md
├── ⚙️ docker-compose.yml
├── 📄 docker-compose.yml.bkp
├── 📄 init-user.sql
└── 📄 run.sh
```

---
*Generated by FileTree Pro Extension*