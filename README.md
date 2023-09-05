# AE Backend Code Challenge

The AE Backend Code Challenge Ship Management is a software application designed to manage and track information related to ships, ports, and their statuses. 
This README provides an overview of the system, its features, instructions for installation and usage, and information about GitHub Actions for continuous integration.

This Simple API is based from AE's https://github.com/angloeastern/backend-coding-task Code Challenge.
![image](https://github.com/AndrewAguirreP/BackendTaskShipManagementAPI/assets/11619541/5f18fb65-c541-4a25-a6dd-9b781b955a58)



## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Testing](#testing)
- [Constraints](#constraints)
- [Deployment](#deployment)
  
## Features

- **Ship Management:**
  - Add, edit, and delete ship records.
  - View ship details including ship status, type, and more.
  - Track ship destinations and statuses.

- **Port Management:**
  - Manage ports, including their names and geographic coordinates.

- **Status Tracking:**
  - Record and update ship statuses, including velocity, latitude, and longitude.
  - Find the closest port to a ship's current location.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- Install .NET 6.0 SDK or .NET CLI [dotnet 6 run-time and sdk](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Docker CLI or Docker Desktop [Docker downloads]([https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://www.docker.com/products/docker-desktop/))

## Usage
 - Installation and Running the Application


1. Clone this repository to your local machine:

  **Via SSH:**

 ```bash
   git clone git@github.com:AndrewAguirreP/BackendTaskShipManagementAPI.git
  ```

 **Via HTTPS:**

 ```bash
   git clone git@github.com:AndrewAguirreP/BackendTaskShipManagementAPI.git
 ```
  or download via Zip file.

 - **Running the application via dotnet CLI**
  - Go to  BackendTaskShipManagementAPI\ShipManagement folder.
  - Open a Terminal or Command Prompt at folder the run command below.

    
     ```bash
          dotnet restore
          dotnet build --no-restore
          dotnet run
     ```
    - Check the PORT where the application is listening to in HTTPS and got to the URL /swagger/index.html sample https://localhost:7121/swagger/index.html


  **Running the application via Docker CLI**
  - Go to BackendTaskShipManagementAPI\ShipManagement folder.
  - Open a Terminal or Command Prompt at folder the run command below.
  - For docker the application needed Docker HTTPS we need to configure dev certificates for SSL connection.

    ```bash
        dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p f@$tChess
        dotnet dev-certs https --trust
    ```
    
  - Build and run the image using HTTPS ensure Docker Daemon is running [https://docs.docker.com/config/daemon/troubleshoot/]

```bash
          docker build -t ae-app:1.0 .
          docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="f@$tChess" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ -e ASPNETCORE_ENVIRONMENT=Development ae-    app:1.0
```
- Use the PORT configured on the `docker run` in this case it's port 8001 mapped with docker's image 443 port https://localhost:8001/swagger/index.html

## Testing

   - Go to BackendTaskShipManagementAPI\ShipManagement.Tests folder.
   - Run the command below.

      ```bash
          dotnet restore
          dotnet test
      ```

## Constraints
  - Constraints & Assumptions
    
- The Project is an ASP.NET Core Web API built in C# comprised with REST APIs
-  There will be:
   - An API that can ADD Ships.
   - An API that can return ALL Ships.
   - An API that can update the Ships Velocity
   - An API that can return the Closest Port to a Ship with Estimated Arrival Time together with relevant details.
- On The Entities and DTOs(Data Transfer Objects)
   - Each port has a name and geolocation.
   - Each ship has a name, a unique ship id, geolocation (current longitude and latitude of the ship) and velocity.
   - Port data is seeded. 
   - No REST API is required for creating ports.
-Heads-up
  - The Distance calculation and closest port is using havesine formula[https://www.vcalc.com/wiki/vcalc/haversine-distance].
  - I assumed that the closest port calculation doen't consider the ports status (is_closed, in_repair, etc.) this can be due to bad weather the port is closed.
  - I assumed that the terrain is not into consider on this POC(Proof of concept) to find the nearest PORT.
  - I assumed since we are building a ship management for now the distance measurement is nmi(Nautical Miles) and ship velocity is knot, however the measurement can be configured and method can be extend to implement, Miles and Kilometers, can be changed easily etc.
  - I used `InMemory` datatabase for this POC, However the DbContext and Entities can be migrated to live database if needed.
  - In the Ship Entity, I identified the Shipname and UniqueId as static fields or fields that are not transactional, I designed the table based on real life used, 
	- The Ship Status class is to be populated each time the Ship checked in to the system, this table can be used to show AuditLogs, or see ships previous data, instead of having a all Data in Ship Entity, since Velocity, Longitude/Latitude are transactional for the Ship, where in Port Entity Longitude and Latitude are static fields.

 ### For Code Structure
 - I used Repository Pattern with Service layer implementation.
 - To Keep it simple for the POC, instead of breaking each Layer into Projects I organized the code via Folder
   , However in real life application each Layer is best to be separated by Projects.
- The Pattern has 3 Layers
   - **Controllers** - This Layer is the endpoint or client access layer.
   - **Service**     - This Layer is the Business layer where validation and business logic is being implemented.
   - **Repository**  - This Layer is the Data access layer where the application code access the data.

- For this POC I have implemented this simple layers to decouple each concerns needed for each requirements.
- The Models are Separated too by DTOs and Entities. For me this is the most important part of the application and provides following advantage:
 
	 - **Separation of Concerns:** DTOs and Entities serve different purposes. Entities represent your data model, and DTOs represent the data you want to transfer between different parts of your application, such as between your server and client. By keeping them separate, you maintain a clear separation of concerns and ensure that your data model isn't tightly coupled with your communication layer.

	- **Reduced Overhead:** Entities often contain more data than you need to transfer over the network or between different layers of your application. By using DTOs, you can create lightweight representations of your data, containing only the necessary fields, which can reduce the amount of data transferred and improve performance.

	- **Versioning and Compatibility:** As your application evolves, your data model (Entities) might change. If you use DTOs, you have more control over the versioning and compatibility of your API or communication protocols. You can introduce new DTOs without necessarily changing your Entities, ensuring backward compatibility for existing clients.

	- **Security:** DTOs allow you to control which data is exposed externally. You can omit sensitive or unnecessary information from DTOs, providing an additional layer of security. This is important when dealing with APIs exposed to the internet.

	- **Optimized Queries:** When retrieving data from a database, you can often use DTOs to specify exactly which fields you need, reducing the overhead of loading unnecessary data from the database. This is known as "projection" and can significantly improve database query performance.

	- **Flexibility:** DTOs provide flexibility when it comes to structuring and organizing your data for different use cases. You can create multiple DTOs for the same Entity to cater to various scenarios, without altering the underlying data model.

	- **Client-Specific Data:** DTOs allow you to shape data specifically for the needs of different clients or consumers of your API. For example, a web client and a mobile app may require slightly different data structures. DTOs enable you to customize the data sent to each client without affecting the underlying Entities.

	- **Testing:** DTOs can simplify unit testing. Since they are typically plain data objects without complex behavior, you can create and manipulate them more easily in tests.

	- **Documentation and Self-Documentation:** DTOs can serve as documentation for the data exchanged between different parts of your application. They make it clear what data is expected and what data is being transferred.

	- **Code Maintainability:** Separating DTOs from Entities often leads to cleaner and more maintainable code. It helps prevent issues related to serialization, data validation, and data mapping from contaminating your domain model. 

## Deployment
 - Deployment and Continuous Integration


  ### GitHub Actions

  This application is deployed using GitHub Actions, which automate the build, test, and deployment processes to streamline development and ensure code quality.

  - **Build and Test:** GitHub Actions automatically build and test the application whenever changes are pushed to the repository. This ensures that the code remains reliable and functional.
      ![image](https://github.com/AndrewAguirreP/BackendTaskShipManagementAPI/assets/11619541/e4988fa2-b2b1-44b7-bd8a-6d398ef41659)

### Docker Deployment

  This POC also setup for deployment any environment via docker, upon version **Release** a github action that build and push the docker image to this repository for deployment, this image can be pulled in any hosting environment on Azure/AWS.
  Docker image repository[https://hub.docker.com/r/aaguirre02/andrew-aguirre-ship-management] 
     ![image](https://github.com/AndrewAguirreP/BackendTaskShipManagementAPI/assets/11619541/f5c4e175-2311-4834-be1a-65b0cbe28027)
     ![image](https://github.com/AndrewAguirreP/BackendTaskShipManagementAPI/assets/11619541/bc73d28f-d6f8-4b77-bd85-3e4ab079d9d7)

  - To Pull and Run the Image on your local environment
  
```bash
    docker pull aaguirre02/andrew-aguirre-ship-management:latest

    dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p f@$tChess
    dotnet dev-certs https --trust

    docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="f@$tChess" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ -e ASPNETCORE_ENVIRONMENT=Development         aaguirre02/andrew-aguirre-ship-management:latest

```

## Many Thanks

I would like to say thank you for this opportunity, This is very amazing, in a short period of time (around 2 days), This challenge have pushed me to build an application with Test, CI, and a production ready app for a POC, and I am very grateful for this Code Challenge.
I look forward for your review and feedback, I admit the app is not perfect and not bullet proof at the moment but if given more time I can add more improvement to the code.


  
        

        
      
       
