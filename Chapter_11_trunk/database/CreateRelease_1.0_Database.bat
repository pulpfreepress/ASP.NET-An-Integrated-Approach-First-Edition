@echo off

echo.

echo Setting up environment parameters...
call Set_Environment.bat

echo Dropping EmployeeTraining Database...
sqlcmd -i "Release_1.0_DB_Scripts\DropDatabase.sql" -b -o %DB_LOGS%\DropDatabaseOutput.txt
echo Dropping EmployeeTraining Login...
sqlcmd -i "Release_1.0_DB_Scripts\DropLogin.sql" -b -o %DB_LOGS%\DropLoginOutput.txt
echo Creating EmployeeTraining Database...
sqlcmd -i "Release_1.0_DB_Scripts\CreateDatabase.sql" -b -o %DB_LOGS%\CreateDatabaseOutput.txt
echo Creating EmployeeTraining Login...
sqlcmd -i "Release_1.0_DB_Scripts\CreateLogin.sql" -b -o %DB_LOGS%\CreateLoginOutput.txt
echo Creating EmployeeTraining User
sqlcmd -i "Release_1.0_DB_Scripts\CreateUser.sql" -b -o %DB_LOGS%\CreateUserOutput.txt
echo Creating EmployeeTraining Database Tables...
sqlcmd -i "Release_1.0_DB_Scripts\CreateTables.sql" -b -o %DB_LOGS%\CreateTablesOutput.txt
echo Inserting test data...
sqlcmd -i "Release_1.0_DB_Scripts\InsertTestData.sql" -b -o %DB_LOGS%\InsertTestDataOutput.txt

echo Updating 1st record with reference image
cd ../utils/ImageInserter
ImageInserter
cd ../../database



