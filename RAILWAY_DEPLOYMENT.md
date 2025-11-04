# Railway Deployment Guide

## Prerequisites
- GitHub account (or GitLab/Bitbucket)
- Railway account (sign up at https://railway.com)
- Push your code to a Git repository

## Step 1: Add PostgreSQL Database on Railway

1. Go to your Railway dashboard
2. Click "New Project"
3. Select "Provision PostgreSQL"
4. Railway will automatically create a PostgreSQL database and provide connection details

## Step 2: Deploy Your Application

1. In the same Railway project, click "New Service"
2. Select "GitHub Repo" (connect your repository)
3. Choose your Admin Hub API repository
4. Railway will automatically detect the Dockerfile and build your application

## Step 3: Configure Environment Variables

In your Railway service settings, add these environment variables:

### Required Variables

```
ConnectionStrings__DefaultConnection=${{Postgres.DATABASE_URL}}
```

**Note**: Railway provides `${{Postgres.DATABASE_URL}}` automatically when you link the PostgreSQL service to your app.

### JWT Settings (generate secure values for production)

```
JwtSettings__SecretKey=<your-secure-secret-key-min-32-chars>
JwtSettings__Issuer=AdminHubAPI
JwtSettings__Audience=AdminHubClient
JwtSettings__ExpirationInMinutes=60
```

### Admin User Password

```
AdminUser__Password=<your-secure-admin-password>
```

### CORS Configuration (add your frontend URLs)

```
Cors__AllowedOrigins__0=https://your-frontend-domain.com
Cors__AllowedOrigins__1=http://localhost:3000
```

Add more origins as needed: `Cors__AllowedOrigins__2`, `Cors__AllowedOrigins__3`, etc.

### ASP.NET Environment

```
ASPNETCORE_ENVIRONMENT=Production
```

## Step 4: Link PostgreSQL to Your Service

1. In your Railway service, go to "Settings"
2. Under "Service Variables", link the PostgreSQL database
3. This will automatically provide the `DATABASE_URL` variable

## Step 5: Deploy

Railway will automatically deploy when you push to your repository. The first deployment:
- Builds the Docker image
- Runs Entity Framework migrations
- Seeds initial data (roles, admin user, permissions)
- Starts the application

## Step 6: Access Your API

Railway will provide a public URL like: `https://your-app.up.railway.app`

API documentation will be available at: `https://your-app.up.railway.app/scalar/v1`

## Monitoring

- View logs in Railway dashboard under your service
- Monitor database connections in PostgreSQL service
- Set up alerts for errors

## Updating Your Application

Simply push to your Git repository - Railway will automatically rebuild and redeploy.

## Generating Secure Keys

For `JwtSettings__SecretKey`, use a secure random string (minimum 32 characters):

```bash
# Using PowerShell
[Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Maximum 256 }))

# Using OpenSSL
openssl rand -base64 32
```

## Troubleshooting

### Migrations not running
- Check logs for Entity Framework errors
- Ensure `ConnectionStrings__DefaultConnection` is correctly set
- Verify PostgreSQL service is running and linked

### CORS errors
- Add your frontend domain to `Cors__AllowedOrigins__0`
- Check Railway service URL in frontend API calls

### JWT authentication failing
- Verify `JwtSettings__SecretKey` is set and minimum 32 characters
- Check `JwtSettings__Issuer` and `JwtSettings__Audience` match your frontend configuration

## Cost Optimization

- Railway offers $5 free credit per month
- PostgreSQL and your app service will consume this credit
- Monitor usage in Railway dashboard
- Consider upgrading for production workloads
