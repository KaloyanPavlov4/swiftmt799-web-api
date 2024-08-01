# swiftmt799-web-api

## Web-api built with ASP and Dapper. Uses SQLite as database and logs errors to a file using NLog.

### Saves Swift MT799 Messages. Accepts new messages by file.

## Paths

### GET /api/swiftmt799 - returns all messages from the database.

### Post /api/swiftmt799 - accepts file with the data for a new message, constructs it and adds it to the database.
