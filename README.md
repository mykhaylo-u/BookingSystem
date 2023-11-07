# Booking System


## Features

Movie Management:
● Retrieve a list of available movies with their details (title, description, duration, genre,
etc.).
● Add new movies to the system.
● Update existing movie information.
● Delete movies from the system.
Theater and Showtime Management:
● Create and manage multiple theaters, each having a specific seating arrangement.
● Define showtimes for each movie in each theater, including the date, time, and available
seats.
Seat Reservation:
● Allow users to reserve seats for a particular showtime.
● Ensure that a user can only reserve available seats.
● Implement a reservation timeout to release unconfirmed seats after a specific duration.
Booking Confirmation:
● Enable users to confirm their reservations and complete the booking process.
● Provide booking details, including the movie, showtime, seats reserved, and total price.
Guidelines:
● Use latest stable version .Net + Entity Framework Core.
● Utilize proper API design principles to ensure clarity, consistency, and usability of the
endpoints.
● Implement appropriate error handling and validation mechanisms to prevent unexpected
behaviors and improve user experience.
● Would be great to see unit tests.

### Prerequisites

git clone https://github.com/mykhaylo-u/BookingSystem.git
cd BookingSystem

You can use Visual Studio to up and run a project
#### Usage
To make a new reservation:

1. Create a movie with details.
2. Create a theater with details.
3. Create a showtime connected with movie, theater and seats.
4. Create a reservation with available seats in particular showtime (each reservation life time is 2 min -it is configurable in project).
5. Confirm you reservation also within 2 min otherwise it will not be accessible anymore

##### API Documentation
Endpoints for managing movies, theaters, showtimes, and reservations are documented with request examples on swagger page.

###### Built With
.NET 6 - The framework used
Entity Framework Core - ORM for database access
AutoMapper - Object-to-object mapper
