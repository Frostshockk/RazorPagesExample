## Running the Project with Docker

This project includes a Docker setup for building and running the ASP.NET Core application (`WebApplication1`) using .NET 8.0. The provided Dockerfile and Docker Compose configuration ensure a reproducible environment for development and deployment.

### Requirements
- **Docker** and **Docker Compose** installed on your system.
- The project uses **.NET 8.0** (as specified by `ARG DOTNET_VERSION=8.0` in the Dockerfile).

### Build and Run Instructions
1. **Build and start the application:**
   ```sh
   cd ./WebApplication1
   docker compose up --build
   ```
   This command builds the Docker image for `WebApplication1` and starts the container.

2. **Access the application:**
   - The application will be available at [http://localhost:8080](http://localhost:8080).

### Configuration Details
- **Ports:**
  - The service `csharp-webapplication1` exposes port **8080** (container) to **8080** (host).
- **Environment Variables:**
  - The application listens on `ASPNETCORE_URLS=http://+:8080` (set in the Dockerfile).
  - No additional environment variables are required by default. If you need to provide custom environment variables, you can uncomment and use the `env_file` section in the `docker-compose.yml`.
- **User Permissions:**
  - The application runs as a non-root user (`appuser`) for improved security.
- **No persistent volumes** are configured, as the application does not require persistent storage by default.

### Special Notes
- The Docker Compose file is set up for a single service. If you add external dependencies (e.g., databases), update the `depends_on` and `networks` sections as needed.
- The build context for the service is `./WebApplication1`, but it uses the Dockerfile at the project root (`../Dockerfile`).


## Running the Project with .NET SDK

This project can be run locally using the .NET SDK for development and testing. The application uses SQLite for database storage and Entity Framework Core for data management.

---

### Requirements
- **.NET 8.0 SDK** (or later)
- **SQLite** (automatically embedded, no separate installation required)
- **Entity Framework Core Tools** (for database migrations)

---

### Build and Run Instructions

1. **Restore dependencies:**
   ```sh
   dotnet restore


2. **Apply database migrations**
  ```sh
  dotnet ef database update

3. **Run the application**
  ```sh
  dotnet run --environment Development

4. **Application will be availiable at localhost:listetning_port (check out the console)**
