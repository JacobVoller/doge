# Approach

This section outlines my thought process as I work through the prompt, ensuring I meet all acceptance criteria while providing insight into my development approach. My goal is to document key decisions, challenges, and next steps for reviewers. 

## Kickoff

At the time of writing, I have briefly reviewed the eCFR website (~20 seconds), created this GitHub repository, and committed the prompt to the README.

1. **Domain:** Research what the eCFR is and how its data is structured.
1. **API:** The website does not appear to be a technical documentation page. I need to locate and review the API specification.
1. **Iteration:** I have 24 hours to complete this project. My focus is on building a Minimum Viable Product (MVP) as quickly as possible. If time permits, I will make iterative enhancements.
1. **Repository:** GitHub is a requirement. Due to time constraints, I will commit directly to main, despite this being against best practices.
1. **Structure:** The solution requires a frontend, backend, and potentially a database. I typically enjoy a monorepos, speed is the priority, a monorepo will help.
1. **Backend:** I will use C# for the backend REST API, as it is my strongest language. I need to determine whether a database is necessary. If so, I am leaning towards using Entity Framework with OData to simplify querying and filtering from the frontend.
1. **Frontend:** Frontend development is my weakest area, so I am considering two options:
    1. **Option A:** Jekyll + Static HTML/SCSS + JavaScript. Fastest approach, but dynamic filtering might be a challenge. I will need to research potential JavaScript libraries to support filtering before committing to this approach.
    1. **Option B:** React. A more scalable, production-quality solution. If I cannot quickly validate the Jekyll approach, I will default to React.
1. **Database:** PostgreSQL. Quick and free.
1. **Deployment:** GitHub Actions for CI/CD; I can reference previous professional experiance, personal projects and countless online examples. GitHub Pages for frontend hosting (free and simple). Backend and database deployment method still to be determined to find a cheap and quick option. Use Docker for both the backend and the database.
1. **UI:** To start, UI will be bare bones. I can improve it after completing an MVP. Use doge.gov as a loose style reference.
1. **Tools**
    1. **IDE:** VS Code or Visual Studio Community
    1. **Research:** Google & ChatGPT

## API Research

While exploring the eCFR.gov website, I found a link to the developer documentation under Reader Aids > Developer Resources. The styling of the docs suggests they were generated using Swagger, which is great news—this means I can leverage OpenAPI to generate the HTTP client, saving time on manually constructing API requests.

Initially, I couldn't find the corresponding Swagger JSON file on the site, even after searching via Google and ChatGPT. However, upon inspecting the network traffic while browsing the site, I discovered a v1.json file. Opening the URL confirmed that it is the Swagger definition, which should work for auto-generating the client as expected.

1. **API Documentation:** https://www.ecfr.gov/developers/documentation/api/v1#/
1. **Base URL:** https://www.ecfr.gov
1. **API Swagger Definition:** https://www.ecfr.gov/developers/documentation/api/v1.json

## Timeline

### 19-MAR

1. 20:19 - Hit major delays with generating client in .net. Needed to update to v9.0 runtime, then v9.0 SDK.
2. 20:37 - Still blocked. swagger file is breaking client generator validation rules. Java issues.
3. 21:29 - Still blocked. time for bed.

### 20-MAR

1. 07:32 - Spending some time before work.
2. 08:00 - HTTP Client generation worked but didn't have enough time to get it working within the backend. Time for work.
3. 17:30 - Starting post-work again.
