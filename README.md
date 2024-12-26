# Hotel Management System

## Overview
The **Hotel Management System** is a web-based application built using the ASP.NET MVC framework. It streamlines essential hotel operations, including room reservations, guest management, service orders, and billing. This project aims to provide a user-friendly interface and efficient backend processes to improve hotel operations and enhance guest satisfaction.

---

## Features

### 1. **Room Management**
- Add new rooms with details like type, price, and availability.
- View and delete rooms as needed.

### 2. **Guest Management**
- Maintain guest records with essential details for better service.

### 3. **Reservations**
- Book rooms by searching for availability based on check-in and check-out dates.
- Manage active and past reservations with ease.

### 4. **Service Orders**
- Allow guests to request additional services.
- Track and bill service orders efficiently.

### 5. **Billing and Payments**
- Automate bill generation for completed stays and services.
- Process payments and update payment statuses.

---

## Technology Stack

### **Frontend**
- **HTML5, CSS3, Bootstrap**: For responsive and intuitive design.
- **JavaScript, jQuery, AJAX**: For dynamic and interactive elements.

### **Backend**
- **ASP.NET MVC**: Framework for application logic and routing.
- **Entity Framework**: For database interaction.

### **Database**
- **SQL Server**: Relational database to manage data efficiently.

---

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-repo/hotel-management-system.git
   ```

2. **Open the Solution**:
   - Open the project in **Visual Studio**.

3. **Configure the Database**:
   - Update the connection string in the `appsettings.json` file to match your SQL Server configuration.

4. **Run Migrations**:
   ```bash
   Update-Database
   ```

5. **Start the Application**:
   - Run the project using IIS Express or your preferred method in Visual Studio.

6. **Access the Application**:
   - Navigate to `http://localhost:[port]` in your browser.

---

## Usage

### **Landing Page**
- Navigate the system using the main menu, which includes:
  - **Add Room**
  - **Add Guest**
  - **Show Reservations**
  - **Order Service**
  - **Checkout**
  - **Pay Bill**

### **Search Rooms**
- Enter check-in and check-out dates to find available rooms.
- Book or delete rooms directly from the search results.

