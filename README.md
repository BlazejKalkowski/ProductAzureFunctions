# Template API and Azure Functions - Queue and Table Integration Example

## Project Description

This is an example of using **Azure Functions** in the context of a simple API that demonstrates how to integrate systems utilizing **queues** and **tables** in **Azure**. The project illustrates a very simplified scenario where we have two simulated databases and one Azure Function that sends messages to different systems (e.g., financial and shipping systems) based on new products.

### Scenario

1. **API Endpoint**:
   A new product is created. After adding the product to the system, the information is sent to a **queue**.
   
2. **Azure Function Trigger**:
   An Azure Function monitors the queue and triggers an action when a new product is detected. This function is responsible for updating two systems:
   - **Financial System** (adds the product to a financial table).
   - **Shipping System** (adds the product to a shipping table).
   
3. **Simulated Databases**: 
   We have two simulated databases that store product data:
   - **Financial System** - table `ProductFinancial`
   - **Shipping System** - table `ProductShipping`

After making a request to the API endpoint, a new product is sent to the queue, and the triggered Azure Function immediately creates records in both systems, ensuring that they are available in the system right after the product is added.

## Project Structure

- **Azure Function**:
  A function in Azure that triggers when a new item is detected in the queue, integrating with two "simulated databases".
  
- **API**:
  A simple API endpoint that allows adding a new product to the system. It sends product data to a queue.
  
- **Queues and Tables**:
  Queues to transmit messages between systems, and tables that store product data in the financial and shipping systems.

## How to Run

### Prerequisites

- **Azure Functions Core Tools** - to run functions locally.
- **.NET Core** - to run the API.
- **Azure Storage Emulator** (or installed Azure Storage) - to emulate queues and tables.
