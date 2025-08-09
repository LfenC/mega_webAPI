# EntertainmentHub – Fourth Sprint
Project made by Lizeth Consuelo Bañuelos Ruelas.

# Description
Entertainment Hub is an entertainment platform where you can watch movies, TV shows, upcoming movies, top-rated and popular movies. It also features a simulated login.

# Objectives
- Implement backend with .NET  
- Develop the database  

# Dependencies and Libraries
- **.NET** v8

# Project Screenshot
![image](https://github.com/user-attachments/assets/8bfbde55-cdbb-4f1f-ad92-2cff96742783)

# Instructions
1. Get the repository URL: click the **Code** button and copy the repository URL.  
2. Clone the repository: open the terminal and run  
   ```
   git clone <repository-url>
   ```  
3. Install the necessary dependencies and restore them with:  
   ```
   dotnet restore
   ```  
4. Build the project with:  
   ```
   dotnet build
   ```  
5. Run the project with:  
   ```
   dotnet run
   ```

# How It Was Made
I started by researching **Express.js** since I had used it a little before. Later, when we switched to .NET, I set out to learn how to implement it. Realizing its similarity to **Spring Boot** helped me understand it better from my perspective.  

I then watched tutorials on creating CRUD operations in .NET to understand how to implement them and see practical examples. However, I realized I needed a deeper understanding to develop my project properly.  

Once I grasped the basics, I proceeded to define models, controllers, and other backend components.

# Known Issues
- Database connection: I’m not sure if it’s correct since I connected using Microsoft authentication instead of credentials.  
- Missing controllers.

# Retrospective

## ✅ What went well?
- Gained a basic understanding of .NET.  
- Added a new functionality to my project allowing movies to be added to favorites in Angular.  
- Implemented basic controllers such as **Movies** and functions for **Top Rated** and **Upcoming Movies** fetched from the database.

## ⚠️ What didn’t go well?
- Building the backend in .NET took more time than expected to understand how it should be done.  
- Since my database didn’t allow connecting with a password and ID, I had to use Microsoft authentication, which I believe isn’t the right approach and should be clarified.  
- I didn’t implement login with the database.

## 💡 What can I do differently?
- Manage my time better when learning something new, as this time I focused too much on understanding rather than progressing and applying it.  
- Learn .NET in greater depth.

# Database Diagram
![image](https://github.com/user-attachments/assets/2ad4c12b-ad72-45d9-9f7e-4d66ed9dcbf8)
