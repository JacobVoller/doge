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

## Build

<pre>
./doge.sh -option1 -option2
</pre>

### Optional Parameters

| Parameter | Description                                  |
| --------- | -------------------------------------------- |
| -launch   | Launch local instance of database w/ Docker. |

## Prompt

> **Project: eCFR Analyzer**
>
> The goal of this project is to create a simple website to analyze Federal Regulations. The eCFR is available at https://www.ecfr.gov/. There is a public api for it.
>
> Please write code to download the current eCFR and analyze it for items such as word count per agency and historical changes over time. Feel free to add your own custom metrics.
> 
> There should be a front end visualization for the content where we can click around and ideally query items. Additionally, there should be a public github project with the code.

## Links

1. [Approach](readmes/approach.md)
