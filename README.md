dotnet ef migrations add Initial --project ..\Intranet.Users --context UsersContext

dotnet ef database update --project ..\Intranet.Users --context UsersContext


--output-dir Migrations

dotnet ef database drop --project ..\Intranet.Users --context UsersContext

dotnet ef migrations script --project ..\Intranet.Users --context UsersContext | Out-File -filepath sql.sql
