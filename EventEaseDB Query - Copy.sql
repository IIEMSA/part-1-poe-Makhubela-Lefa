--DATABASE CREATION
USE MASTER
ALTER DATABASE EventEaseDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE EventEaseDB;

CREATE DATABASE EventEaseDB;
USE EventEaseDB;
--TABLE CREATION
CREATE TABLE Venue (
    VenueID INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing ID
    VenueName NVARCHAR(100) NOT NULL,     -- Venue name (required)
    Location NVARCHAR(200) NOT NULL,      -- Venue location (required)
    Capacity INT NOT NULL,                -- Venue capacity (required)
    ImageURL NVARCHAR(MAX) NULL           -- Image URL (optional)
);

CREATE TABLE Event (
    EventID INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing ID
    EventName NVARCHAR(100) NOT NULL,      -- Event name (required)
    EventDate DATE NOT NULL,               -- Event date (required)
    Description NVARCHAR(500) NULL,        -- Event description (optional)
    VenueID INT NOT NULL,                  -- Foreign key for VenueID
    CONSTRAINT FK_Event_Venue FOREIGN KEY (VenueID) REFERENCES Venue(VenueID)
);

CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing ID
    EventID INT NOT NULL,                    -- Foreign key for EventID
    VenueID INT NOT NULL,                    -- Foreign key for VenueID
    BookingDate DATE NOT NULL,               -- Booking date (required)
    CONSTRAINT FK_Booking_Event FOREIGN KEY (EventID) REFERENCES Event(EventID),
    CONSTRAINT FK_Booking_Venue FOREIGN KEY (VenueID) REFERENCES Venue(VenueID)
);
--This constraint helps to prevent double bookingsS
ALTER TABLE Booking
ADD CONSTRAINT UQ_VenueBooking UNIQUE (VenueID, BookingDate);

--Table insertion to test DB
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)  
VALUES  
('Shepstone Gardens', 'Johannesburg', 500, 'https://shorturl.at/HlCk9'), 
('Number Nineteen','Cape Town' , 300, 'https://shorturl.at/4tBp5'), 
('Kirstenbosh National Botanical Gardens', 'Cape Town', 450, 'https://shorturl.at/lAuZD'), 
('Cayley Lodge', 'Drakensburg', 200, 'https://shorturl.at/5jWN3'), 
('SunBet Arena', 'Pretoria', 1000, 'https://shorturl.at/3DQhB');


SET IDENTITY_INSERT Event ON;

INSERT INTO Event (EventID, EventName, EventDate, Description, VenueID)
VALUES
(1, 'Gospel Night Live', '2025-05-10', 'A night of praise and worship', 1),
(2, 'Summer Art Festival', '2025-12-02', 'Exhibition of local artists', 2),
(3, 'Coastal Jazz Evening', '2025-07-18', 'Smooth jazz by the ocean', 3),
(4, 'Youth Leadership Camp', '2025-09-22', 'Empowering youth leaders', 4),
(5, 'National Dance Finals', '2025-08-05', 'Dance teams compete for the title', 5);

SET IDENTITY_INSERT Event OFF;


SET IDENTITY_INSERT Booking ON;

INSERT INTO Booking (BookingId, EventId, VenueId, BookingDate)  
VALUES  
(1, 1, 1, '2025-03-01'),
(2, 2, 2, '2025-08-15'),
(3, 3, 3, '2025-06-10'),
(4, 4, 4, '2025-07-01'),
(5, 5, 5, '2025-05-20');

SET IDENTITY_INSERT Booking OFF;

--Table Manipulation
SELECT * FROM Venue;
SELECT * FROM Event;
SELECT * FROM Booking;

--drop table Venue;
--drop table Event;
--drop table Booking;

-- Below codes work for Azure database to drop tables
--drop table [dbo].Venue;
--drop table [dbo].Event;
--drop table [dbo].Booking;

