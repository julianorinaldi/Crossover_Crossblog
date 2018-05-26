Challenge Statement

Cross-blogs is a backend blogging application written by a startup company called WritingForAll. It allows users to create / update / delete their articles, accepting comments for each article.

Notes:
- Articles have a 120 character limit for their title, and a 32k limit for their body.
- The frontend application is excluded from the current scope. It is a separate, fully-functional application handled by another team, so we do not want to modify it.

Your tasks:
- Increase unit test coverage to reach 70%. Achieving more than 70% will only consume your valuable time without extra score.
- Find bugs and fix them, hint: we provided Cross-Blogs application in a good structure, so no need to spend your valuable time on structure modifications,  just focus on fixing bugs.
- Articles search endpoint is very slow, please optimize it.
- Recently Crossover acquired Cross-Blogs and found that Cross-Socials -it is another project for social networking owned by crossover - is very eligible to promote Cross-Blogs,  they want to display articles only in Cross-Socials news feed without comments using get article endpoint, the problem is Cross-Blogs traffic was 10M page views per day while Cross-Socials page views are 900M per day, Your goal is to scale Cross-Blogs app to serve expected traffic, Cross-Blogs web application was hosted in a single huge AWS node and the node was loaded for 10M per day’s traffic which makes increasing node size not a valid option, the conclusion is we need to have multi nodes of Cross-Blogs, and offload database.
    Notes about last task:
    - Perform required modifications in application level, and in case application modifications requires another 3rd party please add it to docker-compose.yml file.
    - Database replication is not required in this task.
    - Pay attention to articles only, and feel free to exclude comments from current scope of scaling as comments will not appear in Cross-Social at all.

Prerequisites
	- Any IDE
	- .NET Core SDK 2.1.4
	- Docker (follow this link to install https://store.docker.com/search?type=edition&offering=community)

=====================================
Development Environment
=====================================
MySQL:
- We assume that no MySQL is available in your machine, if it is running please stop it to avoid port conflicts, and use following command to run pre-configured MySQL

docker-compose up -d db

Cross-blogs application:
- On any terminal move to the "crossblog" folder (the folder containing the "crossblog.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet ef database update
dotnet run

- The application will be listening on http://localhost:5000
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

=====================================
Production Environment
=====================================
This is how we are going to run and evaluate your submission, so please make sure to run below steps before submit your answer.
If you add any new 3rd party application, please add it to docker-compose.yml file exactly like MySQL.

1) On any terminal move to the folder containing the "docker-compose.yml" file and execute this command:

docker-compose up -d

- The application will be listening on http://localhost:5000
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

2) Please add your project to a zip file with the name cross-blogs-dotnet-<YOUR-FULL-NAME>.zip (use dash "-" instead of any space).

=====================================
To run unit tests
=====================================
- On any terminal move to the "crossblog.tests" folder (the folder containing the "crossblog.tests.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet test

- To check code coverage, execute the batch script:

coverage.bat

=====================================
Some Curl command examples
=====================================
curl -i -H "Content-Type: application/json" -X POST -d "{'title':'How to use docker', 'content':'xyz', 'date': '2018-03-10', 'published':false}" http://localhost:5000/articles
curl -i -H "Content-Type: application/json" http://localhost:5000/articles/search?title=doc