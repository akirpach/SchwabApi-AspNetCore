# SchwabAPI.WebApi

This is a web application that allows users to authenticate with their Schwab API account and pull data into a table.

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/your-username/SchwabAPI.WebApi.git
   cd SchwabAPI.WebApi
   ```

2. Set up environment variables:
   ```sh
   export SCHWAB_API_CLIENT_ID="your-client-id"
   export SCHWAB_API_SECRET_ID="your-secret-id"
   ```

3. Restore dependencies and run the application:
   ```sh
   dotnet restore
   dotnet run
   ```

### Usage

After running the application, navigate to `http://localhost:5000` in your browser to access the web app.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
