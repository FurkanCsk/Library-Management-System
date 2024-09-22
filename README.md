# Library Management System

This project is an ASP.NET Core MVC application developed to manage the book and author operations of a library. The project is designed following object-oriented programming principles and includes multiple models, controllers, and views.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Usage](#usage)
- [Configuration](#configuration)
- [Testing](#testing)
- [Contributing](#contributing)
## Features
- Add, edit, and delete books and authors.
- User registration and login functionalities.
- View book and author details.
- User-friendly interface based on algorithms.

## Technologies Used
- ASP.NET Core MVC
- C#
- HTML/CSS

## Usage
The project can be run on a local server. Users can register as members and log in to view books.

## Configuration
- **MVC Services:** The application is configured using MVC architecture settings in the `Program.cs` file.
- **Routing:** Requests are directed to the correct controllers and action methods.
- **Static Files:** Static files from the `wwwroot` folder are utilized.

## Testing

### Book Operations
#### Adding a Book
- Add a new book with valid information.
- **Result:** The book was successfully added and is visible in the book list.

#### Editing a Book
- Update the details of an existing book.
- **Result:** The book information was successfully updated and displays correctly on the details page.

#### Deleting a Book
- Perform the deletion of a book.
- **Result:** The book was successfully deleted and is no longer present in the book list.

#### Book Details
- Navigate to the details page of a specific book.
- **Result:** All information is displayed correctly.

### Author Operations
#### Adding an Author
- Add a new author with valid information.
- **Result:** The author was successfully added and is visible in the author list.

#### Editing an Author
- Update the details of an existing author.
- **Result:** The author information was successfully updated and displays correctly on the details page.

#### Deleting an Author
- Perform the deletion of an author.
- **Result:** The author was successfully deleted and is no longer present in the author list.

### User Operations
#### Sign Up
- Register with a valid email and password.
- **Result:** Registration was successful, and the user was redirected to the homepage.

#### Login
- Log in with a valid email and password.
- **Result:** Login was successful, and the user was redirected to the homepage.

#### Invalid Login
- Attempt to log in with an invalid email or password.
- **Result:** An error message was displayed correctly.

## Contributing
If you would like to contribute, please feel free to submit a pull request or report any issues.
