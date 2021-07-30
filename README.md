# Rocket_Elevators_REST_API

To answer the 9 questions with Rest Api using `Postman`: import the link below in your postman, to have access to the Rocket elevator REST API collection:

<https://www.getpostman.com/collections/bd56bd3e3e9dfd45c07b

# Rocket Elevators REST API Collection includes: 

1- Retrieving the current status of a specific Battery
`https://rocketelevatorrestapi.azurewebsites.net/api/batteries/1`

2.1-Changing the status of a specific Battery to active
`https://rocketelevatorrestapi.azurewebsites.net/api/batteries/1/active`

2.2-Changing the status of a specific Battery to intervention
`https://rocketelevatorrestapi.azurewebsites.net/api/batteries/1/intervention`

2.3-Changing the status of a specific Battery to inactive
`https://rocketelevatorrestapi.azurewebsites.net/api/batteries/1/inactive`

3-Retrieving the current status of a specific Column
`https://rocketelevatorrestapi.azurewebsites.net/api/columns/5`

4.1-Changing the status of a specific Column to active
`https://rocketelevatorrestapi.azurewebsites.net/api/columns/5/active`

4.2-Changing the status of a specific Column to inactive
`https://rocketelevatorrestapi.azurewebsites.net/api/columns/5/inactive`

4.3-Changing the status of a specific Column to intervention
`https://rocketelevatorrestapi.azurewebsites.net/api/columns/5/intervention`

5-Retrieving the current status of a specific Elevator
`https://rocketelevatorrestapi.azurewebsites.net/api/elevators/55`

6.1-Changing the status of a specific Elevator to active
`https://rocketelevatorrestapi.azurewebsites.net/api/elevators/55/active`

6.2-Changing the status of a specific Elevator to inactive
`https://rocketelevatorrestapi.azurewebsites.net/api/elevators/55/inactive`

6.3-Changing the status of a specific Elevator to intervention
`https://rocketelevatorrestapi.azurewebsites.net/api/elevators/55/intervention`

7-Retrieving a list of Elevators that are not in operation at the time of the request
`https://rocketelevatorrestapi.azurewebsites.net/api/elevators/offline`

8-Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention
`https://rocketelevatorrestapi.azurewebsites.net/api/buildings/intervention`

9-Retrieving a list of Leads created in the last 30 days who have not yet become customers.
`https://rocketelevatorrestapi.azurewebsites.net/api/leads`

# NOTE:
 Each link works individually as well , by changing the ID or status you can test different senarios on each link. 
