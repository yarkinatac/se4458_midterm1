# se4458_midterm1
SE-4458 Midterm Project Report
Yarkın Ataç - 19070006020
Project Link: https://github.com/yarkinatac/se4458_midterm1
About My Design:
I used .NET Web API for my project and I designed my database in AzureSQL Server.
I created a repositories for the functions and get a functions. I create a controllers for the versioning and get, post methods. 
I used DB first and then integrated the models from the database I created into my project and performed dependency injection. Then, I performed the operations in the repository and wrote controller classes for the methods.
Assumptions:
In the buy ticket section, the form asks for the date, origin, destination, and passenger's full name. However, because there might be multiple flights sharing the same date, origin, and destination, I've included an additional field for the flight number. This ensures a passenger can be accurately booked onto the correct specific flight.
Entity Relationship Diagram:
In my database I created flight and user tables taking into account the requirements. I also created a relation table for flight passengers to keep track the flight passengers.
I did not store the password information directly in my database. I use hashing and salting to store password information. 

#![image](https://github.com/yarkinatac/se4458_midterm1/assets/93092486/db254f5b-36e5-4eb5-8bb5-feeea29a00e5)

