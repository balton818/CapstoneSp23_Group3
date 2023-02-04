To run tests:
	> In GUI: Right-click on the project (RecipePlannerWebAppTESTS) and click 'Run Tests'
	> In CLI: Right-click on the project (RecipePlannerWebAppTESTS) and click 'Open in Terminal'
		> Enter: dotnet test --collect: "XPlat Code Coverage"
		> Enter: dotnet reportgenerator "-reports:TestResults\{HASH OF LAST RUN}\coverage.cobertura.xml" "-targetdir:TestResults\html" -reporttypes:HTML;