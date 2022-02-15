# DtnAssesment
The application reads lightning events data from a flat file and matches that data against a source of assets in json format to produce an alert.

The Solution consists of 1 Console, 2 Class libraries and a test project.

![image](https://user-images.githubusercontent.com/8957790/154078089-029c292e-8773-41ed-a6a6-191461b0e5df.png)

**DtnAssesment.Core**
Core layer consists model classes and all interfaces implemented by the repositories and services. 
There is no business logic exists here. And it does not have any reference to the other projects.

**DtnAssesment.Infrastructure**

This layer includes the logic of retrieving data from json and flat text files, converting longitude and latitude values to quadkey and matching lightning and assets.
Repositories retrieves lightning and assets from files. Services performing logging, converting, and matching tasks.
As output it just writes alert to the console. But it is totally open to the extensions.

**DtnAssesment.Console**
As user interface i just used console application as requested. It does not have any logic just aligns the Dependency Injection configuration.

**DtnAssesment.Tests**
I wrote all unit tests on NUnit framework and for mocking i preferred Moq package. 
It covers the entire logic in Repository and Service classes.


**Running the application**
Just run the DtnAssesment.Console.exe without any parameter. 

**Prerequisites**
Visual Studio 2022
.NET6.0
