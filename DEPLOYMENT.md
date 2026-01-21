# Personas API - Backend .NET 8

API REST para gestión de personas con autenticación, desarrollada en .NET 8 y Supabase.

## 🚀 Despliegue con Docker

### Variables de Entorno Requeridas

```bash
SUPABASE_URL=tu_url_de_supabase
SUPABASE_KEY=tu_api_key_de_supabase
```

### Build Local

```bash
docker build -t personas-api .
docker run -p 8080:8080 \
  -e SUPABASE_URL="https://pulqiaqavvammsozqgaq.supabase.co" \
  -e SUPABASE_KEY="tu_key" \
  personas-api
```

## 📦 Despliegue en Render.com

### Opción 1: Desde GitHub (Recomendado)

1. **Crear repositorio en GitHub:**
   ```bash
   cd PersonasApi
   git init
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git remote add origin https://github.com/tu-usuario/personas-api-backend.git
   git push -u origin main
   ```

2. **En Render.com:**
   - New → Web Service
   - Connect repository: `personas-api-backend`
   - Environment: **Docker**
   - Region: Oregon (Free)
   - Instance Type: **Free**
   - Advanced → Add Environment Variables:
     - `SUPABASE_URL` = tu URL
     - `SUPABASE_KEY` = tu Key
   - Create Web Service

### Opción 2: Deploy Manual (Sin Git)

```bash
# Instalar Render CLI
npm install -g render-cli

# Login
render login

# Deploy
render deploy
```

## 🌐 Despliegue en Railway.app

1. **Crear repositorio en GitHub** (igual que arriba)

2. **En Railway.app:**
   - New Project → Deploy from GitHub repo
   - Seleccionar `personas-api-backend`
   - Railway detecta Dockerfile automáticamente
   - Settings → Variables:
     - `SUPABASE_URL` = tu URL
     - `SUPABASE_KEY` = tu Key
   - Deploy

## 🔧 Despliegue en Fly.io

```bash
# Instalar flyctl
powershell -Command "iwr https://fly.io/install.ps1 -useb | iex"

# Login
fly auth login

# Lanzar app
fly launch
  # Name: personas-api
  # Region: Miami (mia) o el más cercano
  # Database: No (usas Supabase)
  
# Configurar secrets
fly secrets set SUPABASE_URL="tu_url"
fly secrets set SUPABASE_KEY="tu_key"

# Deploy
fly deploy
```

## 📋 Endpoints Principales

- `GET /api/personas` - Listar personas
- `POST /api/personas` - Crear persona
- `POST /api/auth/register` - Registrar usuario
- `POST /api/auth/login` - Login

## 🔍 Verificar Deployment

```bash
# Render/Railway
curl https://tu-app.onrender.com/api/personas

# Fly.io
curl https://personas-api.fly.dev/api/personas
```

## 📝 Notas

- Puerto: **8080** (configurable vía `ASPNETCORE_URLS`)
- Base de datos: Supabase PostgreSQL
- Stored procedures: `insert_persona()`, `insert_usuario()`
- Swagger UI: `/swagger` (solo en Development)

## 🆓 Free Tier Limits

| Plataforma | CPU | RAM | Sleep | Build Time |
|------------|-----|-----|-------|------------|
| Render.com | 0.1 | 512MB | Después de 15min inactivo | 20min |
| Railway.app | Shared | 512MB | $5 crédito/mes | Ilimitado |
| Fly.io | Shared | 256MB | No duerme | 10min |

**Recomendación:** Render.com para simplicidad, Fly.io si necesitas que no duerma.
