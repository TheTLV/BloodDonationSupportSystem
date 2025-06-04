CREATE DATABASE BloodDonationSupport;
USE BloodDonationSupport;


CREATE TABLE Roles (
    role_id INT AUTO_INCREMENT PRIMARY KEY,
    role_name VARCHAR(50)
);


CREATE TABLE Users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    phoneNumber VARCHAR(20),
    role_id INT,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);


CREATE TABLE Profiles (
    profile_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT UNIQUE,
    date_of_birth DATE,
    gender VARCHAR(10),
    address TEXT,
    blood_group VARCHAR(10),
    last_donation_date DATE,
    last_received_date DATE,  
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


CREATE TABLE Donations (
    donation_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    blood_group VARCHAR(10),
    status VARCHAR(50),
    quantity INT,
    donation_date DATE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


CREATE TABLE BloodRequests (
    request_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    blood_group VARCHAR(10),
    status VARCHAR(50),
    quantity INT,
    request_date DATE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


CREATE TABLE Events (
    event_id INT AUTO_INCREMENT PRIMARY KEY,
    created_by INT,
    title VARCHAR(255),
    description TEXT,
    event_date DATE,
    FOREIGN KEY (created_by) REFERENCES Users(user_id)
);


CREATE TABLE Notifications (
    notification_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    event_id INT,
    message TEXT,
    notif_date DATE,
    is_action_required BOOLEAN DEFAULT FALSE,
    response_status ENUM('pending', 'accepted', 'declined') DEFAULT 'pending',
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);


CREATE TABLE Reports (
    report_id INT AUTO_INCREMENT PRIMARY KEY,
    created_by INT,
    report_type VARCHAR(50),
    content TEXT,
    report_date DATE,
    FOREIGN KEY (created_by) REFERENCES Users(user_id)
);


CREATE TABLE Blogs (
    blog_id INT AUTO_INCREMENT PRIMARY KEY,
    created_by INT,
    title VARCHAR(255),
    content TEXT,
    FOREIGN KEY (created_by) REFERENCES Users(user_id)
);
