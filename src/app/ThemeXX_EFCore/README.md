# EFCore

## Create initial migration
```bash
# work dir = root of the project
dotnet ef migrations add Initial --project "src/app/HWGA.csproj" -o "ThemeXX_EFCore/Migrations"
```

## Migrate database
### 1.via CLI
```bash
# work dir = root of the project
dotnet ef database update --project src/app/HWGA.csproj
```

### 2. via C#
```cs
using var db = new HWGADbContext();
await db.Database.MigrateAsync(); 
```
