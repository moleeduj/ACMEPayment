# ACMEPayment

## Description
This is an example of a .Net Core 3.1 application to calculate payment amount for worked hours according a static rates table. This requires a simple character string with the employee’s name and blocks of days with time ranges (comma separated). Example:

*RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00*

## Solution Requirement
This solution can be explored and tested using Visual Studio 2019 and .Net Core 3.1. 

## Projects included
### ACMEPayment
This contains a simple Console application and it's set as default to run. This includes the project **ACMELibrary** as reference, to access all validations and calculation class.
### ACMELibrary
This contains a single class with all required methods. Also, it includes a folder with models and additional with a connection class to data source. This library can be used to build a different UI.
### ACMETest
This contains a few examples to test main class with all validation and calculation. These can be run used the Run Test Tool in Visual Studio.

## Important Notes:
- When console program runs, it will show some basic instructions, and will ask if you optionally want to show the calculation details according the static rates table. If you enter *N* letter, you only get the total amount.
- For this solution, it will consider hours rounding depending on it's less or greater than the half. For example, 10:29 will be rounded to 10:00, and 10:30 will be rounded to 11:00.
- Due it's only a sample for hours calculation, this solution will not connect to a external data source. You will find into Data folder a class that creates a static list with hours range and rates for each weekday.
- Test project only run 3 success test, and 3 error controls. Obviously, you can create a lot of tests to validate different requests.
