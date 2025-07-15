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

2. IMPORTANT: Retrieve your Schwab API developer credentials:
   - Sign in to your Schwab Developer account at [Schwab Developer Portal](https://developer.schwab.com/).
   - Navigate to the API credentials section.
   - Create a new application if you haven't already.
   - Note down your `client_id` and `client_secret`.

3. Configure your environment variables in appsettings.json:
   ```sh
   "AllowedHosts": "*",
   "SchwabApi": {
      "ClientId": [CLIENT_ID_STRING],
      "ClientSecret": [CLIENT_SECRET_STRING],
      "RedirectUri": "https://127.0.0.1:5001/api/auth/callback", <-- Validate this in your Schwab Dev Portal.
      "TokenUrl": "https://api.schwabapi.com/v1/oauth/token",
      "AuthorizeUrl": "https://api.schwabapi.com/v1/oauth/authorize"
   ```

4. Restore dependencies and run the application:
   ```sh
   dotnet restore
   dotnet run
   ```

### Usage

1. After running the application, navigate to `http://127.0.0.1:5001` in your browser to access the web app.
2. In project directory navigate to requests/Authorize/GetAuthorize.http
3. Execute the above request. This will authorize your account and provide access token which is stored in token store locally.
4. Navigate to requests/Accounts/getAccountNumbers.http and execute this request. This will return you a hashed account number(s) associated with your Schwab API.
5. Navigate to requests/Transactions/GetAccountTransactions.http and configure the @accountNumber variable with value provided from previous step. Now you're ready to retrieve account transactions!


## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
