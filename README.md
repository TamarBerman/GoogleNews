# Real-Time Google News
A professional .NET Core project that provides real-time updates from Google News using external RSS APIs. The project is designed with a focus on high user experience, utilizing AJAX and jQuery for dynamic content updates and efficient data handling with HTTP caching.

## Project Overview
This web application displays topics from Google News RSS feeds. By clicking on a topic, users can view the title, content, link to the full article, and the publication date. An admin page allows for cache clearing to demonstrate loading times with and without cached data.

## Features
- **High UX Design:** Ensures swift loading times and efficient backend performance.
- **Admin Page:** Allows cache clearing to show real-time data fetching.
- **Real-Time Updates:** Uses AJAX and jQuery for dynamic content updates.
- **HTTP Caching:** Optimizes performance by caching data and minimizing external API calls.

## Technologies Used

### Languages & Frameworks:
* .NET Core 7 (C#)
* ASP.NET Core Web API
* AJAX, jQuery
* Semantic HTML, CSS, Bootstrap

### Tools & Libraries:
* Postman
* Swagger
* IMemoryCache
* Semaphore for thread safety
  
## Project Structure

* **DAL** (Data Access Layer): Handles external API calls and caching.
* **UI** (User Interface): Manages the presentation layer and user interactions.
* Admin Panel: Provides cache management functionalities.
## Setup & Installation

Clone the repository:
```
git clone <repo-url>
```
Install dependencies:
```
dotnet restore
```
Build the project:
```
dotnet build
```
Run the application:
```
dotnet run
```
Running the App

Navigate to http://localhost:5000 to view the application.

Screenshots
* Home Page: Displays news topics on the left, with detailed post information on the right.
* Admin Page: Allows for cache management and demonstrates loading times.

## Code Quality
The code is written with a focus on efficiency, clean coding practices, and thorough error handling. Key features include:

* API Integration: External API calls to Google News RSS.
* XML Parsing: Efficient handling and parsing of XML data.
* HTTP Caching: Improved performance through data caching.
* Thread Safety: Ensures consistent data with semaphore locking.

## Conclusion
This project showcases a professional approach to developing a web application with real-time data updates and efficient backend management. It leverages modern web technologies and best practices to deliver a high-quality user experience.

## Links
- [Project Code on GitHub](https://github.com/TamarBerman/GoogleNews)

- [Demo Video](https://drive.google.com/file/d/1XuR02W80CvUcZVjhKoDITulfFGPULayA/view)
