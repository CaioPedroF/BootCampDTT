dotnet test `
"/p:CollectCoverage=true" `
"/p:CoverletOutput=TestResults/coverage/" `
"/p:CoverletOutputFormat=cobertura"

reportgenerator `
"-reports:**/coverage.cobertura.xml" `
"-targetdir:TestReport" `
"-reporttypes:TextSummary"
