# Couchbase .NET Core Example

This repository contains a .NET Core application demonstrating how to integrate and use Couchbase for data storage and retrieval. 
The application showcases basic CRUD operations using Couchbase and is configured to work with a Couchbase cluster.

## Features

- Connect to Couchbase cluster
- Perform CRUD operations (Create, Read, Update, Delete)
- Configure Couchbase connection and bucket settings
- Integration with ASP.NET Core for web APIs

## Prerequisites

- .NET 6 or later
- Couchbase Server
- Couchbase SDK for .NET

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/KardelRuveyda/couchbase-dotnetcore-dpoll.git
    ```

2. Navigate to the project directory:

    ```bash
    cd DotnetCouchbaseExample
    ```

3. Restore the project dependencies:

    ```bash
    dotnet restore
    ```

4. Configure Couchbase settings:

    - Open `appsettings.json` and set your Couchbase connection details:

      ```json
      {
        "Couchbase": {
          "ConnectionString": "your-couchbase-connection-string",
          "Username": "your-username",
          "Password": "your-password",
          "Buckets": [ "your-bucket-name" ]
        }
      }
      ```

5. Build and run the project:

    ```bash
    dotnet build
    dotnet run
    ```

## Usage

- The application is configured to expose APIs for interacting with Couchbase.
- You can access the API endpoints and perform operations using tools like Postman or Curl.

## Example

To insert a document into Couchbase, you can use the provided API endpoints, or modify the code in the `UserInfoService` class to suit your needs.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Feel free to open issues and submit pull requests for any improvements or fixes.

## Contact

For any questions or feedback, please open an issue or contact [your-email@example.com](mailto:your-email@example.com).

