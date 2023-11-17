# Inventory-Service

I mappen InventoryService:
1. Kopier `appsettings.Template.json` og kald den nye fil `appsettings.json`
3. Tag connection string fra Discord og sæt ind under `ConnectionStrings` i `appsettings.json` ELLER sæt env variable DB_CONN til connection string
4. How to run: `dotnet run` eller bare åbn i VS og tryk start. Måske åbner det i Docker som standard. Hvis det bare skal være alm. https server så kør: `dotnet run --launch-profile "https"`
