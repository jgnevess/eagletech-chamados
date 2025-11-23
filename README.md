# 🚀 Eagletechsolutions  

![Status](https://img.shields.io/badge/status-active-success.svg)  
![License](https://img.shields.io/badge/license-Acadêmico--Restrito-red.svg)  
![Backend](https://img.shields.io/badge/backend-C%23%20%7C%20ASP.NET-512BD4?logo=dotnet)  
![Frontend](https://img.shields.io/badge/frontend-React%20%7C%20TypeScript-61DAFB?logo=react)  
![Database](https://img.shields.io/badge/database-MariaDB-003545?logo=mariadb)  
![AI](https://img.shields.io/badge/AI-Google%20Gemini-4285F4?logo=google)  
![Docker](https://img.shields.io/badge/docker-ready-2496ED?logo=docker)  

---

### Sistema Inteligente de Gestão de Chamados e Suporte Técnico  

O **Eagletechsolutions** é uma plataforma desenvolvida para otimizar o atendimento técnico, combinando **gestão de chamados** com **inteligência artificial**.  
Esse projeto foi criado como parte do **PIM (Projeto Integrado Multidisciplinar)** do último semestre do curso de **Análise e Desenvolvimento de Sistemas**.  

Com ele, empresas podem organizar, priorizar e resolver chamados de forma ágil, contando ainda com um **chatbot inteligente** para resolver problemas simples sem precisar abrir chamados.  

---

## 🛠️ Tecnologias Utilizadas  

### 🔹 Backend  
- **C#**  
- **ASP.NET**  
- **MariaDB**  

### 🔹 Frontend  
- **TypeScript**  
- **React**  
- **Axios**  
- **Bootstrap**  

### 🔹 Inteligência Artificial  
- **Google Gemini API**  

A IA é usada para:  
✅ **Definir automaticamente a prioridade** dos chamados, analisando a descrição do problema.  
✅ **Chatbot no frontend**, ajudando clientes a resolverem problemas simples antes de abrir um chamado.  

---

## ⚙️ Como Rodar o Projeto  

### Pré-requisitos  
- Docker  
- Docker Compose  
- Chave de API do **Gemini** configurada  

### Executando  
```bash
docker compose up -d --build
```

### Esse comando irá subir automaticamente 3 containers:

- 🗄️ MariaDB (porta 3306)

- ⚙️ Backend .NET (porta 5000)

- 🌐 Frontend React + Nginx (porta 80)

## 💡 Diferenciais

- Chamados classificados automaticamente por prioridade com base na análise da IA

- Estrutura containerizada com Docker, fácil de rodar em qualquer ambiente

- Chatbot integrado para atendimento rápido e inteligente

