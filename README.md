# Shopping Cart

A small shopping cart project containing separate folders for the Database (db), API (api/price-calculator) and UI (ui).

This README provides:
- Tech stack overview (explicit list below)
- NuGet and NPM packages used
- Step-by-step instructions to execute the database, run the API, run the UI, and verify URLs

---

## Project layout

- db/  
  Contains database scripts and/or SQL files required to initialize the database.
- api/price-calculator/  
  Backend API project (ASP.NET Core Web API).
- ui/  
  Frontend application (ReactJS) — package.json lives in the root of this folder.
  
---

## Tech stack

- .NET Core (ASP.NET Core Web API)
- ReactJS (frontend)
- MS-SQL (database)

---

## Packages / Dependencies

- NuGet packages used in .NET:
  - Dapper
  - Moq
  - AutoFixture
  - Swashbuckle

- NPM packages used in ReactJS:
  - axios
  - classnames
  - sass
  - react-bootstrap
  - react-icons
  - 
---

## Prerequisites

- .NET SDK (compatible version for the project) — verify with `dotnet --version`
- Node.js (LTS) and npm — verify with `node --version && npm --version`
- MS-SQL Server (or the SQL Server instance you will use)
- (Optional) Docker if you prefer running SQL Server in a container

---

## Steps to run the APP

1. Execute the DB scripts in the following order:
   - Create DB
   - Items
   - itemprice
   - itemdiscount
   - Dataseed
   - (And any remaining scripts in `db/` as required)
   - Note: Check the `db/` folder for script filenames and run them in the order above using `sqlcmd` / SSMS / your preferred tooling.

2. Update the server name in the `pricecalculatordb` connection string:
   - Open `api/price-calculator/appsettings.json`
   - Find the connection string named `pricecalculatordb` and update the server/instance, database.

3. Run the API project and verify Swagger:
   - Run the API in https profile
   - Make sure Swagger is up and running at:
     - https://localhost:7061/swagger
     - (The project is expected to run on port 7061 — if it starts on a different port, follow the console output or update `launchSettings.json` / run arguments.)

4. Start the UI:
   - Navigate to the root folder of the UI where `package.json` is located:
     - cd ui
   - Install packages:
     - npm i
   - Start the app:
     - npm start
   - Open the UI in your browser (common dev URL):
     - http://localhost:3000 (or the URL shown in terminal)

5. Verify end-to-end:
   - Ensure the UI is loading and making API calls to the API base URL (update UI env config if necessary to point to http://localhost:7061).
   - Use browser devtools Network tab or curl to confirm API requests return 200 OK.

---
