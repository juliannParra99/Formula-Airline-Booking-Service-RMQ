# FormulaAirline API Documentation

## Overview

The FormulaAirline API is designed to manage flight bookings. It includes functionalities for creating and handling booking requests and integrates with RabbitMQ to manage asynchronous message processing for bookings.

### Components

- **Models:** Represents the structure of a booking.
- **Services:** Handles message production and interaction with RabbitMQ.
- **Controllers:** Manages HTTP requests related to bookings.
- **Consumer:** A console application that processes messages from the RabbitMQ queue.

## Booking Model

The `Booking` class defines the structure of a booking within the system. Each booking contains the following properties:

- **Id:** Unique identifier for the booking.
- **PassengerName:** Name of the passenger making the booking.
- **PassportNumber:** Passport number of the passenger.
- **From:** Departure location.
- **To:** Destination location.
- **Status:** Status of the booking.

## Services

### IMessageProducer Interface

Defines the contract for the `MessageProducer` service, which includes:

- **`SendingMessage<T>(T message):`** Sends a generic message to a specified RabbitMQ queue.

### MessageProducer Class

This class implements `IMessageProducer` and handles the communication with RabbitMQ. It performs the following steps:

1. Establishes a connection to RabbitMQ.
2. Declares the bookings queue.
3. Serializes the message to JSON and publishes it to the bookings queue.

## Controllers

### BookingsController

Handles HTTP requests related to bookings. It exposes a POST endpoint:

- **`CreatingBooking([FromBody] Booking newBooking):`** Receives a booking request, validates it, and if valid, adds the booking to the in-memory list and sends the booking details to RabbitMQ via the `MessageProducer`.

## RabbitMQ Consumer

A console application that listens for new messages in the bookings queue and processes them:

- The consumer listens to the bookings queue and prints the booking details to the console when a new booking is processed.

## Usage

### Creating a Booking

Send a POST request with booking details to the `BookingsController`. The booking will be saved, and a message will be sent to RabbitMQ.

### Processing Bookings

Run the console application to consume and process messages from the bookings queue.

## Setup

### Using Docker

To run the API and RabbitMQ in Docker, follow these steps:

1. **Create a Docker network:**
   ```bash
   docker network create formula_airline_network
Run the FormulaAirline API:

Build the Docker image for your API.
Ensure the API is configured to connect to RabbitMQ at rabbitmq:5672 within the Docker network.
Start the API container, connecting it to formula_airline_network.
Run the RabbitMQ Consumer:

Ensure the console application is also configured to connect to RabbitMQ at rabbitmq:5672 within the Docker network.
Execute the consumer to start processing messages.
RabbitMQ Extension
Ensure the RabbitMQ client library (e.g., RabbitMQ.Client) is included in your API project.
Configure the connection settings in your application to match the RabbitMQ instance running in Docker.
