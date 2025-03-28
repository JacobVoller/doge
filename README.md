[![pages-build-deployment](https://github.com/JacobVoller/doge/actions/workflows/pages/pages-build-deployment/badge.svg)](https://github.com/JacobVoller/doge/actions/workflows/pages/pages-build-deployment)

# DOGE

* Public Site: **https://jacobvoller.com/doge/**

## Apps

| Application | Description                      |
| ----------- | -------------------------------- |
| build       | Mono Repository Build Scripts    |
| db          | Relational Database (PostgreSQL) |
| server      | Backend HTTP Server              |
| web         | Public facing website            |

## Build & Deploy

<pre>
./doge.sh -option1 -option2
</pre>

### Optional Parameters

| Parameter | Description                                  |
| --------- | -------------------------------------------- |
| -launch   | Launch local instance of database w/ Docker. |

### Deployment

| Application | Description                      |
| ----------- | -------------------------------- |
| db          |  |
| server      |               |
| web         |             |

## Structure

<pre>
.
├── .github
|   └── workflows
|       └── cicd.yml      # CI/CD pipeline
├── apps                  #
|   ├── build             # APP: build scripts for monorepo 
|   |   └── src
|   |       ├── util      # build script utility functions
|   |       └── entry.sh  # build script orchestrator
|   ├── db                # APP: database
|   |   ├── scripts
|   |   |   ├── build.sh  # database build script
|   |   |   └── launch.sh # database launch script for local dev environment
|   |   └── src
|   ├── server            # APP: backend HTTP server
|   |   ├── scripts
|   |   |   └── build.sh  # backend build script
|   |   └── src
|   └── web               # APP: frontend website
|       ├── scripts
|       |   └── build.sh  # frontend build script
|       └── src
├── docs                  # generated, static files 
├── readmes               # ancillary readmes
|   └── approach.md       #
├── .gitignore
├── doge.sh               # monorepo build script entry point
└── README.md             # <-- You are here
</pre>

## Prompt

> **Project: eCFR Analyzer**
>
> The goal of this project is to create a simple website to analyze Federal Regulations. The eCFR is available at https://www.ecfr.gov/. There is a public api for it.
>
> Please write code to download the current eCFR and analyze it for items such as word count per agency and historical changes over time. Feel free to add your own custom metrics.
> 
> There should be a front end visualization for the content where we can click around and ideally query items. Additionally, there should be a public github project with the code.

## Improvements

1. **db**
   1. Swap InMemoryDB for real PostgreSQL database
   2. Implement indexing
2. **server**
   1. Utalize a load balancer for `server`
   2. Implement health checks & auto-scaling
   3. Introduce unit tests
3. **web**
   1. Swap static Jekyll for React to make the application more extencible
   2. Introduce unit tests
   3. Introduce integration tests

## Links

1. [Approach](readmes/approach.md)