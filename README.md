# ContosoUniversity

ContosoUniversity is a Basic crude .NetCore academic application which use NHIbernate as its ORM and Fluent NHibernate for mappings. Also utilize redis cache for basic caching.

## SetUP
a) Create an empty database with MSSQL manager and modify the database connection string. The Schema would be built on application startup.
b) Make sure redis is installed on your system and modify the Redis connection string.
c) All this modification should happen in the appsettings.json file
