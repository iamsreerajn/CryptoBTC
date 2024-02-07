# CryptoBTC
        This is a Web application can be used to get the day to day rate of Bit Coin. 
        It is developped in .net core(6) MVC with the backend support of MSSQL and Entity framework. 
Current version of **CryptoBTC**  app is capable of handling below functions.
**1. Get the closed rate of Bit Coin for last 31 days**  It has been added to the landing page of the application.
     **-** There will be a table with data and the table will have the vertical scroll enabled to visit all the available data
     **-** There is a lable down to the data table and it show that when is the last time the data took from the API, If it exceeds 24 hours, it will automatically update the data or the user itself can forcecully update the data by clickin on it. Below are the screenshots
     
   _**It is the landing page of the application**_
   ![image](https://github.com/iamsreerajn/CryptoBTC/assets/81985462/d9949c02-0f40-4609-b790-f81178d7c2eb)
   
_**Here, when the user moves over the lable, it will change the text and allow user to update the data by clicking on it.**_
   ![image](https://github.com/iamsreerajn/CryptoBTC/assets/81985462/c7b52de3-44e8-4038-864c-6e1c79a26957)
   
**2. Get the input dates from user and display the Average between selected days** This page is added as a new tab in the landing page
  **-** This is having necessary validations and will calculate average of closed values and print it inside a textbox. Also it prints on a messagebox.
  
  _**Average calculating page**_
  ![image](https://github.com/iamsreerajn/CryptoBTC/assets/81985462/5162dada-a8e5-415b-a7ed-7f28fec5a242)
  
  _**Validation**_
  ![image](https://github.com/iamsreerajn/CryptoBTC/assets/81985462/562b1994-444c-4509-ad13-22f7bd31fcc3)

  _**Display Average after successfull calculation**_
  ![image](https://github.com/iamsreerajn/CryptoBTC/assets/81985462/aea69bc6-3eb3-4a80-942a-db36d5253647)


**CryptoBTC** 
        App is developped by following standard coding/design principles. Code is covered with SOLID principles. It is loosely coupled and enhancement and testability is more in current version. Dependency injection followed as the standard coding practices. Also, unit test is added with Two mock tests and both runs successfully.

**Database** 
        Database is configured on the local system. It is required to keep the last updated data information. 

**Testing** 
        Along with unit test,I have perfomed a few round of testing with the given validation scenarios and found that it worked.


**Technical Competencies Used**
        • Dot Net Core MVC 
        • Entity Framework - Code first model
        • Sql Server
        • Jquery
        • Bootstrap

**I have created a seperate logic to manipulate data from a variable containing api output when the API is not accessl**



