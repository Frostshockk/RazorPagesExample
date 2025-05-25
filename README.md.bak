## Running the Project with Docker

This project includes a Docker setup for building and running the ASP.NET Core application (`WebApplication1`) using .NET 8.0. The provided Dockerfile and Docker Compose configuration ensure a reproducible environment for development and deployment.

### Requirements
- **Docker** and **Docker Compose** installed on your system.
- The project uses **.NET 8.0** (as specified by `ARG DOTNET_VERSION=8.0` in the Dockerfile).

### Build and Run Instructions
1. **Build and start the application:**
   ```sh
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

---

_This section was last updated to reflect the current Docker setup. Please ensure your local environment matches the requirements above for a smooth experience._
