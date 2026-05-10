dotnet test `
  --filter "FullyQualifiedName~Unit" `
  /p:CollectCoverage=true `
  /p:CoverletOutputFormat=cobertura `
  /p:CoverletOutput=TestResults\unit-coverage\ `
  /p:Include="[AxiomaReporting.Infrastructure]AxiomaReporting.Infrastructure.Services.*" `
  /p:Exclude="[AxiomaReporting.Infrastructure]AxiomaReporting.Infrastructure.Services.EmailService%2c[AxiomaReporting.Infrastructure]AxiomaReporting.Infrastructure.Services.CurrentUserService" `
  /p:Threshold=80 `
  /p:ThresholdType=line `
  /p:ThresholdStat=total
